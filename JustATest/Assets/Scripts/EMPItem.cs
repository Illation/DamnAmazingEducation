using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPItem : MonoBehaviour, IItem
{
    public float PickupRadius = 3.0f;
    public float ThrowingVelocity = 5.0f;
    private GameObject _throwingPlayer;
    private GameObject _enemyPlayer;
    private Vector3 _targetPos;
    private WallController _wall;
    private List<Thruster> _thrusters;
    private bool _thrown;
    private bool _attached;
    private Thruster _targetThruster;
    private int _ownerNum;
    private float _explosionTimer = 0;
    private float _selectionTimer = 0;
    public float SelectionSwitchDelay = 0.3f;
    public float ExplosionTime = 2.0f;
    private bool _selectingTarget = false;
    private int _thrusterID = 0;
    private int _maxThrusterID = 0;

    public bool Grab(Transform origin)
    {
        if (!_thrown)
        {
            _selectingTarget = true;
            _wall = GameObject.Find("Wall").GetComponent<WallController>();
            _maxThrusterID = _wall.LeftThrusters.Count - 1;
            _thrusterID = (int)Random.Range(0, _maxThrusterID + 0.1f);

            _throwingPlayer = origin.root.gameObject;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            if (players[0] == _throwingPlayer) _enemyPlayer = players[1];
            else _enemyPlayer = players[0];

            transform.SetParent(origin);
            transform.localPosition = Vector3.zero;
            int ownerNum = (origin.transform.root.gameObject.name == "Player 1" ? 1 : 2); //hacks

            if (ownerNum == 1)
            {
                _thrusters = _wall.RightThrusters;
            }
            else
            {
                _thrusters = _wall.LeftThrusters;
            }

            _thrusters[_thrusterID].IsHighlighted = true;

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
        if (!_thrown)
        {
            _targetThruster = _thrusters[_thrusterID];
            _targetPos = _targetThruster.transform.Find("EMPanchor").position;
            transform.SetParent(null);
            _thrown = true;
            _selectingTarget = false;
        }


        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_selectingTarget)
        {
            _selectionTimer += Time.deltaTime;
            if (_selectionTimer >= SelectionSwitchDelay)
            {
                _thrusters[_thrusterID].IsHighlighted = false;

                _selectionTimer = 0;
                ++_thrusterID;
                if (_thrusterID > _maxThrusterID)
                {
                    _thrusterID = 0;
                }

                _thrusters[_thrusterID].IsHighlighted = true;
            }
        }

        if (_attached)
        {
            Vector3 selfPos = transform.position;
            selfPos.y = 0;
            Vector3 playerPos = _enemyPlayer.transform.position;
            playerPos.y = 0;
            float distToPlayer = (_enemyPlayer.transform.position - transform.position).magnitude;

            if (distToPlayer < PickupRadius)
            {
                // eat
                _enemyPlayer.GetComponent<PlayerController>().SpeedBoost();
                ObjectController objCont = this.GetComponent<ObjectController>();
                if (objCont != null) objCont.Destroy();
                return;
            }

            _explosionTimer += Time.deltaTime;

            if (_explosionTimer > ExplosionTime)
            {
                Explode();
            }
        }
        else if (_thrown)
        {
            Vector3 posSelf = transform.position;
            posSelf.y = 0;
            Vector3 posThruster = _targetPos;
            posThruster.y = 0;

            float distFromThruster = (posThruster - posSelf).magnitude;

            if (distFromThruster < 0.1f)
            {
                AttachToThruster();
                return;
            }

            Vector3 dir = (_targetPos - transform.position).normalized;
            transform.position += dir * ThrowingVelocity * Time.deltaTime;
        }

        transform.Rotate(new Vector3(0, _explosionTimer * 20.0f, 0));
    }

    void AttachToThruster()
    {
        _attached = true;
        _targetThruster.IsHighlighted = false;
    }

    void Explode()
    {
        _targetThruster.Discharge();
        ObjectController objCont = this.GetComponent<ObjectController>();
        if (objCont != null) objCont.Destroy();
    }
}