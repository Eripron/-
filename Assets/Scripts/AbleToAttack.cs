using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbleToAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Attack Enemy");
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
                enemy.Damaged(10);
        }
    }
}
