using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFlow_Loops
{
    public class Chef
    {
        public void Cook()
        {
            Potato potato = GetPotato();
            Carrot carrot = GetCarrot();

            Peel(potato);
            Peel(carrot);

            Cut(potato);
            Cut(carrot);

            Bowl bowl = GetBowl();
            bowl.Add(potato);
            bowl.Add(carrot);
        }

        private Potato GetPotato()
        {
            //...
        }

        private Carrot GetCarrot()
        {
            //...
        }

        private void Cut(Vegetable potato)
        {
            //...
        }

        private Bowl GetBowl()
        {
            //... 
        }
    }
}