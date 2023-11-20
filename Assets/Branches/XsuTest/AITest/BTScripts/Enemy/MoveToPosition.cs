using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class MoveToPosition : ActionNode
{
	public float stoppingDistance = 0.1f;

	protected override void OnStart()
	{
		context.agent.isStopped = false;
		context.agent.speed = blackboard.moveSpeed;
		context.agent.stoppingDistance = stoppingDistance;
		context.agent.destination = blackboard.moveToPosition;
    }

	protected override void OnStop()
	{
		context.agent.isStopped = true;
	}

	protected override State OnUpdate()
	{
		if(blackboard.CanAttackPlayer(context.transform) || blackboard.CanSeePlayer(context.transform))
		{
			return State.Failure;
		}
		
		if (context.agent.pathPending)
		{
			return State.Running;
		}

		if (context.agent.remainingDistance < stoppingDistance)
		{
			return State.Success;
		}

		if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
		{
			return State.Failure;
		}

		return State.Running;
	}
}
