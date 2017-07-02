using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    [HideInInspector]
    public static GameManager instance = null; // Static instance of GameManager which allows it to be accessed by any other script.
    public List<ObjectController> objects = new List<ObjectController>();
    public uint maxObjects = 10;
    [HideInInspector]
    public WallController wall;
    public PostProcessingProfile profile;
    public GameObject GameQuitPanel, GameOverPanel;
    
    [SerializeField]
    public bool GameOver = true;

    private bool quittingGame = false;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        wall = GameObject.Find("Wall").GetComponent<WallController>();
        GameQuitPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start")) {
            if (quittingGame) CancelQuit();
            else QuitGame();
        }
        else if (quittingGame) {
            if (Input.GetButtonDown("Select")) {
                profile.colorGrading.enabled = false;
                SceneManager.LoadScene(0);
            }
            else if (Input.GetButtonDown("Back")) CancelQuit();
        }

        wall = GameObject.Find("Wall").GetComponent<WallController>();

        if(wall.LeftWon || wall.RightWon)
        {
            GameOver = true;
            GameOverPanel.SetActive(true);
        }
        else
        {
            GameOverPanel.SetActive(false);
        }
    }

    public void RestartGame()
    {
        wall.LeftWon = false;
        wall.RightWon = false;
        objects.Clear();
        profile.colorGrading.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DestroyObject(ObjectController obj)
    {
        objects.Remove(obj);
        Destroy(obj.gameObject);
    }

    private void QuitGame() {
        quittingGame = true;
        profile.colorGrading.enabled = true;
        GameQuitPanel.SetActive(true);
    }

    private void CancelQuit() {
        quittingGame = false;
        profile.colorGrading.enabled = false;
        GameQuitPanel.SetActive(false);
    }
}
