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

    private List<Thruster> LeftThrusters;
    private List<Thruster> RightThrusters;

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
                Quaternion.Euler(new Vector3(0, 180, 0)), this.transform);
            LeftThrusters.Add(instance);
        }

        RightThrusters = new List<Thruster>();
        for (int i = 0; i < NumThrusters; i++)
        {
            var instance = Instantiate(ThrusterPrefab, 
                new Vector3(-WallWidth * 0.5f + PosOffsetDelta *0.5f+i*PosOffsetDelta, transform.position.y, WallDepth*0.5f), 
                Quaternion.identity, this.transform);
            RightThrusters.Add(instance);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        		
	}
}
