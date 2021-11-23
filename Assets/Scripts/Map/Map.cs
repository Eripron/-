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

        // ���� �ɼ��� �ִ� start�� ������ ���� 
        // MapUI set ���� ���� �ҷ��� ���� ������ null������ ���ؼ� ���� �ذ� 
        MapInfoUI.Instance.OnSetMapInfoUI(this);
    }

}
