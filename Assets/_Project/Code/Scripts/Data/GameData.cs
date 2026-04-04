using System;
using System.Collections.Generic;
using _Project.Code.Scripts.Configs;
using _Project.Code.Scripts.Garden;
using UnityEngine;

namespace _Project.Code.Scripts.Data
{
    public class GameData
    {
        private readonly GameConfig _config;

        public static GameData Instance;
        public Dictionary<ResourceType, int> Resources { get; set; } = new ();
        public event Action OnResourcesChanged; 
        
        public int ProductionProductivityMultiplier = 1;
        
        public Upgrades Upgrades = new();

        public GameData(GameConfig config)
        {
            _config = config;
        }
        
        public void Initialize()
        {
            Instance = this;
            
            GenerateResourceData();

            GenerateStartPlantsStates();
        }

        private void GenerateStartPlantsStates()
        {
            GeneratePlantUpgradesData(PlantType.Crystal);
            GeneratePlantUpgradesData(PlantType.NanoGel);
            GeneratePlantUpgradesData(PlantType.Polymer);
        }

        private void GeneratePlantUpgradesData(PlantType plantType)
        {
            var plantConfig = _config.GardenConfig.GetGrowableResourceData(plantType.GetResourceType());
            Upgrades.PlantUpgradesData.Add(plantType, new PlantUpgradeData
            {
                Productivity = plantConfig.DefaultProductivity,
                GrowTime = plantConfig.GrowthTime
            });
        }

        public void AddResource(ResourceType resourceType, int amount)
        {
            Resources[resourceType] += amount;
            OnResourcesChanged?.Invoke();
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