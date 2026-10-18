namespace RaceDay.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string AgeRange { get; set; }
        public string Distance {  get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }
    }
}
