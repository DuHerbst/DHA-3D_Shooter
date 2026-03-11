using System;
using UnityEngine;

public class QuestObjects : MonoBehaviour
{
    //collect 3 objects and open a door to win the game
    // need an array of 3 objects to go through and collect
    // when the item is collected, it will update a 1 out of 3 counter
    // when the counter reaches 3, it will open the door and win the game
    // the player doesn't hold the object as item, instead it just counts as collected and disappears from the world
    
    [SerializeField] private GameObject[] questItems; // array of quest items to collect
    [SerializeField] private GameObject door; // door to open when all items are collected
    
    private int _collectedItems = 0;
    private int _currentItem = 0;
    private int _totalItems;
    private string _itemID; // ID of the item to collect, can be used to identify which item is collected and update the counter accordingly
    
    void Start()
    {
        _totalItems = questItems.Length; // get the total number of items to collect
    }

    void CheckForItems()
    {
        if (_collectedItems < _totalItems)
        {
            if (questItems[_currentItem] == null) // check if the current item is collected (destroyed)
            {
                _collectedItems++; // increase the collected items counter
                _currentItem++; // move to the next item in the array
            }
        }
        
        if (_collectedItems >= _totalItems) // check if all items are collected
        {
            OpenDoor(); // open the door to win the game
        }
        
    }
    
    void OpenDoor()
    {
        //logic to open door asset from unity store. This will be a simple animation - door will open and player can walk through to win the game
        // sets win screen canvas to active
    }
    
}
