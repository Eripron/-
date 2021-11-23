using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;

    Movement player;

    void Start()
    {
        Debug.Log("Town Start");

        Movement player = FindObjectOfType<Movement>();
        //Debug.Log($"player ¼ö : {player.Length}");

        if (player == null)
            Debug.Log("player ¾øÀ½ in town");
        else
            Debug.Log(player.Num);

        if (player != null && respawnPoint != null)
        {
            Debug.Log("set player position in town");
            player.TeleportToPosition(respawnPoint.position, respawnPoint.rotation);
            player.IsTown = true;
        }
    }
}
