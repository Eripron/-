using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] MapClearOrFailUI clearOrFailUI; 


    new void Awake()
    {
        base.Awake();
    }

    public void OnMapClear()
    {
        // boss ������ ȣ�� 
        clearOrFailUI.OnMapClearOrFailUI(true);
    }

    public void OnMapFail()
    {
        // - ��ư Ŭ���� ȣ���ϴ� �ɷθ� �ϱ�  
        clearOrFailUI.OnMapClearOrFailUI(false);
    }

    public void OnRetryMap()
    {
        // scene mover
        SceneManager.LoadScene("Main");
    }

    public void OnGoBackHome()
    {
        // scene mover
    }

}

