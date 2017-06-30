using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHandler : MonoBehaviour {

    private Transform _transSelf;
    private bool _itemPickedUp;
	// Use this for initialization
	void Start () {
        _transSelf = transform;
	}
	
	// Update is called once per frame
	void Update () {
        // pick up item
        // if item.Grab()
        // _itemPickedUp = true;
        // if item.Release()
        // _itemPickedUp = false;
	}
}
