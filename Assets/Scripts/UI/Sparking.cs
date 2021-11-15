using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparking : MonoBehaviour
{
    [SerializeField] GameObject sparkTarget;

    bool state;

    WaitForSeconds wait = new WaitForSeconds(0.8f);

    void Start()
    {
        state = sparkTarget.activeSelf;

        StartCoroutine(SparkCoroutine());
    }

    IEnumerator SparkCoroutine()
    {
        sparkTarget.SetActive(!state);
        state = !state;
        yield return wait;

        StartCoroutine(SparkCoroutine());
    }
}
