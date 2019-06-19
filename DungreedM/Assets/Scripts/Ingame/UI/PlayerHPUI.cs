using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : MonoBehaviour
{
    public Text level, hpT;

    private Slider slider;
    private int maxHp, level1;

    void Start()
    {
        slider = GetComponent<Slider>();

        maxHp = 100;
    }

    void Update()
    {
        slider.value = GameManager.playerCon.getHP()/maxHp;
        hpT.text = GameManager.playerCon.getHP().ToString() + " / " + maxHp.ToString();
    }

    public void UpdateStatus()
    {
        int[] status = StatusData.instance.GetStatus();
        maxHp = 100 + status[0] * 10;
        slider.GetComponent<RectTransform>().sizeDelta = new Vector2(400 + status[0] * 40, 150);
        slider.GetComponent<RectTransform>().transform.localPosition = new Vector2(410 + status[0] * 20, -45);

        GameManager.playerCon.SetHp(maxHp);
    }
}
