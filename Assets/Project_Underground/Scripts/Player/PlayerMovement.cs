using UnityEngine;

namespace Player
{
    public class PlayerMovement
    {
        private Vector2 _linearForce;
        private Vector2 _maxLinearVelocity;
        private float _angularForce;
        private float _maxAngularVelocity;

        public PlayerMovement(Vector2 linearForce, Vector2 maxLinearVelocity, float angularForce, float maxAngularVelocity)
        {
            _linearForce = linearForce;
            _maxLinearVelocity = maxLinearVelocity;
            _angularForce = angularForce;
            _maxAngularVelocity = maxAngularVelocity;
        }
    }
}