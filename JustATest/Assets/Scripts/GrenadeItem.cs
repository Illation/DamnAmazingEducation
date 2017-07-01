using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeItem : MonoBehaviour, IItem  {
    private bool _thrown = false;
    public float ThrowingVelocity = 5.0f;
    private GameObject _throwingPlayer;
    private GameObject _enemyPlayer;

    public bool Grab(Transform origin)
    {
        _throwingPlayer = origin.root.gameObject;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players[0] == _throwingPlayer) _enemyPlayer = players[1];
        else _enemyPlayer = players[0];

        if (_thrown)
        {
            ThrowingVelocity *= 1.2f;
            return true;
        }
        else
        {
            transform.SetParent(origin);
            transform.localPosition = Vector3.zero;
            return true;
        }
    }

    public bool Release()
    {
        _thrown = true;
        transform.SetParent(null);
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_thrown)
        {
            Vector3 dir = (_enemyPlayer.transform.position - transform.position).normalized;
        }   
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject rootObj = other.transform.root.gameObject;
        if (rootObj.tag == "Player" && _thrown)
        {
            if (rootObj == _throwingPlayer) return; 
            // explode                
            ObjectController objCont = this.GetComponent<ObjectController>();
            if (objCont != null) objCont.Destroy();
        }
    }
}
