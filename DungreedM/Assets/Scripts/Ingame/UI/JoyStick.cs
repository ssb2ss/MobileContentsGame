﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class JoyStick : MonoBehaviour
{

    // 공개
    public PlayerController playerCon;
    public PlayerFoot playerFoot;
    public Transform stick;         // 조이스틱.
    public bool isPlayerCon;
    public GameObject canvas;
    public GameObject weapon;

    // 비공개
    private Vector3 joyPos;
    private Vector3 stickFirstPos;  // 조이스틱의 처음 위치.
    private Vector3 joyVec;         // 조이스틱의 벡터(방향)
    private float radius;           // 조이스틱 배경의 반 지름.
    private bool isEdge;
    private float s_width;
    private float speed;

    void Start()
    {
        radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        stickFirstPos = transform.position;

        // 캔버스 크기에대한 반지름 조절.
        float can = canvas.GetComponent<RectTransform>().localScale.y;
        radius *= can;
        s_width = canvas.GetComponent<RectTransform>().localScale.x;

        speed = 0.001f;
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
            //GameManager.playerCon.MoveX((joyPos.x - transform.position.x)/s_width * speed);
            playerCon.MoveX((joyPos.x - transform.position.x) / s_width * speed);
            //점프하기
            if (playerFoot.GetIsGround() && (angle > 60 && angle < 120))
                //GameManager.playerCon.Jump();\
                playerCon.Jump();
        }
        else
        {
            //조이스틱의 끝에 닿는다면
            if (isEdge)
                //GameManager.playerCon.OnAttack(true, angle);
                playerCon.OnAttack(true, angle);
            else
                //GameManager.playerCon.OnAttack(false, angle);
                playerCon.OnAttack(false, angle);
        }
    }

    // 드래그
    public void Drag(BaseEventData _data)
    {
        //조이스틱 움직일 위치와 방향, 각도구하기
        PointerEventData data = _data as PointerEventData;
        joyPos = data.position;
        joyVec = (joyPos - stickFirstPos).normalized;
        float dis = Vector3.Distance(joyPos, stickFirstPos);

        //지름보다 클 때 지름만큼만 움직이기
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
    
    public Vector3 GetJoyVec()
    {
        return (stick.transform.position - stickFirstPos).normalized;
    }

    //각도 구하기 함수
    private float GetDegree(Vector2 from, Vector2 to)
    {
        return Mathf.Atan2(to.y - from.y, to.x - from.x) * 180 / Mathf.PI;
    }
}