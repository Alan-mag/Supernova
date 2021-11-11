using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button _NewGameButton = default;

    public UnityAction NewGameButtonAction;

    public void SetMenuScreen()
    {
        _NewGameButton.Select();
    }

    public void NewGameButton()
    {
        NewGameButtonAction.Invoke();
    }

    /*public void ExitButton()
    {
        ExitButtonAction.Invoke();
    }*/
}
