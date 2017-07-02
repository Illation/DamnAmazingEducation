using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GlobalSounds
{
    LeftMove,
    RightMove,

    EmpThrow,
    EmpExplode,
    EmpDefuse,
    GrenadeExplode,
    GrenadeThrow,
    ItemSpawn,
    PickUpEmpGrenade,
    PickUpFuel,
    PlaceFuelTank,

    ButtonPress,
    DeflectGrenade,
    EmpCharge,
    EmpLockTarget,
    EmpScrollTarget,
    RocketThrusters,

    UI1,
    UI2,
    UI3
}

public enum SourcePosition
{
    Left, 
    Right,
    Center
}

public class GlobalSoundManager : MonoBehaviour
{
    [HideInInspector]
    public static GlobalSoundManager instance = null; // Static instance of GameManager which allows it to be accessed by any other script.

    [Header("Source Settings")]
    [SerializeField]
    AudioSource SourceLeft;
    [SerializeField]
    AudioSource SourceRight;
    [SerializeField]
    AudioSource SourceLeftPlayer;
    [SerializeField]
    AudioSource SourceRightPlayer;
    [SerializeField]
    AudioSource SourceLeftThrust;
    [SerializeField]
    AudioSource SourceRightThrust;
    [SerializeField]
    AudioSource SourceCenter;
    [SerializeField]
    AudioSource SourceEmpCharge;
    [SerializeField]
    float SideSourceDistance = 0.4f;


    [Header("Music")]
    [SerializeField]
    AudioClip Music;
    [SerializeField]
    float MusicVolume = 1;
    private AudioSource _sourceMusic;

    [Header("Player Sounds")]
    [SerializeField] AudioClip LeftMove;
    [SerializeField] AudioClip RightMove;
    [Header("Item Sounds")]
    [SerializeField] AudioClip EmpThrow;
    [SerializeField] AudioClip EmpExplode;
    [SerializeField] AudioClip EmpDefuse;
    [SerializeField] AudioClip GrenadeExplode;
    [SerializeField] AudioClip GrenadeThrow;
    [SerializeField] AudioClip ItemSpawn;
    [SerializeField] AudioClip PickUpEmpGrenade;
    [SerializeField] AudioClip PickUpFuel;
    [SerializeField] AudioClip PlaceFuelTank;
    [Header("Other Sounds")]
    [SerializeField] AudioClip ButtonPress;
    [SerializeField] AudioClip DeflectGrenade;
    [SerializeField] AudioClip EmpCharge;
    [SerializeField] AudioClip EmpLockTarget;
    [SerializeField] AudioClip EmpScrollTarget;
    [SerializeField] AudioClip RocketThrusters;
    [Header("UI Sounds")]
    [SerializeField] AudioClip UI1;
    [SerializeField] AudioClip UI2;
    [SerializeField] AudioClip UI3; 

    //
    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start ()
    {
        _sourceMusic = GetComponent<AudioSource>();
        _sourceMusic.clip = Music;
        _sourceMusic.loop = true;

        SourceLeftPlayer.clip = LeftMove;
        SourceLeftPlayer.loop = true;
        SourceLeftPlayer.volume = 0;
        SourceLeftPlayer.Play();

        SourceRightPlayer.clip = RightMove;
        SourceRightPlayer.loop = true;
        SourceRightPlayer.volume = 0;
        SourceRightPlayer.Play();

        SourceLeftThrust.clip = RocketThrusters;
        SourceLeftThrust.loop = true;
        SourceLeftThrust.volume = 0;
        SourceLeftThrust.Play();

        SourceRightThrust.clip = RocketThrusters;
        SourceRightThrust.loop = true;
        SourceRightThrust.volume = 0;
        SourceRightThrust.Play();

        SourceEmpCharge.clip = EmpCharge;
        SourceEmpCharge.loop = false;
        SourceEmpCharge.volume = 1;

        SetMusicPlaying(true);
	}
	
	// Update is called once per frame
	void Update ()
    {
        _sourceMusic.volume = MusicVolume;

        SourceLeft.transform.localPosition = new Vector3(0, 0, -SideSourceDistance);
        SourceRight.transform.localPosition = new Vector3(0, 0, SideSourceDistance);
	}

    AudioSource GetSource(SourcePosition pos)
    {
        switch (pos)
        {
            case SourcePosition.Left:
                return SourceLeft;
            case SourcePosition.Right:
                return SourceRight;
            case SourcePosition.Center:
                return SourceCenter;
        }
        return null;
    }

    AudioClip GetClip(GlobalSounds clip)
    {
        switch (clip)
        {
            case GlobalSounds.LeftMove:
                return LeftMove;
            case GlobalSounds.RightMove:
                return RightMove;
            case GlobalSounds.EmpThrow:
                return EmpThrow;
            case GlobalSounds.EmpExplode:
                return EmpExplode;
            case GlobalSounds.EmpDefuse:
                return EmpDefuse;
            case GlobalSounds.GrenadeExplode:
                return GrenadeExplode;
            case GlobalSounds.GrenadeThrow:
                return GrenadeThrow;
            case GlobalSounds.ItemSpawn:
                return ItemSpawn;
            case GlobalSounds.PickUpEmpGrenade:
                return PickUpEmpGrenade;
            case GlobalSounds.PickUpFuel:
                return PickUpFuel;
            case GlobalSounds.PlaceFuelTank:
                return PlaceFuelTank;
            case GlobalSounds.ButtonPress:
                return ButtonPress;
            case GlobalSounds.DeflectGrenade:
                return DeflectGrenade;
            case GlobalSounds.EmpCharge:
                return EmpCharge;
            case GlobalSounds.EmpLockTarget:
                return EmpLockTarget;
            case GlobalSounds.EmpScrollTarget:
                return EmpScrollTarget;
            case GlobalSounds.RocketThrusters:
                return RocketThrusters;
            case GlobalSounds.UI1:
                return UI1;
            case GlobalSounds.UI2:
                return UI2;
            case GlobalSounds.UI3:
                return UI3;
        }
        return null;
    }

    public void PlayClip(GlobalSounds clip, SourcePosition pos, float volume)
    {
        AudioSource aSource = GetSource(pos);
        AudioClip aClip = GetClip(clip);
        aSource.PlayOneShot(aClip, volume);
    }

    public void SetMusicPlaying(bool playing)
    {
        if(playing)
        {
            if(_sourceMusic.isPlaying)
            {
                _sourceMusic.UnPause();
            }
            else
            {
                _sourceMusic.Play();
            }
        }
        else
        {
            _sourceMusic.Pause();
        }
    }

    public void SetPlayerVolumePitch(bool left, float volume, float pitch)
    {
        if(left)
        {
            SourceLeftPlayer.volume = volume;
            SourceLeftPlayer.pitch = pitch;
        }
        else
        {
            SourceRightPlayer.volume = volume;
            SourceRightPlayer.pitch = pitch;
        }
    }
    public void SetThrusterVolume(bool left, float volume)
    {
        if(left)
        {
            SourceLeftThrust.volume = volume;
        }
        else
        {
            SourceRightThrust.volume = volume;
        }
    }
    public void PlayStopEmp(bool play)
    {
        if(play)
        {
            SourceEmpCharge.Play();
            SourceEmpCharge.volume = 1;
            SourceEmpCharge.clip = EmpCharge;
        }
        else
        {
            SourceEmpCharge.Stop();
        }
    }
}
