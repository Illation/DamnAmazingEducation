using UnityEngine;

public class ObjectController : MonoBehaviour {

    public Waypoint nextWaypoint;
    public float speed = 1.0f;
	
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.gameObject.transform.position, 0.01f * speed);
        if (Vector3.SqrMagnitude(nextWaypoint.transform.position - transform.position) < Mathf.Epsilon) {
            if (nextWaypoint.nextWaypoint != null) nextWaypoint = nextWaypoint.nextWaypoint;
            else Destroy(gameObject);
        }
	}
}
