using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public static GameManager instance = null; // Static instance of GameManager which allows it to be accessed by any other script.
    public uint totalObjects = 0;
    public uint maxObjects = 10;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
