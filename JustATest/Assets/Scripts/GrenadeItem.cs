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
    private Vector3 _startingPoint;
    private float _startY;
    private Vector3 _spinDir;

    [SerializeField]
    Explosion ExplosionPrefab;

    public bool Grab(Transform origin)
    {
        _throwingPlayer = origin.root.gameObject;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players[0] == _throwingPlayer) _enemyPlayer = players[1];
        else _enemyPlayer = players[0];

        if (_thrown)
        {
            GlobalSoundManager.instance.PlayClip(GlobalSounds.DeflectGrenade, SourcePosition.Center, 1);
            _startingPoint = transform.position;
            _spinDir = -_spinDir;
            _pauseTime = Time.realtimeSinceStartup;
            Time.timeScale = 0;
            _paused = true;
            ThrowingVelocity += 1.5f;
            return true;
        }
        else
        {
            GlobalSoundManager.instance.PlayClip(GlobalSounds.PickUpEmpGrenade, SourcePosition.Center, 1);
            _spinDir = new Vector3(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
            _startY = transform.position.y;
            transform.SetParent(origin);
            transform.localPosition = Vector3.zero;
            return true;
        }
    }
    public bool Release()
    {
        GlobalSoundManager.instance.PlayClip(GlobalSounds.GrenadeThrow, SourcePosition.Center, 1);
        if (!_enemyPlayer.GetComponent<PlayerController>().alive) return false;
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
            transform.Rotate(_spinDir * 10.0f);
            Vector3 posSelf = transform.position;
            posSelf.y = 0;
            Vector3 posEnemy = _enemyPlayer.transform.position;
            posEnemy.y = 0;
            Vector3 posStart = _startingPoint;
            posStart.y = 0;

            float distFromEnemy = (posEnemy - posSelf).magnitude;

            if (distFromEnemy < 6.0f && !_enemyPlayer.GetComponent<ItemHandler>().HoldingItem())
            {
                _enemyPlayer.GetComponent<ItemHandler>().EnableButtonPrompt();
            }


            if (distFromEnemy < 0.5f)
            {
                Explode();
                _enemyPlayer.GetComponent<PlayerController>().Damage();
            }

            float maxDist = (posEnemy - posStart).magnitude;
            float val = 1.0f - Mathf.Abs((maxDist - distFromEnemy) / maxDist * 2.0f - 1.0f);
            Vector3 dir = (posEnemy - posSelf).normalized;
            transform.position += dir * ThrowingVelocity * Time.deltaTime;
            Vector3 pos = transform.position;
            pos.y =  _startY + val * 2.0f;
            transform.position = pos;
        }   
    }

    void Explode()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController camC = null;
        if (cam) camC = cam.GetComponent<CameraController>();
        if (camC) camC.AddScreenShake(10.0f, 2.0f, 0.5f, true);
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        ObjectController objCont = this.GetComponent<ObjectController>();
        if (objCont != null) objCont.Destroy();
        GlobalSoundManager.instance.PlayClip(GlobalSounds.GrenadeExplode, SourcePosition.Center, 1);
    }
}
