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
        // �ƽ� �������̰� ��ŵ�� �ȵǾ����� skip�� �� ������ check�Ѵ�.
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
        // ���� �ƽ� �߸� ���� �����ִ� ��� ��Ż�� �ݴ´�.
        Portal[] portals = FindObjectsOfType<Portal>();
        foreach (Portal portal in portals)
            portal.SetPortal(true);
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

        foreach (var ob in offObjects)
            ob.SetActive(_state);

        foreach (var ob in onObjects)
            ob.SetActive(!_state);

        isSkip = true;
    }

}
