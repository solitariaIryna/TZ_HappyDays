using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    private Stack<Ammo> _ammos;
    [SerializeField] private Transform _startAmmoPoint;
    [SerializeField] private Transform _ammoPoint;
    private float _offsetY;
    private const float FACTOR = 0.6f;
    private const int MAX_COUNT_AMMO = 10;

    private SpawnerAmmo _spawnerAmmo;
    private void Awake()
    {
        _offsetY = 0;
        _ammos = new Stack<Ammo>();
    }
        

    private void OnEnable()
    {
        EventSystem.OnDetectAmmo += AddSpawnerAmmo;
        EventSystem.OnAddedAmmo += AddAmmo;
    }
        
    private void OnDisable()
    {
        EventSystem.OnDetectAmmo -= AddSpawnerAmmo;
        EventSystem.OnAddedAmmo -= AddAmmo;
    }

    public int GetCountAmmo()
        => _ammos.Count;


    public Ammo GetAmmo()
    {
        _ammoPoint.transform.position -= Vector3.up * FACTOR;
        return _ammos.Pop();
    }
        
    private void AddSpawnerAmmo(SpawnerAmmo spawnerAmmo) => 
        _spawnerAmmo = spawnerAmmo;
    private void AddAmmo()
    {
        if (GetCountAmmo() < MAX_COUNT_AMMO)
        {
            if (_spawnerAmmo.GetCountAmmo() > 0)
            {
                Ammo ammo = _spawnerAmmo.GetAmmo();
                ammo.OnMove += UpPoint;
                ammo.StartAnimation(_ammoPoint);
               
                _ammos.Push(ammo);
            }
        }
           
    }

    private void UpPoint(Ammo ammo)
    {
        _ammoPoint.transform.position += Vector3.up * FACTOR;
        ammo.transform.parent = _startAmmoPoint;
        ammo.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        ammo.OnMove -= UpPoint;
        ammo.transform.localScale = new Vector3(1f, 0.5f, 1f);
    }

    
}
