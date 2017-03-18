using LivingThings.Enums;

namespace LivingThings
{
    public class Human
    {
        public Human(int age)
        {
            this.Age = age;

            if (age % 2 == 0)
            {
                this.Name = "Батката";
                this.Gender = GenderType.Male;
            }
            else
            {
                this.Name = "Мацето";
                this.Gender = GenderType.Female;
            }
        }

        public GenderType Gender { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}