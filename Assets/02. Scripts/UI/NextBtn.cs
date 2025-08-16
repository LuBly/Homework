using UnityEngine;
using UnityEngine.UI;

public class NextBtn : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnNextButtonClick);
    }

    void OnNextButtonClick()
    {
        PageSetting.inst.NextPage();
    }
}
