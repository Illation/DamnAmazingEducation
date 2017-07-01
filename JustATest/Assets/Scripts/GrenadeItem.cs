using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeItem : MonoBehaviour, IItem  {
    private bool _thrown = false;
    public float ThrowingVelocity = 5.0f;
    private float _pauseTime;
    private bool _paused = false;
    private GameObject _throwingPlayer;
    private GameObject _enemyPlayer;
    private float _playerGrabRange = 0;
    private Vector3 _startingPoint;

    public bool Grab(Transform origin)
    {
        _throwingPlayer = origin.root.gameObject;
        _playerGrabRange = _throwingPlayer.GetComponent<ItemHandler>().GrabDistance;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players[0] == _throwingPlayer) _enemyPlayer = players[1];
        else _enemyPlayer = players[0];

        if (_thrown)
        {
            _pauseTime = Time.realtimeSinceStartup;
            Time.timeScale = 0;
            _paused = true;
            ThrowingVelocity += 1.5f;
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
        _startingPoint = transform.position;
        _thrown = true;
        transform.SetParent(null);
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_paused)
        {
            if (Time.realtimeSinceStartup - _pauseTime > 0.1f)
            {
                _paused = false;
                Time.timeScale = 1;
            }
        }

        if (_thrown)
        {
            Vector3 posSelf = transform.position;
            posSelf.y = 0;
            Vector3 posEnemy = _enemyPlayer.transform.position;
            posEnemy.y = 0;
            Vector3 posStart = _startingPoint;
            posStart.y = 0;

            float distFromEnemy = (posEnemy - posSelf).magnitude;
            float maxDist = (posEnemy - posStart).magnitude;
            float val = 1.0f - Mathf.Abs((maxDist - distFromEnemy) / maxDist * 2.0f - 1.0f);
            Vector3 dir = (posEnemy - posSelf).normalized;
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
