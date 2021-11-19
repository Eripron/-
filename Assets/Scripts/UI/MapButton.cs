using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    /*
     have info

    - string mapName
    - 오픈해야하는 dungeon지역 버튼 창 on 
    - 나머지는 전부 꺼야한다.
     */
    [SerializeField] string mapName;

    
    public void OnOpenDungeonCollection()
    {
        DungeonMapUI.Instance.OnOpenCollectoins(mapName);
    }

}
