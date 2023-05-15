using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Vector3 _offsetToTarget;
    private void Awake()
    {
        _offsetToTarget = transform.position - _target.position;
    }

    private void Update()
    {
        transform.position = _target.position + _offsetToTarget;
    }
}
