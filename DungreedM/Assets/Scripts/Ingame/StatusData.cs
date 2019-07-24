using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusData : MonoBehaviour
{
    //public static StatusData instance;

    private int skillCode1, skillCode2;
    private int saveCode, playTime, weapon, soulLevel;
    private int[] statusData;
    private int remainPoint;
    private int level;
    private float exp;

    void Awake()
    {
        //StatusData.instance = this;
        DontDestroyOnLoad(this);
        //초기화
        statusData = new int[5] { 0, 5, 0, 0, 0 };
        remainPoint = 10;
        skillCode1 = 1;
        skillCode2 = 2;

        /*
         PlayerPrefs 초기 설정
         - 저장 번호
         - 무기 코드
         - 정신 레벨
         - 플레이 타임
         - 스킬 트리
         - 화폐(스킬 북)
         */

        saveCode = PlayerPrefs.GetInt("SaveCode");
        playTime = PlayerPrefs.GetInt("Playtime" + saveCode);
        soulLevel = PlayerPrefs.GetInt("soulLevel" + saveCode);
        weapon = PlayerPrefs.GetInt("Weapon" + saveCode);
        exp = PlayerPrefs.GetFloat("Exp" + saveCode);
    }

    public void UpdateStatus(int[] status)
    {
        statusData = status;
    }

    //index : 0부터 - 체력, 힘, 방어력, 회피율, 크확
    public int[] GetStatus()
    {
        return statusData;
    }

    public int GetRemainPoint()
    {
        return remainPoint;
    }
    public void SetRemainPoint(int remain)
    {
        remainPoint = remain;
    }

    public int GetLevel()
    {
        return level;
    }
    public void SetLevel(int lev)
    {
        level = lev;
    }

    public int GetSkillCode1()
    {
        return skillCode1;
    }
    public int GetSkillCode2()
    {
        return skillCode2;
    }

    public void PlusExp(float _exp)
    {
        exp += _exp;
    }
}
