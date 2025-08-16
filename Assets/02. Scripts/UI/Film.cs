using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 드래그 시작시 Film 하위의 이미지 오브젝트를 드래그&드롭
public class Film : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    // 드래그 시작 시: 해당 오브젝트를 SetActive로 비활성화, 마우스 위치에 Prefab을 생성하여 드래그 시작

    public Sprite FilmData; // 드래그된 이미지의 데이터(실제 비교에 사용)
    public static GameObject beingDraggImg; // 현재 드래그 중인 이미지 오브젝트
    public Image filmImage; // 드래그 중인 이미지 오브젝트의 이미지 컴포넌트
    private Color imageColor; // 드래그 중인 이미지의 색상 정보
    [SerializeField] private FilmPanel filmPanel; // 클릭 시 해당 이미지를 확대하여 보여줄 패널

    //Film Data라는 이름의 ScriptableObject를 통해 드래그된 이미지 정보 저장/관리

    public Vector3 startPosition; // 드래그 시작 위치, 중간에 놓칠 경우 원복위치. 부모 위치로 고정해야함

    // 이미지 드래그 중 변경할 부모 RacTransform
    [SerializeField] Transform onDragParent;

    [SerializeField] public Transform startParent; // 슬롯이 아닌 다른 오브젝트에 드랍시 원복할 부모

    private void Awake()
    {
        filmImage = GetComponent<Image>();
    }

    // 드래그 시작 시 호출되는 함수
    public void OnBeginDrag(PointerEventData eventData)
    {
        var clip = AudioManager.inst.audioDictionary["FilmInteractionSFX"];
        AudioManager.inst.PlaySFX(clip);
        beingDraggImg = gameObject;
        if(filmImage == null)
        {
            filmImage = GetComponent<Image>();
        }
        else if (filmImage != null)
        {
            filmImage.raycastTarget = false;
            imageColor = filmImage.color;
            imageColor.a = 0.5f;
            filmImage.color = imageColor;
        }
        var cg = GetComponent<CanvasGroup>();
        if (cg == null)
            cg = gameObject.AddComponent<CanvasGroup>();
        cg.blocksRaycasts = false;

        startPosition = transform.position; // 드래그 시작 위치 저장
        startParent = transform.parent; // 드래그 시작 부모 저장

        transform.SetParent(onDragParent);
        // FilmImage의 Sprite를 FilmData에 저장
        FilmData = filmImage.sprite;
        Debug.Log("드래그 시작");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // 마우스 위치에 따라 오브젝트 위치 변경
        if (filmImage != null)
        {
            filmImage.color = imageColor; // 드래그 중인 이미지의 색상 적용
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        beingDraggImg = null;
        var cg = GetComponent<CanvasGroup>();
        if (cg != null)
            cg.blocksRaycasts = true;
        if (filmImage != null)
        {
            filmImage.raycastTarget = true;
            imageColor.a = 1f;
            filmImage.color = imageColor;
        }
        transform.SetParent(startParent);
        transform.position = startParent.position; // 기존 부모 오브젝트의 위치로 이동

        // 드래그 종료 후, 정상적으로 drop되었으면 SetActive(false), 아니라면 활성화 상태로 유지

        if (transform.parent == onDragParent)
        {
            gameObject.SetActive(false); // 드래그 종료 후, 부모가 onDragParent라면 비활성화
        }
        else
        {
            // 다른 부모에 드롭되었을 경우, 아무동작도 하지 않음
        }

        Debug.Log("드래그 종료");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var clip = AudioManager.inst.audioDictionary["FilmInteractionSFX"];
        AudioManager.inst.PlaySFX(clip);
        // 클릭하면 해당 이미지 확대 => 다시 누르면 원래 자리에 원래 크기로
        // 혹은 클릭 시 DisplayImage로 확대된 이미지 표시
        if (filmPanel != null && filmImage != null)
        {
            filmPanel.gameObject.SetActive(true);
            filmPanel.panelImage.sprite = filmImage.sprite; // 패널 이미지에 드래그된 이미지의 스프라이트 설정
        }
        else
        {
            Debug.Log($" FilmPanel이 비활성화 상태이거나, filmImage가 null입니다. FilmPanel: {filmPanel}, filmImage: {filmImage}");
        }
    }
}
