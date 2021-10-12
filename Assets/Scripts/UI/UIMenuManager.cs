using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] private UIMainMenu _mainMenuPanel = default;
    [SerializeField] private InputReader _inputReader = default;

    [Header("Broadcasting on")]
    [SerializeField]
    private VoidEventChannelSO _startNewGameEvent = default;

    private IEnumerator Start()
    {
        _inputReader.EnableMenuInput();
        yield return new WaitForSeconds(0.4f);
        SetMenuScreen();
    }

    void SetMenuScreen()
    {
        _mainMenuPanel.NewGameButtonAction += ButtonStartNewGameClicked;
    }

    void ButtonStartNewGameClicked()
    {
        ConfirmStartNewGame();
    }

    void ConfirmStartNewGame()
    {
        _startNewGameEvent.RaiseEvent();
    }
}
