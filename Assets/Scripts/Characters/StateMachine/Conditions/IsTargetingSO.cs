using UnityEngine;
using Supernova.StateMachine;
using Supernova.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsTargeting", menuName = "State Machines/Conditions/Is Targeting")]
public class IsTargetingSO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsTargeting();
}

public class IsTargeting : Condition
{
	protected new IsTargetingSO OriginSO => (IsTargetingSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
	}
	
	protected override bool Statement()
	{
		return true;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
