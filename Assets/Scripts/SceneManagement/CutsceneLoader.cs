using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameplaySceneTrigger : MonoBehaviour
{
    public int levelCompleteNumber = 0;
    [SerializeField] private GameSceneSO _locationToLoad = default;

    [Header("Broadcasting on")]
    [SerializeField] private LoadEventChannelSO _locationLoadChannel = default;
    [SerializeField] private GameStateSO _gameState = default;
    public void LoadGameplayScene()
    {
        if (_locationToLoad)
        {
            _gameState.latestCompletedLevel = levelCompleteNumber;
            _locationLoadChannel.RaiseEvent(_locationToLoad, false, false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        LoadGameplayScene();
    }
}
