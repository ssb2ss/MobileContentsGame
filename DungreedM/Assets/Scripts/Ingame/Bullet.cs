using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 dir;
    private float moveSpeed;

    void Start()
    {
        moveSpeed = 4f;
        dir = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
    }

    private void Update()
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
}
