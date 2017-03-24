using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceAndPolymorphism
{
    public class Course
    {
        private string name;
        private string teacherName;
        private IList<string> students;

        public Course(string name, string teacherName, IList<string> students)
        {
            this.Name = name;
            this.TeacherName = teacherName;
            this.Students = students;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Course name cannot be null or empty");
                }

                this.name = value;
            }
        }

        public string TeacherName { get; set; }

        public IList<string> Students
        {
            get
            {
                return this.students;
            }
            private set
            {
                if (value == null)
                {
                    this.students = new List<string>();
                }

                this.students = value;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("{ Name = ");
            result.Append(this.Name);

            if (this.TeacherName != null)
            {
                result.Append("; Teacher = ");
                result.Append(this.TeacherName);
            }

            result.Append("; Students = ");
            result.Append(this.GetStudentsAsString());

            return result.ToString();
        }

        protected string GetStudentsAsString()
        {
            if (this.Students == null || this.Students.Count == 0)
            {
                return "{ }";
            }
            else
            {
                return "{ " + string.Join(", ", this.Students) + " }";
            }
        }        
    }
}
