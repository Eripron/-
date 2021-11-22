using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    [SerializeField] string mapName;

    public void OnOpenDungeonCollection()
    {
        DungeonMapUI.Instance.OnOpenCollectoins(mapName);
    }

}
