using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorController : MonoBehaviour
{
    /*
    eventCutScene�� ������ 
     
    ȭ�� ���̵� -> ĳ���� ui �� �����ϴ� ui�� ���� ĳ���͸� ����. �Ѿ��ϴ°� �Ѿ��Ѵ�.
    -> timeline ��� -> ������ ĳ���� �ٽ� ���� 
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
        // �����ϴ� object�� ���� ���¸� �޾Ƽ� �� �ݴ�� �ٲ۴�.
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
