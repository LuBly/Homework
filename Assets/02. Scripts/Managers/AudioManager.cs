
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : GlobalSingletonMono<AudioManager>
{
    #region [ Defines ]
    private const float initMasterVolume = 1f;
    private const float initBGMVolume = 1f;
    private const float initSFXVolume = 1f;

    [Serializable]
    public struct AudioData
    {
        public string name;
        public AudioClip clip;
    }
    #endregion [ Defines ]

    #region [ Components ]
    [SerializeField] private AudioComponent bgmPref;
    [SerializeField] private AudioComponent sfxPref;

    private AudioComponent bgmInst;
    private AudioComponent sfxInst;
    #endregion [ Components ]

    #region [ Fields ]
    private AudioMixer audioMixer;
    private float masterVolume = 1f;
    private float bgmVolume = 1f;
    private float sfxVolume = 1f;
    private bool canDuplicate = false;

    private bool isSFXOn;
    private bool isBGMOn;

    [field : SerializeField] public List<AudioData> audioDatas { get; private set; } = new List<AudioData>();
    public Dictionary<string, AudioClip> audioDictionary { get; private set; } = new Dictionary<string, AudioClip>();
    #endregion [ Fields ]

    #region [ Unity Method ]

    private void Start()
    {
        foreach(var data in audioDatas)
        {
            audioDictionary.Add(data.name, data.clip);
        }
        PlayBGM(audioDictionary["TitleBGM"]);
    }
    #endregion [ Unity Method ]

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
    public void PlayBGM(AudioClip clip, bool isLoop = true)
    {
        if(bgmInst == null)
        {
            var go = Instantiate(bgmPref, transform).gameObject;
            bgmInst = go.GetOrAddComponent<AudioComponent>();
        }

        bgmInst.Play(clip, isLoop);
    }

    public void PlaySFX(AudioClip clip, bool isLoop = false)
    {
        if (sfxInst == null)
        {
            var go = Instantiate(sfxPref, transform).gameObject;
            sfxInst = go.GetOrAddComponent<AudioComponent>();
        }

        sfxInst.Play(clip, isLoop);
    }

    #endregion

    #region [ Private Method ]
    #endregion [ Private Method ]
}
