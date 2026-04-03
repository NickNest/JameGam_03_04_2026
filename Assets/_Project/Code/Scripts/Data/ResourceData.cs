namespace _Project.Code.Scripts.Data
{
    public class ResourceData
    {
        public ResourceType Type { get; set; }
        public int Amount { get; set; }

        public ResourceData(ResourceType type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}