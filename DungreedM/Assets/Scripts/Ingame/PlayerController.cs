using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Camera cameraMain;
    public GameObject weapon, attackEffect;

    private Rigidbody2D rigid2;
    private Animator animator;
    //방향
    private Quaternion right, left, effectRot;
    private float posBefore;
    private bool isAttackCool;
    private bool isAttacked;
    private float hp;
    

    void Start()
    {
        rigid2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        right = Quaternion.Euler(new Vector3(0, 0, 0));
        left = Quaternion.Euler(new Vector3(0, 180, 0));
        
        isAttackCool = false;
        isAttacked = false;
        posBefore = transform.position.x;
        hp = 100;
    }

    void FixedUpdate()
    {
        cameraMain.transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, -10);

        if (posBefore != transform.position.x)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);

        posBefore = transform.position.x;
    }

    public float getHP()
    {
        return hp;
    }

    //x좌표 이동
    public void MoveX(float impet) //impetus --> 운동량
    {
        if(transform.rotation == right)
            transform.Translate(new Vector3(impet, 0, 0));
        else
            transform.Translate(new Vector3(-impet, 0, 0));
        
    }
    //점프
    public void Jump()
    {
        rigid2.velocity = Vector2.zero;
        rigid2.AddForce(Vector2.up*10f, ForceMode2D.Impulse);
    }

    //maxHP초기화
    public void SetHp(int hp_)
    {
        hp = hp_;
    }

    //검 회전각도 : 25, -115

    public void OnAttack(bool isEdge, float angle)
    {
        //조이스틱의 끝에 닿아있다면
        if (isEdge)
        {
            if (!isAttackCool)
            {
                attackEffect.transform.rotation = Quaternion.Euler(new Vector3(0, weapon.transform.rotation.y, angle - 42));
                attackEffect.transform.position = transform.position;
                attackEffect.SetActive(true);
                StartCoroutine(CheckAttackCoolTime(0.6f));
                if (isAttacked)
                    isAttacked = false;
                else
                    isAttacked = true;
            }
        }
        //플레이어 방향
        if(angle != 0)
        {
            if (angle < 90 && angle > -90)
                transform.rotation = right;
            else
                transform.rotation = left;
            //칼 배치
            if (isAttacked)
                weapon.transform.rotation = Quaternion.Euler(new Vector3(0, weapon.transform.rotation.y, angle - 195));
            else
                weapon.transform.rotation = Quaternion.Euler(new Vector3(0, weapon.transform.rotation.y, angle + 15));
        }  
    }

    IEnumerator CheckAttackCoolTime(float cooltime) //공격 쿨타임
    {
        isAttackCool = true;
        yield return new WaitForSeconds(0.4f);
        attackEffect.SetActive(false);
        yield return new WaitForSeconds(cooltime - 0.4f);
        isAttackCool = false;
        yield break;
    }
}
