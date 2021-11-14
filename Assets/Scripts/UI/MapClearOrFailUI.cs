using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapClearOrFailUI : MonoBehaviour
{
    /*
     ���� - �� �Ϸ� or ������ ��� ���� ui controller
    
                ����          vs       ���� 
    - �̹���    �����̹���             ���� �̹���
    - ����     �� Ŭ����           �׾����� �����ϱ� ��ư or ���̻� �һ� �Ұ��� ��� 


    - ��ư                   ���� 
    - text            �ٸ����� ���� ������ x
    - off ui           map info, skill ui
     */

    [SerializeField] GameObject clearImage;
    [SerializeField] GameObject failImage;

    [SerializeField] Button homeButton;
    [SerializeField] Button retryButton;


    void Start()
    {
        homeButton.onClick.AddListener(Home);
        retryButton.onClick.AddListener(Retry);
    }


    private void Home()
    {
    }
    private void Retry()
    {
    }


}
