using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] Text playerNameText;
    [SerializeField] Image playerHpGageImage;


    public void OnSetPlayerInfoUI(string _playerName, int hp, int maxHp)
    {
        playerNameText.text = _playerName;
        playerHpGageImage.fillAmount = (float)hp / maxHp;
    }

    public void SetHpGage(float _fillAmount)
    {
        playerHpGageImage.fillAmount = _fillAmount;
    }
}
