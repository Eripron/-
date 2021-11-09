using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOffObject : MonoBehaviour
{
    ParticleSystem effect;

    void Start()
    {
        effect = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (effect.isPlaying == false)
            gameObject.SetActive(false);
    }


}
