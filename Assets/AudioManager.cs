using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public List<AudioSource> AudioSources = new List<AudioSource>();
    public HashSet<string> AudioSounds = new HashSet<string>();
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void Play(string name, AudioClip clip, float pitch, bool loop)
    {
        if (!AudioManager.Instance.AudioSounds.Contains(name))
        {
            var source = AudioManager.Instance.gameObject.AddComponent<AudioSource>();

            source.name = name;

            AudioManager.Instance.AudioSounds.Add(name);
            AudioManager.Instance.AudioSources.Add(source);
        }

        var audioSource = AudioManager.Instance.AudioSources.Find(a => a.name == name);
        audioSource.clip = clip;
        audioSource.pitch = pitch;
        audioSource.loop = loop;
        audioSource.Play();
    }
}
