using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonMapUI : Singleton<DungeonMapUI>
{
    [Header("Window")]
    [SerializeField] GameObject entireWindow;
    [SerializeField] GameObject regionSelectWin;
    [SerializeField] DungeonDetail detailWindow;

    [Header("Dungeon Show Info")]
    [SerializeField] Text mapNameText;       
    [SerializeField] DungeonCollections[] dungeonCollections;
     
    [Header("Button")]
    [SerializeField] Button startButton;

    string initMapName = "전투 준비";


    new void Awake()
    {
        base.Awake();

        OnOffDungeonMapUI();
    }
    void InitUI()
    {
        // entire false 할 필요 x 
        OnSetRegionSelectWindow(true);
        OnSetStartButton(SceneMover.SCENE.None);
        SetMapNameText(initMapName);
        OnOpenCollectoins();

        detailWindow.OnOffDetailDungeonUI();
    }

    // 플레이어가 주변에 있는냐 없느냐에 따라 UI on / off
    public void OnOpenDungeonMapUI()
    {
        if (!entireWindow.activeSelf)
            entireWindow.SetActive(true);
    }
    public void OnOffDungeonMapUI()
    {
        // 초기화 
        InitUI();

        if (entireWindow.activeSelf)
            entireWindow.SetActive(false);
    }

    public void OnSetRegionSelectWindow(bool active)
    {
        regionSelectWin.SetActive(active);
    }

    public void OnSetStartButton(SceneMover.SCENE _sceneInfo)
    {
        if (_sceneInfo == SceneMover.SCENE.None)
            startButton.interactable = false;
        else
            startButton.interactable = true;

        startButton.GetComponent<SceneMoveDevice>().OnSetScene(_sceneInfo);
    }
    // -------------------------------------------------------------------------------------

    // show map button
    public void OnClickShowMapButton()
    {
        if (detailWindow.gameObject.activeSelf)
            detailWindow.OnOffDetailDungeonUI();

        regionSelectWin.SetActive(!regionSelectWin.activeSelf);
    }

    // Map Button 이랑 연결 됨
    public void OnOpenCollectoins(string mapName = "reset")
    {
        foreach(DungeonCollections collection in dungeonCollections)
        {
            if (collection.ContainMapName.Equals(mapName) && mapName != "reset")
            {
                collection.OnSetActive(true);
                SetMapNameText(mapName);
            }
            else
                collection.OnSetActive(false);
        }
    }
    public void SetMapNameText(string mapName)
    {
        mapNameText.text = mapName;
    }
    // ----------------------------------------------------------------------------------
}
