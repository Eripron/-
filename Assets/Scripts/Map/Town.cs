using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;
    [SerializeField] int minimapCamSize;
    [SerializeField] BGM townBgm;

    void Start()
    {
        Movement player = Movement.Instance;

        if (player != null && respawnPoint != null)
        {
            player.TeleportToPosition(respawnPoint.position, respawnPoint.rotation);
            player.IsTown = true;
        }

        FindObjectOfType<MinimapCameraContoller>().GetComponent<Camera>().orthographicSize = minimapCamSize;

        SoundManager.Instance.PlayBGM(townBgm.ToString());
    }
}
