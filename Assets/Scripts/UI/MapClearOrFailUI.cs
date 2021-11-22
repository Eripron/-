using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapClearOrFailUI : MonoBehaviour
{
    /*
     역할 - 맵 완료 or 실패인 경우 띄우는 ui controller
    
                성공          vs       실패 
    - 이미지    성공이미지             실패 이미지
    - 조건     맵 클리어           죽었을때 포기하기 버튼 or 더이상 소생 불가인 경우 


    - 버튼                   같음 
    - text            다르지만 아직 구현은 x
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

    [SerializeField] Button homeButton;
    [SerializeField] Button retryButton;


    void Start()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        canvasGroup.alpha = 0f;

        //homeButton.onClick.AddListener(Home);
        //retryButton.onClick.AddListener(Retry);
    }


    private void Home()
    {
    }

    private void Retry()
    {

    }


    public void OnMapClearOrFailUI(bool isClear)
    {
        uiWindow.gameObject.SetActive(true);

        clearImage.gameObject.SetActive(isClear);
        failImage.gameObject.SetActive(!isClear);

        foreach (GameObject ui in offUIWindows)
            ui.SetActive(false);

        StartCoroutine(AppearUI());
    }


    void ResetUI()
    {

    }


    // 1초동안 걸쳐서 알파값 1로 간다.
    IEnumerator AppearUI()
    {
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

