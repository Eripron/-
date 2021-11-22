using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : Singleton<SceneMover>
{
    public enum SCENE
    {
        None = -1,

        Menu,
        Town,
        Main,

        Scene_Count,
    }

    // load�� scene�� ���� 
    SCENE scene = SCENE.None;

    new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }


    public void OnMoveScene(SCENE _scene)
    {
        if (_scene == SCENE.None)
            return;

        scene = _scene;
        FadeManager.Instance.FadeIn(true, MoveScene);
    }

    // Fade��� �߰��� �θ��� ���ؼ� 
    private void MoveScene()
    {
        SceneManager.LoadScene(scene.ToString());
        scene = SCENE.None;
    }

}
