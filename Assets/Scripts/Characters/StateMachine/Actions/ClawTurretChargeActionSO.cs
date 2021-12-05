using UnityEngine;
using Supernova.StateMachine;
using Supernova.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ClawTurretChargeAction", menuName = "State Machines/Actions/Claw Turret Charge Action")]
public class ClawTurretChargeActionSO : StateActionSO
{
	protected override StateAction CreateAction() => new ClawTurretChargeAction();
}

public class ClawTurretChargeAction : StateAction
{
	Transform _clawTransform;
	protected new ClawTurretChargeActionSO OriginSO => (ClawTurretChargeActionSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
		_clawTransform = stateMachine.transform;
	}
	
	public override void OnUpdate()
	{
		if (_clawTransform)
        {
			var turret = _clawTransform.Find("Turret");
			var rend = turret.GetComponent<Renderer>();
			rend.material.color = Color.red;
        }
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
