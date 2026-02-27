using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager: MonoBehaviour
{
    public static AudioManager instance;
    private float _stepLength;
    

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
        audioSource.volume = volume;
        audioSource.Play();
        _stepLength = audioSource.clip.length;

        yield return new WaitForSeconds(_stepLength);
        
        
        Destroy(audioSource);
    }
    
}
