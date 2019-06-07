using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    public InfoController info;
    public int code;
    

    public void DeleteData()
    {
        info.infoDown();
        PlayerPrefs.SetInt("SelectedNow", 0);

        PlayerPrefs.DeleteKey("Name" + code);
        PlayerPrefs.DeleteKey("PlayTime" + code);
        PlayerPrefs.DeleteKey("SoulLevel" + code);
        PlayerPrefs.DeleteKey("Weapon" + code);

        info.LoadData();
    }
}
