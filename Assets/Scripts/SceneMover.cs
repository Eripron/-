using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : Singleton<SceneMover>
{
    [SerializeField] DontDestroyManager DDM;

    public enum SCENE
    {
        None = -1,

        Menu,
        Town,
        Main,

        Scene_Count,
    }

    // load할 scene의 정보 
    SCENE scene = SCENE.None;

    new void Awake()
    {
        base.Awake();
    }


    public void OnMoveScene(SCENE _scene)
    {
        if (_scene == SCENE.None)
            return;

        scene = _scene;
        FadeManager.Instance.FadeIn(true, MoveScene);
    }

    // Fade경우 중간에 부르기 위해서 
    private void MoveScene()
    {
        SceneManager.LoadScene(scene.ToString());
        scene = SCENE.None;
    }

}
