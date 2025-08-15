using UnityEngine;
using UnityEngine.UI;

public class FilmPanel : MonoBehaviour
{
    public Image panelImage;

    public void ClosePanel()
    {
        panelImage.sprite = null; // 패널 이미지의 스프라이트를 null로 설정하여 이미지 제거
        // sprite가 있다면 false로 변경
        this.SetActive(false);
    }
}
