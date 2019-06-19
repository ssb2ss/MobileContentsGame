using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class JoyStick : MonoBehaviour
{

    // 공개
    public PlayerFoot playerFoot;
    public Transform stick;         // 조이스틱.
    public PlayerController playerCon;
    public bool isPlayerCon;
    public GameObject canvas;
    public GameObject weapon;

    // 비공개
    private Vector3 joyPos;
    private Vector3 stickFirstPos;  // 조이스틱의 처음 위치.
    private Vector3 joyVec;         // 조이스틱의 벡터(방향)
    private float radius;           // 조이스틱 배경의 반 지름.
    private bool isEdge;
    private int s_width = Screen.width;

    void Start()
    {
        radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        stickFirstPos = transform.position;

        // 캔버스 크기에대한 반지름 조절.
        float can = canvas.GetComponent<RectTransform>().localScale.y;
        radius *= can;
    }

    void FixedUpdate()
    {
        //조이스틱과 중심의 각도
        float angle = GetDegree(new Vector2(transform.position.x, transform.position.y), new Vector2(stick.position.x, stick.position.y));

        //플레이어 조이콘 혹은 스킬 조이콘인지
        if (isPlayerCon)
        {
            //플레이어 x좌표 움직이기
            joyPos = stick.transform.position;
            playerCon.MoveX((joyPos.x - transform.position.x)/(float)s_width * 2f);

            //점프하기
            if (playerFoot.GetIsGround() && (angle > 60 && angle < 120))
                playerCon.Jump();
        }
        else
        {
            //조이스틱의 끝에 닿는다면
            if (isEdge)
                playerCon.OnAttack(true, angle);
            else
                playerCon.OnAttack(false, angle);
        }
    }

    // 드래그
    public void Drag(BaseEventData _data)
    {
        PointerEventData data = _data as PointerEventData;
        joyPos = data.position;
        joyVec = (joyPos - stickFirstPos).normalized;
        float dis = Vector3.Distance(joyPos, stickFirstPos);


        if (dis < radius){
            isEdge = false;
            stick.position = stickFirstPos + joyVec * dis;
        }
        else
        {
            isEdge = true;
            stick.position = stickFirstPos + joyVec * radius;
        }  
    }

    // 드래그 끝.
    public void DragEnd()
    {
        stick.position = stickFirstPos; // 스틱을 원래의 위치로.
        joyVec = Vector3.zero;          // 방향을 0으로.
        isEdge = false;
    }

    private float GetDegree(Vector2 from, Vector2 to)
    {
        return Mathf.Atan2(to.y - from.y, to.x - from.x) * 180 / Mathf.PI;
    }
}