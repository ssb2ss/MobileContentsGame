using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    private bool isGround;
    public bool referenc;

    void OnTriggerEnter2D(Collider2D other)
    {
        //착지 처리
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //점프 시
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    public bool GetIsGround()
    {
        return isGround;
    }

    private void Update()
    {
        referenc = isGround;
    }
}
