using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEndMenu : MonoBehaviour
{
    #region [ Components ]
    [SerializeField] private Button endBtn;
    [SerializeField] private TextMeshProUGUI endBtnTxt;
    [SerializeField] private TextMeshProUGUI endingText;

    private Canvas canvas;
    #endregion [ Components ]

    #region [ Fields ]
    //[SerializeField] private float duration;
    #endregion [ Fields ]

    #region [ Unity Method ]
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        endBtn.enabled = false;
    }

    private void Start()
    {
        endBtn.onClick.RemoveAllListeners();
        endBtn.onClick.AddListener(GameManager.inst.Exit);
    }
    #endregion [ Unity Method ]

    #region [ Internal Method ]

    #endregion [ Internal Method ]

    #region [ External Method ]
    public void Show()
    {
        canvas.enabled = true;
        endingText.DOText("3�ϰ��� �������� �����п��� \n���� �߾����� ���⸦ �ٶ��ϴ�.\n�����ϼ̽��ϴ�.", 5f)
            .OnComplete(() => 
            {
                endBtn.SetActive(true);
                endBtn.enabled = true;
                endBtn.interactable = false;
                endBtn.image.DOFade(1f, 1f);
                endBtnTxt.DOFade(1f, 1f)
                .OnComplete(() => 
                { 
                    endBtn.interactable = true;
                });
            });
    }
    #endregion [ External Method ]
}
