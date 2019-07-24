using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleBossFSMState : MonoBehaviour
{
    protected EagleBossFSMManager manager;

    public virtual void BeginState()
    {

    }

    void Awake()
    {
        manager = GetComponent<EagleBossFSMManager>();
    }
}
