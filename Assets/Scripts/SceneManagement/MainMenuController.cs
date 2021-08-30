using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject _canvas;

    void Awake()
    {
        if (!_canvas)
            _canvas = GameObject.Find("Main Menu Canvas");
    }

    public void StartFirstLevel()
    {
        _canvas.SetActive(false);
        SceneManager.LoadScene("Island", LoadSceneMode.Additive);
    }
}
