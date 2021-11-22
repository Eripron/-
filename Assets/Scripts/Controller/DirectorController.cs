using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorController : MonoBehaviour
{
    /*
    eventCutScene에 들어오면 
     
    화면 페이딩 -> 캐릭터 ui 등 꺼야하는 ui를 끄고 캐릭터를 끈다. 켜야하는건 켜야한다.
    -> timeline 재생 -> 끝나면 캐릭터 다시 반전 
     */
    [SerializeField] GameObject[] offObjects;
    [SerializeField] GameObject[] onObjects;

    [SerializeField] PlayableDirector director;

    Camera objectToBind;
    string trackName = "CamTrack";

    bool isPlayed = false;
    bool isSkip = true;

    void Start()
    {
        objectToBind = Camera.main;

        foreach(var output in director.playableAsset.outputs)
        {
            if(output.streamName.Equals(trackName))
            {
                director.SetGenericBinding(output.sourceObject, objectToBind);
                break;
            }
        }
    }

    void Update()
    {
        // 컷신 실행중이고 스킵이 안되었으면 skip할 지 안할지 check한다.
        if (isPlayed && isSkip)
            return;

        if(Input.GetKeyDown(KeyCode.Escape) && !isSkip)
        {
            isSkip = true;
            FadeManager.Instance.FadeIn(false, SkipTimeline);
        }

    }

    public void OnSkipAble()
    {
        isSkip = false;
    }

    void SkipTimeline()
    {
        director.time = 909.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isPlayed)
        {
            isPlayed = true;

            if (director != null && other.gameObject.CompareTag("Player"))
            {
                FadeManager.Instance.FadeIn(false, SwitchObject);
                ColseAllPortal();
            }
        }
    }

    void ColseAllPortal()
    {
        // 보스 컷신 뜨면 현재 열려있는 모든 포탈을 닫는다.
        Portal[] portals = FindObjectsOfType<Portal>();
        foreach (Portal portal in portals)
            portal.SetPortal(true);
    }


    void SwitchObject()
    {
        // 꺼야하는 object의 현재 상태를 받아서 그 반대로 바꾼다.
        bool state = offObjects[0].activeSelf;

        foreach(var ob in offObjects)
            ob.SetActive(!state);

        foreach (var ob in onObjects)
            ob.SetActive(state);

        director.Play();

        StartCoroutine(SwitchObjectCoroutine(state));
    }

    IEnumerator SwitchObjectCoroutine(bool _state)
    {
        yield return new WaitUntil(() => director.state != PlayState.Playing);

        foreach (var ob in offObjects)
            ob.SetActive(_state);

        foreach (var ob in onObjects)
            ob.SetActive(!_state);

        isSkip = true;
    }

}
