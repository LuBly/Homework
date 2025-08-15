using UnityEngine;

public class UIManager : GlobalSingletonMono<UIManager>
{
    private Transform popupUiTransform;
    private GameObject curUI;      

    public void OpenUI(GameObject obj)
    {
        obj.SetActive(true);
    }
    // 단일 PopupUI를 끄는 함수
    public void CloseUI(GameObject obj)
    {
        obj.SetActive(false);
    }

}
