using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerLidar
    {
        private readonly Transform _player;
        private readonly LayerMask _playerMask;

        private readonly int _amountOfPings;
        private float _delay;
        private readonly float _maxDistance;

        private readonly GameObject _pingPrefab;

        private bool _isOn;

        public PlayerLidar(Transform player, LayerMask mask, int amountOfPings, float delay, float maxDistance, GameObject pingPrefab)
        {
            _player = player;
            _playerMask = mask;
            _amountOfPings = amountOfPings;
            _delay = delay;
            _maxDistance = maxDistance;
            _pingPrefab = pingPrefab;

            _isOn = true;
        }

        public void ToggleLidar()
        {
            _isOn = !_isOn;
        }

        public IEnumerator Emit()
        {
            float angleMove = 360f / _amountOfPings;
            float currentEmmiterAngle = 0f;

            while (true)
            {
                if (_isOn)
                {
                    while (currentEmmiterAngle < 360f)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(_player.position,
                            Quaternion.AngleAxis(currentEmmiterAngle, Vector3.forward) * _player.up, _maxDistance,
                            ~_playerMask);
                        if (hit)
                        {
                            // TODO make it use pooling
                            Object.Instantiate(_pingPrefab,
                                hit.point + new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)),
                                Quaternion.identity);
                        }

                        currentEmmiterAngle += angleMove;
                    }

                    currentEmmiterAngle -= 360f;

                    yield return new WaitForSeconds(_delay);
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}