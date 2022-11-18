using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    [SerializeField] private float _musicVolume = 1;
    private AudioSource _musicAudioSource = null;
    private AudioClip _currentAudioClip = null;

    protected override void Awake()
    {
        base.Awake();

        _musicAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SetAudioClipByLevel();        
    }

    /// <summary>
    /// Устанавливает аудио клип согласно уровню
    /// </summary>
    private void SetAudioClipByLevel()
    {
        if (GameManager.Instance.currentLevelDetails.audioClip != null)
        {
            _currentAudioClip = GameManager.Instance.currentLevelDetails.audioClip;
            _musicAudioSource.clip = _currentAudioClip;
            _musicAudioSource.Play();
            SetMusicVolume(_musicVolume);
        }
    }

    /// <summary>
    /// перезапуск музыки
    /// </summary>
    public void RestartMusic()
    {
        SetAudioClipByLevel();
    }

    public void SetMusicVolume(float musicVolume)
    {
        float muteDecibels = -80f;
        _musicVolume = musicVolume;

        if (musicVolume == 0)
        {
            GameManager.Instance.musicMasterMixerGroup.audioMixer.SetFloat("musicVolume", muteDecibels);
        }
        else
        {
            GameManager.Instance.musicMasterMixerGroup.audioMixer.SetFloat("musicVolume", Helper.LinearToDecibels(_musicVolume));
        }

    }

}
