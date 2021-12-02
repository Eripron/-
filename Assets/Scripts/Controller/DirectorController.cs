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
    eventCutScene�� ������ 
     
    ȭ�� ���̵� -> ĳ���� ui �� �����ϴ� ui�� ���� ĳ���͸� ����. �Ѿ��ϴ°� �Ѿ��Ѵ�.
    -> timeline ��� -> ������ ĳ���� �ٽ� ���� 
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
        // gameobject �� ���� ������ ���� ���.
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

    // �ƽ� ������� ������ ���� ��ŵ�ϴ°� ���������� ����
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
        Debug.Log("�ƽ� ���� Change");

        foreach (var ob in offObjects)
            ob.SetActive(_state);

        foreach (var ob in onObjects)
            ob.SetActive(!_state);

        isSkip = true;
        isPlayed = false;

        SoundManager.Instance.PlayBGM(BGM.BGM_BOSS.ToString(), true);
    }

}
