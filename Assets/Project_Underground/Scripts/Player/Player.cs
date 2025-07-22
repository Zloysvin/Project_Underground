using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _config;

        private PlayerMovement _movement;
        private PlayerLidar _lidar;
        private PlayerRadar _radar;

        private void Awake()
        {
            _lidar = new PlayerLidar(transform, _config.PlayerMask, _config.AmountOfPings, _config.Delay,
                _config.MaxDistance, _config.PingPrefab);

            StartCoroutine(_lidar.Emit());
        }
    }
}

