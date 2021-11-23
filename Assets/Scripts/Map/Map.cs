using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("MapInfo")]
    [SerializeField] Sprite mapSprite;
    [SerializeField] string mapDifficulty;
    [SerializeField] string mapName;

    [SerializeField] Transform startPoint;
    [SerializeField] RegionInfo[] allRegionHave;


    public string MapName => mapName;
    public string MapLevel => mapDifficulty;
    public Sprite MapImage => mapSprite;


    void Start()
    {
        Movement player = Movement.Instance;

        for(int i=0; i<allRegionHave.Length; i++)
        {
            allRegionHave[i].OnOpenRegion();

            if (i == 0)
                allRegionHave[i].OnSetMinimapCamera();
            else
                allRegionHave[i].OnCloseRegion();
        }

        if (player != null)
        {
            player.TeleportToPosition(startPoint.position, startPoint.rotation);
            player.IsTown = false;
        }

        // 문제 될수도 있다 start의 순서에 따라서 
        // MapUI set 보다 먼저 불려서 문제 였지만 null방지로 통해서 오류 해결 
        MapInfoUI.Instance.OnSetMapInfoUI(this);
    }

}
