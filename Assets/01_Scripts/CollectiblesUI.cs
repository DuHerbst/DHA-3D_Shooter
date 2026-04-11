using UnityEngine;
using UnityEngine.UI;

public class CollectiblesUI : MonoBehaviour
{
    
    [SerializeField] private Image[] chestIcons;
    [SerializeField] private Sprite missingChest;
    [SerializeField] private Sprite collectedChest;

    private int _collectedChests;
    private void Start()
    {
        
        _collectedChests = 0;
        UpdateChestUI();
        
    }

    public void CollectedChest()
    {
        _collectedChests++;
        UpdateChestUI();
    }

    public void UpdateChestUI()
    {
        
        for (int i = 0; i < chestIcons.Length; i++)
        { 
            if (i < _collectedChests)
            {
                chestIcons[i].sprite = collectedChest;
            }
            else
            {
                chestIcons[i].sprite = missingChest;
            }
        }
    }

}
