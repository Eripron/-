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


    // [SerializeField] 나중에 지우면 됩니다 ---------------------------
    // 이 포탈이 포함되어 있는 맵 object
     RegionInfo containedRegion;

    // 이 포탈이 연결된 다음 포탈 
     [SerializeField] Portal destination;
     BoxCollider col;


    // 상태 체크 
    //bool isLock = false;

    void Awake()
    {
        col = GetComponent<BoxCollider>();
        containedRegion = GetComponentInParent<RegionInfo>();
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
        containedRegion.OnOpenRegion();
    }
    public void CloseRegion()
    {
        containedRegion.OnCloseRegion();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            FadeManager.Instance.FadeIn(true, Teleport);
        }
    }


    void Teleport()
    {
        if (destination == null)
            return;

        destination.OpenRegion();
        destination.containedRegion.OnSetMinimapCamera();
        Movement.Instance.TeleportToPosition(destination.transform.position, destination.transform.rotation);
        CloseRegion();
    }


}
