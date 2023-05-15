using UnityEngine;

public class PlayerTriger : MonoBehaviour
{
    [SerializeField] private TriggerObserver _triger;

    private const string AMMO = "Ammo";
    private const string BOX = "Box";
    private const string MACHINE = "Machine";
    private const string ROCKET = "Rocket";
    private const string TRASH = "Trash";

    private float _timerBox;
    private float _timerAmmo;
    private float _timerMachine;
    private float _timerRocket;
    private float _timerTrash;
    private float _delay;

    private void OnEnable()
    {
        _triger.OnEnter += TriggerEnter;
    }
    private void OnDisable()
    {
        _triger.OnEnter -= TriggerEnter;
    }
    private void Start()
    {

        _timerAmmo = 0f;
        _timerBox = 0f;
        _timerMachine = 0f;
        _timerRocket = 0f;
        _timerTrash = 0f;
        _delay = 0.4f;
    }

    private void TriggerEnter(Collider other)
    {
        if (other.CompareTag(AMMO))
        {
            SpawnerAmmo spawnerAmmo = other.GetComponent<SpawnerAmmo>();
            EventSystem.OnDetectAmmo?.Invoke(spawnerAmmo);
            
        }
        if (other.CompareTag(BOX)) 
        { 
            Box box = other.GetComponent<Box>();
            box.AddPlayer(GetComponent<PlayerAmmo>());

        }
        if (other.CompareTag(MACHINE))
        {
            Machine machine = other.GetComponent<Machine>();
            machine.AddPlayer(GetComponent<PlayerAmmo>());
        }

        if (other.CompareTag(ROCKET))
        {
            SpawnerRocket spawnerRocket = other.GetComponent<SpawnerRocket>();
            EventSystem.OnDetectRocket?.Invoke(spawnerRocket);

        }
        if (other.CompareTag(TRASH))
        {
            Trash trash = other.GetComponent<Trash>();
            trash.AddPlayer(GetComponent<PlayerRocket>());
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(AMMO))
        {
            if (_timerAmmo >= _delay * 0.5f)
            {
                EventSystem.OnAddedAmmo?.Invoke();
                _timerAmmo = 0f;
            }
                
            _timerAmmo += Time.deltaTime;
        }
        if (other.CompareTag(BOX))
        {
            if (_timerBox >= _delay)
            {
                EventSystem.OnPutOnBox?.Invoke();
                _timerBox = 0f;
            }

            _timerBox += Time.deltaTime;
        }
        if (other.CompareTag(MACHINE))
        {
            if (_timerMachine >= _delay)
            {
                EventSystem.OnPutOnMachine?.Invoke();

                _timerMachine = 0f;
            }

            _timerMachine += Time.deltaTime;
        }
        if (other.CompareTag(ROCKET))
        {
            if (_timerRocket >= _delay)
            {
                EventSystem.OnAddedRocket?.Invoke();

                _timerRocket = 0f;
            }

            _timerRocket += Time.deltaTime;
        }
        if (other.CompareTag(TRASH))
        {
            if (_timerTrash >= _delay)
            {
                EventSystem.OnRemoveRocket?.Invoke();
                _timerTrash = 0f;
            }

            _timerTrash += Time.deltaTime;
        }
    }
}
