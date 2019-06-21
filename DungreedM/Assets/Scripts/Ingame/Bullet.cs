using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 dir;
    public float moveSpeed;
    public Transform playerTransform;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void Follow()
    {
        /*
        if (Vector3.Distance(GameManager.playerTransform.position, transform.position) <= Screen.height / 2)
        {
            dir = (GameManager.playerTransform.position - transform.position).normalized;
            if (transform.localScale.x == -1)
            {
                transform.Translate(dir * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(dir * moveSpeed * Time.deltaTime);
            }
        }
        */
        if (Vector3.Distance(playerTransform.position, transform.position) <= Screen.height / 2)
        {
            dir = (playerTransform.position - transform.position).normalized;
            if (transform.localScale.x == -1)
            {
                transform.Translate(dir * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(dir * moveSpeed * Time.deltaTime);
            }
        }
    }
}
