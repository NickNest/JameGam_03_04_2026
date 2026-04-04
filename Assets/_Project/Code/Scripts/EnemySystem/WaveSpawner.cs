using System.Collections.Generic;
using _Project.Code.Scripts.Game;
using UnityEngine;

namespace _Project.Code.Scripts.EnemySystem
{
    public class WaveSpawner : MonoBehaviour, IManualUpdate
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private CenterTarget _centerTarget;
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private WaveConfig _waveConfig;

        private float _gameTime;
        private int _currentWaveIndex;
        private readonly List<Enemy> _activeEnemies = new();

        private bool _isSpawningWave;
        private readonly Queue<EnemyType> _spawnQueue = new();
        private float _intraSpawnTimer;
        private float _currentIntraSpawnInterval;

        public void ManualAwake() { }

        public void ManualUpdate(float deltaTime)
        {
            _gameTime += deltaTime;

            TryStartNextWave();
            ProcessSpawnQueue(deltaTime);
            UpdateEnemies(deltaTime);
        }

        private void TryStartNextWave()
        {
            if (_currentWaveIndex >= _waveConfig.Waves.Count) return;
            if (_isSpawningWave) return;

            var wave = _waveConfig.Waves[_currentWaveIndex];
            if (_gameTime >= wave.StartTime)
                StartWave(wave);
        }

        private void StartWave(WaveData wave)
        {
            _spawnQueue.Clear();

            for (int i = 0; i < wave.ScoutCount; i++)
                _spawnQueue.Enqueue(EnemyType.Scout);
            for (int i = 0; i < wave.GnawerCount; i++)
                _spawnQueue.Enqueue(EnemyType.Gnawer);
            for (int i = 0; i < wave.TankCount; i++)
                _spawnQueue.Enqueue(EnemyType.Tank);

            _currentIntraSpawnInterval = wave.IntraSpawnInterval;
            _intraSpawnTimer = 0f;
            _isSpawningWave = true;
        }

        private void ProcessSpawnQueue(float deltaTime)
        {
            if (!_isSpawningWave) return;

            _intraSpawnTimer -= deltaTime;
            if (_intraSpawnTimer > 0f) return;

            if (_spawnQueue.Count > 0)
            {
                SpawnEnemy(_spawnQueue.Dequeue());
                _intraSpawnTimer = _currentIntraSpawnInterval;
            }

            if (_spawnQueue.Count == 0)
            {
                _isSpawningWave = false;
                _currentWaveIndex++;
            }
        }

        private void SpawnEnemy(EnemyType type)
        {
            var stats = _enemyConfig.GetStats(type);
            var go = Instantiate(stats.Prefab, _spawnPoint.position, Quaternion.identity);
            var enemy = go.GetComponent<Enemy>();
            enemy.Initialize(stats, _centerTarget.transform, _centerTarget);
            enemy.OnDied += HandleEnemyDied;
            _activeEnemies.Add(enemy);
        }

        private void UpdateEnemies(float deltaTime)
        {
            for (int i = _activeEnemies.Count - 1; i >= 0; i--)
            {
                if (_activeEnemies[i] == null)
                {
                    _activeEnemies.RemoveAt(i);
                    continue;
                }

                _activeEnemies[i].Tick(deltaTime);
            }
        }

        private void HandleEnemyDied(Enemy enemy)
        {
            enemy.OnDied -= HandleEnemyDied;
            _activeEnemies.Remove(enemy);
        }
    }
}
