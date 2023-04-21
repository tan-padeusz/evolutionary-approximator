namespace Controls;

public class NumericInputControl: ApproximatorControl
{
    private Label LabelControl { get; } = new Label();
    private NumericUpDown ValueControl { get; } = new NumericUpDown();

    public void ConfigureControl(string name, int xPosition, int yPosition)
    {
        this.LabelControl.Font = this.font;
        this.LabelControl.Location = new Point(xPosition, yPosition);
        this.LabelControl.Size = new Size(this.controlWidth, this.controlHeight);
        this.LabelControl.Text = name;
        this.LabelControl.TextAlign = ContentAlignment.MiddleCenter;
        this.ValueControl.Font = this.font;
        this.ValueControl.Location = new Point(xPosition, yPosition + this.controlHeight);
        this.ValueControl.Size = new Size(this.controlWidth, this.controlHeight);
    }

    public void ConfigureControlConstraints(int minimalValue, int maximalValue, int defaultValue, int deltaValue)
    {
        this.ValueControl.Increment = deltaValue;
        this.ValueControl.Maximum = maximalValue;
        this.ValueControl.Minimum = minimalValue;
        this.ValueControl.Value = defaultValue;
    }

    public override Control[] GetControls()
    {
        return new Control[] { this.LabelControl, this.ValueControl };
    }

    public int GetValue()
    {
        return (int) this.ValueControl.Value;
    }
}