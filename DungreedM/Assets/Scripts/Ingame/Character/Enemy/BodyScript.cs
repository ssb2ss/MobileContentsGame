using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScript : MonoBehaviour
{
    public void Dead()
    {
        EagleFSMManager manager = GetComponentInParent<EagleFSMManager>();
        manager.statusData.PlusExp(manager.exp);
        Destroy(transform.parent.gameObject);
    }

    public void EndFire()
    {
        EagleFSMManager manager = GetComponentInParent<EagleFSMManager>();
        manager.SetState(EagleState.CHASE);
    }
}
