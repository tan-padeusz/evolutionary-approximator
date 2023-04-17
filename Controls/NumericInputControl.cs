namespace Controls;

public class NumericInputControl
{
    private Label LabelControl { get; } = new Label();
    private NumericUpDown ValueControl { get; } = new NumericUpDown();

    public void ConfigureControl(string name, int xPosition, int yPosition)
    {
        const int controlWidth = 300;
        const int controlHeight = 30;
        const float fontSize = 16;
        this.LabelControl.Font = new Font(FontFamily.GenericMonospace, fontSize);
        this.LabelControl.Location = new Point(xPosition, yPosition);
        this.LabelControl.Size = new Size(controlWidth, controlHeight);
        this.LabelControl.Text = name;
        this.LabelControl.TextAlign = ContentAlignment.MiddleCenter;
        this.ValueControl.Font = new Font(FontFamily.GenericMonospace, fontSize);
        this.ValueControl.Location = new Point(xPosition, yPosition + controlHeight);
        this.ValueControl.Size = new Size(controlWidth, controlHeight);
    }

    public void ConfigureControlConstraints(int minimalValue, int maximalValue, int defaultValue, int deltaValue)
    {
        this.ValueControl.Increment = deltaValue;
        this.ValueControl.Maximum = maximalValue;
        this.ValueControl.Minimum = minimalValue;
        this.ValueControl.Value = defaultValue;
    }

    public Control[] GetControls()
    {
        return new Control[] { this.LabelControl, this.ValueControl };
    }

    public int GetValue()
    {
        return (int) this.ValueControl.Value;
    }
}