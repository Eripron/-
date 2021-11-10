using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // 가지고 있어야 하는 정보 
    // 던전 이름  
    // 던전 단계 
    // 던전 보스 sprite

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

    // init을 나중에 함수로  ㄱ 

}
