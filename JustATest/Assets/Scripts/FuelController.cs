using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelController : MonoBehaviour, IItem {

    private PlayerController owner = null;
    [SerializeField]
    float interactionDistance = 6.0f; // Careful, this should be squared!
    private List<Thruster> _thrusters;

    public bool Grab(Transform origin) {
        transform.SetParent(origin);
        transform.localPosition = Vector3.zero;
        owner = origin.root.GetComponent<PlayerController>();
        _thrusters = owner.playerTwo ? GameManager.instance.wall.RightThrusters : GameManager.instance.wall.LeftThrusters;

        foreach (Thruster t in _thrusters)
        {
            if (!t.IsLoaded)
            {
                t.FuelHighlightEnabled = true;
            }
        }

        GlobalSoundManager.instance.PlayClip(GlobalSounds.PickUpFuel, SourcePosition.Center, 1);
        return true;
    }

    public bool Release() {
        Thruster closestThruster = FindClosestThruster();
        Debug.Log("Trying to load " + closestThruster);
        if (closestThruster != null && closestThruster.FuelInRange && !closestThruster.IsLoaded) {
            Debug.Log("Loading " + closestThruster);
            closestThruster.Load();
            GameManager.instance.DestroyObject(this.GetComponent<ObjectController>());
            GlobalSoundManager.instance.PlayClip(GlobalSounds.PlaceFuelTank, SourcePosition.Center, 1);

            foreach (Thruster t in _thrusters)
            {
                t.FuelHighlightEnabled = false;
            }
            return true;
        }
        else {
            if (!closestThruster.IsLoaded ) Debug.Log("Thruster too far away!");
            else Debug.Log("Thruster already loaded!");
        }
        return false;
    }

    private Thruster FindClosestThruster() {
        if (GameManager.instance.wall.RightThrusters.Count <= 0 || GameManager.instance.wall.LeftThrusters.Count <= 0) {
            Debug.Log("One or more thruster lists in Wallcontroller are empty!");
            return null;
        }
      
        Thruster closestThruster = _thrusters[0];
        float distance = (closestThruster.transform.position - transform.position).sqrMagnitude;

        float newDistance = 0.0f;
        foreach (Thruster t in _thrusters) {
            newDistance = (t.transform.position - transform.position).sqrMagnitude;
            if (newDistance < distance) {
                distance = newDistance;
                closestThruster = t;
            }
        }
        return closestThruster;
    }
}
