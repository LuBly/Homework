using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStartMenu : MonoBehaviour
{
    #region [ Components ]
    [SerializeField] private Image startImg;
    [SerializeField] private TextMeshProUGUI startTxt;

    [SerializeField] private Image settingImg;
    [SerializeField] private TextMeshProUGUI settingTxt;

    [SerializeField] private Image endImg;
    [SerializeField] private TextMeshProUGUI endTxt;
    #endregion [ Components ]

    #region [ Fields ]
    [SerializeField] private float duration;
    #endregion [ Fields ]

    #region [ External Method ]
    public void UIStartSeq()
    {
        FadeBtn();
    }

    private void FadeBtn()
    {
        startImg.DOFade(0, duration);
        startTxt.DOFade(0, duration).OnComplete(FadeEndBtn);
    }

    private void FadeSettingBtn() { }

    private void FadeEndBtn() 
    {
        endImg.DOFade(0, duration);
        endTxt.DOFade(0, duration);
    }
    #endregion [ External Method ]
}
