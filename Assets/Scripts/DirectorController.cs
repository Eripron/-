using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorController : MonoBehaviour
{
    PlayableDirector director;

    bool isContacted = false;

    void Start()
    {
        director = GetComponent<PlayableDirector>();     
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isContacted)
        {
            isContacted = true;

            if(director != null)
            {
                director.Play();
            }
        }
    }

}
