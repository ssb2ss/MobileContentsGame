using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bullet1, bullet2;
    public GameObject childEagle1, childEagle2, childEagle3, childEagle4;
    public int hp;
    public float moveSpeed;
    public StatusData instance;
    public Transform playerTransform;

    private Animator anim;
    private Vector3 dir;
    

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (hp <= 0)
            StartCoroutine(OnDeath());

        //if (GameManager.playerTransform.position.x < transform.position.x)
        //    transform.localScale = new Vector3(1, 1, 1);
        //else
        //    transform.localScale = new Vector3(-1, 1, 1);
        if (playerTransform.position.x < transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    //플레이어 따라가기
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

    IEnumerator Eagle()
    {
        yield return 0;
    }

    IEnumerator Shake()
    {
        Vector3 originPos = transform.position;
        float timer = 0;
        while (timer <= 0.01f)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * 0.5f + originPos;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Common_Attack"))
        {
            //hp -= StatusData.instance.GetStatus()[1];
            hp -= instance.GetStatus()[1];
        }
    }

    IEnumerator OnDeath()
    {
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        yield break;
    }
}
