using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : Singleton<EnemyController>
{
    /*
     �������� ���� �Ÿ��� ������� �÷��̾ ���� �̵� & ȸ��

    �����߿��� �̵� ���� -> activation(bool)�� ������ ���� 

     �Ÿ��� ����� ���� ���ݸ�� �߿��� ���� ���ݸ������ ���� 
     ��� �����ϰ� ������ ������ ���ΰ�?
     1. animation�� switch�� �ؼ� ���� ��ȣ�� ���� 
     2. �̸��� �迭�� ������ �־ ���� �迭���� animation�� �����Ѵ� 
     3. animation clip�� ������ �־ �����ϰ� animation clip�� �־ �����Ѵ�. 

    ���� ������ ������ ������ ���������� ���ϸ鼭 �ٽ� ���ƿ´�. 
    
     ? -> animation�� �迭�� ������ �־ ���� �Ҽ��ִ°�?

     */

    // ���߿� �˻��ؼ� ã�ƿ��� �ɷ� 

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

                Debug.Log("���� ����");
                Invoke("Attack", 1f);   
            }
        }
    }
    

    // tmp func
    void Attack()
    {
        Debug.Log("���� ��");
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
