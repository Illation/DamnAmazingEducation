using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelController : MonoBehaviour, IItem {

    private PlayerController owner = null;
    [SerializeField]
    float interactionDistance = 6.0f; // Careful, this should be squared!

    public bool Grab(Transform origin) {
        transform.SetParent(origin);
        transform.localPosition = Vector3.zero;
        owner = origin.root.GetComponent<PlayerController>();
        GlobalSoundManager.instance.PlayClip(GlobalSounds.PickUpFuel, SourcePosition.Center, 1);
        return true;
    }

    public bool Release() {
        Thruster closestThruster = FindClosestThruster();
        Debug.Log("Trying to load " + closestThruster);
        if (closestThruster != null && (closestThruster.transform.position - transform.position).sqrMagnitude <= interactionDistance && !closestThruster.IsLoaded) {
            Debug.Log("Loading " + closestThruster);
            closestThruster.Load();
            GameManager.instance.DestroyObject(this.GetComponent<ObjectController>());
            GlobalSoundManager.instance.PlayClip(GlobalSounds.PlaceFuelTank, SourcePosition.Center, 1);
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
        List<Thruster> thrusterArray = owner.playerTwo ? GameManager.instance.wall.RightThrusters : GameManager.instance.wall.LeftThrusters;
        Thruster closestThruster = thrusterArray[0];
        float distance = (closestThruster.transform.position - transform.position).sqrMagnitude;

        float newDistance = 0.0f;
        foreach (Thruster t in thrusterArray) {
            newDistance = (t.transform.position - transform.position).sqrMagnitude;
            if (newDistance < distance) {
                distance = newDistance;
                closestThruster = t;
            }
        }
        return closestThruster;
    }
}
