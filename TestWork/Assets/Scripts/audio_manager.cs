using UnityEngine;
using UnityEngine.UI;

public sealed class audio_manager : MonoBehaviour
{
    [SerializeField] private  AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private Slider _audioVolume;
    [Header("Музыка")] [SerializeField] private AudioClip[] _musicClips;
    [Header("Звуки")] [SerializeField] private AudioClip[] _soundsClips;
   

    private void Start()
    {   
        MusicPlay(0);    
    }
    public void SoundMute(Toggle value)
    {
        _soundSource.mute = !value.isOn;
    }

    public void MusicMute(Toggle value)
    {
        _musicSource.mute = !value.isOn;
    }

    public void ChangeVolume()
    {
        _musicSource.volume = _audioVolume.value;
        _soundSource.volume = _audioVolume.value;
    }

    public void SoundPlay(int num)
    {
        _soundSource.PlayOneShot(_soundsClips[num]);
    }
    private void MusicPlay(int num)
    {
        _musicSource.clip = _musicClips[num];
        _musicSource.Play();
    }
    
}
