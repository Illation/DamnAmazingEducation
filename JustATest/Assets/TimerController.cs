using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{

    [SerializeField]
    Text TimerText;
    WallController _wall;

	// Use this for initialization
	void Start ()
    {
        _wall = GameObject.Find("Wall").GetComponent<WallController>();		
	}
	
	// Update is called once per frame
	void Update ()
    {
        TimerText.text = ((int)_wall.UpgradeTimer).ToString();	
	}
}
