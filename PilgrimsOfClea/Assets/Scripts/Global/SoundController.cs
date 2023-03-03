using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;

    [SerializeField] private AudioClip[] _sounds;
    [SerializeField] private AudioSource _efx;
    float _efxTimeFlow;
    float _efxTime;
    public bool IsEFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _efxTimeFlow = 0f;
        IsEFX = false;
        _sounds = Resources.LoadAll<AudioClip>("Sound");
        EventManager.SetEFXSoundID += SetEFX;
    }

    private void Update()
    {
        if (IsEFX)
        {
            _efxTimeFlow += Time.deltaTime;
            if( _efxTimeFlow > _efxTime)
            {
                _efx.Pause();
                _efxTimeFlow = 0f;
                IsEFX = false;
            }
        }
    }

    private void SetEFX(DataBase.SoundID value1, float value2)
    {
        _efxTime = value2;
        _efx.clip = _sounds[(int)value1];
        if(value2 == -1)
        {
            IsEFX = false;
            _efx.loop = true;
        }
        else if(value2 == 0)
        {
            IsEFX = false;
            _efx.loop = false;
        }
        else
        {
            IsEFX = true;
            _efx.loop = false;
        }
        _efx.Play();
    }



}
