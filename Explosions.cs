using System;
using UnityEngine;
using Harmony;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheBestDefense
{
    public class Explosions : MonoBehaviour
    {
        public float concussionMaxDamage = 300f;
        public float concussionDetonateRadius = 20f;
        public float nukeMaxDamage = 4000f;
        public float nukeDetonateRadius = 50f;

        public static GameObject ConcussionExplodePrefab;
        public static GameObject NukeExplodePrefab;

        public void AssignPrefabs()
        {
            //var crash = (Resources.Load("WorldEntities/Environment/crash") as GameObject).GetComponent<Crash>();
            var crash = gameObject.GetComponent(typeof(Crash)) as Crash;
            var seamothtorpedo = gameObject.GetComponent(typeof(SeamothTorpedo)) as SeamothTorpedo;

            ConcussionExplodePrefab = crash.detonateParticlePrefab;
            NukeExplodePrefab = seamothtorpedo.explosionPrefab;
        }
        
        public void ConcussionExplosion()
        {
            Utils.PlayOneShotPS(ConcussionExplodePrefab, transform.position, transform.rotation, null);
            DamageSystem.RadiusDamage(concussionMaxDamage, transform.position, concussionDetonateRadius, DamageType.Explosive, gameObject);
            gameObject.GetComponent<LiveMixin>().Kill(DamageType.Normal);
        }

        public void NukeExplosion()
        {
            Utils.PlayOneShotPS(NukeExplodePrefab, transform.position, transform.rotation, null);
            DamageSystem.RadiusDamage(nukeMaxDamage, transform.position, nukeDetonateRadius, DamageType.Explosive, gameObject);
            gameObject.GetComponent<LiveMixin>().Kill(DamageType.Normal);
        }
    }
}