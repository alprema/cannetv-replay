namespace CanneTVReplay.Repositories
{
    public class Encounter
    {
        public int Id { get; set; }
        public int DurationInSeconds { get; set; }
        public IList<EncounterFileInfo> FileInfoList { get; set; }
        public Team RedTeam { get; set; }
        public Team BlueTeam { get; set; }
    }
}
