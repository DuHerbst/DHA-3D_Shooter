using UnityEngine;

public class WinCondition : MonoBehaviour
{
    // Script will handle win conditions for the level
    // if the player has collected all chests in the level in an array
    // show win UI

    [SerializeField] private GameObject[] chests;
    private int _collectedChests;
    
    public void CollectiblesCollected(GameObject chest)
    {
        _collectedChests++;
        Debug.Log("Collected chest count: " + _collectedChests + " / " + chests.Length);

        if (_collectedChests >= chests.Length)
        {
            Debug.Log("All chests collected! Level Complete!");
            GameManager.instance.LevelComplete();
        }
        
    }

}
