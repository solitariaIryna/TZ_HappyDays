using System.Collections.Generic;
using UnityEngine;

public class SpawnerAmmo : MonoBehaviour
{
    [SerializeField] private Ammo _prefab;
    [SerializeField] private Transform _spawnPoint;
    private int rows = 3;
    private int collumns = 2;
    private int height = 2;

    private Stack<Ammo> _arrayAmmo;
    private void Start()
    {
        _arrayAmmo = new Stack<Ammo>();
        Spawn();
    }
    public int GetCountAmmo() 
        => _arrayAmmo.Count;
    public Ammo GetAmmo() =>
        _arrayAmmo.Pop();
    private void Spawn()
    {
        for (int i = 0; i < height; i++) 
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < collumns; col++)
                {
                    Vector3 position = new Vector3(_spawnPoint.position.x + col * 0.75f, _spawnPoint.position.y + i * 0.6f, _spawnPoint.position.z + row * 0.75f);
                    Ammo ammo = Instantiate(_prefab, position, Quaternion.identity);                   
                    ammo.transform.parent = _spawnPoint;
                    _arrayAmmo.Push(ammo);
                }
            }

        }
        
    }




}
