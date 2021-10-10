using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public enum gameStates { Playing, GameOver };
    public gameStates gameState = gameStates.Playing;

    // make game manager public static so can access this from other scripts
    public static GameManager gm;

    // public variables
    public int score = 0;
    public Text mainScoreDisplay;

    public GameObject mainCanvas;
    public GameObject gameOverCanvas;

    private Health playerHealth;
    private Hunger playerHunger;
    private Silk playerSilk;

    // Start is called before the first frame update
    void Start()
    {
        // get a reference to the GameManager component for use by other scripts
        if (gm == null)
            gm = this.gameObject.GetComponent<GameManager>();

        // init scoreboard to 0
        mainScoreDisplay.text = "0";

        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        playerHunger = GameObject.FindWithTag("Player").GetComponent<Hunger>();
        playerSilk = GameObject.FindWithTag("Player").GetComponent<Silk>();

        gameOverCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case gameStates.Playing:
                if (!playerHealth.isAlive())
                {
                    // Update game state
                    gameState = gameStates.GameOver;

                    mainCanvas.SetActive(false);
                    gameOverCanvas.SetActive(true);
                }
                break;
            case gameStates.GameOver:
                gameState = gameStates.Playing;
                break;
        }
    }

    public void EnemyEaten()
    {
        score++;
        mainScoreDisplay.text = score.ToString();
    }

    public void ResetGame()
    {
        gameOverCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        playerHealth.ResetHealth();
        playerHunger.ResetHunger();
        playerSilk.ResetSilk();
    }
    
}
