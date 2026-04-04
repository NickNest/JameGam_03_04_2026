using System.Threading.Tasks;
using _Project.Code.Scripts.Configs;
using UnityEngine;

namespace _Project.Code.Scripts
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig/GameConfig")]
    public class GameConfig: ScriptableObject
    {
        public GardenConfig GardenConfig;
        public TaskConfig TaskConfig;
    }
}