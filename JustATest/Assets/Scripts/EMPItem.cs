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
    private bool _attached;
    private Thruster _targetThruster;
    private int _ownerNum;
    private float _timer;
    public float ExplosionTime = 2.0f;
    public bool Grab(Transform origin)
    {
        if (!_thrown)
        {
            _wall = GameObject.Find("Wall").GetComponent<WallController>();
            _throwingPlayer = origin.root.gameObject;

            transform.SetParent(origin);
            transform.localPosition = Vector3.zero;
            _ownerNum = (origin.gameObject.name == "Player 1" ? 1 : 2); //hacks
            return true;
        }
        else if (_attached)
        {
            // do stuff
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Release()
    {
        List<Thruster> thrusters;
        if (_ownerNum == 1)
        {
            thrusters = _wall.LeftThrusters;
        }
        else
        {
            thrusters = _wall.RightThrusters;
        }

        foreach (Thruster t in thrusters)
        {
            if (t.IsLoaded)
            {
                _targetThruster = t;
                continue;
            }
        }

        if (_targetThruster == null)
        {
            return false;
        }


        _startingPoint = transform.position;
        transform.SetParent(null);


        _thrown = true;
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_attached)
        {
            _timer += Time.deltaTime;

            if (_timer > ExplosionTime)
            {
                Explode();
            }
        }
        if (_thrown)
        {
            Vector3 posSelf = transform.position;
            posSelf.y = 0;
            Vector3 posThruster = _targetThruster.transform.position;
            posThruster.y = 0;
            Vector3 posStart = _startingPoint;
            posStart.y = 0;

            float distFromEnemy = (posThruster - posSelf).magnitude;

            if (distFromEnemy < 0.5f)
            {
                AttachToThruster();
                return;
            }

            float maxDist = (posThruster - posStart).magnitude;
            float val = 1.0f - Mathf.Abs((maxDist - distFromEnemy) / maxDist * 2.0f - 1.0f);
            Vector3 dir = (posThruster - posSelf).normalized;
            transform.position += dir * ThrowingVelocity * Time.deltaTime;
            Vector3 pos = transform.position;
            pos.y = _startingPoint.y + val * 2.0f;
            transform.position = pos;
        }
    }

    void AttachToThruster()
    {
        _attached = true;
    }

    void Explode()
    {
        
        ObjectController objCont = this.GetComponent<ObjectController>();
        if (objCont != null) objCont.Destroy();
    }
}