using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    
    // GAME STATES
    
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject levelCompleteMenu;
    public bool isPaused = false;
    public bool isGameOver = false;
    public bool isLevelComplete = false;
    
    public static GameManager Instance {get; private set;} // singleton: only one of this class that can exist

    private void Awake()
    {
        isPaused = false;
        Time.timeScale = 1f;
        
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        //INVALIDATE MENUS ON START
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        levelCompleteMenu.SetActive(false);
        
    }

    private void Update()
    {
        if (isPaused || isGameOver || isLevelComplete)
        {
            return;
        }
        
        
        //DEBUG DELETE LATER - TESTING GAME OVER AND LEVEL COMPLETE
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelComplete();
        }
        
    }
    
    public void PauseGame() // Pauses the game by setting timescale to 0
    {
        if (isGameOver || isLevelComplete)
        {
            return;
        }
        
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Debug.Log("Game Paused");
        
    }

    public void TogglePauseMenu()

    {
        if (isPaused)
        {
            ResumeGame();
        }
        
        else 
        {
            PauseGame();
        }
        
    }

    public void ResumeGame()
    {
        
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
        Debug.Log("Game Resumed");
        
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reloads the current scene
    }
    
    public void QuitGame()
    { 
        Application.Quit(); 
        // add what scene to load here later
    }

    public void GameOver()
    {
        isPaused = false;
        
        if (isGameOver || isLevelComplete)
        {
            return;
        }
        
        isGameOver = true;
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        
        Debug.Log("Game Over");
        
    }
    
    public void LevelComplete()
    {
        isPaused = false;
        
        if (isGameOver || isLevelComplete)
        {
            return;
        }
        
        isLevelComplete = true;
        Time.timeScale = 0f;
        levelCompleteMenu.SetActive(true);
        
        Debug.Log("You Win!");
    }
    
    
    
}
