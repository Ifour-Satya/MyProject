namespace InMemoryCachingDemo.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        // Navigation property to states belonging to this country
        public List<State> States { get; set; }
    }
}
