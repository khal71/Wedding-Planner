namespace WeddingPlannerDomain
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] ImageData { get; set; }

        public Item(int id,string name, string type, byte[] imageData)
        {
            Id = id;
            Name = name;
            Type = type;
            ImageData = imageData;
        }
        protected Item() { }

    }
}