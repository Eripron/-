using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeadUIManager : PoolManager<DeadUIManager, HelpUI>
{
    [SerializeField] Text remainReviveCountText;
    [SerializeField] CanvasGroup daedUiGroup;

    [SerializeField] Transform parent;
    Transform target;   // world position target

    public void OnSetTarget(Transform deadPlayer)
    {
        target = deadPlayer;
    }

    // 활성화 
    public void SetDeadUI(bool actication, int count = -1)
    {
        if(actication)
        {
            daedUiGroup.alpha = 1;
            remainReviveCountText.text = string.Format("({0})회 가능", count);

            // help ui 생성 해야 한다
            HelpUI helpUi = GetPool();
            helpUi.ResetUI();

            if (target != null)
                helpUi.SetWorldPosition(target);
        }
        else
        {
            Clear();
            daedUiGroup.alpha = 0;
        }
    }

}
