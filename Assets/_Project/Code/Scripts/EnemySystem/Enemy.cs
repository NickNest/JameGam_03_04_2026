using System;
using UnityEngine;

namespace _Project.Code.Scripts.EnemySystem
{
    /// <summary>
    /// Враг: двигается к CenterTarget, атакует баррикады на пути.
    /// Требует Collider на префабе для обнаружения кликов (raycast).
    /// Баррикады обнаруживаются через OnTriggerEnter — нужен Rigidbody (Kinematic) на враге
    /// и Collider (Is Trigger) на баррикаде.
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnDied;

        private float _currentHp;
        private float _speed;
        private float _centerDamage;
        private float _barricadeDamage;
        private float _attackInterval;
        private float _attackTimer;

        private Vector3 _attackPosition;
        private CenterTarget _centerTarget;
        private Barricade _targetBarricade;
        private bool _reachedCenter;

        public bool IsDead => _currentHp <= 0f;

        public void Initialize(EnemyStats stats, CenterTarget centerTarget)
        {
            _currentHp = stats.HP;
            _speed = stats.Speed;
            _centerDamage = stats.CenterDamage;
            _barricadeDamage = stats.BarricadeDamage;
            _attackInterval = stats.AttackInterval;
            _centerTarget = centerTarget;
            _attackTimer = 0f;
            _reachedCenter = false;
            _targetBarricade = null;

            _attackPosition = ComputeAttackPosition(centerTarget);
        }

        private Vector3 ComputeAttackPosition(CenterTarget centerTarget)
        {
            var collider = centerTarget.GetComponent<Collider>();
            if (collider != null)
            {
                Vector3 closestPoint = collider.ClosestPoint(transform.position);
                Vector3 dir = (transform.position - closestPoint).normalized;
                return closestPoint + dir * centerTarget.AttackRange;
            }

            if (centerTarget.Collider != null)
            {
                Vector2 closestPoint = centerTarget.Collider.ClosestPoint(transform.position);
                Vector2 dir = ((Vector2)transform.position - closestPoint).normalized;
                return (Vector3)(closestPoint + dir * centerTarget.AttackRange);
            }

            Vector3 fallbackDir = (transform.position - centerTarget.transform.position).normalized;
            return centerTarget.transform.position + fallbackDir * centerTarget.AttackRange;
        }

        public void Tick(float deltaTime)
        {
            if (IsDead) return;

            if (_targetBarricade != null && !_targetBarricade.IsDead)
            {
                AttackBarricade(deltaTime);
                return;
            }

            _targetBarricade = null;

            if (_reachedCenter)
            {
                AttackCenter(deltaTime);
                return;
            }

            Move(deltaTime);
        }

        private void Move(float deltaTime)
        {
            Vector3 dir = _attackPosition - transform.position;
            float distance = dir.magnitude;

            if (distance < 0.2f)
            {
                _reachedCenter = true;
                _attackTimer = 0f;
                return;
            }

            transform.position += dir.normalized * _speed * deltaTime;
        }

        private void AttackCenter(float deltaTime)
        {
            _attackTimer -= deltaTime;
            if (_attackTimer <= 0f)
            {
                _centerTarget.TakeDamage(_centerDamage);
                _attackTimer = _attackInterval;
            }
        }

        private void AttackBarricade(float deltaTime)
        {
            _attackTimer -= deltaTime;
            if (_attackTimer <= 0f)
            {
                _targetBarricade.TakeDamage(_barricadeDamage);
                _attackTimer = _attackInterval;
            }
        }

        public void TakeDamage(float damage)
        {
            if (IsDead) return;

            _currentHp -= damage;
            if (_currentHp <= 0f)
            {
                _currentHp = 0f;
                OnDied?.Invoke(this);
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_targetBarricade == null && other.TryGetComponent(out Barricade barricade) && !barricade.IsDead)
            {
                _targetBarricade = barricade;
                _attackTimer = 0f;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_targetBarricade == null && other.TryGetComponent(out Barricade barricade) && !barricade.IsDead)
            {
                _targetBarricade = barricade;
                _attackTimer = 0f;
            }
        }
    }
}
