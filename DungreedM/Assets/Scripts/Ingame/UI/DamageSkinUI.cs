using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageSkinUI : MonoBehaviour
{
    private Transform target;
    private Camera cameraMain;
    private Text text;

    void Awake()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        text = GetComponent<Text>();
    }

    public void StartMoving(Transform _target, int damage)
    {
        target = _target;
        text.text = damage.ToString();
        transform.position = cameraMain.WorldToScreenPoint(new Vector3(target.position.x, target.position.y + 1, target.position.z));

        StartCoroutine(DamageUI());
    }

    IEnumerator DamageUI()
    {
        gameObject.SetActive(true);
        Color color = GetComponent<Text>().color;
        RectTransform rect = GetComponent<RectTransform>();
        color.a = 1;
        GetComponent<Text>().color = color;

        for (int i = 0; i <= 100; i++)
        {
            color.a -= 0.01f;
            text.color = color;
            transform.position = cameraMain.WorldToScreenPoint(new Vector3(target.position.x, target.position.y + 1, target.position.z));
            rect.transform.localPosition = new Vector2(rect.transform.position.x, rect.transform.position.y + 0.3f * i);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
        yield break;
    }


}
