using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    new public Camera camera;
    public GameObject weapon, attackEffect;

    private Rigidbody2D rigid2;
    //방향
    private Quaternion right, left;
    private bool isAttackCool;
    private bool isAttacked;
    

    void Start()
    {
        rigid2 = GetComponent<Rigidbody2D>();

        right = Quaternion.Euler(new Vector3(0, 0, 0));
        left = Quaternion.Euler(new Vector3(0, 180, 0));
        
        isAttackCool = false;
        isAttacked = false;
    }

    void FixedUpdate()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, -10);
    }
    //x좌표 이동
    public void MoveX(float impet) //impetus --> 운동량
    {
        transform.Translate(new Vector3(impet, 0, 0));
    }
    //점프
    public void Jump()
    {
        rigid2.velocity = Vector2.zero;
        rigid2.AddForce(Vector2.up*10f, ForceMode2D.Impulse);
    }

    //검 회전각도 : 25, -115

    public void OnAttack(bool isEdge, float angle)
    {
        //조이스틱의 끝에 닿아있다면
        if (isEdge)
        {
            if (!isAttackCool)
            {
                StartCoroutine(CheckAttackCoolTime(1f));
                if (isAttacked)
                    isAttacked = false;
                else
                    isAttacked = true;
            }
        }
        //플레이어 방향
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

    IEnumerator CheckAttackCoolTime(float cooltime)
    {
        Debug.Log("Coroutine");
        isAttackCool = true;
        yield return new WaitForSeconds(cooltime);
        isAttackCool = false;
        yield return 0;
    }
}
