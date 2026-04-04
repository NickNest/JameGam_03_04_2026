using UnityEngine;

namespace _Project.Code.Scripts.Configs
{
    [CreateAssetMenu(fileName = "UpgradesConfig", menuName = "Configs/UpgradesConfig")]
    public class UpgradesConfig: ScriptableObject
    {
        public float[] GrowMultipliers;
        public int[] ProduceMultipliers;
        public float[] CraftingSpeedMultipliers;
    }
}