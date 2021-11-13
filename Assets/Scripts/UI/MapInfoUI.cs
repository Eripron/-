using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapInfoUI : Singleton<MapInfoUI>
{
    [Header("Map Info")]
    [SerializeField] Text mapNameText;
    [SerializeField] Text mapLevelText;
    [SerializeField] Image mapImage;

    [Header("Player Info")]
    [SerializeField] PlayerInfoUI playerInfoPrefab;
    [SerializeField] Transform playerInfoUiParent;


    Dictionary<string, PlayerInfoUI> playerInfoUIs = new Dictionary<string, PlayerInfoUI>();


    new void Awake()
    {
        base.Awake();    
    }

    public void OnSetMapInfoUI(Map mapInfo)
    {
        Debug.Log("OnSetMapInfo");

        mapNameText.text = mapInfo.MapName;
        mapLevelText.text = mapInfo.MapLevel;
        mapImage.sprite = mapInfo.MapImage;

        PlayerStatus[] players = FindObjectsOfType<PlayerStatus>();
        foreach(PlayerStatus player in players)
        {
            if (player == null)
                continue;

            PlayerInfoUI playerInfo = Instantiate(playerInfoPrefab, playerInfoUiParent);
            playerInfo.OnSetPlayerInfoUI(player.Name, player.Hp, player.MaxHp);

            playerInfoUIs.Add(player.Name, playerInfo);
        }
    }

    public void OnSetEachPlayerHpGage(string _playerName, int hp, int maxHp)
    {
        Debug.Log("OnSetEach");

        if (playerInfoUIs.ContainsKey(_playerName))
        {
            float fill = (float)hp / maxHp;
            playerInfoUIs[_playerName].SetHpGage(fill);
        }
        else
        {
            Debug.Log("플레이어 정보 없음");
        }
    }

}
