using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    new public Camera camera;

    private Rigidbody2D rigid2;
    //방향
    private Quaternion right, left;
    

    void Start()
    {
        rigid2 = GetComponent<Rigidbody2D>();

        right = Quaternion.Euler(new Vector3(0, 0, 0));
        left = Quaternion.Euler(new Vector3(0, 180, 0));
    }

    void FixedUpdate()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, -10);
    }

    //x좌표 이동
    public void MoveX(float impet) //impetus --> 운동량
    {
        //플레이어 방향 조정
        if(impet > 0)
        {
            transform.Translate(new Vector3(impet, 0, 0));
            transform.rotation = right;
        }
        else if (impet < 0)
        {
            transform.Translate(new Vector3(-impet, 0, 0));
            transform.rotation = left;
        }
        
    }
    //점프
    public void Jump()
    {
        rigid2.velocity = Vector2.zero;
        rigid2.AddForce(Vector2.up*10f, ForceMode2D.Impulse);
    }
}
