using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    /*
     have info

    - string mapName
    - �����ؾ��ϴ� dungeon���� ��ư â on 
    - �������� ���� �����Ѵ�.
     */
    [SerializeField] string mapName;

    
    public void OnOpenDungeonCollection()
    {
        DungeonMapUI.Instance.OnOpenCollectoins(mapName);
    }

}
