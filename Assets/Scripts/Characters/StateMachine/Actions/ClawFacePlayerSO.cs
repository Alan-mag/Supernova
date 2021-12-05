using UnityEngine;
using Supernova.StateMachine;
using Supernova.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ClawFacePlayer", menuName = "State Machines/Actions/Claw Face Player")]
public class ClawFacePlayerSO : StateActionSO
{
	public TransformAnchor playerAnchor;
	protected override StateAction CreateAction() => new ClawFacePlayer();
}

public class ClawFacePlayer : StateAction
{
	TransformAnchor _protagonist;
	Transform _actor;
	protected new ClawFacePlayerSO OriginSO => (ClawFacePlayerSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
		_actor = stateMachine.transform;
		_protagonist = ((ClawFacePlayerSO)OriginSO).playerAnchor;
	}
	
	public override void OnUpdate()
	{
		Debug.Log(_protagonist);
		if (_protagonist.isSet)
        {
			Vector3 relativePos = _protagonist.Transform.position - _actor.position;
			relativePos.y = 0f;

			Quaternion rotation = Quaternion.LookRotation(relativePos);
			_actor.rotation = rotation;
        }
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
