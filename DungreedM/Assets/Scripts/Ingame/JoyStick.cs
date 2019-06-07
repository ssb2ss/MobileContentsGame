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

    // 비공개
    
    private Vector3 joyPos;
    private Vector3 stickFirstPos;  // 조이스틱의 처음 위치.
    private Vector3 joyVec;         // 조이스틱의 벡터(방향)
    private float radius;           // 조이스틱 배경의 반 지름.

    void Start()
    {
        radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        stickFirstPos = transform.position;

        // 캔버스 크기에대한 반지름 조절.
        float can = transform.parent.GetComponent<RectTransform>().localScale.x;
        radius *= can;
    }

    void FixedUpdate()
    {
        if (isPlayerCon)
        {
            //플레이어 x좌표 움직이기
            joyPos = stick.transform.position;
            playerCon.MoveX((joyPos.x - transform.position.x) * 0.001f);

            //조이스틱과 중심의 각도구하기
            float angle = GetDegree(new Vector2(transform.position.x, transform.position.y), new Vector2(stick.position.x, stick.position.y));
            //점프하기
            if (playerFoot.GetIsGround() && (angle > 60 && angle < 120))
                playerCon.Jump();
        }
    }

    // 드래그
    public void Drag(BaseEventData _data)
    {
        PointerEventData data = _data as PointerEventData;
        joyPos = data.position;
        joyVec = (joyPos - stickFirstPos).normalized;
        float dis = Vector3.Distance(joyPos, stickFirstPos);
        if (dis < radius)
            stick.position = stickFirstPos + joyVec * dis;
        else
            stick.position = stickFirstPos + joyVec * radius;
    }

    // 드래그 끝.
    public void DragEnd()
    {
        stick.position = stickFirstPos; // 스틱을 원래의 위치로.
        joyVec = Vector3.zero;          // 방향을 0으로.
    }

    private float GetDegree(Vector2 from, Vector2 to)
    {
        return Mathf.Atan2(to.y - from.y, to.x - from.x) * 180 / Mathf.PI;
    }
}