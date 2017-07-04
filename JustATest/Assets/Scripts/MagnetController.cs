using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour {

    [SerializeField]
    PlayerController Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.parent.position + new Vector3(0, 1 + Player.ArmHeight, 0);	
	}
}
