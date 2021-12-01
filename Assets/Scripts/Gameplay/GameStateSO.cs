using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// only create 1 then comment out
// [CreateAssetMenu(fileName = "GameState", menuName = "Gameplay/GameState")]
public class GameStateSO : ScriptableObject
{
    [ReadOnly] public int latestCompletedLevel = 0;
}
