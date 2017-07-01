using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField]
    float Lifetime = 0.5f;
    float _timer = 0;

	// Use this for initialization
	void Start ()
    {
        _timer = Lifetime;		
	}
	
	// Update is called once per frame
	void Update ()
    {
        _timer -= Time.deltaTime;        		
        if(_timer < 0)
        {
            foreach(GameObject child in this.transform)
            {
                Destroy(child);
            }
            Destroy(this);
        }
	}
}
