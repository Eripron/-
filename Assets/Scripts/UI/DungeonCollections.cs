using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DungeonCollections : MonoBehaviour
{
    // 역할 어떠한 맵에 포함되어 있는 던전들의 버튼들을 관리 
    // 이 던전들이 포함되어 있는 맵의 이름 
    [SerializeField] string containedMapName;
    public string ContainMapName => containedMapName;


    public void SetOnOff(bool active)
    {
        gameObject.SetActive(active);
    }


    /// <summary>
    /// 0 - normal
    /// 1 - highlight
    /// 2 - selected
    /// </summary>
    //[SerializeField] Color[] buttonColor;

    //[SerializeField] Button[] dungeonButton;

}
