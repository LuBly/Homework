using System.Collections;
using UnityEngine;

public class VideoAnmiationMatController : MonoBehaviour
{
    public Material UImaterial;
    public float DrawInDurationTime;
    public float DrawOutDurationTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 배열 안의 모든 머티리얼을 순회해서 값 초기화 구간
        if (UImaterial != null) // null 체크 필수
        {
            UImaterial.SetFloat("_LerpValue", 0);
        }
    }

    public void UIDrawIn()
    {
        StartCoroutine(UIDrawInCoroutine());
    }
    public void UIDrawOut()
    {
        StartCoroutine(UIDrawOutCoroutine());
    }


    IEnumerator UIDrawInCoroutine()
    {
        float elapsed = 0f;
        float tempvalue = 0f;

        while (elapsed < DrawInDurationTime)
        {
            elapsed += Time.deltaTime;
            //머티리얼 조정구간
            tempvalue = Mathf.Lerp(0f, 1f, elapsed / DrawInDurationTime);
            UImaterial.SetFloat("_LerpValue", tempvalue);

            yield return null;  // 다음 프레임까지 대기
        }
        //머티리얼 확인구간이자 마지막 값 정확하게 세팅
        tempvalue = 1f;
        UImaterial.SetFloat("_LerpValue", tempvalue);

        Debug.Log("비디오 페이드 인 애니메이션 완료");
    }

    IEnumerator UIDrawOutCoroutine()
    {
        float elapsed = 0f;
        float tempvalue = 1.0f;

        while (elapsed < DrawOutDurationTime)
        {
            elapsed += Time.deltaTime;
            //머티리얼 조정구간
            tempvalue = Mathf.Lerp(1f, 0f, elapsed / DrawOutDurationTime);
            UImaterial.SetFloat("_LerpValue", tempvalue);

            yield return null;  // 다음 프레임까지 대기
        }
        //머티리얼 확인구간이자 마지막 값 정확하게 세팅
        tempvalue = 0.0f;
        UImaterial.SetFloat("_LerpValue", tempvalue);

        Debug.Log("비디오 페이드 아웃 애니메이션 완료");
        this.SetActive(false);
    }
}
