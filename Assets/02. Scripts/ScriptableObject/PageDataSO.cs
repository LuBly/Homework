using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Page Data", menuName = "Scriptable Object/Page Data")]
public class PageDataSO : ScriptableObject
{
    [SerializeField] public List<Sprite> pieceImgs;
    [SerializeField] public List<Sprite> answerImgs;
    [SerializeField] private string desc;
}
