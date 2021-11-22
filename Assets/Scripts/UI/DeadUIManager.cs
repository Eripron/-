using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeadUIManager : PoolManager<DeadUIManager, HelpUI>
{
    [SerializeField] CanvasGroup deadUiWindow;
    [SerializeField] Transform parent;

    // �÷��̾� ��Ȱ ���� Ƚ�� �˷��ִ� ���� 
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

    // Ȱ��ȭ 
    public void SetDeadUI(bool activeState, int count = -1)
    {
        if(activeState)
        {
            if (count <= 0)
                count = 0;
            remainReviveCountText.text = string.Format("({0})ȸ ����", count);

            // help ui ���� �ؾ� �Ѵ�
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
