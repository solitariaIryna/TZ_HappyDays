using System;
using UnityEngine;

public enum StateAnimation
{
    ScaleUp,
    Move,
    ScaleDown
}
public enum TypePoint
{
    Transform,
    Vector
}

public class Ammo : MonoBehaviour
{
    [SerializeField] private Animation _animation;
    [SerializeField] private Transform _child;
    private StateAnimation _stateAnimation;
    private TypePoint _typePoint;
    private float _timer;
    private bool _isStartAnimation;
    private Transform _transformMove;
    private Vector3 _positionMove;


    public Action<Ammo> OnMove;
    private void Update()
    {
        if (_isStartAnimation)
            Animation();

    }

    public void StartAnimation(Transform transformMove)
    {
        _isStartAnimation = true;
        _transformMove = transformMove;
        _typePoint = TypePoint.Transform;
        _stateAnimation = StateAnimation.ScaleDown;

    }
    public void StartAnimation(Vector3 positionMove)
    {
        _isStartAnimation = true;
        _positionMove = positionMove;
        _typePoint = TypePoint.Vector;
        _stateAnimation = StateAnimation.ScaleDown;
    }

    private void Animation()
    {
        switch (_stateAnimation)
        {
            case StateAnimation.ScaleUp:
                {
                    _animation.Play();
                    _isStartAnimation = false;
                    break;
                }
            case StateAnimation.Move:
                {
                    Move();
                    break;
                }
            case StateAnimation.ScaleDown:
                {
                    ScaleDown();
                    break;
                }
        }
    }

    private void ScaleUp()
    {
        _timer += Time.deltaTime * 7f;
        if (_timer <= 1)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(1f, 1f, 1f), _timer);
        }
        else
        {
            _timer = 0f;
            _isStartAnimation = false;
        }
    }

    private void ScaleDown()
    {
        _timer += Time.deltaTime * 7f;
        if (_timer <= 1)
        {
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, _timer);
        }
        else
        {
            _stateAnimation = StateAnimation.Move;
            _timer = 0f;
        }
    }

    private void Move()
    {
        if (_typePoint == TypePoint.Transform)
        {
            transform.position = _transformMove.position;
        }
        else
            transform.position = _positionMove;
        _stateAnimation = StateAnimation.ScaleUp;
        _child.localScale = Vector3.zero;
        OnMove?.Invoke(this);
        _timer = 0f;
  
    }


}

