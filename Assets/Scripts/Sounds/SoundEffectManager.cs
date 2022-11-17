using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class SoundEffectManager : Singleton<SoundEffectManager>
{
    [SerializeField] private GameObject soundPrefab;
    [SerializeField] private float _soundsVolume = 2;

    /// <summary>
    /// Play the sound effect
    /// </summary>
    /// 

    private void Start()
    {
        SetSoundVolume(_soundsVolume);
    }

    public void PlaySoundEffect(SoundEffectSO soundEffect)
    {
        // ������� �� ���� ������
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
    /// Disable sound effect object after it has played thus returning it to the object pool
    /// </summary>
    private IEnumerator DisableSound(SoundEffect sound, float soundDuration)
    {
        yield return new WaitForSeconds(soundDuration);
        sound.gameObject.SetActive(false);
    }

    /// <summary>
    /// ��������� ���������
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
            GameManager.Instance.soundsMasterMixerGroup.audioMixer.SetFloat("soundsVolume", LinearToDecibels(soundsVolume));
        }
    }

    /// <summary>
    /// ��������� � ��������
    /// </summary>
    public static float LinearToDecibels(float linear)
    {
        float linearScaleRange = 20f;

        // formula to convert from the linear scale to the logarithmic decibel scale
        return Mathf.Log10((float)linear / linearScaleRange) * 20f;
    }

}
