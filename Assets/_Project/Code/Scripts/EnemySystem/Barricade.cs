using System;
using UnityEngine;

namespace _Project.Code.Scripts.EnemySystem
{
    public class Barricade : MonoBehaviour
    {
        public event Action<Barricade> OnDestroyed;

        [SerializeField] private float _maxHp = 80f;

        private float _currentHp;

        public bool IsDead => _currentHp <= 0f;

        private void Awake()
        {
            _currentHp = _maxHp;
        }

        public void TakeDamage(float damage)
        {
            if (IsDead) return;

            _currentHp -= damage;
            if (_currentHp <= 0f)
            {
                _currentHp = 0f;
                OnDestroyed?.Invoke(this);
                gameObject.SetActive(false);
            }
        }
    }
}
