using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{

    public float Thrust = 0;
    public bool IsLoaded = false;
    public bool IsActivated = false;

    [SerializeField]
    private float ThrustDeceleration = 0.25f;
    private float _thrustDec = 0;

    [SerializeField]
    ParticleSystem ThrustParticles;
    [SerializeField]
    float ThrustParticleLifetime = 2;

    [SerializeField]
    GameObject FuelCell;

	// Use this for initialization
	void Start ()
    {
        IsLoaded = false;
        FuelCell.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Thrust > 0)
        {
            _thrustDec += ThrustDeceleration*Time.deltaTime;
            Thrust -= _thrustDec * Time.deltaTime;
            if(Thrust<0)
            {
                _thrustDec = 0;
                Thrust = 0;
                IsActivated = false;
                IsLoaded = false;
                FuelCell.SetActive(false);
            }
        }
        ThrustParticles.startLifetime = ThrustParticleLifetime * Thrust;
	}

    public void Activate()
    {
        if((!IsActivated) && (IsLoaded))
        {
            Thrust = 1;
            IsActivated = true;
        }
    }

    public void Load()
    {
        IsLoaded = true;
        FuelCell.SetActive(true);
    }
}
