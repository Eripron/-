using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : Singleton<SceneMover>
{
    [SerializeField] DontDestroyManager DDM;
    FadeManager fadeManager;

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
    }

    void Start()
    {
        fadeManager = FindObjectOfType<FadeManager>();     
    }


    public void OnMoveScene(SCENE _scene)
    {
        if (_scene == SCENE.None || fadeManager == null)
            return;

        SoundManager.Instance.StopBGM();
        scene = _scene;
        fadeManager.FadeIn(true, MoveScene);
    }

    // Fade��� �߰��� �θ��� ���ؼ� 
    private void MoveScene()
    {

        SceneManager.LoadScene(scene.ToString());
        scene = SCENE.None;
    }

}
