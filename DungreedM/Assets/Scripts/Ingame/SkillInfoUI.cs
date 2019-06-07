using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfoUI : MonoBehaviour
{

    public GameObject background;

    private void OnEnable()
    {
        background.SetActive(true);
    }

    private void OnDisable()
    {
        background.SetActive(false);
    }

    public void OnExit()
    {
        gameObject.SetActive(false);
    }

}
