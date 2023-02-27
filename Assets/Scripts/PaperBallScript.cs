using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PaperBallScript : MonoBehaviour
{

    public float maxThrust;
    public float maxSpin;
    public Rigidbody2D rb;

    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;

    public int points;
    public GameObject player;

    


    // Start is called before the first frame update
    void Start()
    {

        //random amount of thrust and spin
        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float spin = Random.Range(-maxSpin, maxSpin);

        rb.AddForce (thrust);
        rb.AddTorque (spin);

        //find player
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //wrap the paperball if it goes off screen
        Vector2 newPos = transform.position;
        if (transform.position.y > screenTop)
        {
            newPos.y = screenBottom;
        }
        if (transform.position.y < screenBottom)
        {
            newPos.y = screenTop;
        }
        if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }
        if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }
        transform.position = newPos;

        
    }

    // if bullet collide with paperball then destroy paperball
    void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<AudioManager>().Play("Explosion");
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }

        player.SendMessage("ScorePoints", points);

        // Destroy PaperBall
        Destroy(gameObject);
    }

    
    
}
