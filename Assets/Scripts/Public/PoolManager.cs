using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool<T>
{
    void Setup(System.Action<T> OnReturnPool);
    void OnReturnForce();
}

// 내가 pool manager로 쓰고 싶은 자료형은 singletone도 쓰고 싶어서 이렇게 표현 
public class PoolManager<TargetObject, PoolObject> : Singleton<TargetObject>
    where TargetObject : MonoBehaviour
    where PoolObject : MonoBehaviour, IPool<PoolObject>
{
    /*
    Pool Manager의 역할 
    - pool object의 자료형의 프리팹을 가지고 있다.
    - start에서 어느정도 오브잭트를 만들어 놓는다. 
    - 외부에서 달라고 요청하면 저장소를 보고 재고가 있다면 주고 없다면 만들어서 준다 
    - 외부에서 다 사용이 끝난 거는 다시 저장소에 보관한다.
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
        // 외부에서 object 요청 
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
        // 저장소에 프리팹을 만들어서 저장시에 각각의 오브젝트들은 이 함수들을 내부에 가지게 해서
        // manager가 아닌 오브젝트가 콜 하면 이 함수가 불린다.
        if (storage.Contains(pool))
            return;

        pool.gameObject.SetActive(false);
        pool.transform.SetParent(transform);
        storage.Push(pool);
    }

}
