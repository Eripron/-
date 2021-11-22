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
    new SpriteRenderer renderer;

    // 이 포탈이 연결된 다음 포탈 
    [SerializeField] Portal destination;

     RegionInfo containedRegion;
     BoxCollider col;

    void Awake()
    {
        containedRegion = GetComponentInParent<RegionInfo>();
        renderer = GetComponentInChildren<SpriteRenderer>();
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
