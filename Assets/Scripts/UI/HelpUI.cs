using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpUI : PoolObject<HelpUI>
{
    Camera cam;

    float offset = 0.4f;
    float interval = 0.01f;

    Vector3 pos;
    Vector3 standard;

    bool isUp = true;
    bool isRunning = false;

    WaitForSeconds waitTime = new WaitForSeconds(0.02f);

    public void SetWorldPosition(Transform target)
    {
        cam = Camera.main;

        pos = new Vector3(target.position.x, target.position.y + 2f, target.position.z);
        standard = pos;

        transform.position = cam.WorldToScreenPoint(pos);
    }


    void Update()
    {
        if(!isRunning)
            StartCoroutine(UpNDownCoroutine());
    }


    IEnumerator UpNDownCoroutine()
    {
        isRunning = true;

        if(isUp)
        {
            while(pos.y < standard.y + offset)
            {
                pos.y += interval;
                transform.position = cam.WorldToScreenPoint(pos);

                yield return waitTime;
            }
        }
        else
        {
            while (pos.y > standard.y)
            {
                pos.y -= interval;
                transform.position = cam.WorldToScreenPoint(pos);

                yield return waitTime;
            }
        }

        isUp = !isUp;
        isRunning = false;
    }

    public void ResetUI()
    {
        isRunning = false;
    }
}
