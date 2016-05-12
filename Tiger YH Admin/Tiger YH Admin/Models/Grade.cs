using FileHelpers;

namespace Tiger_YH_Admin.Models
{
    [DelimitedRecord("|")]
    public class Grade
    {
        public string GradeId => GetGradeId();
        public string StudentId { get; set; }
        public string CourseId { get; set; }
        public GradeLevel Result { get; set; } = GradeLevel.IG;

        private string GetGradeId()
        {
            return $"{CourseId}:{StudentId}";
        }
    }
}
