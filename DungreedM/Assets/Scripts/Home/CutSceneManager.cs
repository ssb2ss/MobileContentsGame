using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{

    public GameObject rightButton, leftButton, cutScene, startButton;
    public Sprite[] cutSceneSprite;

    private Image currentImage;
    private int currentCutScene;

    private void Start()
    {
        currentCutScene = 1;
        currentImage = cutScene.GetComponent<Image>();

        UpdateCutScene();
    }

    public void OnStart()
    {
        SceneManager.LoadScene("TestMain");
    }

    public void OnRightButton()
    {
        currentCutScene++;
        UpdateCutScene();
    }

    public void OnLeftButton()
    {
        currentCutScene--;
        UpdateCutScene();
    }

    private void UpdateCutScene()
    {
        currentImage.sprite = cutSceneSprite[currentCutScene - 1];

        if(currentCutScene == 1)
            leftButton.SetActive(false);
        else
            leftButton.SetActive(true);

        if (currentCutScene == 10)
        {
            rightButton.SetActive(false);
            startButton.SetActive(true);
        }
        else
        {
            rightButton.SetActive(true);
            startButton.SetActive(false);
        }
    }

}
