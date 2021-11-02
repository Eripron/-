using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool<T>
{
    void Setup(System.Action<T> OnReturnPool);      // �¾�.
    void OnReturnForce();                           // ������ �ǵ�����.
}

public class PoolManager<TargetObject, PoolObject> : Singleton<TargetObject>
    where TargetObject : MonoBehaviour
    where PoolObject : MonoBehaviour, IPool<PoolObject>  // ���׸� �ڷ��� PoolObject�� MonoBehaviour�� ����ϰ� �־�� �Ѵ�.
{
    [SerializeField] PoolObject poolPrefab;     // Ǯ���� ������Ʈ ������.
    [SerializeField] int initCount;             // �ʱ� ���� ����.

    Stack<PoolObject> storage;                  // �����.
    System.Action OnReturnAll;                  // ���� ����ҷ� ������ �̺�Ʈ.

    private new void Awake()
    {
        base.Awake();

        storage = new Stack<PoolObject>();      // stack ���� ��ü ����.
        for (int i = 0; i < initCount; i++)
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
        if (storage.Contains(pool))                             // �̹� ����ҿ� �ִ� Ǯ�� ���ϵ��� �ʴ´�.
            return;

        pool.gameObject.SetActive(false);                       // ���ƿ� pool�� ����.
        pool.transform.SetParent(transform);
        storage.Push(pool);                                     // ����ҿ� push�Ѵ�.
    }
}
