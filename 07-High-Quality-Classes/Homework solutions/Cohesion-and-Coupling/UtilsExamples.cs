using System;

namespace CohesionAndCoupling
{
    class UtilsExamples
    {
        static void Main()
        {
            Console.WriteLine(FileName.GetExtension("example"));
            Console.WriteLine(FileName.GetExtension("example.pdf"));
            Console.WriteLine(FileName.GetExtension("example.new.pdf"));

            Console.WriteLine(FileName.GetName("example"));
            Console.WriteLine(FileName.GetName("example.pdf"));
            Console.WriteLine(FileName.GetName("example.new.pdf"));

            Console.WriteLine("Distance in the 2D space = {0:f2}",
                Space3D.CalcDistance2D(1, -2, 3, 4));
            Console.WriteLine("Distance in the 3D space = {0:f2}",
                Space3D.CalcDistance3D(5, 2, -1, 3, -6, 4));

            Parallelipiped quadrangle = new Parallelipiped(3, 4, 5);
            Console.WriteLine("Volume = {0:f2}", quadrangle.CalcVolume());

            Console.WriteLine("Diagonal XYZ = {0:f2}", Space3D.CalcDiagonalXYZ(3, 4, 5));
            Console.WriteLine("Diagonal XY = {0:f2}", Space3D.CalcDiagonalXY(3, 4));
            Console.WriteLine("Diagonal XZ = {0:f2}", Space3D.CalcDiagonalXZ(3, 5));
            Console.WriteLine("Diagonal YZ = {0:f2}", Space3D.CalcDiagonalYZ(4, 5));
        }
    }
}
