namespace RMS.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }

    public class UnitViewModel
    {
        public string UnitName { get; set; }
        public string UnitType { get; set; }
        public string UnitOwner { get; set; }
        public string Town { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
