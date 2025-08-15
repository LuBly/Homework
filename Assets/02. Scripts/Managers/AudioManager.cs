
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : GlobalSingletonMono<AudioManager>, IAudioPool
{
    #region [ Defines ]
    private const float initMasterVolume = 1f;
    private const float initBGMVolume = 1f;
    private const float initSFXVolume = 1f;
    #endregion [ Defines ]

    #region [ Components ]
    [SerializeField] private AudioComponent bgmInst;
    [SerializeField] private AudioComponent sfxInst;
    #endregion [ Components ]

    #region [ Fields ]
    private AudioMixer audioMixer;
    private AudioSource audioSource;
    private float masterVolume = 1f;
    private float bgmVolume = 1f;
    private float sfxVolume = 1f;
    private bool canDuplicate = false;

    private bool isSFXOn;
    private bool isBGMOn;
    #endregion [ Fields ]

    #region [ Properties ]
    public float MasterVolume => masterVolume;
    public float BGMVolume => bgmVolume;
    public float SFXVolume => sfxVolume;
    public bool IsSFXOn => isSFXOn;
    public bool IsBGMOn => isBGMOn;
    #endregion [ Properties ]

    #region [ Unity Method ]
    #endregion [ Unity Method ]

    #region [ Public Method ] 
    public void Play(AudioClip clip, bool isOneShot = false)
    {
        if(bgmInst == null)
        {
            var go = Instantiate(bgmInst, transform).gameObject;
            bgmInst = go.GetOrAddComponent<AudioComponent>();
        }

        bgmInst.Play(clip, isOneShot);
    }

    #endregion

    #region [ Private Method ]
    protected override void OnCreated()
    {
        audioSource = GetComponent<AudioSource>();
    }
    #endregion [ Private Method ]
}
