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

    public int[] GetStatus()
    {
        return statusData;
    }
}
