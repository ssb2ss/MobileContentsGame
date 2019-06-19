using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bullet1, bullet2;
    public GameObject childEagle1, childEagle2, childEagle3, childEagle4;
    public int hp;

    private Animator anim;
    

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Common_Attack"))
        {
        }
    }
}
