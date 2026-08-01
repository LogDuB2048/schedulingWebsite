namespace personal_assigment.Models
{
    //class meant to hold multiple classes so we can pass class to a page, holds student array,coursee array,instrucotrs array,and studentcourse array
    public class Multmodel
    {
        public Student[] m_students { get; set; }
        public Course[] m_courses { get; set; }

        public Instrucotors[] m_instrucoors { get; set; }

        public StudentCourses[] m_studentCourses { get; set; }
    }
}
