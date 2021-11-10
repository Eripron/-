using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // ������ �־�� �ϴ� ���� 
    // ���� �̸�  
    // ���� �ܰ� 
    // ���� ���� sprite

    [Header("MapInfo")]
    [SerializeField] string mapName;
    [SerializeField] string mapDifficulty;
    [SerializeField] Sprite mapSprite;

    [SerializeField] Transform startPoint;
    [SerializeField] RegionInfo[] allRegionHave;


    Movement player;


    void Start()
    {
        player = FindObjectOfType<Movement>();

        for(int i=0; i<allRegionHave.Length; i++)
        {
            allRegionHave[i].OnOpenRegion();

            if (i != 0)
                allRegionHave[i].OnCloseRegion();
        }

        if (player != null)
            player.TeleportToPosition(startPoint.position, startPoint.rotation);
    }

    // init�� ���߿� �Լ���  �� 

}
