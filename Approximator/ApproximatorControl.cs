namespace Controls;

public abstract class ApproximatorControl
{
    protected int controlWidth = 300;
    protected int controlHeight = 30;
    protected Font font = new Font(FontFamily.GenericMonospace, 16F);

    public abstract Control[] GetControls();
}