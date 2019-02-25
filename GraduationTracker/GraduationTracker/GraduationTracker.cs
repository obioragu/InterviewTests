using System;
using System.Linq;


namespace GraduationTracker
{
    public partial class GraduationTracker
    {

        public int credits { get; set; }
        public decimal average { get; set; }


        /// <summary>
        /// This function computes a student's average score and total credits for a given diploma program
        /// </summary>
        /// <param name="student"></param>
        /// <param name="diploma"></param>
        /// <param name="diplomareqId"></param>
        public void getStudentReqCreditsandAverage(Student student, Diploma diploma, int diplomareqId)
        {
            foreach (var stdcourse in student.Courses)
            {
                var requirement = Repository.GetRequirement(diplomareqId);

                foreach (int reqcourseId in requirement.Courses)
                {
                    if (requirement.Courses.Contains(stdcourse.Id))
                    {
                        average += stdcourse.Mark;
                        credits += stdcourse.Mark > requirement.MinimumMark ? requirement.Credits : 0;
                    }
                }
            }
        }

        public Tuple<bool, STANDING>  HasGraduated(Diploma diploma, Student student)
        {
            credits = 0;
            average = 0;

            //calculate student credits and average score for each diploma requirement
            diploma.Requirements.ToList().ForEach(x => getStudentReqCreditsandAverage(student, diploma, x));         
            average = average / student.Courses.Length;

            var standing = STANDING.None;

            if (average < 50)
                standing = STANDING.Remedial;
            else if (average < 80)
                standing = STANDING.Average;
            else if (average < 95)
                standing = STANDING.MagnaCumLaude;
            else
                standing = STANDING.SumaCumLaude;

            switch (standing)
            {
                case STANDING.Remedial:
                    return new Tuple<bool, STANDING>(false, standing);
                case STANDING.Average:
                    return new Tuple<bool, STANDING>(true, standing);
                case STANDING.SumaCumLaude:
                    return new Tuple<bool, STANDING>(true, standing);
                case STANDING.MagnaCumLaude:
                    return new Tuple<bool, STANDING>(true, standing);

                default:
                    return new Tuple<bool, STANDING>(false, standing);
            } 
        }
    }
}
