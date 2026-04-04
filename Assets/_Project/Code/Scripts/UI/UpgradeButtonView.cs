using System;
using System.Globalization;
using _Project.Code.Scripts.Data;
using TMPro;
using UnityEngine;

namespace _Project.Code.Scripts.UI
{
    public class UpgradeButtonView: MonoBehaviour
    {
        [SerializeField] private UpgradeType _type;
        [SerializeField] private TMP_Text _currentMultiplier;
        [SerializeField] private TMP_Text _nextMultiplier;
        [SerializeField] private TMP_Text _upgradeCost;

        private int _upgradeStep;

        public void Initialize()
        {
            var gameData = GameData.Instance;
            
            var currentMultiplier = _type switch
            {
                UpgradeType.GrowSpeed => gameData.GrowSpeedMultiplier.ToString(CultureInfo.InvariantCulture),
                UpgradeType.Produce => gameData.ProduceMultiplier.ToString(),
                UpgradeType.CraftSpeed => gameData.CraftingMultiplier.ToString(CultureInfo.InvariantCulture)
            };
            
            var config = gameData.GetConfig();

            var nextMultiplier = string.Empty;
            switch (_type)
            {
                case UpgradeType.GrowSpeed:
                    var growMultipliers = config.UpgradesConfig.GrowMultipliers;
                    if (_upgradeStep + 1 < growMultipliers.Length)
                    {
                        nextMultiplier = growMultipliers[_upgradeStep + 1].ToString(CultureInfo.InvariantCulture);
                    }
                    break;
                case UpgradeType.Produce:
                    var produceMultipliers = config.UpgradesConfig.ProduceMultipliers;
                    if (_upgradeStep + 1 < produceMultipliers.Length)
                    {
                        nextMultiplier = produceMultipliers[_upgradeStep + 1].ToString();
                    }
                    break;
                case UpgradeType.CraftSpeed:
                    var craftMultipliers = config.UpgradesConfig.CraftingSpeedMultipliers;
                    if (_upgradeStep + 1 < craftMultipliers.Length)
                    {
                        nextMultiplier = craftMultipliers[_upgradeStep + 1].ToString(CultureInfo.InvariantCulture);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_type));
            }

            _currentMultiplier.text = $"x{currentMultiplier}";
            _nextMultiplier.text = $"x{nextMultiplier}";
        }
    }
}