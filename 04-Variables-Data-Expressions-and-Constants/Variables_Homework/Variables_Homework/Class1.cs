namespace Variables_Homework
{
    public class Class1
    {
        public void PrintStatistics(double[] statisticData, int count)
        {
            double max = double.MinValue;

            for (int i = 0; i < count; i++)
            {
                if (statisticData[i] > max)
                {
                    max = statisticData[i];
                }
            }

            PrintMax(max);

            double min = double.MaxValue;

            for (int i = 0; i < count; i++)
            {
                if (statisticData[i] < min)
                {
                    min = statisticData[i];
                }
            }

            PrintMin(min);

            double sum = 0;

            for (int i = 0; i < count; i++)
            {
                sum += statisticData[i];
            }

            double average = sum / count;

            PrintAvg(average);
        }
    }
}
