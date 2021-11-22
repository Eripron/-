using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    /*
     ���� 
    - Ʈ���ſ� �÷��̾ ������ ���� �����Ѵ�.(Ʈ���ſ� ������ �� �ѹ�)
    - ���� �����ϸ� �÷��̾ �����ִ� ��� ��Ż�� �ݾ� ������. (����)

    ������ �־�� �ϴ°� 
    - �� ������ 
    - ���� ������ ��ġ 

    - ����Ǿ� �ִ� ��Ż 
     */

    WaitForSeconds time = new WaitForSeconds(1f);

    // this transform
    Transform _transform;

    [Header("Enemy Create")]
    [SerializeField] GameObject enemyPrefabs;
    [SerializeField] Transform[] createPos;
    [SerializeField] ParticleSystem appearEffect;

    [Header("Portal State")]
    [SerializeField] Portal[] portals;

    // ������ ���� �� �׾����� �ƴ��� �Ǵ��ϱ� ���ؼ� 
    List<NormalMonsterController> enemys = new List<NormalMonsterController>();

    int count = 0;
    bool isUpdate = false;

    void Start()
    {
        _transform = this.transform;
    }

    void Update()
    {
        if (!isUpdate)
            return;

        if(enemys.Count <= 0)
        {
            SetAllPortalInRegion(false);
            isUpdate = false;
        }

    }

    void SetAllPortalInRegion(bool lockState)
    {
        foreach(Portal portal in portals)
        {
            portal.SetPortal(lockState);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            count++;
            if (count == 1)
            {
                SetAllPortalInRegion(true);

                for (int i = 0; i < createPos.Length; i++)
                    StartCoroutine(CreateMoster(createPos[i].position, createPos[i].rotation));
            }

        }
    }

    // monstart ������ �ܺο��� ��û 
    public void OnChangeEnemyCount(NormalMonsterController enemy)
    {
        if(enemys.Contains(enemy))
        {
            enemys.Remove(enemy);
        }
    }

    IEnumerator CreateMoster(Vector3 position, Quaternion rotation)
    {
        ParticleSystem effect = Instantiate(appearEffect, position, rotation);
        effect.transform.SetParent(_transform);

        yield return time;

        GameObject enemy = Instantiate(enemyPrefabs, position, rotation);
        enemy.transform.SetParent(_transform);
        enemys.Add(enemy.GetComponent<NormalMonsterController>());

        if (!isUpdate)
            isUpdate = true;
    }

}
