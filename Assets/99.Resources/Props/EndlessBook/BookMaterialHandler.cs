using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class BookMaterialHandler : MonoBehaviour
{
    public Material[] materials;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �迭 ���� ��� ��Ƽ������ ��ȸ�ؼ� �� �ʱ�ȭ ����
        foreach (Material mat in materials)
        {
            if (mat != null) // null üũ �ʼ�
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
            //��Ƽ���� ��������
            tempvalue = Mathf.Lerp(0f, 1f, elapsed / FadeDuration);
            materials[index].SetFloat("_LerpValue", tempvalue);

            Debug.Log(tempvalue); // ���� �� Ȯ��
            yield return null;  // ���� �����ӱ��� ���
        }

        //��Ƽ���� Ȯ�α������� ������ �� ��Ȯ�ϰ� ����
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
            //��Ƽ���� ��������
            tempvalue = Mathf.Lerp(0f, 1f, elapsed / ScrollDuration);
            materials[index].SetFloat("_TimeGoStoper", tempvalue);

            Debug.Log(tempvalue); // ���� �� Ȯ��
            yield return null;  // ���� �����ӱ��� ���
        }

        //��Ƽ���� Ȯ�α������� ������ �� ��Ȯ�ϰ� ����
        tempvalue = 1f;
        materials[index].SetFloat("_TimeGoStoper", tempvalue);


        var videoClip = PageSetting.inst.pageDataSOs[index].VideoClip;
        VideoManager.inst.PlayVideo(videoClip);
    }
}