using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float magnitude;

    public void OnShake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        Vector3 origin = transform.localPosition;

        float elapsd = 0.0f;

        while (elapsd < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-0.8f, 0.8f) * magnitude;

            transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(x, y, origin.z), Time.deltaTime * 10f);

            elapsd += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = origin;
    }
}