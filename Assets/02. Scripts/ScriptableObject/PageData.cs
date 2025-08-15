using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Page Data", menuName = "Scriptable Object/Page Data")]
public class PageData : ScriptableObject
{
    [SerializeField] private List<Sprite> pieceImgs;
    [SerializeField] private List<Sprite> answerImgs;
    [SerializeField] private string desc;
}
