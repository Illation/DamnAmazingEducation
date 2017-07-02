using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public struct ScreenShakeData
    {
        public float Strength;
        public float Speed;
        public float Duration;
        public float GlobalTimer;
        public float ShakeTimer;
        public Vector2 Offset;
        public Vector2 SpringForce;
        public bool DecayOverTime;
    }

    private Vector3 _basePos;
    private Vector3 _targetOffset;
    private Vector3 _currentOffset;
    private List<ScreenShakeData> _shakeQueue;
    // Update is called once per frame
    void Start()
    {
        _shakeQueue = new List<ScreenShakeData>();
        _basePos = transform.position;
    }

	void Update () {
        transform.position = _basePos;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Vector2 avg = Vector3.zero;
        for (int i = 0; i < players.Length; i++)
        {
            avg += new Vector2(players[i].transform.position.z, players[i].transform.position.x);
        }
        avg /= players.Length;
        avg /= 2.0f;

        float zVal = (players[0].transform.position - players[1].transform.position).magnitude;
        zVal /= 5.0f;
        _targetOffset = new Vector3(avg.x, -avg.y, -zVal + 5.0f);
        _currentOffset = (_targetOffset - _currentOffset) / 2.0f;
        transform.Translate(_currentOffset);

        for (int i = 0; i < _shakeQueue.Count; ++i)
        {
            ScreenShakeData data = _shakeQueue[i];
            data.GlobalTimer += Time.deltaTime;
            data.ShakeTimer += Time.deltaTime;

            if (data.ShakeTimer >= 0.05f / data.Speed)
            {
                data.ShakeTimer = 0;
                float strength;

                if (data.DecayOverTime)
                {
                    strength = data.Strength * (1.0f - (data.GlobalTimer / data.Duration));
                }
                else
                {
                    strength = data.Strength;
                }

                data.SpringForce = new Vector2(Random.Range(-strength, strength), Random.Range(-strength, strength));
            }

            data.Offset += data.SpringForce * Time.deltaTime;
            data.Offset -= (data.Offset / 2.0f) * Time.deltaTime * data.Speed * 2.0f;
            data.SpringForce -= (data.SpringForce / 2.0f) * Time.deltaTime * data.Speed * 2.0f;

            _shakeQueue[i] = data;

            Vector3 t = new Vector3(data.Offset.x, data.Offset.y, 0);
            transform.Translate(t);
        }

        for (int i = 0; i < _shakeQueue.Count; ++i)
        {
            ScreenShakeData data = _shakeQueue[i];

            if (data.GlobalTimer > data.Duration)
            {
                _shakeQueue.Remove(_shakeQueue[i]);
            }
        }


        if (_shakeQueue.Count == 0)
        {
            if ((transform.position - _basePos).magnitude > 0.05f)
            {
                transform.Translate(((_basePos - transform.position) / 2.0f) * Time.deltaTime * 5.0f, Space.World);
            }
        }
    }

    public void AddScreenShake(float strength, float speed, float duration, bool decay = false)
    {
        ScreenShakeData data;
        data.Strength = strength;
        data.Speed = speed;
        data.Duration = duration;
        data.GlobalTimer = 0;
        data.ShakeTimer = 0;
        data.DecayOverTime = decay;
        data.Offset = Vector2.zero;
        data.SpringForce = new Vector2(Random.Range(-strength, strength), Random.Range(-strength, strength));
        _shakeQueue.Add(data);
    }
}
