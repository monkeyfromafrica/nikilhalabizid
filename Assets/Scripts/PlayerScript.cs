#define USING_ACCELEROMETER

using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    public float accelerometerSensivity;
    private float lastMousePos = -1;
    private Text scoreText;
    private int score;
    private float initialY;

	void Start () {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        initialY = 0;
    }
	
	// Update is called once per frame
	void Update () {
        // Set initial position after game (re)start
        if (GameManager.instance.justStarted)
        {
            initialY = Input.acceleration.y;
            GameManager.instance.justStarted = false;
        }
#if UNITY_EDITOR || UNITY_STANDALONE
        if (GameManager.instance.playing)
        {
            GameManager.instance.score += (GameManager.instance.speed * (float) System.Math.Exp(transform.localScale.y)) / 20;
            if (Input.GetMouseButton(0))
            {
                if (lastMousePos != -1)
                {
                    float y = transform.localScale.y + (lastMousePos - Input.mousePosition.y) * Time.deltaTime;
                    if (y > 0.2f && y <= 3.2f)
                    {
                        transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
                    }
                }
                lastMousePos = Input.mousePosition.y;
            }
            if (Input.GetMouseButtonUp(0))
            {
                lastMousePos = -1;
            }
        }
#endif

#if UNITY_ANDROID
        if (GameManager.instance.playing)
        {
            GameManager.instance.score += (GameManager.instance.speed * (float)System.Math.Exp(transform.localScale.y)) / 20;
            // Use touch input
            /*if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Moved)
                {
                    float y = transform.localScale.y - t.deltaPosition.y * Time.deltaTime;
                    if (y > 0.2f && y <= 3.2f)
                    {
                        transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
                    }
                }
            }*/

            float y = transform.localScale.y - (Input.acceleration.y - initialY) * Time.deltaTime * accelerometerSensivity;
            if (y > 0.2f && y <= 3.2f)
            {
                transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
            }
        }
#endif
    }
}
