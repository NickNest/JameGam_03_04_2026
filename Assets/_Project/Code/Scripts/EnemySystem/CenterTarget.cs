using System;
using UnityEngine;

namespace _Project.Code.Scripts.EnemySystem
{
    public class CenterTarget : MonoBehaviour
    {
        public event Action<float> OnDamageReceived;

        public void TakeDamage(float damage)
        {
            OnDamageReceived?.Invoke(damage);
        }
    }
}
