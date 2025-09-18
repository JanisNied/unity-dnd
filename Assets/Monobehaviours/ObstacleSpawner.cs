using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] _obstaclesPrefabs;
    public GameObject[] _cloudsPrefabs;
    public Transform _spawnLoc;

    public float _cloudSpawnInterval = 3f;
    public float _obstacleSpawnInterval = 2f;

    public CarBounds _screenBounds;
    private float _minY, _maxY; // -540, 540

    public float _cloudMinSpeed = 1.5f;
    public float _cloudMaxSpeed = 150f;

    public float _obstacleMinSpeed = 2f;
    public float _obstacleMaxSpeed = 200f;

    public AudioSource _audio;
    public AudioClip _effect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _minY = (_screenBounds.maxY / 2) * -1;
        _maxY = (_screenBounds.maxY / 2);

        if (_cloudsPrefabs.Length != 0) InvokeRepeating(nameof(SpawnCloud), 0f, _cloudSpawnInterval);
        if (_obstaclesPrefabs.Length != 0)  InvokeRepeating(nameof(SpawnObstacles), 0f, _obstacleSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SpawnCloud()
    {
        GameObject cloudPrefab = _cloudsPrefabs[Random.Range(0, _cloudsPrefabs.Length)];
        float y = Random.Range(_minY, _maxY);
        Vector3 spawnPos = new Vector3(_spawnLoc.position.x, y, _spawnLoc.position.z);
        GameObject cloud = Instantiate(cloudPrefab, spawnPos, Quaternion.identity, _spawnLoc);
        float speed = Random.Range(_cloudMinSpeed, _cloudMaxSpeed);
        ObstaclesController controller = cloud.GetComponent<ObstaclesController>();
        controller.speed = speed;
    }
    void SpawnObstacles()
    {
        GameObject obstaclePrefab = _obstaclesPrefabs[Random.Range(0, _obstaclesPrefabs.Length)];
        float y = Random.Range(_minY, _maxY);
        Vector3 spawnPos = new Vector3(-_spawnLoc.position.x, y, _spawnLoc.position.z);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity, _spawnLoc);
        float speed = Random.Range(_obstacleMinSpeed, _obstacleMaxSpeed);
        ObstaclesController controller = obstacle.GetComponent<ObstaclesController>();
        controller.speed = -1 * speed;
    }
}
