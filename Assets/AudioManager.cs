using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Dictionary<string, AudioSource> AudioSources = new Dictionary<string, AudioSource>();

    private AudioSource MainAudioSource;

    public AudioClip KayLeniTayo;
    public AudioClip KayLeniTayoSpecial;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        MainAudioSource = GetComponent<AudioSource>();
    }

    public static void PlayNormalMusic()
    {
        Instance.MainAudioSource.clip = Instance.KayLeniTayo;
        Instance.MainAudioSource.Play();
    }

    public static void PlaySpecialMusic()
    {
        Instance.MainAudioSource.clip = Instance.KayLeniTayoSpecial;
        Instance.MainAudioSource.Play();
    }

    public static void Play(string name, AudioClip clip, float pitch, bool loop, float volume = 0.65f)
    {
        if (!AudioManager.Instance.AudioSources.ContainsKey(name))
        {
            var source = AudioManager.Instance.gameObject.AddComponent<AudioSource>();
            AudioManager.Instance.AudioSources.Add(name, source);
        }

        var audioSource = AudioManager.Instance.AudioSources[name];
        audioSource.clip = clip;
        audioSource.pitch = pitch;
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.Play();
    }
}
