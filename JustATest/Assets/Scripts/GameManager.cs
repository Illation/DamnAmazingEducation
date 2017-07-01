using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public static GameManager instance = null; // Static instance of GameManager which allows it to be accessed by any other script.
    public List<ObjectController> objects = new List<ObjectController>();
    public uint maxObjects = 10;
    [HideInInspector]
    public WallController wall;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        wall = GameObject.Find("Wall").GetComponent<WallController>();
    }

    public void DestroyObject(ObjectController obj)
    {
        objects.Remove(obj);
        Destroy(obj.gameObject);
    }
}
