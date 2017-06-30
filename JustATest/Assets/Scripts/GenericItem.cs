using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericItem : MonoBehaviour, IItem
{
    public bool Grab(Transform origin)
    {
        transform.SetParent(origin);
        return true;
    }

    public bool Release()
    {
        transform.SetParent(null);
        return true;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
