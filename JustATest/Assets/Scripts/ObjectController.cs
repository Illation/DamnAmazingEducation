using UnityEngine;

public class ObjectController : MonoBehaviour {

    private GameManager GameManagerObj;
    public Waypoint nextWaypoint;
    public float speed = 1.0f;

    void Start()
    {
        GameManagerObj = GameObject.FindObjectOfType<GameManager>();
    }
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.gameObject.transform.position, 0.01f * speed);
        if (Vector3.SqrMagnitude(nextWaypoint.transform.position - transform.position) < Mathf.Epsilon)
        {
            if (nextWaypoint.nextWaypoint != null) nextWaypoint = nextWaypoint.nextWaypoint;
            else if (GameManagerObj != null) GameManagerObj.DestroyObject(this);
        }
	}

    public void Destroy()
    {
        if (GameManagerObj != null) GameManagerObj.DestroyObject(this);
    }
}
