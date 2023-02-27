using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject gameOverScreen;

    public GameObject paperBall;

    public int count;

    public bool gameCheck;

    public float spawnSpeed;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        // Pause screen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameOverScreen.gameObject.SetActive(!gameOverScreen.gameObject.activeSelf);
        }

        
        

        if (gameCheck = true && count == 1)
        {
            count = count - 1;
            StartCoroutine(ExampleCoroutine());
            
        }

        

    }

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Manager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Scene Loaders
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
        gameCheck = false;
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        gameCheck = false;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
        gameCheck = true;
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(spawnSpeed);
        GameObject newpaperBall = Instantiate (paperBall, transform.position, transform.rotation);
        count = count + 1;
        spawnSpeed = spawnSpeed - 0.01f;
    }



}
