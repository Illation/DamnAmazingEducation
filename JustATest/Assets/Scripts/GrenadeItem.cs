using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeItem : MonoBehaviour {
    private bool _thrown = false;
    public float ThrowingForce = 5.0f;
    public float FuseTime = 4.0f;
    private float _timer = 0;
    private Rigidbody _body;
    public bool Grab(Transform origin)
    {
        transform.SetParent(origin);
        transform.localPosition = Vector3.zero;
        if (_body) _body.isKinematic = true;
        return true;
    }

    public bool Release()
    {
        _thrown = true;
        if (_body)
        {
            _body.isKinematic = false;
            _body.AddForce(transform.parent.forward * ThrowingForce);
        }
        return true;
    }
    // Use this for initialization
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        if (_body != null) _body.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_thrown)
        {
            _timer += Time.deltaTime;

            if (_timer >= FuseTime)
            {
                ObjectController objCont = this.GetComponent<ObjectController>();
                if (objCont != null) objCont.Destroy();
            }
        }   
    }
}
