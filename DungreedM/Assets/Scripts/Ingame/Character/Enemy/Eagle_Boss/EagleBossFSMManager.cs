using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EagleBossState
{
    IDLE = 0,
    FIRE,
    SUMMON,
    DEAD
}

public class EagleBossFSMManager : MonoBehaviour
{
    public GameObject DamageUIPrefab, DamageCriticalUIPrefab, canvas;
    public EagleBossState currentState, startState;
    public float exp;
    private StatusData statusData;
    private Animator anim;
    private int hp;

    Dictionary<EagleBossState, EagleBossFSMState> states = new Dictionary<EagleBossState, EagleBossFSMState>();

    void Awake()
    {
        hp = 200;
        exp = Random.Range(130f, 170f);
        anim = GetComponentInChildren<Animator>();
        statusData = GameObject.FindGameObjectWithTag("StatusData").GetComponent<StatusData>();

        states.Add(EagleBossState.IDLE, GetComponent<EagleBossIDLE>());
        states.Add(EagleBossState.FIRE, GetComponent<EagleBossFIRE>());
        states.Add(EagleBossState.SUMMON, GetComponent<EagleBossSUMMON>());
        states.Add(EagleBossState.DEAD, GetComponent<EagleBossDEAD>());
    }

    void Start()
    {
        SetState(startState);
    }

    public void SetState(EagleBossState newState)
    {
        foreach (EagleBossFSMState fsm in states.Values)
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
            bool isCritical = (Random.Range(1, 101) <= statusData.GetStatus()[4]) ? true : false;
            int damage = DamageMake(isCritical);
            hp -= damage;
            if (hp <= 0)
                SetState(EagleBossState.DEAD);

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
}
