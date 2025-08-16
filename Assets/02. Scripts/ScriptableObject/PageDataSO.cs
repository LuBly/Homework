using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Page Data", menuName = "Scriptable Object/Page Data")]
public class PageDataSO : ScriptableObject
{
    public List<Sprite> PieceImgs;
    public List<Sprite> AnswerImgs;
    [TextArea] public string Desc;
    public VideoClip VideoClip;
    public List<string> Keywords;
}
