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


    public void OnDetailDungeonUI(DungeonButton dunInfo)
    {
        gameObject.SetActive(true);

        bossImage.sprite = dunInfo.BossSprite;
        goalImage.sprite = dunInfo.GoalSprite;

        dungeonNameText.text = dunInfo.DunName;
        goalText.text = dunInfo.Goal;
        stroyText.text = dunInfo.Story;
    }
}
