using echo17.EndlessBook;
using UnityEngine;

public class GameManager : GlobalSingletonMono<GameManager>
{
    #region [ Fields ]
    [SerializeField] private float stateAnmationTime = 2f;
    [SerializeField] private float turnTime = 1f;
    #endregion [ Fields ]

    #region [ Components ]
    [SerializeField] private UIStartMenu uiStartMenu;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private EndlessBook book;
    #endregion [ Components ]

    #region [ Public Method ]
    public void StartGame()
    {
        Debug.Log("Start Game");
        var changeState = true;
        var newState = EndlessBook.StateEnum.OpenMiddle;

        if(changeState)
            book.SetState(newState, stateAnmationTime, OnBookStateChanged);

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

    public void OnTurnButtonClicked(int direction)
    {
        if (direction == -1)
        {
            book.TurnBackward(turnTime,
                onCompleted: OnBookTurnToPageCompleted,
                onPageTurnStart: OnPageTurnStart,
                onPageTurnEnd: OnPageTurnEnd);
        }
        else
        {
            book.TurnForward(turnTime,
                onCompleted: OnBookTurnToPageCompleted,
                onPageTurnStart: OnPageTurnStart,
                onPageTurnEnd: OnPageTurnEnd);
        }
    }

    private void OnBookStateChanged(EndlessBook.StateEnum fromState, EndlessBook.StateEnum toState, int currentPageNumber)
    {
        Debug.Log("State set to " + toState + ". Current Page Number = " + currentPageNumber);
    }

    private void OnBookTurnToPageCompleted(EndlessBook.StateEnum fromState, EndlessBook.StateEnum toState, int currentPageNumber)
    {
        Debug.Log("OnBookTurnToPageCompleted: State set to " + toState + ". Current Page Number = " + currentPageNumber);
    }

    private void OnPageTurnStart(Page page, int pageNumberFront, int pageNumberBack, int pageNumberFirstVisible, int pageNumberLastVisible, Page.TurnDirectionEnum turnDirection)
    {
        Debug.Log("OnPageTurnStart: front [" + pageNumberFront + "] back [" + pageNumberBack + "] fv [" + pageNumberFirstVisible + "] lv [" + pageNumberLastVisible + "] dir [" + turnDirection + "]");
    }

    protected virtual void OnPageTurnEnd(Page page, int pageNumberFront, int pageNumberBack, int pageNumberFirstVisible, int pageNumberLastVisible, Page.TurnDirectionEnum turnDirection)
    {
        Debug.Log("OnPageTurnEnd: front [" + pageNumberFront + "] back [" + pageNumberBack + "] fv [" + pageNumberFirstVisible + "] lv [" + pageNumberLastVisible + "] dir [" + turnDirection + "]");
    }
    #endregion [ Internal Method ]

}
