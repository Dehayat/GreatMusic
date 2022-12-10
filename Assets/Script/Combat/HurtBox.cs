using System;
using UnityEngine;

namespace Rosa
{
    public struct HitInfo
    {
        public HitBox attacker;
        public HurtBox defender;
        public AttackData attackData;
    }

    [Serializable]
    public struct AttackData
    {
        [System.Serializable]
        public struct AttackVariantData
        {
            public float damage;
            public float stunDuration;
        }

        public AttackVariantData data;
    }
    public class HurtBox : MonoBehaviour
    {
        public Action<HitInfo> HitHurtBoxEvent;

        private GameObject owner;

        public void Init(GameObject ownerGO)
        {
            owner = ownerGO;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TestHitBox(collision);
        }

        private void TestHitBox(Collider2D hitBoxCollider)
        {
            var hitbox = hitBoxCollider.GetComponent<HitBox>();
            if (hitbox != null && hitbox.GetOwner() != owner)
            {
                HitInfo hitinfo = new HitInfo { attacker = hitbox, defender = this, attackData = hitbox.GetAttackData() };
                HitHurtBoxEvent?.Invoke(hitinfo);
            }
        }
    }
}