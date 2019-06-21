using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillStatusUI : MonoBehaviour
{
    public StatusData instance;
    //활성화, 비활성화 용
    public PlayerHPUI hpUI;
    public GameObject joystick, relatedSkill, playerInfoUI;

    //탭 버튼 스프라이트
    public Sprite pressedTabButton, releasedTabButton;

    //스킬 탭 버튼 관련
    public GameObject skillUI;
    public Image skillTabButton;

    //스킬 탭 내용 관련
    public GameObject skillInfo, skillIcon;
    public Text skillName, skillLevel;
    public Sprite[] skillSprite;

    //스텟 탭 버튼 관련
    public GameObject statusUI;
    public Image statusTabButton;

    //스텟 탭 내용 관련
    public Text statusLevel, statusRemainPoint;
    public Text[] status, statusPlus;

    public SkillManager skillMan;

    //레벨, 남은 포인트
    private int level, remainPoint;

    private int currentTab;

    //적용되기전 각 스텟
    private int[] nowStatusPlus;
    //적용된 후 각 스텟
    private int[] currentStatus;

    private void Awake()
    {
        nowStatusPlus = new int[5];
        currentStatus = new int[5];

        for (int i = 0; i < 5; i++)
        {
            //현재 스탯 불러오기
            nowStatusPlus[i] = 0;
            // currentStatus[i] = StatusData.instance.GetStatus()[i];
            currentStatus[i] = instance.GetStatus()[i];
        }
        //현재 레벨, 남은 포인트 받아오기
    }

    //활성화 되었을 때
    private void OnEnable()
    {
        //시간 멈추기
        Time.timeScale = 0;

        //남은 포인트와 레벨 받아오기
        //remainPoint = StatusData.instance.GetRemainPoint();
        //level = StatusData.instance.GetLevel();
        //currentStatus = StatusData.instance.GetStatus();
        remainPoint = instance.GetRemainPoint();
        level = instance.GetLevel();
        currentStatus = instance.GetStatus();

        //현재 탭 스텟으로 설정, UI끄기
        currentTab = 2;
        skillTabButton.sprite = releasedTabButton;
        statusTabButton.sprite = pressedTabButton;
        skillUI.SetActive(false);
        statusUI.SetActive(true);

        joystick.SetActive(false);
        relatedSkill.SetActive(false);
        playerInfoUI.SetActive(false);

        UpdateStatus();
    }

    private void OnDisable()
    {
        for (int i = 0; i < 5; i++)
        {
            remainPoint += nowStatusPlus[i];
            nowStatusPlus[i] = 0;
        }
        //UI활성화
        joystick.SetActive(true);
        relatedSkill.SetActive(true);
        playerInfoUI.SetActive(true);
        //시간 흐르게
        Time.timeScale = 1;

        //싱글톤에 올리기
        //StatusData.instance.UpdateStatus(currentStatus);
        //StatusData.instance.SetRemainPoint(remainPoint);
        instance.UpdateStatus(currentStatus);
        instance.SetRemainPoint(remainPoint);
        hpUI.UpdateStatus();
    }

    public int[] GetStatus()
    {
        //현재 스탯 반환
        return currentStatus;
    }

    public void OnBagClicked()
    {
        //가방 클릭되었을 때
        gameObject.SetActive(true);
    }

    //상단 탭 클릭시
    public void OnTabClicked(string mode)
    {
        //탭 변경
        if (mode == "SkillTab")
        {
            if (currentTab == 2)
            {
                OnSkillTabEnable();
            }
        }
        else if (mode == "StatusTab")
        {
            if (currentTab == 1)
            {
                OnStatusTabEnable();
            }
        }
    }

    //나가기 버튼 클릭시
    public void OnExitClicked()
    {
        gameObject.SetActive(false);
    }

    /*
     * -----------------------이하 스킬탭 관련--------------------------------
     */

    //스킬 탭 열릴때
    private void OnSkillTabEnable()
    {
        currentTab = 1;
        skillTabButton.sprite = pressedTabButton;
        statusTabButton.sprite = releasedTabButton;
        skillUI.SetActive(true);
        statusUI.SetActive(false);
    }

    //스킬 아이콘 클릭시
    public void OnSkilliconClicked(string skill)
    {
        skillInfo.SetActive(true);
        Image skillImage = skillIcon.GetComponent<Image>();

        //이하 스킬 정보 팝업창 내용
        if(skill == "Dash" && isEnableSkill(skill))
        {
            skillName.text = "대쉬";
            skillImage.sprite = skillSprite[0];
            //스킬레벨 불러오기
        }
    }

    //스킬 언락 여부
    private bool isEnableSkill(string skill)
    {
        if(skill == "Dash")
        {
            return true;
        }
        return false;
    }

    /*
     * -----------------------이하 스텟탭 관련--------------------------------
     */

    //스텟 탭 열릴때
    private void OnStatusTabEnable()
    {
        currentTab = 2;
        statusTabButton.sprite = pressedTabButton;
        skillTabButton.sprite = releasedTabButton;
        statusUI.SetActive(true);
        skillUI.SetActive(false);

        //육체레벨 불러오기
        //statusLevel.text = ~~~
        //남은 스텟 불러오기
        statusRemainPoint.text = remainPoint.ToString();

        UpdateStatus();
    }

    //스텟 화면 업데이트(동기화)
    private void UpdateStatus()
    {
        statusLevel.text = level.ToString();
        statusRemainPoint.text = remainPoint.ToString();
        for(int i = 0; i < 5; i++)
        {
            statusPlus[i].text = "+ " + nowStatusPlus[i];
            status[i].text = ": " + currentStatus[i];
        }
    }

    //스텟 +버튼 눌렀을때
    public void OnClickPlusButton(int index)
    {
        //index : 0부터 - 체력, 힘, 방어력, 회피율, 크확

        if(remainPoint > 0){
            nowStatusPlus[index]++;
            remainPoint--;
            UpdateStatus();
        }
    }

    public void OnStatusApplyClicked()
    {
        for(int i = 0; i < 5; i++)
        {
            currentStatus[i] += nowStatusPlus[i];
            nowStatusPlus[i] = 0;
        }
        UpdateStatus();
        
    }

    public void OnStatusCancelClicked()
    {
        for(int i = 0; i < 5; i++)
        {
            remainPoint += nowStatusPlus[i];
            nowStatusPlus[i] = 0;
        }
        UpdateStatus();
    }
}
