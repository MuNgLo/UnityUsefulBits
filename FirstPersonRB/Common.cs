using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FirstPersonRB
{
    public delegate void OnLanded(Vector3 hitPoint);
    public delegate void OnJump(Vector3 jumpPoint);
    public delegate void OnBump();
    public delegate void OuterForce(Motor motor, float delta);
}
