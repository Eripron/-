using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonDetail : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] Text dungeonNameText;
    [SerializeField] Text goalText;
    [SerializeField] Text stroyText;

    [Header("Image")]
    [SerializeField] Image goalImage;
    [SerializeField] Image bossImage;

    DungeonMapUI dungeonMapUi;

    public void OnDetailDungeonUI(DungeonButton dunInfo)
    {
        if(dungeonMapUi != null)
        {
            dungeonMapUi.OnSetRegionSelectWindow(false);
            dungeonMapUi.OnSetStartButton(dunInfo.ConnectScene);
        }

        gameObject.SetActive(true);

        bossImage.sprite = dunInfo.BossSprite;
        goalImage.sprite = dunInfo.GoalSprite;

        dungeonNameText.text = dunInfo.DunName;
        goalText.text = dunInfo.Goal;
        stroyText.text = dunInfo.Story;
    }

    // detail Ã¢À» ²ø¶§ call 
    public void OnOffDetailDungeonUI()
    {
        if (dungeonMapUi == null)
            dungeonMapUi = DungeonMapUI.Instance;

        dungeonMapUi.OnSetStartButton(SceneMover.SCENE.None);
        gameObject.SetActive(false);
    }

}
