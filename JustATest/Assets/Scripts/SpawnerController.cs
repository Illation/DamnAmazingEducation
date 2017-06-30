using UnityEngine;

public class SpawnerController : MonoBehaviour {

    public GameObject[] spawnableObjects;
    public float spawnCooldown = 3.0f;
    public Waypoint nextWaypoint;

    private float spawnTimer = 0.0f;

	void Update () {
        if (spawnTimer <= 0.0f) {
            if (GameManager.instance.maxObjects <= GameManager.instance.totalObjects) GameManager.instance.totalObjects++;
            GameObject obj = Instantiate(spawnableObjects[0], transform.position + Vector3.up, Quaternion.identity);
            obj.GetComponent<ObjectController>().nextWaypoint = nextWaypoint;
            spawnTimer = spawnCooldown;
        }
        else {
            spawnTimer -= Time.deltaTime;
        }
	}
}
