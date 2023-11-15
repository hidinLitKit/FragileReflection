using Dan;
using System;
using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

[System.Serializable]
public class ChooseEndpoint : ActionNode
    {
    Animator animator;
    EnemyController controller;
    int currentIndex;

    protected override void OnStart()
    {
        animator = context.gameObject.GetComponent<Animator>();
        controller = context.gameObject.GetComponent<EnemyController>();

        currentIndex = UnityEngine.Random.Range(0, controller.patrolEndpoints.Length-1);
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        var pos = controller.patrolEndpoints[currentIndex];
        if (pos)
        {
            blackboard.moveToPosition = pos.position;
            return State.Success;
        }
        return State.Failure;
    }


}

