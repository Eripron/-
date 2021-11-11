using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeadUIManager : MonoBehaviour
{
    [SerializeField] Text remainReviveCountText;
    [SerializeField] CanvasGroup daedUiGroup;

    // ������ ������ help ui ������ 
    [SerializeField] HelpUI helpUiPrefab;


    Transform target;


    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // Ȱ��ȭ 
    public void SetDeadUI(bool actication, int count = -1)
    {
        if(actication)
        {
            daedUiGroup.alpha = 1;
            remainReviveCountText.text = string.Format("({0})ȸ ����", count);

            // help ui ���� �ؾ� �Ѵ�
            HelpUI helpUi = Instantiate(helpUiPrefab, transform.position, transform.rotation);

            if (target != null)
            {
                helpUi.SetWorldPosition(target);

                // �ӽ� 
                // pool object �� ����ϸ� ���ϰ� �Ҽ����� ������ ?
                helpUi.transform.SetParent(this.transform);
            }

        }
        else
        {
            daedUiGroup.alpha = 0;
        }
    }

}
