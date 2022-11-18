using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderSounds;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        GameManager.Instance.GetPlayerShip().GetComponent<PlayerController>().DisablePLayerContoller();
    }

    private void OnDisable()
    {
        GameManager.Instance.GetPlayerShip().GetComponent<PlayerController>().EnablePLayerContoller();
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Возврат
    /// </summary>
    public void ReturnGame()
    {
        gameObject.SetActive(false);
        GameManager.Instance.IsPause = false;
    }

    public void SetVolumeMusic()
    {
        float volume = _sliderMusic.value;
        MusicManager.Instance.SetMusicVolume(volume);
    }

    public void SetVolumeSounds()
    {
        float volume = _sliderSounds.value;
        SoundEffectManager.Instance.SetSoundVolume(volume);
    }
}
