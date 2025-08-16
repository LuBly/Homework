using TMPro;
using UnityEngine;

public class HintDiary : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diaryDescription;

    public void SetDiary(int Index)
    {
        // 일반 출력
        diaryDescription.text = PageSetting.inst.pageDataSOs[Index].Desc;
    }

    public void ShowHint(int Index, string[] keywords)
    {
        string desc = PageSetting.inst.pageDataSOs[Index].Desc;
        foreach (var keyword in keywords)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                string highlighted = $"<color=#FF2222><b>{keyword}</b></color>";
                desc = desc.Replace(keyword, highlighted);
            }
        }
        diaryDescription.text = desc;
    }
}
