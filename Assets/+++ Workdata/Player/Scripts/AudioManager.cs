using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager: MonoBehaviour
{
    public static AudioManager instance;
    private float _stepLength;
    private float _fxsLength;
    public AudioMixerGroup SoundsFX;
    
    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFX(AudioResource audioResource, float volume = 1f)
    {
        StartCoroutine(PlaySFXCoroutine(audioResource, volume));
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator PlaySFXCoroutine(AudioResource audioResource, float volume = 1f)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.resource = audioResource;
        audioSource.outputAudioMixerGroup = SoundsFX;
        audioSource.volume = volume;
        audioSource.Play();
        //_stepLength = audioSource.clip.length;

        yield return new WaitForSeconds(0.3f);
        
        
        Destroy(audioSource);
    }
    
    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        
        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = SoundsFX;
        audioSource.volume = volume;
        audioSource.Play();
        _fxsLength = audioSource.clip.length;
        
        Destroy(audioSource.gameObject, _fxsLength);
    }


   
    
}
