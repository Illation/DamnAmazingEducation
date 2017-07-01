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
    [SerializeField]
    GameObject WallMesh;

    [Header("ThrusterSettings")]
    [SerializeField]
    Thruster ThrusterPrefab;
    [SerializeField]
    uint NumThrusters = 3;

    public List<Thruster> LeftThrusters;
    public List<Thruster> RightThrusters;

    [Header("Movement")]
    [SerializeField]
    float FieldWidth;
    [SerializeField]
    float MovementMultiplier = 1;
    [SerializeField]
    float MovementDampener = 1;
    private float _movement = 0;

	// Use this for initialization
	void Start ()
    {
        WallMesh.transform.localScale = new Vector3(WallWidth, 1, WallDepth);

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
        _movement += wallForce * MovementMultiplier * Time.deltaTime;
        _movement -= MovementDampener * _movement * Time.deltaTime;

        transform.position += new Vector3(0, 0, _movement) * Time.deltaTime;

        //end state
        if(transform.position.z > FieldWidth)
        {
            //left player wins
        }
        else if(transform.position.z < -FieldWidth)
        {
            //right player wins
        }
	}
}
