namespace StudentApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public int Age { get; set; }               
        public string Email { get; set; } = string.Empty;
        public double AverageGrade { get; set; } = 0.0;
    }
}