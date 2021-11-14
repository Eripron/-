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
