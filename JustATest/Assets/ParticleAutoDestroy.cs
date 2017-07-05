using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour {

    // Use this for initialization
    private ParticleSystem _system;
	void Start () {
        _system = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!_system.IsAlive())
        {
            Destroy(this.gameObject);
        }
	}
}
