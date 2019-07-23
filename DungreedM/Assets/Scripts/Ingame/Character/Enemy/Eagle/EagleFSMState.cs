using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleFSMState : MonoBehaviour
{
    protected EagleFSMManager manager;

    public virtual void BeginState()
    {

    }

    void Awake()
    {
        manager = GetComponent<EagleFSMManager>();
    }
}
