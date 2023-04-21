namespace Controls;

public class OutputControl: ApproximatorControl
{
    private Label LabelControl { get; } = new Label();
    private Label ValueControl { get; } = new Label();

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
        this.ValueControl.TextAlign = ContentAlignment.MiddleCenter;
    }

    public override Control[] GetControls()
    {
        return new Control[] { this.LabelControl, this.ValueControl };
    }

    public void SetValue(string value)
    {
        this.ValueControl.Text = value;
    }
}