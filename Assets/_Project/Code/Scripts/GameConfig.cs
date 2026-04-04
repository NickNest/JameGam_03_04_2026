using UnityEngine;

namespace _Project.Code.Scripts
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
    public class GameConfig: ScriptableObject
    {
        public int CreditStartAmount;
        public int CrystalStartAmount;
        public int PolymerStartAmount;
        public int NanoGelStartAmount;
    }
}