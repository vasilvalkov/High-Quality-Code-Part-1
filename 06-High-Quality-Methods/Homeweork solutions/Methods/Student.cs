using System;

namespace Methods
{
    class Student
    {
        private DateTime birthDate;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherInfo { get; set; }
        public DateTime BirthDate
        {
            get
            {
                return this.birthDate;
            }
        }

        public Student(DateTime birthDate)
        {
            this.birthDate = birthDate;
        }
        
    }
}
