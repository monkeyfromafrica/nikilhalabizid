using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public List<GameObject> spawnList, inScene;
    public float speed;
    public bool playing, justStarted;
    public float score;
    private float spawnInterval;
    private float spawnCountDown;
    private Text countDown, scoreText;
    private GameObject replayButton;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(instance);
    }

	void Start () {
        countDown = GameObject.Find("Timer").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        replayButton = GameObject.Find("ReplayButton");
        replayButton.GetComponent<Button>().onClick.AddListener(StartGame);
        inScene = new List<GameObject>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        StartGame();
	}
	
	void Update () {
        if (playing)
        {
		    if (spawnCountDown <= 0)
            {
                Instantiate(spawnList[Random.Range(0, spawnList.Count)]);
                spawnCountDown = spawnInterval;
            } else
            {
                spawnCountDown -= Time.deltaTime;
            }
            float diff = Time.deltaTime / 10;
            if (speed < 20) {
                speed += diff;
            }
            if (spawnInterval > 0.5)
            {
                spawnInterval -= diff / 5;
            }
            scoreText.text = "Score: " + Mathf.Round(score).ToString();
        }
    }

    public void EndGame()
    {
        countDown.text = "GAME OVER";
        replayButton.SetActive(true);
        playing = false;
    }

    public void StartGame()
    {
        countDown.text = "";
        replayButton.SetActive(false);
        playing = true;
        justStarted = true;
        score = 0;
        speed = 3;
        spawnInterval = spawnCountDown = 1.5f;
        while (inScene.Count > 0)
        {
            GameObject toRemove = inScene[0];
            Destroy(toRemove);
            inScene.Remove(toRemove);
        }
    }
}
