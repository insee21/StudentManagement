using System;

namespace StudentManagement
{
    public class Student
    {
        public string FullName { get; set; }     // ФИО
        public string Group { get; set; }        // Группа
        public DateTime BirthDate { get; set; }  // Дата рождения
        public string Phone { get; set; }        // Номер телефона
        public double Gpa { get; set; }          // Средний балл
    }
}
