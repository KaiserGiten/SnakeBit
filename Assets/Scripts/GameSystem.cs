using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSystem : MonoBehaviour {
    private int Score;
    public GUIText ScoreText;
    public GUIText GameOverText;
    bool restart = false;

	void Start ()
    {

        GameOverText.text = "";
        updatescore();
	}

	void Update ()
    {
        restartGame();
	}
    public void addscore(int val)
    {
        Assert.IsTrue(val >= 0);
        Assert.IsTrue(Score >= 0);
        Score += val;
        updatescore();
    }
    void updatescore()
    {
        ScoreText.text = "Score: " + Score; 
    }
    public void gameover()
    {
        GameOverText.text = "Game Over \nPress r \nto restart";
        restart = true;
    }
    private void restartGame()
    {
        if(restart == true)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
