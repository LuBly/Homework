using UnityEngine;
using UnityEngine.Video;

public class VideoManager : GlobalSingletonMono<VideoManager>
{
    #region [ Components ]
    [SerializeField] private VideoPlayer player;
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
        player.clip = clip;
        player.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        this.SetActive(false);
        Debug.Log("Video has ended.");
        
    }
}
