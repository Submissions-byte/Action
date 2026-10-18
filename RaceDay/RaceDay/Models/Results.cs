namespace RaceDay.Models
{
    public class Results
    {
        public int ResultID {  get; set; }
        public TimeOnly FinishingTime {  get; set; }
        public int FinishingPosition { get; set; }
        public int EnrolmentID { get; set; }
        public Enrolment enrolment { get; set; }
    }
}
