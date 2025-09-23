using UnityEngine;
using UnityEngine.Pool;

public class CarrotPool : MonoBehaviour
{
    [Header("Pool Setup")]
    [SerializeField] private Carrot carrotPrefab;
    [SerializeField] private int defaultCapacity = 32;
    [SerializeField] private int maxSize = 512;


    private ObjectPool<Carrot> _pool;

    public void Initialize()
    {
        if (_pool != null)
            return;

        _pool = new ObjectPool<Carrot>(
            createFunc: () =>
            {
                Carrot newCarrot = Instantiate(carrotPrefab);
                newCarrot.Init(this);
                return newCarrot;
            },

            actionOnGet: null,
            actionOnRelease: c => c.OnRelease(),
            actionOnDestroy: c =>
            {
                if (c) Destroy(c.gameObject);
            },
            collectionCheck: false,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    public void WarmUp(int count)
    {
        if (_pool == null)
            Initialize();

        for (int i = 0; i < count; i++)
        {
            Carrot carrot = _pool.Get();
            _pool.Release(carrot);
        }
    }

    public Carrot Get(Vector3 spawnPos, Quaternion spawnRot)
    {
        if (_pool == null)
            Initialize();

        var carrot = _pool.Get();
        carrot.OnGet(spawnPos, spawnRot);
        return carrot;
    }

     public void Release(Carrot carrot)
    {
        if (carrot == null || _pool == null)
            return;
            
        _pool.Release(carrot); 
    }
}
