using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTitle : MonoBehaviour
{

    public GameObject sky1, sky2, sky3;
    public GameObject ground1, ground2, ground3;
    public GameObject player;
    public Sprite[] playerAnim;

    public GameObject saveloadUI;

    private Image playerImage;

    void Start()
    {
        playerImage = player.GetComponent<Image>();
        StartCoroutine("playerAnimation");
    }
    
    void FixedUpdate()
    {
        sky1.transform.Translate(new Vector3(-1f, 0, 0));
        sky2.transform.Translate(new Vector3(-1f, 0, 0));
        sky3.transform.Translate(new Vector3(-1f, 0, 0));

        if (sky1.transform.position.x <= -1920)
            sky1.transform.position = new Vector3(3840, 540, 0);
        if (sky2.transform.position.x <= -1920)
            sky2.transform.position = new Vector3(3840, 540, 0);
        if (sky3.transform.position.x <= -1920)
            sky3.transform.position = new Vector3(3840, 540, 0);

        ground1.transform.Translate(new Vector3(-10f, 0, 0));
        ground2.transform.Translate(new Vector3(-10f, 0, 0));
        ground3.transform.Translate(new Vector3(-10f, 0, 0));

        if (ground1.transform.position.x <= -2000)
            ground1.transform.position = new Vector3(4000, 540, 0);
        if (ground2.transform.position.x <= -2000)
            ground2.transform.position = new Vector3(4000, 540, 0);
        if (ground3.transform.position.x <= -2000)
            ground3.transform.position = new Vector3(4000, 540, 0);

    }

    IEnumerator playerAnimation()
    {
        int idx = 0;
        while (true)
        {
            idx++;
            if(idx == 6)
            {
                idx = 0;
            }
            playerImage.sprite = playerAnim[idx];
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void OnClickStart()
    {
        saveloadUI.SetActive(true);
    }

    public void OnClickOption()
    {
        //미구현
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

}
