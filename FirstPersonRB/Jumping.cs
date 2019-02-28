using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FirstPersonRB
{

    [System.Serializable]
    public class Jumping
    {
        public OnJump onJump;
        public float _last = 0.0f, _cooldown = 1.0f, _force = 50.0f;
        public void DoJump(Motor motor)
        {
            if (_last < Time.time - _cooldown)
            {
                //Debug.Log("Jump!!");
                _last = Time.time;
                motor._velocity += motor._physics.Gravity.normalized * -5.0f;
                onJump?.Invoke(motor.transform.position);
            }
        }

    }
}
