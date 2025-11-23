namespace MobiriseSanitizer.Classes
{
    [Serializable]
    internal class AppSettings
    {
        public string? ProjPath { get; set; }
        public string? ValueShort { get; set; }
        public string? ValueLong { get; set; }
        public bool DeleteProjectFile { get; set; }
        public bool AntiDragImages { get; set; }
        public string? CustomComment { get; set; }
    }
}