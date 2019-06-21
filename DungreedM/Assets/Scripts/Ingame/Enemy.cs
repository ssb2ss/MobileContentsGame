using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject bullet1, bullet2;
    public GameObject childEagle1, childEagle2, childEagle3, childEagle4;
    public int hp;
    public float moveSpeed;
    public StatusData instance;
    public Transform playerTransform;
    public GameObject attackEffect;
    public GameObject DamageUI, DamageUI1, DamageUI2, DamageUI3;
    public Camera cameraMain;

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

    void OnDisable()
    {
        DamageUI.SetActive(false);
        DamageUI1.SetActive(false);
        DamageUI2.SetActive(false);
        DamageUI3.SetActive(false);
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
            if(DamageUI.activeSelf == false)
            {
                hp -= instance.GetStatus()[1];
                DamageUI.GetComponent<Text>().text = instance.GetStatus()[1].ToString();
                DamageUI.transform.position = cameraMain.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));

                StartCoroutine(Damage());
            }
            else if(DamageUI1.activeSelf == false)
            {
                hp -= instance.GetStatus()[1];
                DamageUI1.GetComponent<Text>().text = instance.GetStatus()[1].ToString();
                DamageUI1.transform.position = cameraMain.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));

                StartCoroutine(Damage1());
            }
            else if(DamageUI2.activeSelf == false)
            {
                hp -= instance.GetStatus()[1];
                DamageUI2.GetComponent<Text>().text = instance.GetStatus()[1].ToString();
                DamageUI2.transform.position = cameraMain.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));

                StartCoroutine(Damage2());
            }
            else if(DamageUI3.activeSelf == false)
            {
                hp -= instance.GetStatus()[1];
                DamageUI3.GetComponent<Text>().text = instance.GetStatus()[1].ToString();
                DamageUI3.transform.position = cameraMain.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));

                StartCoroutine(Damage3());
            }
            //hp -= StatusData.instance.GetStatus()[1];
            
        }
    }

    IEnumerator OnDeath()
    {
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        yield break;
    }

    IEnumerator Damage()
    {
        DamageUI.SetActive(true);
        Color color = DamageUI.GetComponent<Text>().color;
        RectTransform rect = DamageUI.GetComponent<RectTransform>();
        color.a = 1;
        DamageUI.GetComponent<Text>().color = color;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.02f);
            DamageUI.transform.position = cameraMain.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
        }
        for (int i = 1; i <= 100; i++)
        {
            color.a -= 0.01f;
            DamageUI.GetComponent<Text>().color = color;
            DamageUI.transform.position = cameraMain.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            rect.transform.localPosition = new Vector2(rect.transform.localPosition.x, rect.transform.localPosition.y + 0.3f*i);
            yield return new WaitForSeconds(0.01f);
        }
        DamageUI.SetActive(false);
        yield break;
    }

    IEnumerator Damage1()
    {
        DamageUI1.SetActive(true);
        Color color1 = DamageUI.GetComponent<Text>().color;
        RectTransform rect = DamageUI1.GetComponent<RectTransform>();
        color1.a = 1;
        DamageUI1.GetComponent<Text>().color = color1;
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.02f);
            DamageUI1.transform.position = cameraMain.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
        }
        
        for (int i = 1; i <= 100; i++)
        {
            color1.a -= 0.01f;
            DamageUI1.GetComponent<Text>().color = color1;
            DamageUI1.transform.position = cameraMain.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            rect.transform.localPosition = new Vector2(rect.transform.localPosition.x, rect.transform.localPosition.y + 0.3f*i);
            yield return new WaitForSeconds(0.01f);
        }
        DamageUI1.SetActive(false);
        yield break;
    }
    IEnumerator Damage2()
    {
        DamageUI2.SetActive(true);
        Color color2 = DamageUI2.GetComponent<Text>().color;
        RectTransform rect = DamageUI2.GetComponent<RectTransform>();
        color2.a = 1;
        DamageUI2.GetComponent<Text>().color = color2;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.02f);
            DamageUI2.transform.position = cameraMain.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
        }
        for (int i = 1; i <= 100; i++)
        {
            color2.a -= 0.01f;
            DamageUI2.GetComponent<Text>().color = color2;
            DamageUI2.transform.position = cameraMain.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            rect.transform.localPosition = new Vector2(rect.transform.localPosition.x, rect.transform.localPosition.y + 0.3f*i);
            yield return new WaitForSeconds(0.01f);
        }
        DamageUI2.SetActive(false);
        yield break;
    }
    IEnumerator Damage3()
    {
        DamageUI3.SetActive(true);
        Color color3 = DamageUI3.GetComponent<Text>().color;
        RectTransform rect = DamageUI3.GetComponent<RectTransform>();
        color3.a = 1;
        DamageUI3.GetComponent<Text>().color = color3;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.02f);
            DamageUI3.transform.position = cameraMain.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
        }
        for (int i = 1; i <= 100; i++)
        {
            color3.a -= 0.01f;
            DamageUI3.GetComponent<Text>().color = color3;
            DamageUI3.transform.position = cameraMain.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            rect.transform.localPosition = new Vector2(rect.transform.localPosition.x, rect.transform.localPosition.y + 0.3f*i);
            yield return new WaitForSeconds(0.01f);
        }
        DamageUI3.SetActive(false);
        yield break;
    }
}
