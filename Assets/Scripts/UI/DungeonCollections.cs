using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DungeonCollections : MonoBehaviour
{
    // ���� ��� �ʿ� ���ԵǾ� �ִ� �������� ��ư���� ���� 
    // �� �������� ���ԵǾ� �ִ� ���� �̸� 
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
