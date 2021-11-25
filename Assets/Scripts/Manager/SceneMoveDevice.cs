using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoveDevice : MonoBehaviour
{
    // ����Ǿ� �ִ� �� ���� 
    [SerializeField] SceneMover.SCENE connectedScene;


    public void OnMoveScene()
    {
        if(connectedScene != SceneMover.SCENE.None)
            SceneMover.Instance.OnMoveScene(connectedScene);
    }

    // button�� ����� scene������ runtime�� ���Ҷ� call
    public void OnSetScene(SceneMover.SCENE _scene)
    {
        connectedScene = _scene;
    }

}
