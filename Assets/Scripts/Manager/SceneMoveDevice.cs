using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoveDevice : MonoBehaviour
{
    // 연결되어 있는 씬 정보 
    [SerializeField] SceneMover.SCENE connectedScene;


    public void OnMoveScene()
    {
        if(connectedScene != SceneMover.SCENE.None)
            SceneMover.Instance.OnMoveScene(connectedScene);
    }

    // button에 연결된 scene정보가 runtime에 변할때 call
    public void OnSetScene(SceneMover.SCENE _scene)
    {
        connectedScene = _scene;
    }

}
