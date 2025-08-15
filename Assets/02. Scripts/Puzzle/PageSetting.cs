using UnityEngine;

/// <summary>
/// SO를 기반으로 Film리스트와 정답을 관리
/// </summary>


public class PageSetting : MonoBehaviour
{
    public PageDataSO[] pageDataSOs;
    public Film[] films;
    public BookFilm[] bookFilms;
    public GameObject FilmContainer;
    
    [SerializeField] private int currentPageIndex = 0;

    private void Start()
    {
        Setting();
        // 페이지 설정 후 FilmContainer 닫기
        UIManager.inst.CloseUI(FilmContainer);
    }

    private void Setting()
    {
        // 현재 페이지 인덱스가 범위를 벗어나지 않도록 조정
        if (currentPageIndex < 0)
        {
            currentPageIndex = 0;
        }
        else if (currentPageIndex >= pageDataSOs.Length)
        {
            Debug.Log("게임 전체 페이지를 모두 완료했습니다.");
            //게임 종료 로직
            return;
        }
            //모든 film에 pageDataSO의 pieceImgs를 랜덤으로 할당, 중복 불가, answerImgs는 BookFilm에 할당됨(순서대로)
        for (int i = 0; i < films.Length; i++)
        {
            if (pageDataSOs.Length > currentPageIndex)
            {
                var pageData = pageDataSOs[currentPageIndex];
                if (pageData != null && pageData.pieceImgs.Count > 0)
                {
                    // 랜덤으로 이미지 할당
                    int randomIndex = Random.Range(0, pageData.pieceImgs.Count);

                    // 중복 방지: 이미 할당된 이미지가 있다면 다시 랜덤으로 선택
                    while (films[i].filmImage.sprite != null && films[i].filmImage.sprite == pageData.pieceImgs[randomIndex])
                    {
                        randomIndex = Random.Range(0, pageData.pieceImgs.Count);
                    }

                    films[i].filmImage.sprite = pageData.pieceImgs[randomIndex];                    
                }

            }
        }

        // BookFilm의 AnswerImg에 answerImgs를 순서대로 할당
        for(int i = 0; i < bookFilms.Length; i++)
        {
            if (pageDataSOs.Length > currentPageIndex)
            {
                var pageData = pageDataSOs[currentPageIndex];
                if (pageData != null && pageData.answerImgs.Count > i)
                {
                    bookFilms[i].answerID = pageData.answerImgs[i];
                }
            }
        }

        

        
    }
}
