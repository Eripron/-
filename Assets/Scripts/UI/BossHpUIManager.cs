using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpUIManager : Singleton<BossHpUIManager>
{
    enum HP_GAGE_COLOR
    {
        None,
        DarkRed,
        Red,
        DarkYellow,
        Yellow, 
        DarkGreen,
        Green,
    }

    [Header("Hp Gage Color")]
    [SerializeField] Color[] hpGageColors;

    [Header("Hp Gage Image")]
    [SerializeField] Image frontGageImage;
    [SerializeField] Image backGageImage;

    [Header("ETC")]
    [SerializeField] Text bossNameText;
    [SerializeField] Text hpCountText;
    [SerializeField] float smoothSpeed;

    EnemyStatus boss;

    // 보스 하나의 hp 게이지의 양 
    float oneBarHp;

    new void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }
    void Update()
    {

    }

    public void OnInit(EnemyStatus _boss)
    {
        OnSetActivation(true);

        boss = _boss;

        SetBossNameText(boss.Name);
        oneBarHp = boss.MaxHp / 10;

    }


    // boss hp 활성화 / 비활성화 
    void OnSetActivation(bool _activation)
    {
        this.gameObject.SetActive(_activation);
    }


    // set boss name
    void SetBossNameText(string bossName)
    {
        bossNameText.text = bossName;
    }

    public void SetBossHpGage(int _hp)
    {
        float count = _hp / oneBarHp;
        float remainHp = _hp % oneBarHp;
        float remainHpPercent;

        // set hp count text
        if(count < 1)
            hpCountText.text = string.Empty;
        else
            hpCountText.text = "x " + ((int)count).ToString();

        // set hp fillamount
        if (remainHp == 0f)
            remainHpPercent = 1f;
        else
            remainHpPercent = remainHp / oneBarHp;
        
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        frontGageImage.fillAmount = Mathf.Lerp(frontGageImage.fillAmount, remainHpPercent, Time.deltaTime * smoothSpeed);

        // set hp color
        if (count >=  7f)
        {
            frontGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.Green];
            backGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.DarkGreen];
        }
        else if(count > 6f)
        {
            frontGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.Green];
            backGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.DarkYellow];
        }
        else if(count >= 4f)
        {
            frontGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.Yellow];
            backGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.DarkYellow];
        }
        else if(count > 3f)
        {
            frontGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.Yellow];
            backGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.DarkRed];
        }
        else if(count >= 1f)
        {
            frontGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.Red];
            backGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.DarkRed];
        }
        else if(count > 0f)
        {
            frontGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.Red];
            backGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.None];
        }
        else if(count <= 0f)
        {
            frontGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.None];
            backGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.None];
        }
    }

}
