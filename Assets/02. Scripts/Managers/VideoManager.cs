using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : GlobalSingletonMono<VideoManager>
{
    #region [ Components ]
    [SerializeField] private VideoPlayer player;
    [SerializeField] private GameObject videoObject;
    #endregion [ Components ]

    #region [ Fields ]
    private Coroutine fadeCoroutine;
    #endregion [ Fields ]

    #region [ Unity Method ]
    private void Start()
    {
        player.loopPointReached -= OnVideoEnd;
        player.loopPointReached += OnVideoEnd;
    }
    #endregion [ Unity Method ]

    public void PlayVideo(VideoClip clip)
    {
        if (player.isPlaying) return;
        player.clip = clip;
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(DelayPaly());
        
    }

    private IEnumerator DelayPaly()
    {
        var time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            yield return null;
        }
        videoObject.SetActive(true);
        player.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoObject.SetActive(false);
        Debug.Log("Video has ended.");
        
    }
}
