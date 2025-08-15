using UnityEngine;
/// <summary>
/// Film = BookFilm 간의 정답 여부를 맞추기 위한 테스트용 데이터
/// </summary>

[CreateAssetMenu(fileName = "TestData", menuName = "ScriptableObjects/TestData", order = 1)]
public class TestData : ScriptableObject
{
    [SerializeField] private string testName;
    public int TestId; // 테스트용 ID (1, 2, 3) = BookFilm의 ID와 비교하여 동일하면 정답, 다르면 오답
}
