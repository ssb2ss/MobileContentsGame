using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Sky1, Sky2;
    public static Transform playerTransform;
    public static PlayerController playerCon;

    private int saveCode, playTime, weapon;
    private float soulLevel;
    
    /*
     * 플레이어번호
     * 무기 코드 1 2 3
     * 정신레벨 1 2 3
     * 플레이타임 1 2 3
     * 스킬 트리 1 2 3 string
     * 
     */

    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerCon = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        //해상도, 나둬도 꺼지지 않기
        //Screen.SetResolution(1920, 1080, true);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        /*
         PlayerPrefs 초기 설정
         - 저장 번호
         - 무기 코드
         - 정신 레벨
         - 플레이 타임
         - 스킬 트리
         */

        saveCode = PlayerPrefs.GetInt("SaveCode");
        playTime = PlayerPrefs.GetInt("Playtime" + saveCode);
        soulLevel = PlayerPrefs.GetFloat("soulLevel" + saveCode);
        weapon = PlayerPrefs.GetInt("Weapon" + saveCode);
        
    }

    void FixedUpdate()
    {
        //배경 하늘 이동
        Sky1.transform.Translate(new Vector3(-0.01f, 0, 0));
        Sky2.transform.Translate(new Vector3(-0.01f, 0, 0));
        //배경 반복
        if (Sky1.transform.position.x < -43)
            Sky1.transform.position = new Vector3(33, 0, 0);
        else if (Sky2.transform.position.x < -43)
            Sky2.transform.position = new Vector3(33, 0, 0);
    }
}
