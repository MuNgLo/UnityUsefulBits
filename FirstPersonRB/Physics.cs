using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FirstPersonRB
{
    [System.Serializable]
    public class PlayerPhysics
    {
        public LayerMask _walkable, _collidable;
        public float _castLength = 1.0f, _castWidth = 0.2f;
        public float _groundDrag = 0.1f, _airDrag = 0.01f;
        public bool _isGrounded { get; private set; } = true;
        public bool _wasGrounded = true;
        public Vector3 _groundNormal;
        private Vector3 _gravity = Vector3.zero;
        public Vector3 Gravity
        {
            get { return _gravity; }
            set { SetGravity(value); }
        }

        /// <summary>
        /// Returns true if we can move there
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        public bool PreCast(Motor motor, float delta)
        {
            Transform tr = motor.transform.Find("Capsule");

            return !Physics.CapsuleCast(
                tr.position + tr.up * 0.5f,
                tr.position + tr.up * -0.5f,
                0.5f,
                motor._velocity,
                motor._velocity.magnitude * delta, 
                _collidable
                );
            
        }

        public void GroundTest(Transform tr, Motor motor)
        {
            Vector3 castStartPoint = tr.position + tr.up * 0.5f;
            Vector3 castDirection = _gravity.normalized;

            

            Debug.DrawLine(castStartPoint, tr.position + castDirection * _castLength, Color.grey); // DEBUG Groundcast line
            RaycastHit hit = new RaycastHit();
            //if (Physics.SphereCast(castStartPoint, _castWidth, castDirection, out hit, _castLength, _walkable))
            //{ }
            RaycastHit[] hits = Physics.SphereCastAll(castStartPoint, _castWidth, castDirection, _castLength, _walkable);
            if(hits.Length > 0)
            {
                if (hits.Length == 1)
                {
                    hit = hits[0];
                }
                else
                {

                    bool keep = false;
                    foreach (RaycastHit hitB in hits)
                    {
                        if (hitB.normal == _groundNormal)
                        {
                            hit = hitB;
                            keep = true;
                        }
                        if (keep) { continue; }
                    }
                    if (!keep)
                    {
                        hit = hits[0];
                    }
                }
            }else
            {
                _isGrounded = false;
                return;
            }

            if(hits.Length > 0) { 
                /*if (hit.collider.tag == "GravityReset")
                {

                    return;
                }*/
                /*float angleDot = Vector3.Dot(hits[0].normal, _gravity);
                if( angleDot < 0.0f)
                {
                    Debug.Log("TO STEEP!");
                }*/

                if (hit.distance < _castLength - 0.1f)
                {
                    //Debug.Log("To close to ground. adjusting!");
                    tr.position = tr.position + -_gravity.normalized * _castLength;
                }
                _isGrounded = true;
                _groundNormal = hit.normal;
                _gravity = _groundNormal * -1.0f * _gravity.magnitude;
                Debug.DrawLine(castStartPoint, hit.point, Color.black, 2.0f);
                if (_wasGrounded == false)
                {
                    // We landed
                    //_rb.velocity = Vector3.ProjectOnPlane(_rb.velocity, _groundNormal);
                    motor._velocity = Vector3.ProjectOnPlane(motor._velocity, _groundNormal);
                    //_isJumping = false;
                    motor.onLanded?.Invoke(hit.point);
                }
            }
            else
            {
                _isGrounded = false;
            }
        }// END of Groundtest

        internal void ApplyDrag(Motor motor)
        {
            if (_isGrounded)
            {
                motor._velocity -= motor._velocity.normalized * _groundDrag * Time.deltaTime;
            }
            else
            {
                motor._velocity -= motor._velocity.normalized * _airDrag * Time.deltaTime;
            }
        }

        internal void ApplyGravity(Motor motor, float delta)
        {
            if (!_isGrounded)
            {
                motor._velocity += Gravity * delta;
            }
        }

        private void SetGravity(Vector3 value)
        {
            _gravity = value;
        }
    }
}
