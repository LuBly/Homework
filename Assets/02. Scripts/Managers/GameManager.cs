using UnityEngine;

public class GameManager : GlobalSingletonMono<GameManager>
{
    #region [ Components ]
    [SerializeField] private UIStartMenu uiStartMenu;
    [SerializeField] private Canvas mainCanvas;
    #endregion [ Components ]

    #region [ Public Method ]
    public void StartGame()
    {
        Debug.Log("Start Game");
        DisableStartMenu();
        CameraManager.inst.TransCamera3To2(OnTransEnd);
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
    #endregion [ Public Method ]

    #region [ Internal Method ]
    private void DisableStartMenu()
    {
        uiStartMenu.UIStartSeq();
    }

    private void OnTransEnd()
    {
        mainCanvas.enabled = true;
    }
    #endregion [ Internal Method ]

}
