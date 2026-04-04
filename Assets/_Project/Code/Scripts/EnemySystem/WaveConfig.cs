using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Scripts.EnemySystem
{
    [Serializable]
    public struct WaveData
    {
        public int WaveId;
        public float StartTime;
        public int ScoutCount;
        public int GnawerCount;
        public int TankCount;
        public float IntraSpawnInterval;
        [TextArea] public string Comment;
    }

    [CreateAssetMenu(fileName = "WaveConfig", menuName = "GameConfig/WaveConfig")]
    public class WaveConfig : ScriptableObject
    {
        public List<WaveData> Waves;

        /// <summary>
        /// Заполняет все 29 волн из GDD.
        /// ПКМ по ассету → Fill Default Waves.
        /// </summary>
        [ContextMenu("Fill Default Waves")]
        private void FillDefaultWaves()
        {
            Waves = new List<WaveData>
            {
                new() { WaveId = 1,  StartTime = 20f,   ScoutCount = 2, GnawerCount = 0, TankCount = 0, IntraSpawnInterval = 0.6f },
                new() { WaveId = 2,  StartTime = 40f,   ScoutCount = 3, GnawerCount = 0, TankCount = 0, IntraSpawnInterval = 0.5f },
                new() { WaveId = 3,  StartTime = 60f,   ScoutCount = 2, GnawerCount = 1, TankCount = 0, IntraSpawnInterval = 0.5f },
                new() { WaveId = 4,  StartTime = 80f,   ScoutCount = 4, GnawerCount = 0, TankCount = 0, IntraSpawnInterval = 0.45f },
                new() { WaveId = 5,  StartTime = 98f,   ScoutCount = 3, GnawerCount = 1, TankCount = 0, IntraSpawnInterval = 0.45f },
                new() { WaveId = 6,  StartTime = 118f,  ScoutCount = 2, GnawerCount = 2, TankCount = 0, IntraSpawnInterval = 0.45f },
                new() { WaveId = 7,  StartTime = 140f,  ScoutCount = 5, GnawerCount = 0, TankCount = 0, IntraSpawnInterval = 0.4f },
                new() { WaveId = 8,  StartTime = 158f,  ScoutCount = 3, GnawerCount = 2, TankCount = 0, IntraSpawnInterval = 0.4f },
                new() { WaveId = 9,  StartTime = 180f,  ScoutCount = 2, GnawerCount = 3, TankCount = 0, IntraSpawnInterval = 0.4f },
                new() { WaveId = 10, StartTime = 200f,  ScoutCount = 4, GnawerCount = 2, TankCount = 0, IntraSpawnInterval = 0.4f },
                new() { WaveId = 11, StartTime = 220f,  ScoutCount = 2, GnawerCount = 3, TankCount = 0, IntraSpawnInterval = 0.4f },
                new() { WaveId = 12, StartTime = 240f,  ScoutCount = 3, GnawerCount = 2, TankCount = 1, IntraSpawnInterval = 0.45f, Comment = "Первый танк" },
                new() { WaveId = 13, StartTime = 265f,  ScoutCount = 0, GnawerCount = 4, TankCount = 0, IntraSpawnInterval = 0.35f },
                new() { WaveId = 14, StartTime = 283f,  ScoutCount = 5, GnawerCount = 2, TankCount = 0, IntraSpawnInterval = 0.35f },
                new() { WaveId = 15, StartTime = 305f,  ScoutCount = 2, GnawerCount = 3, TankCount = 1, IntraSpawnInterval = 0.45f },
                new() { WaveId = 16, StartTime = 330f,  ScoutCount = 6, GnawerCount = 0, TankCount = 1, IntraSpawnInterval = 0.45f },
                new() { WaveId = 17, StartTime = 350f,  ScoutCount = 4, GnawerCount = 3, TankCount = 0, IntraSpawnInterval = 0.35f },
                new() { WaveId = 18, StartTime = 370f,  ScoutCount = 2, GnawerCount = 4, TankCount = 1, IntraSpawnInterval = 0.4f },
                new() { WaveId = 19, StartTime = 390f,  ScoutCount = 5, GnawerCount = 3, TankCount = 0, IntraSpawnInterval = 0.35f },
                new() { WaveId = 20, StartTime = 408f,  ScoutCount = 3, GnawerCount = 3, TankCount = 1, IntraSpawnInterval = 0.4f },
                new() { WaveId = 21, StartTime = 428f,  ScoutCount = 6, GnawerCount = 2, TankCount = 0, IntraSpawnInterval = 0.35f },
                new() { WaveId = 22, StartTime = 446f,  ScoutCount = 2, GnawerCount = 4, TankCount = 1, IntraSpawnInterval = 0.4f },
                new() { WaveId = 23, StartTime = 468f,  ScoutCount = 3, GnawerCount = 2, TankCount = 2, IntraSpawnInterval = 0.45f },
                new() { WaveId = 24, StartTime = 493f,  ScoutCount = 8, GnawerCount = 0, TankCount = 0, IntraSpawnInterval = 0.3f },
                new() { WaveId = 25, StartTime = 508f,  ScoutCount = 4, GnawerCount = 4, TankCount = 0, IntraSpawnInterval = 0.35f },
                new() { WaveId = 26, StartTime = 526f,  ScoutCount = 2, GnawerCount = 5, TankCount = 1, IntraSpawnInterval = 0.4f },
                new() { WaveId = 27, StartTime = 546f,  ScoutCount = 5, GnawerCount = 2, TankCount = 2, IntraSpawnInterval = 0.45f },
                new() { WaveId = 28, StartTime = 568f,  ScoutCount = 3, GnawerCount = 5, TankCount = 1, IntraSpawnInterval = 0.4f },
                new() { WaveId = 29, StartTime = 586f,  ScoutCount = 6, GnawerCount = 3, TankCount = 1, IntraSpawnInterval = 0.4f, Comment = "Финальная волна" },
            };
        }
    }
}
