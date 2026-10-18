namespace RaceDay.Models
{
   // public enum Type { Run, Swim, Cycle }
    public class Enrolment
    {
        public int EnrolmentID { get; set; }
        public int ParticipantID { get; set; }
        public Participant participant { get; set; }
        public int EventID { get; set; }
        public Event @event { get; set; }
        public int CategoryID { get; set; }
        public Category category { get; set; }
        public DateOnly EnrolmentDate { get; set; }
        public string Status { get; set; }
    }
}
