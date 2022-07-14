using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectiveManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] levelObjectiveEnemies;
    public int objectivesTillCompleted; // todo: prob make this cleaner

    private int currentlyDestroyed = 0;

    [Header("Listening to")]
    [SerializeField ] private VoidEventChannelSO levelObjectiveChannel = default;

    private void OnEnable()
    {
        levelObjectiveChannel.OnEventRaised += CheckIfObjectiveComplete;
    }

    void CheckIfObjectiveComplete()
    {
        currentlyDestroyed++;
        Debug.Log("currentlyDestroyed " + currentlyDestroyed);
        if (currentlyDestroyed >= objectivesTillCompleted)
        {
            Debug.Log("Level over");
        }
    }
}
