using UnityEngine;
using Supernova.StateMachine;
using Supernova.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ClawStopChargingAction", menuName = "State Machines/Actions/Claw Stop Charging Action")]
public class ClawStopChargingActionSO : StateActionSO
{
	protected override StateAction CreateAction() => new ClawStopChargingAction();
}

public class ClawStopChargingAction : StateAction
{
	private Claw _clawScript;
	protected new ClawChargingActionSO OriginSO => (ClawChargingActionSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
		_clawScript = stateMachine.GetComponent<Claw>();
	}

	public override void OnUpdate()
	{
		//Get the Renderer component from the new cube
		var rend = _clawScript.GetComponent<Renderer>();
		//Call SetColor using the shader property name "_Color" and setting the color to red
		rend.material.color = Color.white;
	}

	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
