using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// SO를 기반으로 Film리스트와 정답을 관리
/// </summary>


public class PageSetting : GlobalSingletonMono<PageSetting>
{
    public PageDataSO[] pageDataSOs;
    public Film[] films;
    public BookFilm[] bookFilms;
    public GameObject FilmContainer;
    public GameObject PuzzleUI;
    public GameObject NextPageBtn;
    
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
            UIManager.inst.CloseUI(PuzzleUI);
            Debug.Log("게임 엔딩 로직 실행");
            return;
        }

        filmSetting(); // Film 설정
        BookFilmSetting(); // BookFilm 설정
    }

    public void PageComplete()
    {
        currentPageIndex++;
        // 페이지 설정 후 FilmContainer 닫기
        UIManager.inst.CloseUI(PuzzleUI);
        UIManager.inst.OpenUI(NextPageBtn); // 다음 페이지 버튼 활성화
    }

    public void NextPage()
    {
        UIManager.inst.OpenUI(PuzzleUI);
        if(!FilmContainer.activeSelf)
            UIManager.inst.OpenUI(FilmContainer);
        Setting();
        UIManager.inst.CloseUI(FilmContainer); // PuzzleUI 닫기
        UIManager.inst.CloseUI(NextPageBtn); // 다음 페이지 버튼 비활성화
        // 페이지 설정 후 FilmContainer 닫기
    }

    private void filmSetting()
    {
        //모든 film에 pageDataSO의 pieceImgs를 랜덤으로 할당, 중복 불가, answerImgs는 BookFilm에 할당됨(순서대로)
        int[] randomindexs = new int[pageDataSOs[currentPageIndex].pieceImgs.Count]; // 랜덤 인덱스 배열 초기화


        for (int i = 0; i < films.Length; i++)
        {
            if (pageDataSOs.Length > currentPageIndex)
            {
                var pageData = pageDataSOs[currentPageIndex];
                if (pageData != null && pageData.pieceImgs.Count > 0)
                {
                    //pageData의 pieceImgs의 개수가 films의 개수보다 작을 경우 해당 films 오브젝트는 비활성화
                    // films[i]가 pageData.pieceImgs의 개수보다 크면 films[i]를 비활성화
                    if (i >= pageData.pieceImgs.Count)
                    {
                        films[i].gameObject.SetActive(false);
                        continue; // 다음 반복으로 넘어감
                    }
                    else
                    {
                        if (!films[i].gameObject.activeSelf)
                            films[i].gameObject.SetActive(true);
                    }


                    // 중복 방지: 이미 할당된 이미지가 있다면 다시 랜덤으로 선택, 모든 이미지를 1장씩 할당
                    // 랜덤 인덱스 배열에서 아직 할당되지 않은 인덱스를 찾아 해당 pieceImg를 할당
                    int randomIndex;
                    do
                    {
                        randomIndex = Random.Range(0, pageData.pieceImgs.Count);
                    } while (randomindexs[randomIndex] != 0); // 이미 할당된 인덱스는 제외
                    randomindexs[randomIndex] = 1; // 해당 인덱스는 할당되었음을 표시

                    // films[i]의 pieceImg에 pageData.pieceImgs[randomIndex]를 할당
                    films[i].filmImage.sprite = pageData.pieceImgs[randomIndex];
                }

            }
        }
    }

    private void BookFilmSetting()
    {
        // BookFilm의 AnswerImg에 answerImgs를 순서대로 할당
        for (int i = 0; i < bookFilms.Length; i++)
        {
            if (pageDataSOs.Length > currentPageIndex)
            {
                // answerImgs가 bookFilms의 개수보다 작을 경우 해당 bookFilm 오브젝트는 비활성화
                if (i >= pageDataSOs[currentPageIndex].answerImgs.Count)
                {
                    bookFilms[i].gameObject.SetActive(false);
                    continue; // 다음 반복으로 넘어감
                }
                else
                {
                    // 해당 bookFilm이 setActive되어 있지 않다면 활성화
                    if (!bookFilms[i].gameObject.activeSelf)
                        bookFilms[i].gameObject.SetActive(true);
                }

                var pageData = pageDataSOs[currentPageIndex];
                if (pageData != null && pageData.answerImgs.Count > i)
                {
                    bookFilms[i].answerID = pageData.answerImgs[i];
                }
            }
        }
    }
}
