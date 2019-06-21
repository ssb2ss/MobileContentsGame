using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    public GameObject SaveLoadUI, CreatePlayerUI;
    public GameObject Warning2;

    private Image image;
    private string input;

    void Start()
    {
        input = "Dijkstra Fenwicktree";
        image = Warning2.GetComponent<Image>();
    }

    public void OnCreateButtonClicked(int infoCode)
    {
        PlayerPrefs.SetInt("CreateSelected", infoCode);
        SaveLoadUI.SetActive(false);
        CreatePlayerUI.SetActive(true);
    }

    public void Save()
    {
        if (input == "Dijkstra Fenwicktree")
        {
            StopCoroutine("fadeOut");
            Warning2.SetActive(true);
            Color color = image.color;
            color.a = 1f;
            image.color = color;
            StartCoroutine("fadeOut");
            return;
        }
        int createCode = PlayerPrefs.GetInt("CreateSelected");

        PlayerPrefs.SetString("Name" + createCode, input);
        PlayerPrefs.SetInt("Weapon" + createCode, 1);
        PlayerPrefs.SetInt("SoulLevel" + createCode, 1);
        PlayerPrefs.SetInt("PlayTime" + createCode, 0);
        PlayerPrefs.SetString("SkillTree" + createCode, "11000000000000000000000");

        SaveLoadUI.SetActive(true);
        CreatePlayerUI.SetActive(false);
    }

    public void EndEdit(string input_)
    {
        input = input_;
    }

    IEnumerator fadeOut()
    {
        Color color = image.color;
        yield return new WaitForSeconds(1f);
        for (float i = 0f; i <= 1; i += 0.01f)
        {
            color.a -= 0.01f;
            image.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        Warning2.SetActive(false);
        yield return null;
    }
}
