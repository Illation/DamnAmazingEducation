using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPItem : MonoBehaviour, IItem
{
    public float ThrowingVelocity = 5.0f;
    private GameObject _throwingPlayer;
    private Vector3 _startingPoint;
    private WallController _wall;
    private bool _thrown;
    private Thruster _targetThruster;
    private int _ownerNum;
    public bool Grab(Transform origin)
    {
        if (!_thrown){
            _wall = GameObject.Find("Wall").GetComponent<WallController>();
            _throwingPlayer = origin.root.gameObject;

            transform.SetParent(origin);
            transform.localPosition = Vector3.zero;
            _ownerNum = (origin.gameObject.name == "Player 1" ? 1 : 2); //hacks
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Release()
    {
        _startingPoint = transform.position;
        transform.SetParent(null);
        List<Thruster> thrusters;
        if (_ownerNum == 1)
        {
            thrusters = _wall.RightThrusters;
        }
        else
        {
            thrusters = _wall.LeftThrusters;
        }

        foreach (Thruster t in thrusters)
        {
            if (t.IsActivated && !t.isActiveAndEnabled)
            {

            }
        }

        _thrown = true;
        return true;
    }

    // Update is called once per frame
    void Update()
    {

        if (_thrown)
        {
            Vector3 posSelf = transform.position;
            posSelf.y = 0;
            Vector3 posThruster = _targetThruster.transform.position;
            posThruster.y = 0;
            Vector3 posStart = _startingPoint;
            posStart.y = 0;

            float distFromEnemy = (posThruster - posSelf).magnitude;
            float maxDist = (posThruster - posStart).magnitude;
            float val = 1.0f - Mathf.Abs((maxDist - distFromEnemy) / maxDist * 2.0f - 1.0f);
            Vector3 dir = (posThruster - posSelf).normalized;
            transform.position += dir * ThrowingVelocity * Time.deltaTime;
            Vector3 pos = transform.position;
            pos.y = _startingPoint.y + val * 2.0f;
            transform.position = pos;
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