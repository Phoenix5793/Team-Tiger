using System;
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
        public string CourseGoal { get; set; }

        private string GetGradeId()
        {
            if (string.IsNullOrEmpty(CourseGoal))
            {
                return $"{CourseId}:{StudentId}";
            }
            else
            {
                return $"{CourseId}:{StudentId}:{CourseGoal}";
            }
        }
    }
}
