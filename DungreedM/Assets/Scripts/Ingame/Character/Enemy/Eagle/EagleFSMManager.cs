using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EagleState{
    CHASE = 0,
    FIRE,
    DEAD
}

public class EagleFSMManager : MonoBehaviour
{
    public GameObject DamageUIPrefab, DamageCriticalUIPrefab, canvas;
    public EagleState currentState, startState;
    private StatusData statusData;
    private Animator anim;
    private int hp;

    Dictionary<EagleState, EagleFSMState> states = new Dictionary<EagleState, EagleFSMState>();

    void Awake()
    {
        hp = 200;
        anim = GetComponentInChildren<Animator>();
        statusData = GameObject.FindGameObjectWithTag("StatusData").GetComponent<StatusData>();

        states.Add(EagleState.CHASE, GetComponent<EagleCHASE>());
        states.Add(EagleState.FIRE, GetComponent<EagleFIRE>());
        states.Add(EagleState.DEAD, GetComponent<EagleDEAD>());
    }

    void Start()
    {
        SetState(startState);
    }

    public void SetState(EagleState newState)
    {
        foreach (EagleFSMState fsm in states.Values)
        {
            fsm.enabled = false;
        }

        currentState = newState;
        states[newState].enabled = true;
        states[newState].BeginState();
        anim.SetInteger("CurrentState", (int)currentState);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Common_Attack"))
        {
            Debug.Log("attacked");
            bool isCritical = (Random.Range(1, 101) <= statusData.GetStatus()[4]) ? true : false;
            int damage = DamageMake(isCritical);
            hp -= damage;
            if (hp <= 0)
                SetState(EagleState.DEAD);

            GameObject b;
            if (isCritical)
                b = Instantiate(DamageCriticalUIPrefab);
            else
                b = Instantiate(DamageUIPrefab);

            b.transform.SetParent(canvas.transform);
            b.GetComponent<DamageSkinUI>().StartMoving(transform, damage);

            
        }
    }

    private int DamageMake(bool isCritical)
    {
        //데미지 식 : 기본 데미지 50
        if (isCritical)
            return (int)(Random.Range(48 + statusData.GetStatus()[1], 51 + statusData.GetStatus()[1]) * 1.3f);
        else
            return (int)(Random.Range(48 + statusData.GetStatus()[1], 51 + statusData.GetStatus()[1]));

    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
