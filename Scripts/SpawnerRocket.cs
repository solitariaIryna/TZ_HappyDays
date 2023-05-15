
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRocket: MonoBehaviour
{
    [SerializeField] private Rocket _rocket;
    [SerializeField] private Transform _rocketPoint;

    private const int MAX_COUNT_ROCKET = 6;
    private const int ROWS = 2;
    private const int COLLUMNS = 3;
    private int indexAmmo;
    private int rowAmmo;
    private Vector3 _offset;

    private Stack<Rocket> _rocketStack;

    public int GetCountRocket() =>
        _rocketStack.Count;

    public Rocket GetRocket()
    {
        return _rocketStack.Pop();
    }
    private void Awake()
    {
        _rocketStack = new Stack<Rocket>();
    }
    public void CreateRocket()
    {
        if (GetCountRocket() < MAX_COUNT_ROCKET)
        {
            Rocket rocket = Instantiate(_rocket, _rocketPoint.position, Quaternion.identity);
            Vector3 position = new Vector3(_rocketPoint.position.x + _offset.x, _rocketPoint.position.y + _offset.y, _rocketPoint.position.z + _offset.z);
            rocket.transform.position = position;
            rocket.transform.parent = transform;
            rocket.transform.eulerAngles = new Vector3(90, 8f, 0);
            

                indexAmmo++;
                if (indexAmmo == 1)
                {
                    _offset.x += 0.75f;
                }

                if (indexAmmo == ROWS)
                {
                    _offset.x = 0f;
                    _offset.z -= 0.75f;
                    indexAmmo = 0;
                    rowAmmo++;
                    if (rowAmmo > COLLUMNS - 1)
                    {
                        _offset.y += 0.5f;
                        _offset.z = 0f;
                        _offset.x = 0f;
                        indexAmmo = 0;
                        rowAmmo = 0;
                    }
                }

            _rocketStack.Push(rocket);
            Invoke(nameof(NextRocket), 1.3f);
        }
        
    }
    public void NextRocket()
    {
        EventSystem.OnCreatedRocket?.Invoke();
    }

} 

