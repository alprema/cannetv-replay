namespace CanneTVReplay.Repositories
{
    public class Team
    {
        public string Name { get; set; }
        public IList<Fighter> Members { get; set; }
    }
}
