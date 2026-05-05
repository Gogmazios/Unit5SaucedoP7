using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    private int score;
    public bool isGameActive; 
    
    public TextMeshProUGUI ST;
    //scoreText = ST
    public TextMeshProUGUI GO;
    //GameOverText = GO
    public Button restartButton;
    public GameObject titleScreen;
    public TextMeshProUGUI LT;
    //livesText = LT
    private int lives;
    public GameObject pauseScreen;
    private bool paused; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void StartGame(int difficulty)
    {
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;
        UpdateLives(3); 
    }

    public void GameOver()
    {
        GO.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (score <= -1)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ST.text = "Score: " + score;
    }

    public void Restartgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateLives(int LivesToChange)
    {
        lives += LivesToChange;
        LT.text = "Lives: " + lives; 
        if (lives <= 0)
        {
            GameOver(); 
        }

    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true; 
            pauseScreen.SetActive(true);
            Time.timeScale = 0; 
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1; 
        }
    }

}
