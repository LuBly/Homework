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
        // �迭 ���� ��� ��Ƽ������ ��ȸ�ؼ� �� �ʱ�ȭ ����
        if (UImaterial != null) // null üũ �ʼ�
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
            //��Ƽ���� ��������
            tempvalue = Mathf.Lerp(0f, 1f, elapsed / DrawInDurationTime);
            UImaterial.SetFloat("_LerpValue", tempvalue);

            yield return null;  // ���� �����ӱ��� ���
        }
        //��Ƽ���� Ȯ�α������� ������ �� ��Ȯ�ϰ� ����
        tempvalue = 1f;
        UImaterial.SetFloat("_LerpValue", tempvalue);

        Debug.Log("���� ���̵� �� �ִϸ��̼� �Ϸ�");
    }

    IEnumerator UIDrawOutCoroutine()
    {
        float elapsed = 0f;
        float tempvalue = 1.0f;

        while (elapsed < DrawOutDurationTime)
        {
            elapsed += Time.deltaTime;
            //��Ƽ���� ��������
            tempvalue = Mathf.Lerp(1f, 0f, elapsed / DrawOutDurationTime);
            UImaterial.SetFloat("_LerpValue", tempvalue);

            yield return null;  // ���� �����ӱ��� ���
        }
        //��Ƽ���� Ȯ�α������� ������ �� ��Ȯ�ϰ� ����
        tempvalue = 0.0f;
        UImaterial.SetFloat("_LerpValue", tempvalue);

        Debug.Log("���� ���̵� �ƿ� �ִϸ��̼� �Ϸ�");
        this.SetActive(false);
    }
}
