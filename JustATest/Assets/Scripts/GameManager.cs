using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    [HideInInspector]
    public static GameManager instance = null; // Static instance of GameManager which allows it to be accessed by any other script.
    public List<ObjectController> objects = new List<ObjectController>();
    public uint maxObjects = 10;
    [HideInInspector]
    public WallController wall;

    [SerializeField]
    Canvas GameOverUI;
    public bool GameOver = true;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        wall = GameObject.Find("Wall").GetComponent<WallController>();

        GameOverUI = GameObject.Find("EndGameUI").GetComponent<Canvas>();

        GameOverUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        wall = GameObject.Find("Wall").GetComponent<WallController>();
        if(!GameOverUI)
        {
            GameOverUI = GameObject.Find("EndGameUI").GetComponent<Canvas>();
        }
        if(wall.LeftWon || wall.RightWon)
        {
            //Open UI
            GameOver = true;
            GameOverUI.gameObject.SetActive(true);
        }
        else
        {
            GameOverUI.gameObject.SetActive(false);
        }
    }

    public void RestartGame()
    {
        wall.LeftWon = false;
        wall.RightWon = false;
        objects.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DestroyObject(ObjectController obj)
    {
        objects.Remove(obj);
        Destroy(obj.gameObject);
    }
}
