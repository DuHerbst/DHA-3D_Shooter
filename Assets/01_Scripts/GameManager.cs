using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // REFERENCES
    
    [SerializeField] private MouseBehaviour mouseBehaviour;
    
    // GAME STATES
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject levelCompleteMenu;
    public bool isPaused = false;
    public bool isGameOver = false;
    public bool isLevelComplete = false;
    
    public static GameManager instance {get; private set;} // singleton: only one of this class that can exist

    private void Awake()
    {
        isPaused = false;
        Time.timeScale = 1f;
        
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        
        //INVALIDATE MENUS ON START
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        levelCompleteMenu.SetActive(false);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver && !isLevelComplete)
        {
            TogglePauseMenu();
        }
        
        if (isPaused || isGameOver || isLevelComplete)
        {
            return;
        }
        
    }

    private void PauseGame() // Pauses the game by setting timescale to 0
    {
        if (isGameOver || isLevelComplete)
        {
            return;
        }
        
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        mouseBehaviour.ShowCursor(true);
        
    }

    private void TogglePauseMenu()

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
        
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        mouseBehaviour.ShowCursor(false);
        
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reloads the current scene
        mouseBehaviour.ShowCursor(false);
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
        mouseBehaviour.ShowCursor(true);
        
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
        mouseBehaviour.ShowCursor(true);
        
    }
    
    
    
}
