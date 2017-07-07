using System.Collections;
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
    public GameObject GameQuitPanel, GameOverPanel;
    public GameObject DestroyParticles;

    private float _waitTimer;
    private float _waitTime;
    private bool _waiting = false;

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
            if (Input.GetButtonDown("Select") || Input.GetKeyDown(KeyCode.Return)) {
                SceneManager.LoadScene(0);
            }
            else if (Input.GetButtonDown("Back")) CancelQuit();
        }

        wall = GameObject.Find("Wall").GetComponent<WallController>();

        if(wall.LeftWon || wall.RightWon)
        {
            GameOver = true;
            GameOverPanel.SetActive(true);
            StartCoroutine(DelayedQuit());
        }
        else
        {
            GameOverPanel.SetActive(false);
        }

        if (_waiting)
        {
            if (Time.realtimeSinceStartup - _waitTimer > 0.1f)
            {
                _waitTimer = 0;
                _waiting = false;
                Time.timeScale = 1;
            }
        }
    }

    public void Wait(float ms)
    {
        _waitTimer = Time.realtimeSinceStartup;
        _waiting = true;
        Time.timeScale = 0;
        _waitTime = ms;
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

    private void QuitGame() {
        quittingGame = true;
        GameQuitPanel.SetActive(true);
    }

    private void CancelQuit() {
        quittingGame = false;
        GameQuitPanel.SetActive(false);
    }

    IEnumerator DelayedQuit() {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(0);
    }
}
