using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class SoundEffectManager : Singleton<SoundEffectManager>
{
    [SerializeField] private GameObject soundPrefab;
    [SerializeField] private float _soundsVolume = 8;


    private void Start()
    {
        SetSoundVolume(_soundsVolume);
    }

    public void PlaySoundEffect(SoundEffectSO soundEffect)
    {
        // Достаем из пула префаб
        GameObject goSpawn = PoolManager.Instance.GetFromThePool(soundPrefab);

        if (goSpawn != null)
        {
            if (goSpawn.GetComponent<SoundEffect>() != null)
            {
                goSpawn.GetComponent<SoundEffect>().SetSound(soundEffect);
                goSpawn.gameObject.SetActive(true);
                StartCoroutine(DisableSound(goSpawn.GetComponent<SoundEffect>(), soundEffect.soundEffectClip.length));

            }
        }

    }


    /// <summary>
    /// Отключение звука
    /// </summary>
    private IEnumerator DisableSound(SoundEffect sound, float soundDuration)
    {
        yield return new WaitForSeconds(soundDuration);
        sound.gameObject.SetActive(false);
    }

    /// <summary>
    /// Установка громкости звуков
    /// </summary>
    public void SetSoundVolume(float soundsVolume)
    {
        float muteDecibels = -80f;
        _soundsVolume = soundsVolume;

        if (soundsVolume == 0)
        {
            GameManager.Instance.soundsMasterMixerGroup.audioMixer.SetFloat("soundsVolume", muteDecibels);
        }
        else
        {
            GameManager.Instance.soundsMasterMixerGroup.audioMixer.SetFloat("soundsVolume", Helper.LinearToDecibels(soundsVolume));
        }
    }

}
