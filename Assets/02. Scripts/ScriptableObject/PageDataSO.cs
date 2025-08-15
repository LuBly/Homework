using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Page Data", menuName = "Scriptable Object/Page Data")]
public class PageDataSO : ScriptableObject
{
    [SerializeField] private List<Sprite> pieceImgs;
    [SerializeField] private List<Sprite> answerImgs;
    [TextArea]
    [SerializeField] private string desc;
}
