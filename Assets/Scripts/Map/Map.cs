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
    [SerializeField] Sprite mapSprite;
    [SerializeField] string mapDifficulty;
    [SerializeField] string mapName;


    [SerializeField] Transform startPoint;
    [SerializeField] RegionInfo[] allRegionHave;


    public string MapName => mapName;
    public string MapLevel => mapDifficulty;
    public Sprite MapImage => mapSprite;


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

        // ���� �ɼ��� �ִ� start�� ������ ���� 
        // MapUI set ���� ���� �ҷ��� ���� ������ null������ ���ؼ� ���� �ذ� 
        MapInfoUI.Instance.OnSetMapInfoUI(this);
    }

    // init�� ���߿� �Լ���  �� 

}
