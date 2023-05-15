
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _floatingJoystick;
    [SerializeField] private PlayerAmmo _playerAmmo;
    [SerializeField] private PlayerRocket _playerRocket;
    [SerializeField] private CharacterController _characterController;
    private float _speed = 5f;
    

    private void Update()
    {
        if (_playerAmmo.GetCountAmmo() > 0)
        {
            if (_floatingJoystick.Horizontal != 0 || _floatingJoystick.Vertical != 0)
            {
                EventSystem.OnChangeAnimation?.Invoke(AnimationPlayer.BoxWalk);
                Run();
            }
            else
            {
                EventSystem.OnChangeAnimation?.Invoke(AnimationPlayer.BoxStay);
            }
        }
        else if (_playerRocket.GetCountRocket() > 0)
        {
            if (_floatingJoystick.Horizontal != 0 || _floatingJoystick.Vertical != 0)
            {
                EventSystem.OnChangeAnimation?.Invoke(AnimationPlayer.RocketRun);
                Run();
            }
            else
            {
                EventSystem.OnChangeAnimation?.Invoke(AnimationPlayer.RocketStay);
            }
        }
        else 
        {
            if (_floatingJoystick.Horizontal != 0 || _floatingJoystick.Vertical != 0)
            {
                EventSystem.OnChangeAnimation?.Invoke(AnimationPlayer.Run);
                Run();
            }
            else
            {
                EventSystem.OnChangeAnimation?.Invoke(AnimationPlayer.Idle);
            }
        }

       


    }

    private Vector3 GetJoystickValue() => 
        new Vector3(_floatingJoystick.Horizontal, 0, _floatingJoystick.Vertical);

    private void Run()
    {
        Vector3 direction = GetJoystickValue();

        _characterController.Move(direction * _speed * Time.deltaTime);
        //transform.LookAt(direction + transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f);
    }

}
