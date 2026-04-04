using System;
using UnityEngine;

namespace _Project.Code.Scripts.BattleField
{
    public class Barricade : MonoBehaviour, IFieldPlaceable
    {
        public event Action<Barricade> OnDestroyed;
        public event Action<IFieldPlaceable> OnPlaceableDestroyed;

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
                OnPlaceableDestroyed?.Invoke(this);
                gameObject.SetActive(false);
            }
        }
    }
}
