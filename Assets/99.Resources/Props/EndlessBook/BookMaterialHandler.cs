using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class BookMaterialHandler : MonoBehaviour
{
    public Material[] materials;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 배열 안의 모든 머티리얼을 순회해서 값 초기화 구간
        foreach (Material mat in materials)
        {
            if (mat != null) // null 체크 필수
            {
                mat.SetFloat("_LerpValue", 0);
                mat.SetFloat("_TimeGoStoper", 0);
            }
        }
    }

    public float FadeDuration;
    public float ScrollDuration;

    public void FirstPageGo()
    {
        StartCoroutine(FadePageCoroutine(0));
    }
    public void ThirdPageGo()
    {
        StartCoroutine(FadePageCoroutine(1));
    }

    public void FifthPageGo()
    {
        StartCoroutine(FadePageCoroutine(2));
    }
    public void SeventhPageGo()
    {
        StartCoroutine(FadePageCoroutine(3));
    }
    public void NinethPageGo()
    {
        StartCoroutine(FadePageCoroutine(4));
    }

    IEnumerator FadePageCoroutine(int index)
    {
        float elapsed = 0f;
        float tempvalue = 0f;

        while (elapsed < FadeDuration)
        {
            elapsed += Time.deltaTime;
            //머티리얼 조정구간
            tempvalue = Mathf.Lerp(0f, 1f, elapsed / FadeDuration);
            materials[index].SetFloat("_LerpValue", tempvalue);

            Debug.Log(tempvalue); // 현재 값 확인
            yield return null;  // 다음 프레임까지 대기
        }

        //머티리얼 확인구간이자 마지막 값 정확하게 세팅
        tempvalue = 1f;
        materials[index].SetFloat("_LerpValue", tempvalue);

        StartCoroutine(ScrollPageCoroutine(index));
    }

    IEnumerator ScrollPageCoroutine(int index)
    {
        float elapsed = 0f;
        float tempvalue = 0f;

        while (elapsed < ScrollDuration)
        {
            elapsed += Time.deltaTime;
            //머티리얼 조정구간
            tempvalue = Mathf.Lerp(0f, 1f, elapsed / ScrollDuration);
            materials[index].SetFloat("_TimeGoStoper", tempvalue);

            Debug.Log(tempvalue); // 현재 값 확인
            yield return null;  // 다음 프레임까지 대기
        }

        //머티리얼 확인구간이자 마지막 값 정확하게 세팅
        tempvalue = 1f;
        materials[index].SetFloat("_TimeGoStoper", tempvalue);


        var videoClip = PageSetting.inst.pageDataSOs[index].VideoClip;
        VideoManager.inst.PlayVideo(videoClip);
    }
}