using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Life
    public GameObject lifeOne;
    public GameObject lifeTwo;
    public GameObject lifeThree;
    public GameObject gameOver;
    public int life;
    //screen boarders
    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;

    public GameObject bullet;
    public float bulletForce;

    public Rigidbody2D rb;

    public int score;
    public int scoreTwo;
    public int highscore;

    public Text scoreText;
    public Text scoreTextTwo;
    public Text highscoreText;

    public bool freezCheck;
    
    public Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        lifeOne.SetActive(true);
        lifeTwo.SetActive(true);
        lifeThree.SetActive(true);

        score = 0;
        scoreTwo = 0;

        scoreText.text = "Score: " + scoreTwo;
        scoreText.text = "Score: " + score;
        
        anim = gameObject.GetComponent<Animation>();

    }

    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && freezCheck == false)
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
        }

        if (score > highscore)
        {
            highscore = score;

            PlayerPrefs.SetString("highscore ", highscoreText.text);
            Debug.Log("Your " + PlayerPrefs.GetString("highscore"));
            Debug.Log(highscore);
        }

        highscoreText.text = "Highscore: " + highscore;

        scoreTwo = score;

        //wrap the Player if it goes off screen
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


        // Shoots a bullet when click Fire1 (left click)
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject newBullet = Instantiate (bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletForce);
            Destroy(newBullet, 3.0f);
        }

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


        

       // Player faces mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }

    void Respawn()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Death");
        life = life - 1;
        
        

        if (life == 2)
        {
            lifeThree.SetActive(false);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Invoke("Respawn", 3f);
        }
        if (life == 1)
        {
            lifeTwo.SetActive(false);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Invoke("Respawn", 3f);
        }
        if (life == 0)
        {
            lifeOne.SetActive(false);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            gameOver.SetActive(true);
        }

    }

    void ScorePoints(int pointsAdd)
    {
        score += pointsAdd;
        scoreTwo += pointsAdd;
        scoreText.text = "Score: " + score;
        scoreTextTwo.text = "Score: " + score;
    }


}
