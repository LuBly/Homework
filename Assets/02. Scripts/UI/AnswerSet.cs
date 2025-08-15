using UnityEngine;

// BookFilm 슬롯 별 정답 여부 확인 및 리턴
public class AnswerSet : MonoBehaviour
{
    [SerializeField] private BookFilm[] bookFilms; // BookFilm 슬롯 배열
    private int correctCount = 0;

    public void CheckAnswer()
    {
        correctCount = 0; // 정답 카운트 초기화

        for(int i = 0; i < bookFilms.Length; i++)
        {
            // 각 bookFilm의 IsCorrectFilm의 결과 확인
            if (bookFilms[i].IsCorrectFilm())
            {
                correctCount++;
                Debug.Log($"현재까지 맞춘 개수: {correctCount}개");
            }
            else
            {
                Debug.Log($"{bookFilms[i].name} 정답아님!");
            }
        }
        if (correctCount == bookFilms.Length)
        {
            Debug.Log("모든 정답을 맞추셨습니다!");
        }
        else
        {
            Debug.Log($"오답! 맞춘 개수: {correctCount}개");
        }
    }
}
