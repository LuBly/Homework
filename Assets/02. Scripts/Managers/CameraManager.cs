using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : GlobalSingletonMono<CameraManager>
{
    #region [ Components ] 
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
    #endregion [ Fields ]

    #region [ Unity Method ]
    /*
    private void Awake()
    {
        //shakeCam = cam2d.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    */
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
    public void TransCamera3To2()
    {
        cam2d.enabled = true;
        cam3d.enabled = false;
        curCam = cam2d;
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
