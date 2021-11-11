using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// I just made up this script, the UOP uses diff paradigm
// use StartGame as reference to startnewgame and
// delete this script function
public class StartGame : MonoBehaviour
{
    [SerializeField]
    private GameSceneSO _locationsToLoad;
    [Header("Broadcasting on")]
    [SerializeField]
    private LoadEventChannelSO _startGameEvent = default;
    [SerializeField]
    [Header("Listening to")]
    private VoidEventChannelSO _startNewGameEvent = default;


    public GameObject _canvas;

    void Awake()
    {
        if (!_canvas)
            _canvas = GameObject.Find("Main Menu Canvas");
    }

    private void Start()
    {
        _startNewGameEvent.OnEventRaised += StartNewGame;
    }

    private void OnDestroy()
    {
        _startNewGameEvent.OnEventRaised -= StartNewGame;
    }

    void StartNewGame()
    {
        _startGameEvent.RaiseEvent(_locationsToLoad);
    }

    /*public void StartFirstLevel()
    {
        _canvas.SetActive(false);
        SceneManager.LoadScene("Island", LoadSceneMode.Additive); // need to unload main menu when starting game
    }*/
}
