using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Twinstick
{
    /// <summary>
    /// This is basically controlling the pivvotpoint which rotates according to right stick
    /// </summary>
    public class Aimer : MonoBehaviour
    {
        public TwinstickInput tw;
        // Update is called once per frame
        void Update()
        {
            Vector3 direction = tw.RightStickVector;
            if (direction != Vector3.zero)
            {
                transform.LookAt(transform.position + direction * 10.0f);
            }
        }
    }
}