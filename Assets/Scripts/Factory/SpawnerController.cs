using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [Header("Factories")]
    [SerializeField] private bool randomizeFactory = true;
    [SerializeField] private List<ProductFactoryBaseSO> factories = new(); // RedFactory, GreenFactory
    [SerializeField] private ProductFactoryBaseSO activeFactory; 

    [Header("Raycast")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float yOffset = 0.01f;

    private void Reset()
    {
        cam = Camera.main;
        groundMask = ~0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TrySpawn(Input.mousePosition);

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            TrySpawn(Input.GetTouch(0).position);
    }

    private void TrySpawn(Vector2 screenPos)
    {
        if (!cam) return;

        var ray = cam.ScreenPointToRay(screenPos);
        if (!Physics.Raycast(ray, out var hit, 1000f, groundMask, QueryTriggerInteraction.Ignore)) return;

        var pos = hit.point + Vector3.up * yOffset;
        var rot = Quaternion.identity;

        var factory = SelectFactory();
        factory?.Create(pos, rot);
    }

    private ProductFactoryBaseSO SelectFactory()
    {
        if (randomizeFactory && factories != null && factories.Count > 0)
            return factories[Random.Range(0, factories.Count)];
        return activeFactory;
    }

}
