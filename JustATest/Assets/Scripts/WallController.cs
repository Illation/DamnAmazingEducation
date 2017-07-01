using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{

    [Header("WallShape")]
    [SerializeField]
    float WallWidth = 10;
    [SerializeField]
    float WallDepth = 10;

    [Header("ThrusterSettings")]
    [SerializeField]
    Thruster ThrusterPrefab;
    [SerializeField]
    uint NumThrusters = 3;

    public List<Thruster> LeftThrusters;
    public List<Thruster> RightThrusters;

    [Header("Movement")]
    [SerializeField]
    float MovementMultiplier = 1;
    [SerializeField]
    float MovementDampener = 1;
    private float _movement = 0;

    [Header("Upgrading")]
    [SerializeField]
    float UpgradeMultiplier = 10;
    [SerializeField]
    float UpgradeTime = 10;
    private float _upgradeTimer = 0;
    private uint _upgradeLevel = 0;

    [Header("EndGame")]
    [SerializeField]
    float FieldWidth;
    [SerializeField]
    Explosion ExplosionPrefab;
    public bool LeftWon = false;
    public bool RightWon = false;

	// Use this for initialization
	void Start ()
    {
        float PosOffsetDelta = WallWidth / (NumThrusters);

        LeftThrusters = new List<Thruster>();
        for (int i = 0; i < NumThrusters; i++)
        {
            var instance = Instantiate(ThrusterPrefab, 
                new Vector3(-WallWidth*0.5f+PosOffsetDelta*0.5f+i*PosOffsetDelta, transform.position.y, -WallDepth*0.5f), 
                Quaternion.Euler(new Vector3(0, 180, 180)), this.transform);
            LeftThrusters.Add(instance);
        }

        RightThrusters = new List<Thruster>();
        for (int i = 0; i < NumThrusters; i++)
        {
            var instance = Instantiate(ThrusterPrefab, 
                new Vector3(-WallWidth * 0.5f + PosOffsetDelta *0.5f+i*PosOffsetDelta, transform.position.y, WallDepth*0.5f), 
                Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);
            RightThrusters.Add(instance);
        }
	}
	
	void Update ()
    {
        //Move Wall
        float wallForce = 0;
        for (int i = 0; i < NumThrusters; i++)
        {
            wallForce += LeftThrusters[i].Thrust;

            wallForce -= RightThrusters[i].Thrust;
        }
        float moveMult = MovementMultiplier + UpgradeMultiplier * _upgradeLevel * _upgradeLevel;

        _movement += wallForce * moveMult * Time.deltaTime;
        _movement -= MovementDampener * _movement * Time.deltaTime;

        transform.position += new Vector3(0, 0, _movement) * Time.deltaTime;

        //Upgrade Thrusters
        _upgradeTimer += Time.deltaTime;
        if(_upgradeTimer > UpgradeTime && _upgradeLevel <= 2)
        {
            _upgradeLevel++;
            for (int i = 0; i < NumThrusters; i++)
            {
                LeftThrusters[i].UpgradeLevel = _upgradeLevel;
                RightThrusters[i].UpgradeLevel = _upgradeLevel;
            }
            _upgradeTimer = 0;
        }

        //end state
        if(transform.position.z > FieldWidth)
        {
            //left player wins
            LeftWon = true;
        }
        else if(transform.position.z < -FieldWidth)
        {
            //right player wins
            RightWon = true;
        }

        if(LeftWon)
        {
            foreach(var thruster in RightThrusters)
            {
                Instantiate(ExplosionPrefab, thruster.transform.position, Quaternion.identity);
            }
        }
        else if(RightWon)
        {
            foreach(var thruster in LeftThrusters)
            {
                Instantiate(ExplosionPrefab, thruster.transform.position, Quaternion.identity);
            }
        }
	}
}
