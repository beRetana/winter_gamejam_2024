using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private const string SOUNDS_HOLDER_OBJECT_NAME = "Sounds Holder";

    private static AudioPlayer instance;

    private static Dictionary<string, AudioSource> sounds;

    private GameObject soundsHolder;

    private void Awake()
    {
        if (sounds == null)
        {
            // Initialize static sounds dict
            sounds = new Dictionary<string, AudioSource>();
            soundsHolder = GameObject.Find(SOUNDS_HOLDER_OBJECT_NAME);

            GetSounds(soundsHolder.transform);

            instance = this;
        }
    }

    /// <summary>
    /// Plays a sound with the given name.
    /// </summary>
    public static void PlaySound(string key, bool doRestartSound = true)
    {
        if (sounds[key].isPlaying && !doRestartSound)
        {
            return;
        }
        sounds[key].Play();
    }

    /// <summary>
    /// Plays a sound at the given volume.
    /// </summary>
    public static void PlaySound(string key, float volume, bool doRestartSound = true)
    {
        if (sounds[key].isPlaying && !doRestartSound)
        {
            return;
        }
        sounds[key].volume = volume;
        sounds[key].Play();
    }

    /// <summary>
    /// Plays a sound at a random pitch between the given pitch-bounds.
    /// </summary>
    public static void PlaySound(string key, float minPitch, float maxPitch, bool doRoundSemitone = false)
    {
        sounds[key].pitch = Random.Range(minPitch, maxPitch);
        if (doRoundSemitone)
        {
            sounds[key].pitch = Mathf.Pow(1.05946f, (int)Mathf.Log(sounds[key].pitch, 1.05946f));
        }
        sounds[key].Play();
    }

    /// <summary>
    /// Stops a currently sound.
    /// </summary>
    public static void StopSound(string key)
    {
        sounds[key].Stop();
    }

    /// <summary>
    /// Stops the sound if it's playing. Otherwise, do nothing.
    /// </summary>
    public static void StopSoundIfPlaying(string key)
    {
        if (IsSoundPlaying(key))
        {
            StopSound(key);
        }
    }

    /// <returns>The AudioSource component of a sound.</returns>
    public static AudioSource GetSound(string key)
    {
        return sounds[key];
    }

    /// <returns>Whether the sound with the given name is playing.</returns>
    public static bool IsSoundPlaying(string key)
    {
        return sounds[key].isPlaying;
    }

    /// <summary>
    /// Starts the FadeAudio Coroutine on the AudioPlayer object.
    /// </summary>
    public static void InstanceFadeAudio(string key, float duration, float targetVolume)
    {
        instance.StartCoroutine(FadeAudio(key, duration, targetVolume));
    }
    /// <summary>
    /// An IEnumerator to fade audio. Start the coroutine in the caller script.
    /// </summary>
    public static IEnumerator FadeAudio(string key, float duration, float targetVolume)
    {
        if (sounds[key].volume == targetVolume) { yield break; }
        float currentTime = 0;
        float start = sounds[key].volume;
        while (currentTime <= duration)
        {
            currentTime += Time.deltaTime;
            sounds[key].volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        sounds[key].volume = targetVolume;
        if (sounds[key].volume == 0)
        {
            sounds[key].Stop();
        }
        yield break;
    }

    /// <summary>
    /// Recursively get all sounds and add them into dictionary.
    /// </summary>
    private void GetSounds(Transform transform)
    {
        int childCount = transform.childCount;
        if (childCount > 0)
        {
            for (int i = 0; i < childCount; ++i)
            {
                GetSounds(transform.GetChild(i));
            }
        }
        else
        {
            sounds.Add(transform.name, transform.GetComponent<AudioSource>());
        }
    }
}