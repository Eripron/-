using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAble : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player Damaged");
            Movement player = other.GetComponent<Movement>();

            if (player != null)
                player.Damaged(10);
        }
    }


}
