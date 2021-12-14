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

    CanvasGroup canvasGroup;

    [Header("On/Off Objects")]
    [SerializeField] GameObject uiWindow;
    [SerializeField] GameObject[] offUIWindows;

    [Header("Image")]
    [SerializeField] GameObject clearImage;
    [SerializeField] GameObject failImage;

    //[SerializeField] GameObject[] offUis;


    // �ʿ��������
    [SerializeField] Button homeButton;
    [SerializeField] Button retryButton;


    void Start()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        canvasGroup.alpha = 0f;

        //homeButton.onClick.AddListener(Home);
        //retryButton.onClick.AddListener(Retry);
    }

    public void OnMapClearOrFailUI(bool isClear)
    {
        //SoundManager.Instance.StopBGM();

        uiWindow.gameObject.SetActive(true);

        clearImage.gameObject.SetActive(isClear);
        failImage.gameObject.SetActive(!isClear);

        foreach (GameObject ui in offUIWindows)
            ui.SetActive(false);

        StartCoroutine(AppearUI());
    }


    // 1�ʵ��� ���ļ� ���İ� 1�� ����.
    IEnumerator AppearUI()
    {
        canvasGroup.alpha = 0f;

        float start = 0f;
        float duration = 1f;

        while ((start += Time.deltaTime) <= duration)
        {
            canvasGroup.alpha = start / duration;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }
    

}

