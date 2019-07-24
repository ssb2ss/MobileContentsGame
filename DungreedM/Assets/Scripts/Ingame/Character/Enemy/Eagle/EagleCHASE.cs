using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleCHASE : EagleFSMState
{
    private Vector3 dir;
    private float timer, end;

    public override void BeginState()
    {
        base.BeginState();
        timer = 0;
        end = Random.Range(1f, 2f);
    }

    void Update()
    {
        if (timer > end)
            manager.SetState(EagleState.FIRE);
        timer += Time.deltaTime;
        dir = (manager.playerTransform.position - transform.position).normalized;
        transform.Translate(dir * manager.moveSpeed * Time.deltaTime);
    }
}
