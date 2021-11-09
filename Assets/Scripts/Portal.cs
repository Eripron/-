using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    enum PORTAL_COLOR
    {
        LockColor,
        UnlockColor,
    }

    [SerializeField] Color[] portalColor;
    [SerializeField] new SpriteRenderer renderer;

    // 이 포탈이 포함되어 있는 맵 object
    [SerializeField] GameObject containedRegion;

    // 이 포탈이 연결된 다음 포탈 
    [SerializeField] Portal destination;
     BoxCollider col;


    // 상태 체크 
    //bool isLock = false;

    void Start()
    {
        col = GetComponent<BoxCollider>();     
    }

    public void SetPortal(bool _isLock)
    {
        if(_isLock)
        {
            col.isTrigger = false;
            renderer.color = portalColor[(int)PORTAL_COLOR.LockColor];
        }
        else
        {
            // unlock
            col.isTrigger = true;
            renderer.color = portalColor[(int)PORTAL_COLOR.UnlockColor];
        }

    }

    public void OpenRegion()
    {
        containedRegion.SetActive(true);
    }
    public void CloseRegion()
    {
        containedRegion.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            FadeManager.Instance.FadeIn();
            Invoke("Teleport", 2f);
        }
    }

    void Teleport()
    {
        if (destination == null)
            return;

        destination.OpenRegion();
        Movement.Instance.TeleportToPosition(destination.transform.position, destination.transform.rotation);
        CloseRegion();
    }

}
