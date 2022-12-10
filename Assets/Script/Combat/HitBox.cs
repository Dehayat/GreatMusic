using UnityEngine;

namespace Rosa
{
    public class HitBox : MonoBehaviour
    {
        private GameObject owner;
        private AttackData attackData;

        public void Init(GameObject ownerGO)
        {
            owner = ownerGO;
        }
        public void SetAttackData(AttackData data)
        {
            attackData = data;
        }

        public GameObject GetOwner()
        {
            return owner;
        }
        public AttackData GetAttackData()
        {
            return attackData;
        }
    }
}