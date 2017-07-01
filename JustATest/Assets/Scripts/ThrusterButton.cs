using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterButton : MonoBehaviour
{
    [SerializeField]
    bool IsLeft = true;

    private WallController _wall;

	// Use this for initialization
	void Start ()
    {
        _wall = (GameObject.Find("Wall")).GetComponent<WallController>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var thrusters = IsLeft ? _wall.LeftThrusters : _wall.RightThrusters;
            foreach(var thruster in thrusters)
            {
                thruster.Activate();
            }
        }
    }
}
