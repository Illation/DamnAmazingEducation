using UnityEngine;

public class SpawnerController : MonoBehaviour {

    public GameObject[] spawnableObjects;
    public float spawnCooldown = 3.0f;
    public Waypoint nextWaypoint;

    private float spawnTimer = 0.0f;

	void Update () {
        if (spawnTimer <= 0.0f) {
            if (GameManager.instance.maxObjects > GameManager.instance.objects.Count) {
                GameObject obj = Instantiate(spawnableObjects[0], transform.position + Vector3.up, Quaternion.identity);
                GameManager.instance.objects.Add(obj.GetComponent<ObjectController>());
                obj.GetComponent<ObjectController>().nextWaypoint = nextWaypoint;
                spawnTimer = spawnCooldown;
            }
            else {
                // Something?
            }
        }
        else {
            spawnTimer -= Time.deltaTime;
        }
	}
}
