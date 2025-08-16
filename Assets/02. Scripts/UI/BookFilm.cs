using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 드래그 드롭을 받는 곳
// 드롭을 받으면 해당 위치에 드래그된 이미지를 배치하고, 클릭 시 해당 오브젝트를 해제하여 Film 칸으로 되돌리는 기능을 구현
public class BookFilm : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    // 드래그된 오브젝트를 받았을 때 호출되는 함수
    [SerializeField] private Image filmImage; // 현재 오브젝트의 Image 컴포넌트
    [SerializeField] public Sprite answerID; // Film ID, 드래그된 이미지와 비교하여 정답 여부를 판단하기 위한 ID
    private GameObject droppedFilmObj; // 드롭된 Film 오브젝트 기억


    private void Awake()
    {
        Img(); // 현재 오브젝트의 Image 컴포넌트를 가져옴
    }

    Image Img()
    {
        // 현재 Image 컴포넌트를 가져와서 반환(본인 오브젝트)
        if (filmImage == null)
        {
            filmImage = GetComponent<Image>();
            if (filmImage != null)
            {
                return filmImage;
            }
        }
        else
        {
            return filmImage;
        }

        return null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 기존에 드래그 되었던 이미지가 있다면 해당 오브젝트를 활성화하고, 드래그 중이던 이미지의  스프라이트로 변경

        if (droppedFilmObj != null)
        {
            ResetImg(); // 이전에 드롭된 Film 오브젝트의 상태를 초기화
        }
        if (Film.beingDraggImg != null)
        {
            Image draggedImage = Film.beingDraggImg.GetComponent<Image>();
            if (draggedImage != null)
            {
                filmImage.sprite = draggedImage.sprite; // 드래그된 이미지의 스프라이트를 현재 오브젝트의 이미지로 설정
                Debug.Log("드롭된 이미지: " + filmImage.sprite.name);
                droppedFilmObj = Film.beingDraggImg; // 드롭된 Film 오브젝트 기억                
                droppedFilmObj.SetActive(false); // Film 오브젝트 비활성화
            }            
        }
        IsCorrectFilm(); // 정답 여부 확인
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭 시 드롭된 Film 오브젝트 활성화, BookFilm 이미지 제거
        if (droppedFilmObj != null)
        {
            ResetImg(); // 드롭된 Film 오브젝트의 상태를 초기화
            IsCorrectFilm(); // 정답 여부 확인
        }
    }

    public bool IsCorrectFilm()
    {
        // 오브젝트가 비활성화면 정답 체크에서 제외
        if (!gameObject.activeSelf)
            return false;
        // droppedFilmObj의 FilmData(Sprite)와 answerID(Sprite)를 비교하여 정답 여부를 반환
        if (droppedFilmObj != null)
        {
            Film film = droppedFilmObj.GetComponent<Film>();
            if (film != null && film.FilmData != null && answerID != null)
            {
                return film.FilmData == answerID; // Film ID가 일치하면 true, 아니면 false
            }
        }
        return false; // 드롭된 이미지가 없거나 Film 컴포넌트가 없으면 false
    }

    public void ResetImg()
    {
        Film film = droppedFilmObj.GetComponent<Film>();
        if (film != null)
        {
            droppedFilmObj.SetActive(true);
            droppedFilmObj.transform.SetParent(film.startParent); // 부모 복원
            droppedFilmObj.transform.position = film.startPosition; // 위치 복원
                                                                    // 레이캐스트 타겟 활성화, 이미지 색상 원복(alpha 1로 설정), block raycasts 활성화

            Image image = droppedFilmObj.GetComponent<Image>();
            image.raycastTarget = true;
            var cg = droppedFilmObj.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.blocksRaycasts = true; // Raycast 차단 해제
            }
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f); // 이미지 색상 원복
        }
        filmImage.sprite = null;
        droppedFilmObj = null;
        Film.beingDraggImg = null; // 드래그 중인 이미지 초기화
    }
}