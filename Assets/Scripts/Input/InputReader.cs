using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

// in UOP it's InputReader GameInput, in may game I already have InputAction_Cube thing
// not sure what to do
[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IMenusActions, GameInput.IGameplayActions
{
	// Shared between menus and dialogues [if using dialogues]
	public event UnityAction moveSelectionEvent = delegate { };

	// Player Movement
	public event UnityAction<int> onBarrelRoll;
	public event UnityAction<bool> onBoost;
	public event UnityAction<bool> onBreak;
	public event UnityAction onSomersult;
	public event UnityAction onUTurn;

	// weapons
	// i might need to move these? to runtime scripts?
	public event UnityAction onFireLaser;
	public event UnityAction onReleaseLaser;

	// Other player actions
	public event UnityAction onUpdateHP;
	public event UnityAction<bool> onInputActive;
	public event UnityAction onDamageEffect;

	// Menus
	public event UnityAction menuMouseMoveEvent = delegate { };

	public event UnityAction menuClickButtonEvent = delegate { };
	public event UnityAction menuUnpauseEvent = delegate { };
	public event UnityAction menuPauseEvent = delegate { };
	public event UnityAction menuCloseEvent = delegate { };

	private GameInput gameInput;
	private Vector2 inputValue;

	[Header("Movement Data")]
	public bool physicMovement;

	[Range(0, 100)]
	public float trackSpeed;

	[Range(0, 100)]
	public float allRangeSpeed;

	[Range(0, 1)]
	public float smoothAllRangeMovement;

	[Range(0, 100)]
	public float localSpeed;

	[Range(0, 1)]
	public float smoothLocalSpeed;

	[Range(0, 10000)]
	public float aimSpeed;

	[Range(0, 1)]
	public float smoothAimSpeed;

	[Range(0, 90)]
	public float leanAngle;

	[Range(0, 90)]
	public float horizontalLeanLimit;

	[Range(0, 1)]
	public float leanSpeed;

	[Range(0, 1)]
	public float barrelRollSpeed;

	[Range(0, 100)]
	public float acrobaticSomersultSpeed;

	[Range(0, 100)]
	public float acrobaticUTurnSpeed;

	[Header("Inputs")]
	[ReadOnly] public Vector2 movementInput;
	[ReadOnly] public int leanAxisInput;
	[ReadOnly] public bool isDamageEffect;

	private void OnEnable()
	{
		if (gameInput == null)
		{
			gameInput = new GameInput();
			gameInput.Menus.SetCallbacks(this);
			gameInput.Gameplay.SetCallbacks(this);
		}
	}

	public void EnableGameplayInput()
	{
		gameInput.Menus.Disable();
		gameInput.Gameplay.Enable();
	}

	public void EnableMenuInput()
    {
		// gameInput.Dialogues.Disable();
		gameInput.Gameplay.Disable();
		gameInput.Menus.Enable();
    }

	public void DisableAllInput()
    {
		gameInput.Menus.Disable();
		gameInput.Gameplay.Disable();
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

	public void OnBarrelRoll(int axis) 
	{ 
		onBarrelRoll?.Invoke(axis); 
	}

	public void OnBoost(bool state) 
	{ 
		onBoost?.Invoke(state); 
	}

	public void OnBreak(bool state) 
	{ 
		onBreak?.Invoke(state); 
	}

	public void OnUTurn() { 
		onUTurn?.Invoke(); 
	}

/*	public void OnFireLaser() 
	{ 
		onFireLaser?.Invoke(); 
	}*/

	public void OnReleaseLaser() 
	{ 
		onReleaseLaser?.Invoke(); 
	}

	public void OnUpdateHP() { 
		onUpdateHP?.Invoke(); 
	}

	public void OnInputActive(bool state) 
	{ 
		onInputActive?.Invoke(state); 
	}

	/*public void UpdateInputData(Vector2 newMovement)
	{
		movementInput = newMovement;
	}*/

	public void OnMovement(InputAction.CallbackContext value)
	{
		movementInput = value.ReadValue<Vector2>();
	}

	public void OnLeftLean(InputAction.CallbackContext value)
	{
		if (value.started)
			leanAxisInput = -1;
		else if (value.canceled)
			leanAxisInput = 0;
	}

	public void OnRightLean(InputAction.CallbackContext value)
	{
		if (value.started)
			leanAxisInput = 1;
		else if (value.canceled)
			leanAxisInput = 0;
	}

	public void OnSomersult(InputAction.CallbackContext value)
	{
		if (value.performed)
			onSomersult?.Invoke();
	}

	// onfire and onfirelaser?
	public void OnFire(InputAction.CallbackContext value)
	{
		if (value.performed)
			onFireLaser?.Invoke();
	}
}
