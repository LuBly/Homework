using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopupBtn : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject popupUI;


    private void Awake()
    {
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Button component not found on the GameObject.");
            return;
        }
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        // Check if the button is interactable before proceeding
        if (!button.interactable)
        {
            Debug.Log("Button is not interactable.");
            return;
        }
        
        // 팝업 UI가 열려있으면 닫기

        if (popupUI.activeSelf)
        {
            UIManager.inst.CloseUI(popupUI);
            return;
        }
        else 
            UIManager.inst.OpenUI(popupUI);

    }
    
}
