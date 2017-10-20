using UnityEngine;

public class Mover : MonoBehaviour {
    private Rigidbody2D rb2d;
    private bool moving;

	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.right * GameManager.instance.speed;
        moving = true;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.EndGame();
        }
    }

    void Update()
    {
        if (moving && !GameManager.instance.playing)
        {
            moving = false;
            rb2d.velocity = Vector2.zero;
        }
    }
}
