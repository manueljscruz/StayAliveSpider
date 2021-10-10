using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonLevelLoad : MonoBehaviour
{
    public string LevelToLoad;
	public void loadLevel()
	{
		//Load the level from LevelToLoad
		// GameManager.gm.gameState = GameManager.gameStates.Playing;
		// GameManager.gm.ResetGame();
		//GameObject.FindWithTag("Player").GetComponent<Health>().setHealth();
		//GameObject.FindWithTag("Player").GetComponent<Hunger>().RegeneratePlayer(10);
		SceneManager.LoadScene(LevelToLoad);
	}
}
