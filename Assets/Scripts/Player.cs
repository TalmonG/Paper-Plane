using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public FixedJoint2D Joystick;
    public Rigidbody2D rb;
    Vector2 move;
    public float moveSpeed;

    public GameObject gameOver;

    //public GameObject bullet;
    //public float bulletForce;


    public int score;
    public int scoreTwo;
    public int highscore;

    public Text scoreText;
    public Text scoreTextTwo;
    public Text highscoreText;

    //public bool freezCheck;
    
    //public Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //lifeOne.SetActive(true);
        //lifeTwo.SetActive(true);
        //lifeThree.SetActive(true);

        score = 0;
        scoreTwo = 0;

        scoreText.text = "Score: " + scoreTwo;
        scoreText.text = "Score: " + score;
        
        //anim = gameObject.GetComponent<Animation>();

    }

    

    // Update is called once per frame
    void Update()
    {

        float hAxis = move.x;
        float vAxis = move.x;
        float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;

        /*if (Input.GetKeyDown(KeyCode.LeftShift) && freezCheck == false)
        {
            Time.timeScale = 0.5f;
            anim.Play("FreezeStart");
            freezCheck = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && freezCheck == true)
        {
            Time.timeScale = 1f;
            anim.Play("FreezeStop");
            freezCheck = false;
        }*/

        if (score > highscore)
        {
            highscore = score;

            PlayerPrefs.SetString("highscore ", highscoreText.text);
            Debug.Log("Your " + PlayerPrefs.GetString("highscore"));
            Debug.Log(highscore);
        }

        highscoreText.text = "Highscore: " + highscore;

        scoreTwo = score;

        


        // Shoots a bullet when click Fire1 (left click)
        /*if (Input.GetButtonDown("Fire1"))
        {
            GameObject newBullet = Instantiate (bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletForce);
            Destroy(newBullet, 3.0f);
        }*/

        // Player Movements
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 0.3f));
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.3f, 0f));
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -0.3f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0.3f, 0f));
        }


        

    }

    void Respawn()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    

    void ScorePoints(int pointsAdd)
    {
        score += pointsAdd;
        scoreTwo += pointsAdd;
        scoreText.text = "Score: " + score;
        scoreTextTwo.text = "Score: " + score;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }


}
