using System;

namespace CohesionAndCoupling
{
    public class Parallelipiped
    {
        private double width;
        private double height;
        private double depth;

        public Parallelipiped(double width, double height, double depth)
        {
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
        }

        public double Width
        {
            get
            {
                return this.width;
            }
            private set
            {
                CheckIfPositive(value);
                this.width = value;
            }
        }

        public double Height
        {
            get
            {
                return this.height;
            }
            private set
            {
                CheckIfPositive(value);
                this.height = value;
            }
        }

        public double Depth
        {
            get
            {
                return this.depth;
            }
            private set
            {
                CheckIfPositive(value);
                this.depth = value;
            }
        }

        public double CalcVolume()
        {
            double volume = this.Width * this.Height * this.Depth;
            return volume;
        }

        private void CheckIfPositive(double number)
        {
            if (number <= 0)
            {
                throw new ArgumentException("All sides must be positive numbers!");
            }
        }
    }
}
