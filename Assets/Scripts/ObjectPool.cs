using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private Camera _camera;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject template)
    {
        _camera = Camera.main;

        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawnedObject = Instantiate(template, _container.transform);
            spawnedObject.SetActive(false);

            _pool.Add(spawnedObject);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(obj => obj.activeSelf == false);

        return result != null;
    }

    protected void DisableObjectAbroadScreen()
    {
        foreach (var item in _pool)
        {
            if (item.activeSelf == true)
            {
                Vector3 point = _camera.WorldToViewportPoint(item.transform.position);
                if (point.x < 0)
                    item.SetActive(false);
            }
        }
    }

    public void ResetPool ()
    {
        foreach (var item in _pool)
        {
            item.SetActive(false);
        }
    }
}
