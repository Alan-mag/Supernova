using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public int levelCompleteNumber = 0;
    [SerializeField] private GameSceneSO _locationToLoad = default;
    [SerializeField] private GameSceneSO _menuToLoad = default;

    [Header("Broadcasting on")]
    [SerializeField] private LoadEventChannelSO _locationLoadChannel = default;
    [SerializeField] private IntEventChannelSO _levelCompletedChannel = default;
    [SerializeField] private GameStateSO _gameState = default;
    [SerializeField] private LoadEventChannelSO _menuLoadChannel = default;

    public void LoadMenuScene()
    {
        if (_menuToLoad)
        {
            // _gameState.latestCompletedLevel = levelCompleteNumber;
            _menuLoadChannel.RaiseEvent(_menuToLoad, false, false);
        }
    }

    public void LoadGameplayScene()
    {
        if (_locationToLoad)
        {
            // _gameState.latestCompletedLevel = levelCompleteNumber;
            _locationLoadChannel.RaiseEvent(_locationToLoad, false, false);
        }
    }
}
