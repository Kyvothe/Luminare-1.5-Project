using UnityEngine;

public class Music_Manager : MonoBehaviour
{
    private GameObject BG_Music;
    private void Awake()
    {
        BG_Music = GameObject.FindGameObjectWithTag("BG_Music");      
    }
    
    public void Music_Starter()
    {
        BG_Music.SetActive(true);
    }
    
    public void Music_Stopper()
    {
        BG_Music.SetActive(false);
    }
    
    public void Music_Pause()
    {
        BG_Music.GetComponent<AudioSource>().Pause();
    }
    
    public void Music_Unpause()
    {
        BG_Music.GetComponent<AudioSource>().UnPause();
    }
}
