using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _audio;
    private AudioSource _audioSource;
    private AudioSource _audioSourceMusic;

    private void Start()
    {
        if(MusicManager.Instance.gameObject.GetComponent<AudioSource>())
            _audioSource = MusicManager.Instance.gameObject.GetComponent<AudioSource>();
        else
            throw new System.Exception("Нет AudioSource на MusicManager.");
        if(MusicManager.Instance.gameObject.GetComponent<AudioSource>())
            _audioSourceMusic = Camera.main.GetComponent<AudioSource>();
        else
            throw new System.Exception("Нет AudioSource на Camera.");
    }
    public void SetNewSettingAudio()
    {
        _audioSource.volume = _audio.value;
    }

    public void SetNewSettingMusic()
    {
        _audioSourceMusic.volume = _music.value;
    }
}
