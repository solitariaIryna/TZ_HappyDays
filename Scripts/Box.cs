using System.Collections.Generic;
using UnityEngine;

public class Box: MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    private List<Ammo> _ammos;
    private const int MAX_COUNT_AMMO = 12;
    private const int ROWS = 2;
    private const int COLLUMNS = 3;
    private int indexAmmo;
    private int rowAmmo;
    private Vector3 _offset;

    private PlayerAmmo _player;

    private void Awake()
    {
        _ammos = new List<Ammo>();
        _offset = Vector3.zero;
        indexAmmo = 0;
        EventSystem.OnPutOnBox += AddAmmo;
    }

    private void OnDisable()
    {
        EventSystem.OnPutOnBox -= AddAmmo;
    }
    private int GetCountAmmo()
        => _ammos.Count;

    public void AddPlayer(PlayerAmmo player)
        => _player = player;

    private void AddAmmo()
    {
       if (GetCountAmmo() < MAX_COUNT_AMMO)
       {
          if (_player.GetCountAmmo() > 0)
          {
                Vector3 position = new Vector3(_spawnPoint.position.x + _offset.x, _spawnPoint.position.y + _offset.y, _spawnPoint.position.z + _offset.z);
              
                Ammo ammo = _player.GetAmmo();
                ammo.OnMove += OnMove;
                ammo.transform.parent = null;
                ammo.StartAnimation(position);
                ammo.transform.parent = transform;
                ammo.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

                indexAmmo++;
                if(indexAmmo == 1)
                {
                    _offset.z -= 0.75f;
                }

                if (indexAmmo == ROWS)
                {
                    _offset.z = 0f;
                    _offset.x += 0.75f;
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
            }

            
       }
    }


    private void OnMove(Ammo ammo)
    {
        ammo.OnMove -= OnMove;
        ammo.transform.localScale = Vector3.one;

    }
}

