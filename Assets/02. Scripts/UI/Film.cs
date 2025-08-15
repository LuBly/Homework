using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 드래그 시작시 Film 하위의 이미지 오브젝트를 드래그&드롭
public class Film : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // 드래그 시작 시: 해당 오브젝트를 SetActive로 비활성화, 마우스 위치에 Prefab을 생성하여 드래그 시작

    public TestData FilmData; // 드래그된 이미지의 데이터, 차후 FilmData로 명칭 변경 후 ScriptableObject로 관리
    public static GameObject beingDraggImg; // 현재 드래그 중인 이미지 오브젝트
    public Image filmImage; // 드래그 중인 이미지 오브젝트의 이미지 컴포넌트
    private Color imageColor; // 드래그 중인 이미지의 색상 정보

    //Film Data라는 이름의 ScriptableObject를 통해 드래그된 이미지 정보 저장/관리

    public Vector3 startPosition; // 드래그 시작 위치, 중간에 놓칠 경우 원복위치. 부모 위치로 고정해야함

    // 이미지 드래그 중 변경할 부모 RacTransform
    [SerializeField] Transform onDragParent;

    [SerializeField] public Transform startParent; // 슬롯이 아닌 다른 오브젝트에 드랍시 원복할 부모

    // 드래그 시작 시 호출되는 함수
    public void OnBeginDrag(PointerEventData eventData)
    {
        beingDraggImg = gameObject;
        filmImage = GetComponent<Image>();
        if (filmImage != null)
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
        this.SetActive(false); // 드래그 종료 후 오브젝트 비활성화
        Debug.Log("드래그 종료");
    }

    public void OnClick()
    {
        // beingDraggImg가 null이 아니고 자기 자신일 때만 동작
        if (beingDraggImg == null || beingDraggImg != gameObject)
        {
            Debug.LogWarning("beingDraggImg가 null이거나 자기 자신이 아닙니다. OnClick 동작을 건너뜁니다.");
            return;
        }
        transform.SetParent(startParent);
        transform.position = startPosition;
        beingDraggImg = null;
        Debug.Log("이미지 클릭: Film 칸으로 되돌림");
    }   
}
