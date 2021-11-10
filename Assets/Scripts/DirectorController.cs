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

    PlayableDirector director;

    bool isContacted = false;

    void Start()
    {
        director = GetComponent<PlayableDirector>();     
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isContacted)
        {
            isContacted = true;

            if (director != null && other.gameObject.CompareTag("Player"))
            {
                FadeManager.Instance.FadeIn(false, SwitchObject);
            }
        }
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

        Debug.Log("call coroutine");    
        foreach (var ob in offObjects)
            ob.SetActive(_state);

        foreach (var ob in onObjects)
            ob.SetActive(!_state);
    }

}
