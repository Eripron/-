using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] bool isDuration;
    [SerializeField] float duration;
    [SerializeField] float rangeX;
    [SerializeField] float rangeY;
    [SerializeField] float magnitude;

    WaitForSeconds wait = new WaitForSeconds(0.01f);

    public void OnShake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        Vector3 origin = transform.localPosition;

        float x;
        float y;

        if (isDuration)
        {
            float elapsd = 0.0f;
            while (elapsd < duration)
            {
                x = Random.Range(-rangeX, rangeX) * magnitude;
                y = Random.Range(-rangeY, rangeY) * magnitude;

                transform.localPosition =
                    new Vector3(origin.x + x, origin.y + y, origin.z);

                elapsd += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            x = Random.Range(-rangeX, rangeX) * magnitude;
            y = Random.Range(-rangeY, rangeY) * magnitude;

            transform.localPosition =
                new Vector3(origin.x + x, origin.y + y, origin.z);
            yield return wait;
        }

        transform.localPosition = origin;
    }
}