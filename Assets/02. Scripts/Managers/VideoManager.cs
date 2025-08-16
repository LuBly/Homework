using UnityEngine;
using UnityEngine.Video;

public class VideoManager : GlobalSingletonMono<VideoManager>
{
    #region [ Components ]
    [SerializeField] private VideoPlayer player;
    [SerializeField] private GameObject videoObject;
    #endregion [ Components ]

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
        videoObject.SetActive(true);
        player.clip = clip;
        player.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoObject.SetActive(false);
        Debug.Log("Video has ended.");
        
    }
}
