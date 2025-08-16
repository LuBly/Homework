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
    [SerializeField] private GameObject[] CameraLifeUIs;

    public GameObject FilmContainer;
    public GameObject PuzzleUI;
    public GameObject NextPageBtn;
    public HintDiary hintDiary;
    private float camShakeSec = 0.2f;

    [SerializeField] public int currentPageIndex { get; private set; } = 0;
    [SerializeField] private int cameraLife = 3; // 카메라 생명 초기값(전부 감소하면 게임 오버, Index 0으로 돌아감)

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
            return;
        }

        filmSetting(); // Film 설정
        BookFilmSetting(); // BookFilm 설정
        DiarySetting(); // 일기 설정
    }

    public void PageComplete()
    {
        var videoClip = pageDataSOs[currentPageIndex].ViduoClip;
        currentPageIndex++;
        VideoManager.inst.PlayVideo(videoClip);
        
        //GameManager.inst.NextPage();
        // 페이지 설정 후 FilmContainer 닫기
        UIManager.inst.CloseUI(PuzzleUI);
        UIManager.inst.CloseUI(hintDiary.gameObject);


        UIManager.inst.OpenUI(NextPageBtn); // 다음 페이지 버튼 활성화
    }

    public void Incorrect()
    {
        cameraLife--;
        if (cameraLife <= 0)
        {
            // 카메라 생명이 모두 소진되면 게임 오버 처리
            Debug.Log("카메라 생명이 모두 소진되었습니다. 게임 오버.");
            GameOver();
        }
        else
        {
            GameObject lifUI = CameraLifeUIs[cameraLife]; // 3 2 1 0 순서로 UI를 가져와서 비활성화
            if (lifUI != null)
            {
                UIManager.inst.CloseUI(lifUI); // 현재 생명 UI 닫기
                if(CameraManager.inst != null)
                    CameraManager.inst.CameraShakeEffect(camShakeSec);
                else
                {
                    //카메라 매니저가 없으면 그냥 지나감
                }
                Debug.Log("카메라 생명 감소: " + cameraLife);
            }
            else
            {
                Debug.LogWarning("카메라 생명 UI가 설정되지 않았습니다.");
            }
        }
    }

    private void GameOver()
    {
        // 게임 오버 로직
        currentPageIndex = 0; // 페이지 인덱스 초기화
        GameManager.inst.TurnToPage(currentPageIndex + 1);
        cameraLife = 3; // 카메라 생명 초기화
        ResetPage();
        // 다시하기 버튼 활성화, 클릭 시 RestPage 호출
        // 현재는 바로 ResetPage 호출

    }

    private void GameEnd()
    {
        // 게임 완료 시 엔딩 로직
    }

    public void ResetPage()
    {
        foreach(var cam in CameraLifeUIs)
        {
            UIManager.inst.OpenUI(cam); // 모든 카메라 생명 UI 활성화
        }
        UIManager.inst.CloseUI(PuzzleUI);
        UIManager.inst.OpenUI(NextPageBtn);
    }

    public void NextPage()
    {
        GameManager.inst.NextPage(SetNextPageUI);
        
    }

    private void SetNextPageUI()
    {
        UIManager.inst.OpenUI(PuzzleUI);
        if (!FilmContainer.activeSelf)
            UIManager.inst.OpenUI(FilmContainer);
        Setting();
        UIManager.inst.CloseUI(FilmContainer); // FilmContainerUI 닫기
        UIManager.inst.CloseUI(NextPageBtn); // 다음 페이지 버튼 비활성화
        // 페이지 설정 후 FilmContainer 닫기
    }

    private void filmSetting()
    {
        //모든 film에 pageDataSO의 pieceImgs를 랜덤으로 할당, 중복 불가, answerImgs는 BookFilm에 할당됨(순서대로)
        int[] randomindexs = new int[pageDataSOs[currentPageIndex].PieceImgs.Count]; // 랜덤 인덱스 배열 초기화


        for (int i = 0; i < films.Length; i++)
        {
            if (pageDataSOs.Length > currentPageIndex)
            {
                var pageData = pageDataSOs[currentPageIndex];
                if (pageData != null && pageData.PieceImgs.Count > 0)
                {
                    //pageData의 pieceImgs의 개수가 films의 개수보다 작을 경우 해당 films 오브젝트는 비활성화
                    // films[i]가 pageData.pieceImgs의 개수보다 크면 films[i]를 비활성화
                    if (i >= pageData.PieceImgs.Count)
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
                        randomIndex = Random.Range(0, pageData.PieceImgs.Count);
                    } while (randomindexs[randomIndex] != 0); // 이미 할당된 인덱스는 제외
                    randomindexs[randomIndex] = 1; // 해당 인덱스는 할당되었음을 표시

                    // films[i]의 pieceImg에 pageData.pieceImgs[randomIndex]를 할당
                    films[i].filmImage.sprite = pageData.PieceImgs[randomIndex];
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
                if (i >= pageDataSOs[currentPageIndex].AnswerImgs.Count)
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
                if (pageData != null && pageData.AnswerImgs.Count > i)
                {
                    bookFilms[i].answerID = pageData.AnswerImgs[i];
                }
            }
        }
    }

    private void DiarySetting()
    {
        UIManager.inst.OpenUI(hintDiary.gameObject);
        hintDiary.SetDiary(currentPageIndex);        
    }

    public void ShowHint()
    {
        if(pageDataSOs.Length > currentPageIndex)
        {
            // 현재 페이지의 힌트 키워드 가져오기

            // List를 string[]로 변환

            string[] keywords = pageDataSOs[currentPageIndex].Keywords?.ToArray();

            if (keywords != null && keywords.Length > 0)
            {
                hintDiary.ShowHint(currentPageIndex, keywords);
            }
            else
            {
                Debug.LogWarning("현재 페이지에 힌트 키워드가 없습니다.");
            }
        }
        else
        {
            Debug.LogError("현재 페이지 인덱스가 범위를 벗어났습니다.");
        }
    }
}
