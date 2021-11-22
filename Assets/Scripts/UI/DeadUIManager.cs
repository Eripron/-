using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeadUIManager : PoolManager<DeadUIManager, HelpUI>
{
    [SerializeField] CanvasGroup deadUiWindow;
    [SerializeField] Transform parent;

    // 플레이어 부활 가능 횟수 알려주는 정보 
    [SerializeField] Text remainReviveCountText;

    Transform target;   // world position target

    void Start()
    {
        deadUiWindow.alpha = 0f;     
    }

    public void OnSetTarget(Transform deadPlayer)
    {
        target = deadPlayer;
    }

    // 활성화 
    public void SetDeadUI(bool activeState, int count = -1)
    {
        if(activeState)
        {
            if (count <= 0)
                count = 0;
            remainReviveCountText.text = string.Format("({0})회 가능", count);

            // help ui 생성 해야 한다
            HelpUI helpUi = GetPool();
            helpUi.ResetUI();

            if (target != null)
                helpUi.SetWorldPosition(target);

            deadUiWindow.alpha = 1;
        }
        else
        {
            Clear();
            deadUiWindow.alpha = 0;
        }
    }

}
