using System.Collections;
using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    private AudioSource source;
    protected Coroutine coroutine;

    public bool isPlaying
    {
        get
        {
            return (source != null) ? source.isPlaying : false;
        }
    }

    public void Play()
    {
        source.Play();
    }

    public void Play(AudioClip clip, bool loop = false)
    {
        source.clip = clip;
        source.loop = loop;

        source.Play();
    }

    public void PlayOneShot(AudioClip clip)
    {
        if (clip == null) return;
        //Debug.Log($"<color=orange> Play SFXAudio : {clip.name} </color>");
        source.PlayOneShot(clip);
    }

    public void FadeIn()
    {
        source.volume = 1.0f;
    }

    public void FadeOut(float duration)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(_FadeOut(duration));

        IEnumerator _FadeOut(float duration)
        {
            float startVolume = source.volume;
            while (0 < source.volume)
            {
                source.volume -= startVolume * Time.deltaTime / duration;
                yield return new WaitForEndOfFrame();
            }
            source.Stop();
            source.volume = startVolume;
        }
    }

    public void Stop()
    {
        source.Stop();
    }

    public void Pause()
    {
        source.Pause();
    }

    public void Resume()
    {
        source.UnPause();
    }

    public void SetPriority(int priority)
    {
        if (source != null)
        {
            source.priority = priority;
        }
    }

    private void Awake()
    {
        source = transform.GetOrAddComponent<AudioSource>();
        source.playOnAwake = false;
    }
}