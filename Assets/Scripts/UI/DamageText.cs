using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    Camera cam;

    [SerializeField] float speed;
    [SerializeField] float time;

    Transform position;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnInit(Transform position)
    {
        cam = Camera.main;


    }
}
