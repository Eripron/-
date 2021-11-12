using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool<T>
{
    void Setup(System.Action<T> OnReturnPool);
    void OnReturnForce();
}

// ���� pool manager�� ���� ���� �ڷ����� singletone�� ���� �; �̷��� ǥ�� 
public class PoolManager<TargetObject, PoolObject> : Singleton<TargetObject>
    where TargetObject : MonoBehaviour
    where PoolObject : MonoBehaviour, IPool<PoolObject>
{
    /*
    Pool Manager�� ���� 
    - pool object�� �ڷ����� �������� ������ �ִ�.
    - start���� ������� ������Ʈ�� ����� ���´�. 
    - �ܺο��� �޶�� ��û�ϸ� ����Ҹ� ���� ��� �ִٸ� �ְ� ���ٸ� ���� �ش� 
    - �ܺο��� �� ����� ���� �Ŵ� �ٽ� ����ҿ� �����Ѵ�.
     */

    [SerializeField] PoolObject poolPrefab;
    [SerializeField] int initCount;

    Stack<PoolObject> storage;
    System.Action OnReturnAll;

    new void Awake()
    {
        base.Awake();

        storage = new Stack<PoolObject>();
        for(int i=0; i<initCount; i++)
            CreatePool();
    }

    private void CreatePool()
    {
        PoolObject pool = Instantiate(poolPrefab, transform);

        pool.Setup(OnReturnPool);
        pool.gameObject.SetActive(false);
        storage.Push(pool);

        OnReturnAll += pool.OnReturnForce;
    }

    protected PoolObject GetPool()
    {
        // �ܺο��� object ��û 
        if (storage.Count <= 0)
            CreatePool();

        storage.Peek().gameObject.SetActive(true);
        return storage.Pop();
    }
    protected void Clear()
    {
        OnReturnAll.Invoke();
    }


    private void OnReturnPool(PoolObject pool)
    {
        // ����ҿ� �������� ���� ����ÿ� ������ ������Ʈ���� �� �Լ����� ���ο� ������ �ؼ�
        // manager�� �ƴ� ������Ʈ�� �� �ϸ� �� �Լ��� �Ҹ���.
        if (storage.Contains(pool))
            return;

        pool.gameObject.SetActive(false);
        pool.transform.SetParent(transform);
        storage.Push(pool);
    }

}
