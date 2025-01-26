using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioSource audioSource3;

    private AudioSource[] audioSources;
    private int audioSourceIndex = 0;


    [SerializeField] private AudioClip audioDestroyedBluePeg;



    private void OnEnable()
    {
        audioSources[0] = audioSource1;
        audioSources[1] = audioSource2;
        audioSources[2] = audioSource3;

        EventMessenger.StartListening(EventKey.DestroyedBluePeg, AudioDestroyedBluePeg);
    }
    
    private void PlayAudioSource(AudioClip clip)
    {
        audioSources[audioSourceIndex].clip = clip;
        audioSources[audioSourceIndex++].Play();

        if (audioSourceIndex >= 2) audioSourceIndex = 0;
    }

    private void AudioDestroyedBluePeg()
    {
        PlayAudioSource(audioDestroyedBluePeg);
        Debug.Log("audio destroyed blue peg");
    }


}