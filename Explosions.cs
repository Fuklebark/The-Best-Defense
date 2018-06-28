using System;
using UnityEngine;
using Harmony;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheBestDefense
{
    [HarmonyPatch(typeof(SeamothTorpedoWhirlpool))]
    [HarmonyPatch("TriggerBehaviours")]

    public class Explosions : MonoBehaviour
    {
        public float concussionMaxDamage = 300f;
        public float concussionDetonateRadius = 20f;
        public float nukeMaxDamage = 8000f;
        public float nukeDetonateRadius = 60f;
        public void ConcussionExplosion()
        {
            DamageSystem.RadiusDamage(concussionMaxDamage, transform.position, concussionDetonateRadius, DamageType.Explosive, gameObject);
            gameObject.GetComponent<LiveMixin>().Kill(DamageType.Normal);
        }

        public void NukeExplosion()
        {
            DamageSystem.RadiusDamage(nukeMaxDamage, transform.position, nukeDetonateRadius, DamageType.Explosive, gameObject);
            gameObject.GetComponent<LiveMixin>().Kill(DamageType.Normal);
        }
    }
}


/*
 * 

 * Crashfish Explosion Code
private void Detonate()
	{
		if (this.detonateParticlePrefab)
		{
			global::Utils.PlayOneShotPS(this.detonateParticlePrefab, base.transform.position, base.transform.rotation, null);
		}
		DamageSystem.RadiusDamage(this.maxDamage, base.transform.position, this.detonateRadius, DamageType.Explosive, base.gameObject);
		base.gameObject.GetComponent<LiveMixin>().Kill(DamageType.Normal);
	}

 * Seamoth Electrical Defense Module Explosion Code
		int num5 = UWE.Utils.OverlapSphereIntoSharedBuffer(base.transform.position, num, -1, QueryTriggerInteraction.UseGlobal);
		for (int i = 0; i < num5; i++)
		{
			Collider collider = UWE.Utils.sharedColliderBuffer[i];
			GameObject gameObject2 = UWE.Utils.GetEntityRoot(collider.gameObject);
			if (gameObject2 == null)
			{
				gameObject2 = collider.gameObject;
			}
			Creature component = gameObject2.GetComponent<Creature>();
			LiveMixin component2 = gameObject2.GetComponent<LiveMixin>();
			if (component != null && component2 != null)
			{
				component2.TakeDamage(num2, base.transform.position, DamageType.Electrical, base.gameObject);

    */
