using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manual : MonoBehaviour
{
    private static bool isClose = false;


    [SerializeField] Button XButton;

    Movement player;

    void Start()
    {
        if(isClose == true && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

        if(gameObject.activeSelf)
        {
            player = Movement.Instance;
            if(player != null)
            {
                player.SetActive(false);
                XButton.onClick.AddListener(xButton);
            }
        }
    }


    private void xButton()
    {
        isClose = true;
        player.SetActive(true);
    }


}
