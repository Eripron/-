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


    EnemyStatus boss;


    void Start()
    {
        // boss hp 정리를 어떻게 해야 할지 고민해보자 


            
    }

    public void OnInit(EnemyStatus _boss)
    {
        boss = _boss;

        SetBossNameText(boss.Name);
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


    void SetBossHpCount(int _count)
    {
        if (_count < 1 || _count > 10)
            return;

        string count = "";
        if(_count == 1)
        {
            count = string.Empty;
        }
        else
        {
            count = "x " + _count.ToString();
        }

        hpCountText.text = count;
    }


}
