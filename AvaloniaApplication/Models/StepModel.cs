namespace AvaloniaApplication.Models
{
    public class StepModel
    {
        public int? Id { get; set; }
        public int ModeId { get; set; }
        public double Timer { get; set; }
        public string Destination { get; set; }
        public double Speed { get; set; }
        public string Type { get; set; }
        public double Volume { get; set; }
    }
}
