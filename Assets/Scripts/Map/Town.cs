using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;
    [SerializeField] int minimapCamSize;

    void Start()
    {
        Movement player = Movement.Instance;

        if (player != null && respawnPoint != null)
        {
            player.TeleportToPosition(respawnPoint.position, respawnPoint.rotation);
            player.IsTown = true;
        }

        FindObjectOfType<MinimapCameraContoller>().GetComponent<Camera>().orthographicSize = minimapCamSize;
    }
}
