using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Twinstick
{
    /// <summary>
    /// This is using leftstick vector from twinstickinput to move the transform it is attached to
    /// Check the AImer class for rightstick bit
    /// </summary>
    [RequireComponent(typeof(TwinstickInput))]
    public class Mover : MonoBehaviour
    {
        public float _moveSpeed = 2.0f;
        private TwinstickInput tw;
        private void Start()
        {
            tw = GetComponent<TwinstickInput>();
        }
        // Update is called once per frame
        void Update()
        {
            transform.Translate(tw.LeftStickVector * _moveSpeed * Time.deltaTime);
        }
    }
}