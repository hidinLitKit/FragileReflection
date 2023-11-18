using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class MoveToPosition : ActionNode
{
	public float stoppingDistance = 0.1f;
	Animator animator;
	EnemyController enemyController;
	protected override void OnStart()
	{
		context.agent.isStopped = false;
		context.agent.speed = blackboard.moveSpeed;
		context.agent.stoppingDistance = stoppingDistance;
		context.agent.destination = blackboard.moveToPosition;
		enemyController = context.gameObject.GetComponent<EnemyController>();
		animator = enemyController.animator;

		if(animator.GetBool("Move") != true)
			animator.SetBool("Move", true);
    }

	protected override void OnStop()
	{
		context.agent.isStopped = true;
		if(animator.GetBool("Move") != false)
			animator.SetBool("Move", false);
    }

	protected override State OnUpdate()
	{
        if (enemyController.CanSee())
		{
			return State.Failure;
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
