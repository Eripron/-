using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonButton : MonoBehaviour
{
    [SerializeField] DungeonDetail detailWindow;
    [SerializeField] SceneMover.SCENE connectScene;

    // detail 창에 띄울 정보 
    [SerializeField] Sprite bossSprite;
    [SerializeField] Sprite goalSprite;

    string dungeonName;
    [SerializeField] string goal;
    [SerializeField] string story;


    public string DunName => dungeonName;
    public string Goal => goal;
    public string Story => story;

    public Sprite BossSprite => bossSprite;
    public Sprite GoalSprite => goalSprite;
    public SceneMover.SCENE ConnectScene => connectScene;

    void Start()
    {
        dungeonName = gameObject.name;     
    }

    public void OnClickDungeonButton()
    {
        detailWindow.OnDetailDungeonUI(this);
    }

}
