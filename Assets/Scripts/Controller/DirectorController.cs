using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

interface IDirector
{
    void SetActive(bool _active);
}

public class DirectorController : MonoBehaviour
{
    /*
    eventCutScene에 들어오면 
     
    화면 페이딩 -> 캐릭터 ui 등 꺼야하는 ui를 끄고 캐릭터를 끈다. 켜야하는건 켜야한다.
    -> timeline 재생 -> 끝나면 캐릭터 다시 반전 
     */
    [SerializeField] List<GameObject> offObjects;
    [SerializeField] List<GameObject> onObjects;

    [SerializeField] PlayableDirector director;

    int count = 0;
    bool isPlayed = false;
    bool isSkip = true;

    void Start()
    {
        offObjects.Add(QuickSlotUIManager.Instance.gameObject);

        //SetCameraToTrack();
        //offObjects.Add(Movement.Instance.gameObject);
    }

    /*private void SetCameraToTrack()
    {
        // gameobject 로 넣지 않으면 오류 뜬다.
        GameObject objectToBind = Camera.main.gameObject;
        string trackName = "CamTrack";

        foreach (var output in director.playableAsset.outputs)
        {
            if (output.streamName.Equals(trackName))
            {
                director.SetGenericBinding(output.sourceObject, objectToBind);
                break;
            }
        }
    }*/

    void Update()
    {
        if (!isPlayed)
            return;

        if (Input.GetKeyDown(KeyCode.Escape) && !isSkip)
        {
            Debug.Log("ESC");
            isSkip = true;
            FadeManager.Instance.FadeIn(false, SkipTimeline);
        }
    }

    // 컷신 어느정도 나오기 전에 스킵하는걸 방지용으로 만듬
    public void OnSkipAble()
    {
        isSkip = false;
    }
    void SkipTimeline()
    {
        Debug.Log("Skip");
        director.time = 909.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && count <= 0)
        {
            count++;
            isPlayed = true;
            other.GetComponent<Movement>().PlayerStateReset();
            if (director != null)
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
        Debug.Log("컷신 종료 Change");

        foreach (var ob in offObjects)
            ob.SetActive(_state);

        foreach (var ob in onObjects)
            ob.SetActive(!_state);

        isSkip = true;
        isPlayed = false;

        SoundManager.Instance.PlayBGM(BGM.BGM_BOSS.ToString(), true);
    }

}
