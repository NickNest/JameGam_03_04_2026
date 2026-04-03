using UnityEngine;

namespace _Project.Code.Scripts
{
    [CreateAssetMenu(fileName = "GameData", menuName = "GameData")]
    public class GameConfig: ScriptableObject
    {
        public int CreditStartAmount;
        public int CrystalStartAmount;
        public int PolymerStartAmount;
        public int NanoGelStartAmount;
    }
}