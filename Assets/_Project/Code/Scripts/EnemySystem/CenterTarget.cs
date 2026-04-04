using System;
using UnityEngine;

namespace _Project.Code.Scripts.EnemySystem
{
    public class CenterTarget : MonoBehaviour
    {
        public event Action<float> OnDamageReceived;

        [SerializeField] private float _attackRange = 0.5f;
        [SerializeField] private Collider2D _collider;
        public float AttackRange => _attackRange;
        public Collider2D Collider => _collider;

        public void TakeDamage(float damage)
        {
            Debug.Log($"{nameof(CenterTarget)} took {damage} damage");
            OnDamageReceived?.Invoke(damage);
        }
    }
}
