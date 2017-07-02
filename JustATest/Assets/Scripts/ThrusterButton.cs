using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterButton : MonoBehaviour
{
    [SerializeField]
    bool IsLeft = true;

    private WallController _wall;

    // Use this for initialization
    void Start()
    {
        _wall = (GameObject.Find("Wall")).GetComponent<WallController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalSoundManager.instance.PlayClip(GlobalSounds.ButtonPress, IsLeft ? SourcePosition.Left : SourcePosition.Right, 1);
            var thrusters = IsLeft ? _wall.LeftThrusters : _wall.RightThrusters;
            bool allLoaded = true;
            foreach (var thruster in thrusters)
            {
                if (!(thruster.IsLoaded)) allLoaded = false;
            }
            if (allLoaded)
            {
                foreach (var thruster in thrusters)
                {
                    thruster.Activate();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalSoundManager.instance.PlayClip(GlobalSounds.ButtonPress, IsLeft ? SourcePosition.Left : SourcePosition.Right, 1);
        }
    }
}
