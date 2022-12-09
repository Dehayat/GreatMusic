using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rosa
{
    public class Combat : MonoBehaviour
    {
        public Action<HitInfo> HitEvent;

        [SerializeField]
        private AttackData m_attackData;
        [SerializeField]
        private HitBox[] mc_hitBoxes;
        [SerializeField]
        private HurtBox[] mc_hurtBoxes;

        private void Awake()
        {
            foreach (var hitBox in mc_hitBoxes)
            {
                hitBox.Init(gameObject);
                hitBox.SetAttackData(m_attackData);
            }
            foreach (var hurtBox in mc_hurtBoxes)
            {
                hurtBox.Init(gameObject);
            }
        }
        private void OnEnable()
        {
            foreach (var hurtBox in mc_hurtBoxes)
            {
                hurtBox.HitHurtBoxEvent += OnHit;
            }
        }

        private void OnHit(HitInfo hitInfo)
        {
            HitEvent?.Invoke(hitInfo);
        }

        private void OnDisable()
        {
            foreach (var hurtBox in mc_hurtBoxes)
            {
                hurtBox.HitHurtBoxEvent -= OnHit;
            }
        }
    }
}
