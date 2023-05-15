
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket: MonoBehaviour
{
    private Stack<Rocket> _rockets;
    [SerializeField] private Transform _startRocketPoint;
    [SerializeField] private Transform _rocketPoint;
    private const float FACTOR = 0.5f;
    private const int MAX_COUNT_ROCKET = 3;

    private SpawnerRocket _spawnerRocket;
    private void Awake()
    {
        _rockets = new Stack<Rocket>();
    }

    
    private void OnEnable()
    {
        EventSystem.OnDetectRocket += AddSpawnerRocket;
        EventSystem.OnAddedRocket += AddRocket;
    }

    private void OnDisable()
    {
        EventSystem.OnDetectRocket -= AddSpawnerRocket;
        EventSystem.OnAddedRocket -= AddRocket;
    }

    public int GetCountRocket()
        => _rockets.Count;


    public Rocket GetRocket()
    {
        _rocketPoint.transform.position -= Vector3.up * FACTOR;
        return _rockets.Pop();
    }

    private void AddSpawnerRocket(SpawnerRocket spawnerRocket) =>
        _spawnerRocket = spawnerRocket;
    private void AddRocket()
    {
        if (GetCountRocket() < MAX_COUNT_ROCKET)
        {
            if (_spawnerRocket.GetCountRocket() > 0)
            {
                Rocket rocket = _spawnerRocket.GetRocket();
                rocket.OnMove += UpPoint;
                rocket.StartAnimation(_rocketPoint);
                
                _rockets.Push(rocket);
            }
        }

    }

    private void UpPoint(Rocket rocket)
    {
        _rocketPoint.transform.position += Vector3.up * FACTOR;
        rocket.transform.parent = _startRocketPoint; 
        rocket.OnMove -= UpPoint;
        rocket.transform.localEulerAngles = new Vector3(0, 0, 0);
        rocket.transform.localEulerAngles = new Vector3(0, 90, 0);

    }


}






