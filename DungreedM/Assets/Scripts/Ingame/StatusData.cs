using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusData : MonoBehaviour
{
    public static StatusData instance;

    private int[] statusData;
    private int remainPoint;
    private int level;

    void Awake()
    {
        StatusData.instance = this;
        DontDestroyOnLoad(this);
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
}
