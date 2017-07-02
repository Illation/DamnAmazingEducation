using UnityEngine;

public class SpawnerController : MonoBehaviour {

    public GameObject[] spawnableObjects;
    public float SpawnCooldown = 3.0f;
    [SerializeField]
    float SpawnRandom = 0.5f;
    public Waypoint nextWaypoint;
    [SerializeField]
    bool IsLeft = true;

    private float spawnTimer = 0.0f;
    private void Start()
    {
        spawnTimer = SpawnCooldown + Random.Range(-SpawnRandom, +SpawnRandom);
    }

    void Update () {
        if (spawnTimer <= 0.0f) {
            if (GameManager.instance.maxObjects > GameManager.instance.objects.Count) {
                GameObject obj = Instantiate(spawnableObjects[Random.Range(0, spawnableObjects.Length)], transform.position + Vector3.up, Quaternion.identity);
                GameManager.instance.objects.Add(obj.GetComponent<ObjectController>());
                obj.GetComponent<ObjectController>().nextWaypoint = nextWaypoint;
                spawnTimer = SpawnCooldown + Random.Range(-SpawnRandom, +SpawnRandom);
                GlobalSoundManager.instance.PlayClip(GlobalSounds.ItemSpawn, 
                    IsLeft ? SourcePosition.Left : SourcePosition.Right, 0.15f);
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
