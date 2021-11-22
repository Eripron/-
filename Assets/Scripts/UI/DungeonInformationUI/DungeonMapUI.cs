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

    string initMapName = "���� �غ�";


    new void Awake()
    {
        base.Awake();

        OnOffDungeonMapUI();
    }
    void InitUI()
    {
        // entire false �� �ʿ� x 
        OnSetRegionSelectWindow(true);
        OnSetStartButton(SceneMover.SCENE.None);
        SetMapNameText(initMapName);
        OnOpenCollectoins();

        detailWindow.OnOffDetailDungeonUI();
    }

    // �÷��̾ �ֺ��� �ִ³� �����Ŀ� ���� UI on / off
    public void OnOpenDungeonMapUI()
    {
        if (!entireWindow.activeSelf)
            entireWindow.SetActive(true);
    }
    public void OnOffDungeonMapUI()
    {
        // �ʱ�ȭ 
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

    // Map Button �̶� ���� ��
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
