using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSelectionHandler : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameObject _defaultSelection;
    public GameObject currentSelection;
    public GameObject mouseSelection;

    private void OnEnable()
    {
        _inputReader.menuMouseMoveEvent += HandleMoveCursor;
        _inputReader.moveSelectionEvent += HandleMoveSelection;

        StartCoroutine(SelectDefault());
    }

    private void OnDisable()
    {
        _inputReader.menuMouseMoveEvent -= HandleMoveCursor;
        _inputReader.moveSelectionEvent -= HandleMoveSelection;
    }

    public void UpdateDefault(GameObject newDefault)
    {
        _defaultSelection = newDefault;
    }

    private IEnumerator SelectDefault()
    {
        yield return new WaitForSeconds(0.03f);

        if (_defaultSelection != null)
            UpdateSelection(_defaultSelection);
    }

    public void Unselect()
    {
        currentSelection = null;
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);
    }

    private void HandleMoveSelection()
    {
        Cursor.visible = false;

        // Handle case where no ui element is selected bc mouse left selectable bounds
        if (EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(currentSelection);
    }

    private void HandleMoveCursor()
    {
        if (mouseSelection != null)
        {
            EventSystem.current.SetSelectedGameObject(mouseSelection);
        }

        Cursor.visible = true;
    }

    public void HandleMouseEnter(GameObject UIElement)
    {
        mouseSelection = UIElement;
        EventSystem.current.SetSelectedGameObject(UIElement);
    }

    public void HandleMouseExit(GameObject UIElement)
    {
        if (EventSystem.current.currentSelectedGameObject != UIElement)
        {
            return;
        }

        mouseSelection = null;
        EventSystem.current.SetSelectedGameObject(currentSelection);
    }

    public bool AllowsSubmit()
    {
        // was full check
        /*return !_inputReader.LeftMouseDown()
            || mouseSelection != null && mouseSelection == currentSelection;*/
        return mouseSelection != null && mouseSelection == currentSelection;
    }

    public void UpdateSelection(GameObject UIElement)
    {
        /*if ((UIElement.GetComponent<MultiInputSelectableElement>() != null) || (UIElement.GetComponent<MultiInputButton>() != null))
        {
            mouseSelection = UIElement;
            currentSelection = UIElement;
        }*/
        mouseSelection = UIElement;
        currentSelection = UIElement;
    }

    private void Update()
    {
        if ((EventSystem.current != null) && (EventSystem.current.currentSelectedGameObject == null) && (currentSelection != null))
        {

            EventSystem.current.SetSelectedGameObject(currentSelection);
        }
    }
}
