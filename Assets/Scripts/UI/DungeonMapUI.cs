using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonMapUI : Singleton<DungeonMapUI>
{
    // 전체 맵 ui object
    [Header("Entire")]
    [SerializeField] GameObject entireWindow;

    [Header("Dungeon Select")]
    [SerializeField] Text mapNameText;       // 
    [SerializeField] DungeonCollections[] dungeonCollections;


    [SerializeField] GameObject regionSelectWin;
    [SerializeField] GameObject detailWindow;


    string originMapName = "전투 준비";


    new void Awake()
    {
        base.Awake();

        ResetDungeonMapUI();
    }




    public void OnDungeonSelectWin()
    {
        if (detailWindow.activeSelf)
            detailWindow.SetActive(false);

        bool curState = regionSelectWin.activeSelf;
        regionSelectWin.SetActive(!curState);
    }
    
    public void OnOffDungeonSelectWin()
    {
        if (regionSelectWin.activeSelf)
            regionSelectWin.SetActive(false);
    }


    [ContextMenu("reset")]
    private void ResetDungeonMapUI()
    {
        SetMapNameText(originMapName);
        OnOpenCollectoins();
    }


    // 플레이어가 주변에 있는냐 없느냐에 따라 UI on / off
    public void OnOpenDungeonMapUI()
    {
        if (!entireWindow.activeSelf)
            entireWindow.SetActive(true);
    }
    public void OnOffDungeonMapUI()
    {
        if (entireWindow.activeSelf)
            entireWindow.SetActive(false);
    }

    // collection win 의 map name setting 
    public void SetMapNameText(string mapName)
    {
        mapNameText.text = mapName;
    }


    // 외부 요청을 받아서 해당 맵 관연 던전들에 들어갈수 있는 버튼 창을 연다. 나머지는 끈다.
    public void OnOpenCollectoins(string mapName = "reset")
    {
        foreach(DungeonCollections collection in dungeonCollections)
        {
            if (collection.ContainMapName.Equals(mapName) && mapName != "reset")
            {
                collection.SetOnOff(true);
                SetMapNameText(mapName);
            }
            else
                collection.SetOnOff(false);
        }
    }


}
