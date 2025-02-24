using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _itemPrefabList;
    [SerializeField] private float _spawnInterval;

    private Vector3 _leftSpawnLimit;
    private Vector3 _rightSpawnLimit;
    private float _spawnTimer;

    private void Start()
    {
        if (Camera.main == null) return;
        _leftSpawnLimit = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, Camera.main.nearClipPlane));
        _rightSpawnLimit = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));
    }
    
    private void Update()
    {
        if (_spawnTimer < _spawnInterval)
        {
            _spawnTimer += Time.deltaTime;
        }
        else
        {
            _spawnTimer = 0f;
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        GameObject itemToSpawn = _itemPrefabList[Random.Range(0, _itemPrefabList.Length)];
        Renderer itemRenderer = itemToSpawn.GetComponentInChildren<Renderer>();
        float offset = Math.Max(itemRenderer.bounds.size.x, itemRenderer.bounds.size.y);
        
        float xPosition = Random.Range(_leftSpawnLimit.x + offset, _rightSpawnLimit.x - offset);
        Vector3 spawnPosition = new Vector3(xPosition, transform.position.y, 0f);
        Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);
    }
}
