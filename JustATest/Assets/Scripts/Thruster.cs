using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    public float Thrust = 0;
    public bool IsLoaded = false;
    public bool IsActivated = false;
    public bool FuelInRange = false;
    public bool IsHighlighted = false;
    public bool FuelHighlightEnabled = false;
    [SerializeField]
    private float ThrustDeceleration = 0.25f;
    private float _thrustDec = 0;

    [SerializeField]
    public ParticleSystem ThrustParticles, FuelLoadingParticle, DischargeParticle;
    [SerializeField]
    float ThrustParticleLifetime = 2;

    [SerializeField]
    GameObject FuelCell;

    [SerializeField]
    public uint UpgradeLevel = 0;
    [SerializeField]
    GameObject Upgrade1;
    [SerializeField]
    GameObject Upgrade2;

    [SerializeField]
    GameObject Highlight;
    [SerializeField]
    GameObject FuelHighlight;
    // Use this for initialization
    void Start ()
    {
        IsLoaded = false;
        FuelCell.SetActive(false);
        //ElectricityParticles.Stop();
    }
	
	// Update is called once per frame
	void Update ()
    {
        FuelHighlight.SetActive(FuelHighlightEnabled);
        if (Thrust > 0)
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
        ThrustParticles.startLifetime = ThrustParticleLifetime * Thrust * (UpgradeLevel + 1);

        switch (UpgradeLevel)
        {
            case 0:
                Upgrade1.SetActive(false);
                Upgrade2.SetActive(false);
                break;
            case 1:
                Upgrade1.SetActive(true);
                Upgrade2.SetActive(false);
                break;
            case 2:
                Upgrade1.SetActive(true);
                Upgrade2.SetActive(true);
                break;
        }

        Highlight.SetActive(IsHighlighted);
        FuelHighlight.SetActive(FuelHighlightEnabled);
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
        FuelLoadingParticle.Play();
        FuelCell.SetActive(true);
    }

    public void Discharge()
    {
        _thrustDec = 0;
        Thrust = 0;
        IsActivated = false;
        IsLoaded = false;
        FuelCell.SetActive(false);
        DischargeParticle.Simulate(0, true, true);
        DischargeParticle.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Fuel Item"))
        {
            FuelInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Fuel Item"))
        {
            FuelInRange = false;
        }
    }
}
