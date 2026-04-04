using System.Collections.Generic;

namespace _Project.Code.Scripts.Data
{
    public class GameData
    {
        private readonly GameConfig _config;

        public static GameData Instance;
        public Dictionary<ResourceType, ResourceData> Resources { get; set; } = new ();

        public GameData(GameConfig config)
        {
            _config = config;
        }
        
        public void Initialize()
        {
            Instance = this;
            
            GenerateResourceData();
        }

        private void GenerateResourceData()
        {
            Resources.Add(ResourceType.Crystal, new ResourceData(ResourceType.Crystal, _config.CrystalStartAmount));
            Resources.Add(ResourceType.Polymer, new ResourceData(ResourceType.Polymer, _config.PolymerStartAmount));
            Resources.Add(ResourceType.NanoGel, new ResourceData(ResourceType.NanoGel, _config.NanoGelStartAmount));
            Resources.Add(ResourceType.Credit, new ResourceData(ResourceType.Credit, _config.CreditStartAmount));
        }
    }
}