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
    [SerializeField] GameObject bossHpWindow;
    [SerializeField] Text bossNameText;
    [SerializeField] Text hpCountText;
    [SerializeField] float lerpSpeed;


    EnemyStatus bossInfo;

    
    float maxHpAmountOfOneBar;             // 보스 체력바 하나의 최대 체력 양 
    float curHpAmountOfOneBar;          // 현재 보스 체력바의 체력 양 
    float receivedDamage = 0.0f;

    int remainHpBarCount;
    int colorChangeCount;

   


    new void Awake()
    {
        base.Awake();
        SetActivation(false);
    }

    void Update()
    {
        DrawHP();
    }

    // 체력바 초기화 
    public void OnInit(EnemyStatus _boss)
    {
        SetActivation(true);

        bossInfo = _boss;

        SetBossNameText(bossInfo.Name);

        maxHpAmountOfOneBar = bossInfo.MaxHp / 10;
        curHpAmountOfOneBar = maxHpAmountOfOneBar;

        colorChangeCount = 0;
        HpBarColorChange(colorChangeCount);

        remainHpBarCount = 10;
        SetRemainHpBarCountText(remainHpBarCount);
    }

    void DrawHP()
    {
        if (receivedDamage <= 0.0f)
            return;

        // 한 프레임당 깍아야 하는 양 
        float cutAmount = Time.deltaTime * lerpSpeed;

        curHpAmountOfOneBar -= cutAmount;
        receivedDamage -= cutAmount;

        if (curHpAmountOfOneBar < 0.0f)
        {
            curHpAmountOfOneBar = maxHpAmountOfOneBar + curHpAmountOfOneBar;
            frontGageImage.fillAmount = 1f;
            HpBarColorChange(++colorChangeCount);
            SetRemainHpBarCountText(--remainHpBarCount);

            if (remainHpBarCount <= 0)
            {
                SetActivation(false);
                StartCoroutine(TimeDelayCoroutine());
            }
        }

        frontGageImage.fillAmount = Mathf.Lerp(frontGageImage.fillAmount,
                                               curHpAmountOfOneBar / maxHpAmountOfOneBar,
                                               cutAmount);
    }

    void SetRemainHpBarCountText(int count)
    {
        if (count <= 1)
            hpCountText.text = string.Empty;
        else
            hpCountText.text = "x " + count;
    }
    void HpBarColorChange(int hpCount)
    {
        switch(hpCount)
        {
            case 0:
            case 1:
            case 2:
                frontGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.Green];
                backGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.DarkGreen];
                break;
            case 3:
                frontGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.Green];
                backGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.DarkYellow];
                break;
            case 4:
            case 5:
                frontGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.Yellow];
                backGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.DarkYellow];
                break;
            case 6:
                frontGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.Yellow];
                backGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.DarkRed];
                break;
            case 7:
            case 8:
                frontGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.Red];
                backGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.DarkRed];
                break;
            case 9:
                frontGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.Red];
                backGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.None];
                break;
            case 10:
                frontGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.None];
                backGageImage.color = hpGageColors[(int)HP_GAGE_COLOR.None];
                break;
        }
    }

   
    // set boss name
    void SetBossNameText(string bossName)
    {
        bossNameText.text = bossName;
    }
    // boss hp 활성화 / 비활성화 
    void SetActivation(bool _activation)
    {
        bossHpWindow.SetActive(_activation);
    }

    public void OnDamaged(float damage)
    {
        // 보스가 받은 데미지 양 
        receivedDamage += damage;
    }

    IEnumerator TimeDelayCoroutine()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
    }
}
