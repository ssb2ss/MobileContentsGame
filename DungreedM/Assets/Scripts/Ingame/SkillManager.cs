using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public Camera cameraMain;
    public PlayerFoot foot;
    public Slider screenCross;
    public GameObject skillRange1, skillRange2;
    

    private Rigidbody2D rigid2;
    private Vector3 originPos;

    //스킬 관련
    private int buttonCodeNow;
    private float range1, range2;
    private int skillCode1, skillCode2;

    private void Start()
    {
        rigid2 = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        //스킬 UI가 플레이어 따라가기
        if (skillRange1.activeSelf)
        {
            Vector3 screenPos = cameraMain.WorldToScreenPoint(GameManager.playerTransform.position);
            float x = screenPos.x;
            skillRange1.transform.position = new Vector3(x, screenPos.y, skillRange1.transform.position.z);
        }
        else if (skillRange2.activeSelf)
        {
            Vector3 screenPos = cameraMain.WorldToScreenPoint(GameManager.playerTransform.position);
            float x = screenPos.x;
            skillRange2.transform.position = new Vector3(x, screenPos.y, skillRange2.transform.position.z);
        }
    }

    private float GetDegree(Vector2 from, Vector2 to)
    {
        return Mathf.Atan2(to.y - from.y, to.x - from.x) * 180 / Mathf.PI;
    }

    /*
     * 스킬 관련 메서드
     * 스킬 클릭 시 범위 보이기
     * 범위 클릭 시 스킬 사용
     */

    //스킬 범위 나타내기, 쿨타임 돌리기
    public void OnSkillClicked(int buttonCode)
    {
        if(buttonCode == 1)
        {
            if (false) //즉발 스킬인지 체크
            {

            }
            else
            {
                if (skillRange1.activeSelf)
                    skillRange1.SetActive(false);
                else
                    skillRange1.SetActive(true);
            }
            
        }
        else if(buttonCode == 2)
        {
            if (true) //즉발 스킬인지 체크
            {
                UseSkill(7, null);
            }
            else
            {
                if (skillRange2.activeSelf)
                    skillRange2.SetActive(false);
                else
                    skillRange2.SetActive(true);
            }
        }
    }

    //스킬 사용
    public void OnSkillUsed(BaseEventData _data)
    {
        PointerEventData data = _data as PointerEventData;
        if(buttonCodeNow == 1)
        {
            UseSkill(1, data);
            skillRange1.SetActive(false);
        }
        else if(buttonCodeNow == 2)
        {
            UseSkill(1, data);
            skillRange2.SetActive(false);
        }
        
    }
    //스킬버튼코드 받기
    public void OnSkillButtonDown(int buttonCode)
    {
        buttonCodeNow = buttonCode;
    }

    //스킬버튼에 입력된 스킬을 바꾼다, 스킬 범위를 가져온다.
    public void OnSkillChanged()
    {
        
    }

    IEnumerator CameraShake()
    {
        originPos = cameraMain.transform.position;
        float timer = 0;
        while (timer <= 0.01f)
        {
            cameraMain.transform.localPosition = (Vector3)Random.insideUnitCircle * 0.5f + originPos;

            timer += Time.deltaTime;
            yield return null;
        }
        cameraMain.transform.localPosition = originPos;
    }

    /*
     이하 실제 스킬 코드
         */

    public void UseSkill(int code, PointerEventData data)
    {
        switch (code)
        {
            case 1:
                Dash(data);
                break;
            case 2:
                break;
            case 3:
                break;
            case 7:
                ComboDeathFault();
                break;
        }
    }

    private void Dash(PointerEventData data)
    {
        //float angle = GetDegree(new Vector2(transform.position.x, transform.position.y), new Vector2(data.position.x, data.position.y));
        //float dis = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(data.position.x, data.position.y));

        //플레이어의 스크린 위치
        Vector3 screenPos = cameraMain.WorldToScreenPoint(GameManager.playerTransform.position);
        //플레이어 대쉬 방향
        Vector2 vec = (data.position - new Vector2(screenPos.x, screenPos.y)).normalized;
        //터치위치의 월드 위치
        Vector3 worldPos = cameraMain.ScreenPointToRay(data.position).direction * 12;
        StartCoroutine(Dash(new Vector2(worldPos.x + cameraMain.transform.position.x, worldPos.y + cameraMain.transform.position.y)));

        float dis = Vector2.Distance(data.position, new Vector2(screenPos.x, screenPos.y));
        rigid2.velocity = Vector2.zero;
        rigid2.AddForce(vec * dis * 0.02f, ForceMode2D.Impulse);
        //worldPos.z = 0;
        //player.transform.position = new Vector3(worldPos.x + camera.transform.position.x, worldPos.y+camera.transform.position.y, 0);
    }

    IEnumerator Dash(Vector2 to)
    {
        Vector2 from = new Vector2(GameManager.playerTransform.position.x, GameManager.playerTransform.position.y);
        for(int i = 1; i < 25; i++)
        {
            GameManager.playerTransform.position = Vector2.Lerp(from, to, i * 0.05f);
            yield return new WaitForSeconds(0.01f);
            if (foot.GetIsGround()&&i>3)
                yield break;
        }
        yield return 0;
    }

    private void ComboDeathFault()
    {
        StartCoroutine("ComboDeathFaultCou");
    }

    IEnumerator ComboDeathFaultCou()
    {
        Time.timeScale = 0;
        for(int i = 1; i < 10; i++)
        {
            screenCross.value += 0.08f;
           yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine("CameraShake");

        yield return new WaitForSecondsRealtime(0.4f);

        Time.timeScale = 1;
        screenCross.value = 0.1f;
        yield return 0;
    }
}
