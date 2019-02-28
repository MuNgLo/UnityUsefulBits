using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FirstPersonRB
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Motor))]
    public class PlayerInput : MonoBehaviour
    {
        public Transform _cameraMount;
        private Motor _motor;
        public float Sensitivity = 1.0f, ySensMultiplier = 1.0f, yMinLimit = -85F, yMaxLimit = 85F;
        private float x = 0.0f;// Storing camera angle
        private float y = 0.0f;// Storing camera angle
        private void Start()
        {
            _motor = GetComponent<Motor>();
        }
        // Update is called once per frame
        void Update()
        {
            #region MOUSE INPUT
            // Get MouseInput
            x += Input.GetAxisRaw("Mouse X") * Sensitivity;                                              // Mouse horizontal input
            y -= Input.GetAxisRaw("Mouse Y") * (Sensitivity * ySensMultiplier);                          // Mouse vertical input multiplier needs to be halfed for symmetry
            // Apply changes to camera
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);                    // Clamping the max/min vertical angle
            if (_cameraMount)
            {
                _cameraMount.transform.localEulerAngles = new Vector3(y, 0, 0);   // Set the Cameras Y pitch
            }
            _motor._angleAxis = x;
            #endregion
            _motor._isJumping = Input.GetButton("Jump");
            _motor._isSprinting = Input.GetButton("Sprint");
        }
        /// <summary>
        /// Returns a normalized vector
        /// </summary>
        /// <returns></returns>
        public Vector3 InputVector()
        {
            Vector3 vIn = Vector3.zero;

            if (Input.GetAxis("Horizontal") > 0.0f)
            {
                vIn.x += 1.0f;
            }
            if (Input.GetAxis("Horizontal") < 0.0f)
            {
                vIn.x -= 1.0f;
            }
            if (Input.GetAxis("Vertical") > 0.0f)
            {
                vIn.z += 1.0f;
            }
            if (Input.GetAxis("Vertical") < 0.0f)
            {
                vIn.z -= 1.0f;
            }
            return this.transform.TransformDirection(vIn).normalized;
        }
    }// End of PlayerInput
}
