using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
	Gameplay,// regular state: ship flying and attacks, etc.
	Pause,// pause menu is opened, the whole game is frozen
	Inventory, //when  UI is open [used inventory in uop] -- might change this
	Dialogue,
	Cutscene,
	LocationTransition,// when the player transitions to diff loc, fade to black begins and control is removed from the player // might not need this?
	// Combat,// enemy is nearby and alert, player can't open Inventory or initiate dialogues, but can pause the game
}

// only create 1 then comment out
// [CreateAssetMenu(fileName = "GameState", menuName = "Gameplay/GameState")]
public class GameStateSO : ScriptableObject
{
    [ReadOnly] public int latestCompletedLevel = 0;
	private GameState _currentGameState = default;
	private GameState _previousGameState = default;
	public GameState CurrentGameState => _currentGameState;

	public void UpdateGameState(GameState newGameState)
    {
		if (newGameState != CurrentGameState)
        {
			_previousGameState = _currentGameState;
			_currentGameState = newGameState;
        }
    }

	public void ResetToPreviousGameState()
    {
		_currentGameState = _previousGameState;
    }

}
