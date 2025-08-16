using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : GlobalSingletonMono<CameraManager>
{
    #region [ Components ] 
    [SerializeField] private CinemachineBrain brain;
    [SerializeField] private CinemachineCamera cam2d;
    [SerializeField] private CinemachineCamera cam3d;
    #endregion [ Components ]

    #region [ Fields ]
    private CinemachineCamera curCam;
    [Header("# Shake Info")]
    [SerializeField] private CinemachineBasicMultiChannelPerlin shakeCam;
    [SerializeField] private float frequencyValue;
    [SerializeField] private float amplitudeValue;
    private Coroutine cameraShakeEffectCoroutine;
    private WaitForSeconds wait;

    private Coroutine transCoroutine;
    private WaitForSeconds transDelayTime;
    #endregion [ Fields ]

    #region [ Unity Method ]
    #endregion [ Unity Method ]

    #region [ Public Method ]
    [ContextMenu("2to3")]
    public void TransCamera2To3()
    {
        cam2d.enabled = false;
        cam3d.enabled = true;
        curCam = cam3d;
    }
    [ContextMenu("3to2")]
    public void TransCamera3To2(Action onTransEnd = null)
    {
        curCam = cam2d;
        cam2d.enabled = true;
        cam3d.enabled = false;

        if (transCoroutine != null)
        {
            StopCoroutine(transCoroutine);
        }
        transCoroutine = StartCoroutine(delayOpen(onTransEnd));
    }

    private IEnumerator delayOpen(Action onTransEnd = null)
    {
        if (transDelayTime == null)
        {
            transDelayTime = new WaitForSeconds(brain.DefaultBlend.BlendTime);
        }

        yield return transDelayTime;

        // 차후에는 책 펼쳐지는게 끝나면 invoke
        onTransEnd?.Invoke();
    }

    public void CameraShakeEffect(float duration)
    {
        if (wait == null)
        {
            wait = new WaitForSeconds(duration);
        }

        if (cameraShakeEffectCoroutine != null)
        {
            StopCoroutine(cameraShakeEffectCoroutine);
        }

        cameraShakeEffectCoroutine = StartCoroutine(CameraShakeEffectCoroutine());
    }

    private IEnumerator CameraShakeEffectCoroutine()
    {
        shakeCam.FrequencyGain = frequencyValue;
        shakeCam.AmplitudeGain = amplitudeValue;
        yield return wait;
        shakeCam.FrequencyGain = 0f;
        shakeCam.AmplitudeGain = 0f;
    }
    #endregion [ Public Method ]
}
