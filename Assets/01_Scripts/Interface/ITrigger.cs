using UnityEngine;

public interface ITrigger

{
    /// <summary>
    /// Interface to assign a trigger to a button the player can trigger to activate multiple things in the dungeon
    /// </summary>
    
    void OnActivate();
    // push down animation,
    // changes colour
    //activate targets
    
    void OnDeactivate();
    
    //changes back to who it looks on start after a timer
    // deactivate targets
    
}
