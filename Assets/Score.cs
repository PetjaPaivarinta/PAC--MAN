using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour
{
    public static Score instance;

    public Text scoreText;

    int score = 0;
    int highscore = 0;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = score.ToString() + " Points";
    }


    public void AddPoint()
    {
        score += 10;
        scoreText.text = score.ToString() + " Points";
        if (highscore < score)
            PlayerPrefs.SetInt("highscore", score);

        if (score >= 200)
            EndGame();
    }

    void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Game Over");
    }
}

