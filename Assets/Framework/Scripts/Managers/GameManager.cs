using UnityEngine;

public class GameManager : GlobalSingletonMono<GameManager>
{
    #region [ Components ]
    [SerializeField] private Canvas mainCanvas;
    #endregion [ Components ]

    #region [ Private Method ]
    public void StartGame()
    {
        Debug.Log("Start Game");
        mainCanvas.enabled = true;
    }

    public void OpenSettingUI()
    {
        Debug.Log("OpenSetting UI");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
#endregion [ Private Method ]
}
