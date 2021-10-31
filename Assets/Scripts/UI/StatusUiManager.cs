using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUiManager : Singleton<StatusUiManager>
{
    [Header("Hp UI")]
    [SerializeField] Text hpText;
    [SerializeField] Text maxHpText;
    [SerializeField] RectTransform hpWindow;
    [SerializeField] RectTransform hpTextWindow;
    [SerializeField] Image hpGageImage;

    [Header("Stamina UI")]
    [SerializeField] Text staminaText;
    [SerializeField] Text maxStaminaText;
    [SerializeField] RectTransform staminaWindow;
    [SerializeField] RectTransform staminaTextWindow;
    [SerializeField] Image staminaGageImage;

    [Header("SP UI")]
    [SerializeField] Text spText;
    [SerializeField] Text maxSpText;
    [SerializeField] Image spGageImage;


    // hp ui 
    float hpWinLength;
    float staminaLength;


    new void Awake()
    {
        base.Awake();

        // hp text window width 
        hpWinLength = hpWindow.sizeDelta.x;
        staminaLength = staminaWindow.sizeDelta.x;

    }

    // hp, stamina는 gage에 따라 text 위치가 유동적이다.
    public void SetHpUI(int hp, int maxHp)
    {
        // hp text setting  
        hpText.text = hp.ToString();
        maxHpText.text = maxHp.ToString();

        // set hp gage image
        float hpPercent = (float)hp / maxHp;
        hpGageImage.fillAmount = hpPercent;

        // set hp text position 
        float posX = hpPercent * 600 + 2;
        hpTextWindow.anchoredPosition = new Vector2(posX, 0f);
    }
    public void SetStaminaUI(int stamina, int maxStamina)
    {
        staminaText.text = stamina.ToString();
        maxStaminaText.text = maxStamina.ToString();

        float staminaPercent = (float)stamina / maxStamina;
        staminaGageImage.fillAmount = staminaPercent;

        float posX = staminaPercent * 600 + 2;
        staminaTextWindow.anchoredPosition = new Vector2(posX, 0f);
    }
    // SP는 gage에 상관없이 text 위치가 고정이다.
    public void SetSpUI(int sp, int maxSp)
    {
        spText.text = sp.ToString();
        maxSpText.text = maxSp.ToString();

        float spPercent = (float)sp / maxSp;
        spGageImage.fillAmount = spPercent;
    }

}

