using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FirstPersonRB
{
    public class PlayerInputTranslator : MonoBehaviour
    {
        private Motor _motor;
        private Transform _player;

        void FixedUpdate()
        {
            if (_motor && _player)
            {
                Vector3 normal = _motor.GetGroundNormal();
                Quaternion q1 = Quaternion.Euler(normal);
                Quaternion q2 = Quaternion.FromToRotation(Vector3.up, normal);
                Quaternion quat = q1 * q2;
                transform.rotation = quat;

            }
        }

        public void AssignValues(Motor motor, Transform tr)
        {
            _motor = motor;
            _player = tr;
        }
    }
}