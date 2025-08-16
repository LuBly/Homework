using UnityEngine;

// BookFilm 슬롯 별 정답 여부 확인 및 리턴
public class AnswerSet : MonoBehaviour
{
    [SerializeField] private BookFilm[] bookFilms; // BookFilm 슬롯 배열
    private int correctCount = 0;

    public void CheckAnswer()
    {
        correctCount = 0; // 정답 카운트 초기화
        int activeCount = 0;
        for(int i = 0; i < bookFilms.Length; i++)
        {
            if (!bookFilms[i].gameObject.activeSelf)
                continue; // 비활성화된 오브젝트는 정답 체크에서 제외
            activeCount++;
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
        if (correctCount == activeCount && activeCount > 0)
        {
            Debug.Log("모든 정답을 맞추셨습니다!");
            // 정답을 맞추면 chapterIndex를 증가시키고 다음 페이지 세팅 로직 삽입
            PageSetting.inst.PageComplete();
            // 세팅되어 있던 이미지들을 RestImg()를 통해 초기화
        }
        else
        {
            Debug.Log($"오답! 맞춘 개수: {correctCount}개");
            PageSetting.inst.Incorrect(); // 오답 처리
            // 오답 시 카메라(라이프) 차감
        }
        foreach (var bookFilm in bookFilms)
        {
            bookFilm.RestPosition();
        }
    }
}
