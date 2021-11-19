using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonMapUI : Singleton<DungeonMapUI>
{
    // ��ü �� ui object
    [Header("Entire")]
    [SerializeField] GameObject entireWindow;

    [Header("Dungeon Select")]
    [SerializeField] Text mapNameText;       // 
    [SerializeField] DungeonCollections[] dungeonCollections;


    [SerializeField] GameObject regionSelectWin;
    [SerializeField] GameObject detailWindow;


    string originMapName = "���� �غ�";


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


    // �÷��̾ �ֺ��� �ִ³� �����Ŀ� ���� UI on / off
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

    // collection win �� map name setting 
    public void SetMapNameText(string mapName)
    {
        mapNameText.text = mapName;
    }


    // �ܺ� ��û�� �޾Ƽ� �ش� �� ���� �����鿡 ���� �ִ� ��ư â�� ����. �������� ����.
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
