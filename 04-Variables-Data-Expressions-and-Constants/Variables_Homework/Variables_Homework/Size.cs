using System;

public class Size
{
    private double width;
    private double height;

    public Size(double width, double height)
    {
        this.Width = width;
        this.Height = height;
    }

    public double Width
    {
        get
        {
            return this.width;
        }
        set
        {
            this.width = value;
        }
    }

    public double Height
    {
        get
        {
            return this.height;
        }
        set
        {
            this.height = value;
        }
    }

    public static Size GetRotatedSize(Size size, double rotationAngle)
    {
        double sinusAlpha = Math.Abs(Math.Cos(rotationAngle));
        double cosinusAlpha = Math.Abs(Math.Sin(rotationAngle));

        double rotatedWidth = (sinusAlpha * size.width) + (cosinusAlpha * size.height);
        double rotatedHeight = (cosinusAlpha * size.width) + (sinusAlpha * size.height);

        Size rotatedSize = new Size(rotatedWidth, rotatedHeight);

        return rotatedSize;
    }
}