using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomButton : MonoBehaviour
{
    //public string mode;
    public GameObject TitleUI;
    public GameObject SaveLoadUI;

    public GameObject Warning1;

    private Image image;

    private void Start()
    {
        image = Warning1.GetComponent<Image>();
    }

    public void OnButtonClicked(string mode)
    {
        if (mode == "TitleStart")
        {
            TitleUI.SetActive(false);
            SaveLoadUI.SetActive(true);
        }
        else if(mode == "Back")
        {
            SaveLoadUI.SetActive(false);
            TitleUI.SetActive(true);
        }
        else if(mode == "GameStart")
        {
            if (PlayerPrefs.GetInt("SelectedNow") == 0)
            {
                StopCoroutine("fadeOut");
                Warning1.SetActive(true);
                Color color = image.color;
                color.a = 1f;
                image.color = color;
                StartCoroutine("fadeOut");
            }
            else
            {
                SceneManager.LoadScene("TestMain");
            }
        }
    }

    IEnumerator fadeOut()
    {
        Color color = image.color;
        yield return new WaitForSeconds(1f);
        for(float i = 0f; i <= 1; i += 0.01f)
        {
            color.a -= 0.01f;
            image.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        Warning1.SetActive(false);
        yield return null;
    }

    
}
