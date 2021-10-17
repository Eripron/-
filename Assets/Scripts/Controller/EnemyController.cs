using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : Singleton<EnemyController>
{
    /*
     생성되자 마자 거리에 상관없이 플레이어를 향해 이동 & 회전

    공격중에는 이동 금지 -> activation(bool)로 움직임 제한 

     거리가 가까워 지면 공격모션 중에서 랜덤 공격모션으로 실행 
     어떻게 랜덤하게 공격을 실핼할 것인가?
     1. animation을 switch로 해서 랜덤 번호로 실행 
     2. 이름을 배열로 가지고 있어서 랜덤 배열에서 animation을 실행한다 
     3. animation clip을 가지고 있어서 랜덤하게 animation clip을 넣어서 실행한다. 

    다음 데미지 입으면 색깔이 빨간색으로 변하면서 다시 돌아온다. 
    
     ? -> animation을 배열로 가지고 있어서 실행 할수있는가?

     */

    // 나중에 검색해서 찾아오는 걸로 

    [SerializeField] Transform target;

    MeshRenderer[] meshs;

    NavMeshAgent nav;
    Rigidbody rigid;

    bool activation = true;
    bool isDamaged = false;


    bool isWalk;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        rigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        Debug.Log($"activation : {activation}");
        if (activation)
        {
            nav.SetDestination(target.position);
            isWalk = true;

            if (distance <= nav.stoppingDistance)
            {
                activation = false;
                isWalk = false;

                Debug.Log("공격 시작");
                Invoke("Attack", 1f);   
            }
        }
    }
    

    // tmp func
    void Attack()
    {
        Debug.Log("공격 끝");
        activation = true;
    }


    public void Damaged()
    {
        Debug.Log("Damaged");

        activation = false;

        StopAllCoroutines();

        StartCoroutine(DamagedColorChange());
        StartCoroutine(DamagedKnockBack());
        activation = true;
    }


    IEnumerator DamagedColorChange()
    {
        List<Color> originColor = new List<Color>();

        foreach(MeshRenderer mesh in meshs)
        {
            originColor.Add(mesh.material.color);
            Color changeColor = Color.red;
            changeColor.a = 0.3f;
            mesh.material.color = changeColor;
        }

        yield return new WaitForSeconds(0.05f);

        for (int i = 0; i < meshs.Length; i++)
        {
            meshs[i].material.color = originColor[i];
        }
    }
    IEnumerator DamagedKnockBack(/*Vector3 knockBackDir*/)
    {
        Vector3 curPosition = transform.position;
        rigid.AddForce(-transform.forward * 7f, ForceMode.Impulse);
        yield return null;
    }

    void Dead()
    {
        // play dead animation 
    }

}
