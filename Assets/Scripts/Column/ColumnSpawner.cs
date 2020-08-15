using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : ObjectPool
{
    [SerializeField] private GameObject _template;
    [SerializeField] private float _secondsBetweenSpawns;
    [SerializeField] private float _maxColumnPositionY;
    [SerializeField] private float _minColumnPositionY;

    private float _elapsedTime = 0;

    private void Start()
    {
        Initialize(_template);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _secondsBetweenSpawns)
        {
            if (TryGetObject(out GameObject column))
            {
                _elapsedTime = 0;

                float spawnPositionY = Random.Range(_minColumnPositionY, _maxColumnPositionY);
                Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);
                column.SetActive(true);
                column.transform.position = spawnPoint;

                DisableObjectAbroadScreen();
            }
        }
    }
}
