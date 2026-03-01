using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    
    public void SetMasterVolume(float level)
    {
        _audioMixer.SetFloat("masterVolume", level);
    }
    
    public void SetSoundFXVolume(float level)
    {
        _audioMixer.SetFloat("soundFXVolume", level);
    }
    
    public void SetMusicVolume(float level)
    {
        _audioMixer.SetFloat("musicVolume", level);
    }
    
    public void SetAmbienceVolume(float level)
    {
        _audioMixer.SetFloat("ambienceVolume", level);
    }
    
    
    /*public void SetMasterVolume(float level)
    {
        _audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
    }
    
    public void SetSoundFXVolume(float level)
    {
        _audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
    }
    
    public void SetMusicVolume(float level)
    {
        _audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
    }
    
    public void SetAmbienceVolume(float level)
    {
        _audioMixer.SetFloat("ambienceVolume", Mathf.Log10(level) * 20f);
    }*/
}
