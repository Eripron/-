using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    /*
     역할 
    - 트리거에 플레이어가 들어오면 적을 생성한다.(트리거에 들어오는 딱 한번)
    - 적을 생성하면 플레이어가 갈수있는 모든 포탈을 닫아 버린다. (전부)

    가지고 있어야 하는거 
    - 적 프리팹 
    - 적이 생성될 위치 

    - 연결되어 있는 포탈 
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

    // 생성된 적이 다 죽었는지 아닌지 판단하기 위해서 
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

    // monstart 죽으면 외부에서 요청 
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
