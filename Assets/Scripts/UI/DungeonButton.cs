using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonButton : MonoBehaviour
{
    [SerializeField] DungeonDetail datailWindow;

    [SerializeField] Sprite bossSprite;
    [SerializeField] Sprite goalSprite;

    [SerializeField] string dungeonName;
    [SerializeField] string goal;
    [SerializeField] string story;


    public string DunName => dungeonName;
    public string Goal => goal;
    public string Story => story;

    public Sprite BossSprite => bossSprite;
    public Sprite GoalSprite => goalSprite;

    public void OnDetailWindow()
    {
        datailWindow.OnDetailDungeonUI(this);
        DungeonMapUI.Instance.OnOffDungeonSelectWin();
    }


}
