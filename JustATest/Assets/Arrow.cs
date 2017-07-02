using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Vector3 _initPos;
    private float _timer = 0;
	// Use this for initialization
	void Start () {
        _initPos = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        _timer += Time.deltaTime * 5.0f;
        transform.localPosition = _initPos + transform.up * Mathf.Sin(_timer) / 4.0f;
    }
}
