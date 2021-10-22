using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttackAble : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{name} attack to {other.name}");

        if(other.gameObject.tag == "Enemy")
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
                enemy.Damaged(10);
        }

    }
}
