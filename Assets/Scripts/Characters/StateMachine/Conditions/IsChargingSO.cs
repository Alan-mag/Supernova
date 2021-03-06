using UnityEngine;
using Supernova.StateMachine;
using Supernova.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsCharging", menuName = "State Machines/Conditions/Is Charging")]
public class IsChargingSO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsCharging();
}

public class IsCharging : Condition
{
	private Claw _clawScript;
	protected new IsChargingSO OriginSO => (IsChargingSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
		_clawScript = stateMachine.GetComponent<Claw>();
	}

	protected override bool Statement() => _clawScript.charge;

	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
