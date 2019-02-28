using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FirstPersonRB
{
    public class Motor : MonoBehaviour
    {
        // Events
        public OnLanded onLanded;
        public OnBump onBump;
        public OuterForce onOuterForce;
        [HideInInspector]
        public Vector3 _velocity = Vector3.zero;
        [HideInInspector]
        public float _angleAxis = 0.0f; 
        public bool _isJumping = false, _isSprinting = false;
        public Jumping _jumping = new Jumping();
        public Movement _movement = new Movement();
        public PlayerPhysics _physics = new PlayerPhysics();
        private PlayerInput input;
        // Use this for initialization
        void Start()
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            _physics.Gravity = Physics.gravity;
            input = GetComponent<PlayerInput>();
        }

        // Update is called once per frame
        void Update()
        {
            _physics.GroundTest(this.transform, this);
            _physics.ApplyGravity(this, Time.deltaTime);
            onOuterForce?.Invoke(this, Time.deltaTime);
            //_physics.ApplyDrag(this);
            Vector3 inVec = Vector3.ProjectOnPlane(input.InputVector(), _physics.Gravity).normalized;
            Vector3 flatVelocity = Vector3.ProjectOnPlane(_velocity, _physics.Gravity);
            Vector3 verticalVelocity = Vector3.Project(_velocity, _physics.Gravity);
            
            flatVelocity = inVec * _movement.NewSpeed(this, inVec, flatVelocity, flatVelocity.magnitude, Time.deltaTime);
            
            _velocity = flatVelocity + verticalVelocity;


            Quaternion q2 = Quaternion.FromToRotation(Vector3.up, _physics._groundNormal);
            Quaternion q3 = Quaternion.AngleAxis(_angleAxis, Vector3.up);
            this.transform.rotation = q2 * q3;


            if (_isJumping && _physics._isGrounded)
            {
                _jumping.DoJump(this);
            }

            if (_physics.PreCast(this, Time.deltaTime))
            {
                this.gameObject.transform.Translate(_velocity * Time.deltaTime, Space.World);
            }
            else
            {
                //Debug.Log("Precast BUMP!");
                _velocity = Vector3.zero;
            }

            //setup for next tick
            _physics._wasGrounded = _physics._isGrounded;
        }

        


        private void FixedUpdate()
        {
            /// DO NOIT FUCKING USE THIS IDIOT!!!    
        }

        private void OnCollisionEnter(Collision collision)
        {
            onBump?.Invoke();
            if (collision.contacts.Length != 0)
            {
                Debug.Log($"COLLISION: Hit {collision.contacts.Length} objects");
            }
            if (collision.contacts[0].otherCollider.tag == "GravityReset")
            {
                _physics.Gravity = Physics.gravity;
                Debug.Log("GravityReset");
                return;
            }
            /*Debug.Log("New GroundNormal");
            //_isGrounded = true;
            //_groundNormal = collision.contacts[0].normal;
            if (_wasGrounded == false)
            {
                // We landed
                _gravity = _groundNormal * _gravityStrength * -1.0f;
                _isJumping = false;
                onLanded?.Invoke(collision.contacts[0].point);
            }*/

        }

        

        public Vector3 GetGroundNormal()
        {
            return _physics._groundNormal;
        }

    }// END of Motor
}