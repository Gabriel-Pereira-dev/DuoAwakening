using System.Collections;
using UnityEngine;


public static class FadeAudioSource
{
    public static IEnumerator StartFade(AudioSource audioSource, float targetVolume, float duration)
    {
        float currentime = 0f;
        float start = audioSource.volume;
        while (currentime < duration)
        {
            currentime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentime / duration);
            yield return null;
        }
    } 
}
