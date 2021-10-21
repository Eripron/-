using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAble : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Movement player = other.GetComponent<Movement>();

        if (player == null)
            return;

        if (other.gameObject.tag == "Player")
        {
            player.Damaged(10);
        }
        else if (other.gameObject.tag == "Defence")
        {
            Debug.Log("��� ����");
            player.OnBlockAttack();
        }
    }

}
