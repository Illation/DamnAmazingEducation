using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericItem : MonoBehaviour, IItem
{
    public bool Grab(Transform origin)
    {
        transform.SetParent(origin);
        transform.localPosition = Vector3.zero;
        return true;
    }

    public bool Release()
    {
        ObjectController objCont = this.GetComponent<ObjectController>();
        if (objCont != null) objCont.Destroy();
        return true;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
