﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour {

    public Transform GrabOrigin;
    public int JoyStickNum = 1;
    public float GrabDistance = 2.0f;
    private Transform _transSelf;
    private GameObject _activeItem;
    private bool _pickupKeyDown = false;
    private string _pickupAxis = "";
	// Use this for initialization
	void Start () {
        _transSelf = transform;
        _pickupAxis = "Pickup" + JoyStickNum;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis(_pickupAxis) > 0 && !_pickupKeyDown)
        {
            _pickupKeyDown = true;
            if (_activeItem != null)
            {
                _activeItem.GetComponent<IItem>().Release();
                _activeItem = null;
            }
            else
            {
                GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
                if (items.Length > 0)
                {
                    int closestIndex = 0;
                    Vector3 originPos = GrabOrigin.position;
                    originPos.y = 0;
                    Vector3 itemPos = items[0].transform.position;
                    itemPos.y = 0;
                    float minLen = (originPos - itemPos).sqrMagnitude;
                    for (int i = 1; i < items.Length; i++)
                    {
                        itemPos = items[i].transform.position;
                        itemPos.y = 0;
                        float currLen = (originPos - itemPos).sqrMagnitude;
                        if (currLen < minLen)
                        {
                            minLen = currLen;
                            closestIndex = i;
                        }
                    }

                    if (!(minLen <= (GrabDistance * GrabDistance))) return;

                    IItem itemComponent = items[closestIndex].GetComponent<IItem>();
                    if (itemComponent != null)
                    {
                        if (itemComponent.Grab(GrabOrigin))
                        {
                            _activeItem = items[closestIndex];
                            ObjectController objCont = items[closestIndex].GetComponent<ObjectController>();
                            if (objCont != null) objCont.enabled = false;
                        }
                        else
                        {
                            _activeItem = null;
                        }
                    }
                }
            }

        }
        else if (Input.GetAxis(_pickupAxis) == 0)
        {
            _pickupKeyDown = false;
        }
	}
}
