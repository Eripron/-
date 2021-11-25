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
        mapLevelText.text = mapInfo.MapLevel;
        mapNameText.text = mapInfo.MapName;
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


    public void OnChangePlayerHpGage(string _playerName, int hp, int maxHp)
    {
        if (playerInfoUIs.ContainsKey(_playerName))
        {
            float fill = (float)hp / maxHp;
            if (fill <= 0)
                fill = 0;

            playerInfoUIs[_playerName].SetHpGage(fill);
        }
    }

}
