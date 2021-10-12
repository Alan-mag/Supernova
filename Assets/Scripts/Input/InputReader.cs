using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

// in UOP it's GameInput, in may game I already have InputAction_Cube thing
// not sure what to do
[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IMenusActions
{

	// Menus
	public event UnityAction menuMouseMoveEvent = delegate { };

	public event UnityAction menuClickButtonEvent = delegate { };
	public event UnityAction menuUnpauseEvent = delegate { };
	public event UnityAction menuPauseEvent = delegate { };
	public event UnityAction menuCloseEvent = delegate { };
	/*public event UnityAction openInventoryEvent = delegate { }; // Used to bring up the inventory
	public event UnityAction closeInventoryEvent = delegate { }; // Used to bring up the inventory*/

	private void OnEnable()
    {
		if (gameInput == null)
        {
			gameInput = new GameInput();
			gameInput.Menus.SetCallbacks(this);
        }
    }

	private void OnDisable()
    {
		DisableAllInput();
    }

	public void OnMouseMove(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			menuMouseMoveEvent.Invoke();
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


}
