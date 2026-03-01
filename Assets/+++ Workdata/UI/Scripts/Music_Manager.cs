using UnityEngine;

public class Music_Manager : MonoBehaviour
{
    private GameObject BG_Music;
    private GameObject BossMusic;
    private GameObject RatGangMusic;
    private void Awake()
    {
        BG_Music = GameObject.FindGameObjectWithTag("BG_Music");
        BossMusic = GameObject.FindGameObjectWithTag("BossMusic");
        RatGangMusic = GameObject.FindGameObjectWithTag("RatGangMusic");
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
    
    public void BossMusic_Starter()
    {
        BossMusic.SetActive(true);
    }
    
    public void BossMusic_Stopper()
    {
        BossMusic.SetActive(false);
    }
    
    public void RatGangMusic_Starter()
    {
        RatGangMusic.SetActive(true);
    }
    
    public void RatGangMusic_Stopper()
    {
        RatGangMusic.SetActive(false);
    }
}
