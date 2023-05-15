using UnityEngine;

public class Trash: MonoBehaviour
{
    [SerializeField] private Animation _animation;
    private PlayerRocket _player;
    public void AddPlayer(PlayerRocket player)
    {
        _player = player;
        if (_player.GetCountRocket() > 0)
            _animation.Play();
    }

    private void OnEnable() => 
        EventSystem.OnRemoveRocket += RemoveRocket;
    private void OnDisable() => 
        EventSystem.OnRemoveRocket -= RemoveRocket;
    private void RemoveRocket()
    {
        if (_player.GetCountRocket() > 0)
        {
           Rocket rocket = _player.GetRocket();
           Destroy(rocket.gameObject);
        }
    }
}