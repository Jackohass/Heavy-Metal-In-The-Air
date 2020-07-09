using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text scoreText;
    public int score;

    public GameObject enemy;
    public Vector3 spawnValues;

    public Text endText;
    private bool gameEnded;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";
        score = 0;

        gameEnded = false;
        endText.gameObject.SetActive(false);
        //SpawnEnemy();
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameEnded)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                Scene level = SceneManager.GetActiveScene();
                SceneManager.LoadScene(level.name);
            }
        }
    }

    public void EndGame()
    {
        gameEnded = true;
        endText.gameObject.SetActive(true);
    }

    void SpawnEnemy()
    {
        Vector3 spawnAt = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Instantiate(enemy, spawnAt, Quaternion.identity);
    }
}
