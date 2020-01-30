using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public struct ProjectileHitEventArguments
    {
        public Projectile proj;
        public int damage;
        public int ownerID;
        public int victimeID;
    }
    [System.Serializable]
    public class ProjectileEvent : UnityEngine.Events.UnityEvent<ProjectileHitEventArguments>
    {

    }

    

