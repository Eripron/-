using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : Singleton<FadeManager>
{
    Movement player;
    [SerializeField] CanvasGroup CG;

    [SerializeField] GameObject loadingBar;
    [SerializeField] Slider loadingSlider;

    //
    CameraController mainCam;

    public bool isFading = false;

    new void Awake()
    {
        base.Awake();

        player = FindObjectOfType<Movement>();
        ResetFade();
    }

    [ContextMenu("Fade")]
    public void FadeIn(bool isLoading = true)
    {
        // ���̵� ���̶�� ���� x
        if (isFading)
            return;

        isFading = true;

        // fade �߿��� �÷��̾� �̵� �Ұ��� ���� 
        player.SetActive(false);

        StartCoroutine(FadeInCoroutine(isLoading));
    }

    IEnumerator FadeInCoroutine(bool isLoading)
    {
        float duration = 2f;
        float startTime = 0f;

        while ((startTime += Time.deltaTime) <= duration)
        {
            CG.alpha = startTime / duration;
            yield return null;
        }
        CG.alpha = 1f;

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
        isFading = false;

        FadeOut();
    }


    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        yield return new WaitUntil(() => isFading == false);

        player.SetActive(true);
        ResetFade();
    }
   

    void ResetFade()
    {
        CG.alpha = 0f;
        loadingBar.SetActive(false);
        loadingSlider.value = 0f;
    }


}
