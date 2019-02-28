using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FirstPersonRB
{
    [System.Serializable]
    public class Movement
    {
        public float _currentSpeed = 0.0f;
        public float _maxGroundSpeed = 10.0f;
        public float _extraSprintSpeed = 5.0f;
        public float _maxGroundAcceleration = 2.0f;
        public float _maxGroundDecceleration = 4.0f;
        public float _maxAirSpeed = 10.0f;
        public float _maxAirAcceleration = 2.0f;
        public float _maxAirDecceleration = 0.1f;

        public Vector3 ApplyInputFullControl(Vector3 invec, Vector3 flatVelocity, Motor motor)
        {

            if (invec == Vector3.zero)
            {
                return flatVelocity;
            }
            return invec * flatVelocity.magnitude;
        }

        public float NewSpeed(Motor motor,  Vector3 inVec, Vector3 velVec, float OldSpeed, float delta)
        {
            _currentSpeed = velVec.magnitude;
            float maxSpeed = MaxSpeed(motor._physics._isGrounded, motor._isSprinting);
            if (inVec == Vector3.zero)
            {
                OldSpeed -= ((maxSpeed - _currentSpeed) / maxSpeed) * Decceleration(motor._physics._isGrounded) * delta;
            }
            else
            {
                OldSpeed += ((maxSpeed - _currentSpeed) / maxSpeed) * Acceleration(motor._physics._isGrounded) * delta;
            }
            return OldSpeed;
        }

        internal Vector3 ApplyInput(Vector3 inVec, Vector3 velocity, bool grounded, bool sprinting)
        {
            _currentSpeed = velocity.magnitude;
            float maxSpeed = MaxSpeed(grounded, sprinting);
            if (inVec == Vector3.zero)
            {
                _currentSpeed -= ((maxSpeed - _currentSpeed) / maxSpeed) * _maxGroundDecceleration;
            }
            else
            {
                _currentSpeed += ((maxSpeed - _currentSpeed) / maxSpeed) * _maxGroundAcceleration;
            }
            return inVec.normalized * _currentSpeed;
        }

        private float MaxSpeed(bool grounded, bool sprinting = false)
        {
            if (grounded)
            {
                if (sprinting)
                {
                    return _maxGroundSpeed + _extraSprintSpeed;
                }
                else
                {
                    return _maxGroundSpeed;
                }
            }
            else
            {
                return _maxAirSpeed;
            }
        }
        private float Decceleration(bool grounded)
        {
            if (grounded)
            {
                return _maxGroundDecceleration;
            }
            else
            {
                return _maxAirDecceleration;
            }
        }
        private float Acceleration(bool grounded)
        {
            if (grounded)
            {
                return _maxGroundAcceleration;
            }
            else
            {
                return _maxAirAcceleration;
            }
        }
    }
}
