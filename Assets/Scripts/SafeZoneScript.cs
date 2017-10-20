using UnityEngine;
using UnityEngine.UI;

public class SafeZoneScript : MonoBehaviour {
    private float timer;
    private bool inDangerZone;
    private Text countDown;

    private void Start()
    {
        timer = 3.0f;
        inDangerZone = true; // rly?
        countDown = GameObject.Find("Timer").GetComponent<Text>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            GameManager.instance.inScene.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        } else if (collision.CompareTag("Player")) // Danger Zone: Start timer!
        {
            inDangerZone = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            GameManager.instance.inScene.Add(collision.gameObject);
        } else if (collision.CompareTag("Player")) // Danger Zone left
        {
            inDangerZone = false;
            timer = 3;
            countDown.text = "";
        }
    }

    void Update()
    {
        if (GameManager.instance.playing)
        {
            if (inDangerZone)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    GameManager.instance.EndGame();
                } else
                {
                    countDown.text = System.Math.Round(timer, 1).ToString();
                }
            }
        } else timer = 3.0f;
    }
}
