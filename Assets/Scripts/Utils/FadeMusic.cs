using UnityEngine;

public class FadeMusic : MonoBehaviour
{
    public AudioSource music;
    private float originalVolume;
    public float fadeInduration = 1f;
    public float fadeOutDuration = 1f;

    private void Awake()
    {
        originalVolume = music.volume;
        music.volume = 0;
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeAudioSource.StartFade(music, fadeInduration, originalVolume));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeAudioSource.StartFade(music,fadeOutDuration,0));
    }
}