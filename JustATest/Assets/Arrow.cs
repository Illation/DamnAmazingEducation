using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Vector3 _initPos;
	// Use this for initialization
	void Start () {
        _initPos = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = _initPos + transform.up * Mathf.Sin(Time.realtimeSinceStartup * 5.0f) / 4.0f;
    }
}
