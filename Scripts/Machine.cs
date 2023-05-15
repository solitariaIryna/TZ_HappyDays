using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Machine : MonoBehaviour
{
    [SerializeField] private PlayableDirector _timeLine;
    [SerializeField] private Transform _spawnPoint;

    private Stack<Ammo> _ammos;
    private PlayerAmmo _player;
    private int indexAmmo;
    private int rowAmmo;
    private Vector3 _offset;

    private const int MAX_COUNT_AMMO = 12;
    private const int ROWS = 2;
    private const int COLLUMNS = 3;


    private float _timerDestroy;
    private float _delay;

    private bool _isWorkMachine = false;
    private void Awake()
    {
        _timerDestroy = 0f;
        _delay = 1f;
        _ammos = new Stack<Ammo>();
        EventSystem.OnPutOnMachine += AddAmmo;
        EventSystem.OnCreatedRocket += DestroyAmmo;
    }

    private void Update()
    {
        if (GetCountAmmo() == 1 && !_isWorkMachine)
        {
            Invoke(nameof(DestroyAmmo), 2.6f);
            _isWorkMachine = true;
        }
        if (GetCountAmmo() == 0)
        {
            _isWorkMachine = false;
        }
    }



    private void OnDisable()
    {
        EventSystem.OnPutOnMachine -= AddAmmo;
        EventSystem.OnCreatedRocket -= DestroyAmmo;
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
                ammo.transform.localScale = Vector3.up * 0.5f;
                ammo.transform.localEulerAngles = Vector3.up * 90f;


                indexAmmo++;
                if (indexAmmo == 1)
                {
                    _offset.z += 0.75f;
                }

                if (indexAmmo == ROWS)
                {
                    _offset.z = 0f;
                    _offset.x += 0.64f;
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
                _ammos.Push(ammo);
            }
        }
    }

    private void DestroyAmmo()
    {
        if (GetCountAmmo() > 0)
        {
            _timeLine.Stop();
            Destroy(_ammos.Pop().gameObject);
            _timeLine.Play();
        }
        
    }

    private void OnMove(Ammo ammo)
    {
        ammo.OnMove -= OnMove;
        ammo.transform.localScale = Vector3.one;
    }
    
}
