using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

// in UOP it's InputReader GameInput, in may game I already have InputAction_Cube thing
// not sure what to do
[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IMenusActions
{
	// Shared between menus and dialogues [if using dialogues]
	public event UnityAction moveSelectionEvent = delegate { };

	// Menus
	public event UnityAction menuMouseMoveEvent = delegate { };

	public event UnityAction menuClickButtonEvent = delegate { };
	public event UnityAction menuUnpauseEvent = delegate { };
	public event UnityAction menuPauseEvent = delegate { };
	public event UnityAction menuCloseEvent = delegate { };

	private GameInput gameInput;

	private void OnEnable()
	{
		if (gameInput == null)
		{
			gameInput = new GameInput();
			gameInput.Menus.SetCallbacks(this);
		}
	}

	public void EnableMenuInput()
    {
		// gameInput.Dialogues.Disable();
		gameInput.Menus.Enable();
    }

	public void DisableAllInput()
    {
		gameInput.Menus.Disable();
		// currently handling gameplay input through other script
		// so this wont's turn that off yet
	}

	private void OnDisable()
	{
		DisableAllInput();
	}

	/******** MENU CONTROLS ********/

	// both menu and dialogue [if using dialogue]
	public void OnMoveSelection(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			moveSelectionEvent.Invoke();
	}

	public void OnMouseMove(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			menuMouseMoveEvent.Invoke();
	}

	public void OnConfirm(InputAction.CallbackContext context)
	{

		if (context.phase == InputActionPhase.Performed)
		{
			menuClickButtonEvent.Invoke();
		}
	}

	public void OnUnpause(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
		{
			menuUnpauseEvent.Invoke();
		}
	}

	public void OnPause(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
		{
			menuPauseEvent.Invoke();
		}
	}

	public void OnCancel(InputAction.CallbackContext context)
	{

		if (context.phase == InputActionPhase.Performed)
		{
			menuCloseEvent.Invoke();
		}
	}

	public void OnSubmit(InputAction.CallbackContext context)
    {

    }

	public void OnNavigate(InputAction.CallbackContext context)
	{

	}

	public void OnChangeTab(InputAction.CallbackContext context)
	{

	}

	public void OnInventoryActionButton(InputAction.CallbackContext context)
	{

	}

	public void OnClick(InputAction.CallbackContext context)
	{

	}

	public void OnPoint(InputAction.CallbackContext context)
	{

	}

	public void OnRightClick(InputAction.CallbackContext context)
	{

	}

	public void OnCloseInventory(InputAction.CallbackContext context)
	{

	}
}
