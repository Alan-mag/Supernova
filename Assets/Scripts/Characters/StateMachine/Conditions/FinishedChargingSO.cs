using UnityEngine;
using Supernova.StateMachine;
using Supernova.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "FinishedCharging", menuName = "State Machines/Conditions/Finished Charging")]
public class FinishedChargingSO : StateConditionSO
{
	protected override Condition CreateCondition() => new FinishedCharging();
}

public class FinishedCharging : Condition
{
	private Claw _clawScript;
	protected new IsChargingSO OriginSO => (IsChargingSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
		_clawScript = stateMachine.GetComponent<Claw>();
	}

	protected override bool Statement() => !_clawScript.charge;

	public override void OnStateEnter()
	{
	}

	public override void OnStateExit()
	{
	}
}
