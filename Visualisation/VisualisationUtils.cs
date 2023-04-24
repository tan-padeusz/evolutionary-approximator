using System.Drawing;

namespace Visualisation;

public static class VisualisationUtils
{
    private static Graphics Graphics { get; } = Graphics.FromImage(new Bitmap(1000, 1000));

    public static void InitializeGraphics(PictureBox box)
    {
        var graphics = Graphics.FromImage(new Bitmap(box.Width, box.Height));
    }

    private static double pixelsPerOneX = 50.0;
    private static double pixelsPerOneY = 50.0;

    public static void Paint3DFunction(PictureBox box)
    {
        var graphics = Graphics.FromImage(box.Image);
        graphics.Clear(Color.Black);

        const int width1 = 1000;
        const int height1 = 1000;
        const int width2 = 400;
        const int height2 = 400;
        const int samplesNumber = 31;
        var xs = new int[samplesNumber];
        var ys = new int[samplesNumber];

        int leftMargin, rightMargin, topMargin, bottomMargin;
        int xCenter, yCenter;
        int i, j, prevI, prevJ, k;
        int p, q;
        double inversedXScale, xCoordinate, yCoordinate, zCoordinate;
        double range, x1Delta, x2Delta, y1Delta, y2Delta;

        leftMargin = 50;
        topMargin = 100;
        rightMargin = leftMargin + width1 + width2;
        bottomMargin = topMargin + height1 + height2;
        xCenter = (leftMargin + rightMargin) / 2;
        yCenter = (topMargin + bottomMargin) / 2;

        x1Delta = width1 / (samplesNumber - 1.0);
        x2Delta = width2 / (samplesNumber - 1.0);
        y1Delta = height1 / (samplesNumber - 1.0);
        y2Delta = height2 / (samplesNumber - 1.0);

        inversedXScale = x1Delta / pixelsPerOneX;

        range = 0.5 * (samplesNumber - 1.0);
        
        for (q = 0; q < samplesNumber; q++) for (p = 0; p < samplesNumber; p++)
        {
            i = leftMargin + width2 + (int) (x1Delta * p - x2Delta * q);
            j = topMargin + (int)(y1Delta * p + y2Delta * q);
            graphics.FillEllipse(Brushes.Blue, i - 3, j - 1, 6, 2);
        }
        
        graphics.DrawLine(Pens.Green, leftMargin, topMargin + height2, leftMargin + width2, topMargin);
        graphics.DrawLine(Pens.Green, leftMargin + width2, topMargin, rightMargin, topMargin + height1);
        graphics.DrawLine(Pens.Green, rightMargin, topMargin + height1, leftMargin + width1, bottomMargin);
        graphics.DrawLine(Pens.Green, leftMargin + width1, bottomMargin, leftMargin, topMargin + height2);
        graphics.DrawLine(Pens.Gray, xCenter, yCenter - 250, xCenter, yCenter);

        prevI = xCenter;
        prevJ = yCenter;
        for (q = 0; q < samplesNumber; q++) for (p = 0; p < samplesNumber; p++)
        {
            i = leftMargin + width2 + (int)(x1Delta * p - x2Delta * q);
            j = topMargin + (int)(y1Delta * p + y2Delta * q);
            xCoordinate = inversedXScale * (p - range);
            yCoordinate = inversedXScale * (q - range);
            zCoordinate = xCoordinate * yCoordinate;
            k = j - (int)(pixelsPerOneY * zCoordinate);
            if (p > 0) graphics.DrawLine(Pens.Red, prevI, prevJ, i, k);
            if (q > 0) graphics.DrawLine(Pens.Red, xs[p], ys[p], i, k);
            prevI = i;
            prevJ = k;
            xs[p] = i;
            ys[p] = k;
        }
        
        box.Refresh();
    }
}