using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadUIManager : MonoBehaviour
{
    [SerializeField] Text remainReviveCountText;
    [SerializeField] CanvasGroup daedUiGroup;

    [SerializeField] GameObject helpUiPrefab;

    List<Vector3> deadPlayers = new List<Vector3>();

    public void AddDeadPlayer(Vector3 deadPlayerPos)
    {
        deadPlayers.Add(deadPlayerPos);
    }

    public void PopDeadPlayer(Vector3 deadPlayerPos)
    {
        deadPlayers.Remove(deadPlayerPos);
    }

    // Ȱ��ȭ 
    public void SetDeadUI(bool actication, int count = -1)
    {
        if(actication)
        {
            daedUiGroup.alpha = 1;
            remainReviveCountText.text = string.Format("({0})ȸ ����", count);

            // help ui ���� �ؾ� �Ѵ�

        }
        else
        {
            daedUiGroup.alpha = 0;
        }
    }

}
