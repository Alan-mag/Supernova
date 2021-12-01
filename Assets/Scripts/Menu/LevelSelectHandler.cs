using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

// todo: this probably belongs in the persistent managers or something
// OnIntEventRequest is null because this monobehaviour hasn't been
// created when the loadscenetrigger is raising the event -->
// the listener in this script hasn't been made yet :(
public class LevelSelectHandler : MonoBehaviour
{
    [SerializeField] private GameStateSO _gameState = default;

    [Header("Broadcasting on")]
    [SerializeField] private LoadEventChannelSO _levelToLoad = default;

    [Header("Listening to")]
    [SerializeField] private IntEventChannelSO _levelCompletedChannel = default;

    public GameObject[] levelButtons;
    bool nextLevelToSelect;

    public int _latestCompletedLevel = 0;
    private Dictionary<int, int[]> levelDictionary = new Dictionary<int, int[]>()
    {
        { 1, new []{ 2 } },
        { 2, new []{ 3, 4, 5 } },
        { 3, new []{ 6, 7 } },
        { 4, new []{ 6 } },
        { 5, new []{ 6 } },
        { 6, new []{ 8 } },
        { 7, new []{ 8 } },
    };

    void Awake()
    {
        // Set_latestCompletedLevel(_latestCompletedLevel);
        // _levelCompletedChannel += UpdateLatestLevelInt;
        // _levelCompletedChannel.OnIntEventRequest += UpdateLatestLevelInt;
        //
        if (_gameState != null)
            _latestCompletedLevel = _gameState.latestCompletedLevel;
    }

    void Start()
    {
        if (_latestCompletedLevel == 8)
        {
            DisplayEndGameScreen();
            return;
        }
        Set_latestCompletedLevel(_latestCompletedLevel);
    }

    void DisplayEndGameScreen()
    {
        GameObject levelSelectCanvas = GameObject.Find("LevelSelectCanvas");
        levelSelectCanvas.SetActive(false);
    }

    /*void Update()
    {
        if (_latestCompletedLevel != 0)
        {
            Set_latestCompletedLevel(_latestCompletedLevel);
        }
    }*/

    /*void OnDestroy()
    {
        _levelCompletedChannel.OnIntEventRequest -= UpdateLatestLevelInt;
    }*/

    /*void UpdateLatestLevelInt(int latestCompletedLevel)
    {
        Debug.Log("UpdateLatestLevelInt " + latestCompletedLevel.ToString());
        _latestCompletedLevel = latestCompletedLevel;
    }*/

    void Set_latestCompletedLevel(int completedLevel)
    {
        Debug.Log("Set_latestCompletedLevel " + completedLevel.ToString());
        if (levelDictionary.ContainsKey(completedLevel))
        {
            int[] validNextLevels = levelDictionary[completedLevel];
            MenuSelectionHandler selectionHandler = gameObject.GetComponent(typeof(MenuSelectionHandler)) as MenuSelectionHandler;

            foreach(GameObject btn in levelButtons)
            {
                int btnToInt = Int32.Parse(btn.name);
                // check for invalid next level first
                if (Array.IndexOf(validNextLevels, btnToInt) < 0)
                {
                    btn.GetComponent<Image>().color = Color.grey;
                    btn.GetComponent<Button>().interactable = false;
                } 
                else
                {
                    btn.SetActive(true);
                    if (!nextLevelToSelect)
                    {
                        selectionHandler.UpdateDefault(btn);
                        nextLevelToSelect = true;
                    }
                }
            }
        }
    }

    public void LoadLevel(GameSceneSO gameScene)
    {
        _levelToLoad.RaiseEvent(gameScene, false, false);
    }
}
