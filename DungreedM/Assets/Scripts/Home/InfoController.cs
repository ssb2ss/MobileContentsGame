using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{
    public GameObject noData, Data;
    public Text pname, ptime, level;
    public Image weapon;
    public InfoController info1, info2;
    public Sprite sword;

    private bool isClicked;
    private Vector3 originPos;

    void Start()
    {
        isClicked = false;
        originPos = transform.position;
    }

    private void OnEnable()
    {
        LoadData();
    }

    public void OnInfoClicked(int infoCode)
    {
        if (!PlayerPrefs.HasKey("Name" + infoCode))
            return;

        if (isClicked)
        {
            infoDown();
            PlayerPrefs.SetInt("SelectedNow", 0);
        }
        else
        {
            if (info1.GetIsClicked())
                info1.infoDown();
            else if (info2.GetIsClicked())
                info2.infoDown();

            isClicked = true;
            transform.position = new Vector3(originPos.x, originPos.y + 30, originPos.z);
            PlayerPrefs.SetInt("SelectedNow", infoCode);
        }
    }

    public void LoadData()
    {
        string tag_ = this.tag;
        if (!PlayerPrefs.HasKey("Name" + tag_))
        {
            noData.SetActive(true);
            Data.SetActive(false);
            return;
        }

        noData.SetActive(false);
        Data.SetActive(true);
        pname.text = PlayerPrefs.GetString("Name" + tag_);
        ptime.text = PlayerPrefs.GetInt("PlayTime" + tag_).ToString();
        level.text = PlayerPrefs.GetFloat("SoulLevel" + tag_).ToString();
        weapon.sprite = sword;
    }

    public void infoDown()
    {
        isClicked = false;
        transform.position = new Vector3(originPos.x, originPos.y, originPos.z);
    }

    public bool GetIsClicked()
    {
        return isClicked;
    }
}
