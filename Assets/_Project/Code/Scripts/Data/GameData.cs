using System.Collections.Generic;
using _Project.Code.Scripts.Configs;

namespace _Project.Code.Scripts.Data
{
    public class GameData
    {
        private readonly GameConfig _config;

        public static GameData Instance;
        public Dictionary<ResourceType, int> Resources { get; set; } = new ();
        public int ProductionProductivityMultiplier = 1;

        public GameData(GameConfig config)
        {
            _config = config;
        }
        
        public void Initialize()
        {
            Instance = this;
            
            GenerateResourceData();
        }

        public void AddResource(ResourceType resourceType, int amount)
        {
            Resources[resourceType] += amount;
        }

        private void GenerateResourceData()
        {
            Resources.Add(ResourceType.Crystal, GetResourceStartAmount(ResourceType.Crystal));
            Resources.Add(ResourceType.Polymer, GetResourceStartAmount(ResourceType.Polymer));
            Resources.Add(ResourceType.NanoGel, GetResourceStartAmount(ResourceType.NanoGel));
            Resources.Add(ResourceType.Credit, _config.GardenConfig.CreditStartAmount);
        }

        private int GetResourceStartAmount(ResourceType resourceType) => 
            _config.GardenConfig.GetGrowableResourceData(resourceType).StartAmount;
    }
}