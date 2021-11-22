using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : Singleton<FadeManager>
{
    [SerializeField] GameObject loadingBar;
    [SerializeField] Slider loadingSlider;

    CanvasGroup CG;

    // 존재 이유? 모르겠음 왜 적었지 
    Movement player;
    CameraController mainCam;

    System.Action DelOnEvent;

    public bool isFading = false;

    new void Awake()
    {
        base.Awake();

        CG = GetComponent<CanvasGroup>();

        InitFadeUI();
    }

    public void FadeIn(bool isLoading = true, System.Action func = null)
    {
        if (isFading)
            return;

        isFading = true;

        if (func != null)
            DelOnEvent += func;

        // fade 중에는 플레이어 이동 불가로 만듬 
        //player.SetActive(false);

        StartCoroutine(FadeInCoroutine(isLoading, func));
    }

    IEnumerator FadeInCoroutine(bool isLoading, System.Action func)
    {
        float duration = 1f;
        float startTime = 0f;

        while ((startTime += Time.deltaTime) <= duration)
        {
            CG.alpha = startTime / duration;
            yield return null;
        }
        CG.alpha = 1f;

        // 함수 콜 
        DelOnEvent?.Invoke();

        if (isLoading)
        {
            loadingBar.SetActive(true);

            startTime = 0f;
            while ((startTime += Time.deltaTime) <= duration)
            {
                loadingSlider.value = startTime / duration;
                yield return null;
            }
            loadingSlider.value = 1f;
        }

        yield return new WaitForSeconds(1f);

        FadeOut(func);
    }


    public void FadeOut(System.Action func)
    {
        if (func != null)
            DelOnEvent -= func;

        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        InitFadeUI();
        isFading = false;

        //player.SetActive(true);

        yield return null;
    }
   

    void InitFadeUI()
    {
        loadingSlider.value = 0f;
        loadingBar.SetActive(false);

        CG.alpha = 0f;
    }

}
