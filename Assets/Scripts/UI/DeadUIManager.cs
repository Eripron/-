using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeadUIManager : MonoBehaviour
{
    [SerializeField] Text remainReviveCountText;
    [SerializeField] CanvasGroup daedUiGroup;

    // 죽으면 생성할 help ui 프리팹 
    [SerializeField] HelpUI helpUiPrefab;


    Transform target;


    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // 활성화 
    public void SetDeadUI(bool actication, int count = -1)
    {
        if(actication)
        {
            daedUiGroup.alpha = 1;
            remainReviveCountText.text = string.Format("({0})회 가능", count);

            // help ui 생성 해야 한다
            HelpUI helpUi = Instantiate(helpUiPrefab, transform.position, transform.rotation);

            if (target != null)
            {
                helpUi.SetWorldPosition(target);

                // 임시 
                // pool object 를 사용하면 편하게 할수있지 않을까 ?
                helpUi.transform.SetParent(this.transform);
            }

        }
        else
        {
            daedUiGroup.alpha = 0;
        }
    }

}
