using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;

    Movement player;

    void Start()
    {
        player = Movement.Instance;

        if (player != null && respawnPoint != null)
            player.TeleportToPosition(respawnPoint.position, respawnPoint.rotation);
    }
}
