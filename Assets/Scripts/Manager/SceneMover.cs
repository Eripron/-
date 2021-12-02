using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : Singleton<SceneMover>
{
    [SerializeField] DontDestroyManager DDM;
    [SerializeField] FadeManager fadeManager;

    // scene ��ȯ�� ui â���� ���� 
    System.Action DelCloseUIWindow;

    string curScene;
    SCENE enumCurScene;

    public void AddCloseWindowFun(System.Action fun)
    {
        // �����ϴ� â���� �Լ��� �޾ƿ´�.
        DelCloseUIWindow += fun;
    }

    public enum SCENE
    {
        None = -1,

        Start,
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

        curScene = SceneManager.GetActiveScene().name;
    }


    public void OnMoveScene(SCENE _scene)
    {
        // scene ������ ���� ��� 
        if (_scene == SCENE.None || fadeManager == null)
            return;

        if (_scene == SCENE.Menu && curScene == _scene.ToString())
            return;

        curScene = _scene.ToString();
        enumCurScene = _scene;

        //SoundManager.Instance.StopBGM();
        scene = _scene;
        fadeManager.FadeIn(true, MoveScene);
    }

    // Fade��� �߰��� �θ��� ���ؼ� 
    private void MoveScene()
    {
        DelCloseUIWindow?.Invoke();
        SceneManager.LoadScene(scene.ToString());
        scene = SCENE.None;
    }

    public string CurSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public SCENE CurSceneEnum()
    {
        return enumCurScene;
    }
}
