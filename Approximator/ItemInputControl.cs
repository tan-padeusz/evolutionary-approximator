namespace Controls;

public class ItemInputControl: ApproximatorControl
{
    private Label LabelControl { get; } = new Label();
    private ComboBox ValueControl { get; } = new ComboBox();

    public void ConfigureControl(string name, int xPosition, int yPosition)
    {
        this.LabelControl.Font = this.font;
        this.LabelControl.Location = new Point(xPosition, yPosition);
        this.LabelControl.Size = new Size(this.controlWidth, this.controlHeight);
        this.LabelControl.Text = name;
        this.LabelControl.TextAlign = ContentAlignment.MiddleCenter;
        this.ValueControl.DropDownStyle = ComboBoxStyle.DropDownList;
        this.ValueControl.Font = this.font;
        this.ValueControl.Location = new Point(xPosition, yPosition + this.controlHeight);
        this.ValueControl.Size = new Size(this.controlWidth, this.controlHeight);
    }

    public override Control[] GetControls()
    {
        return new Control[] { this.LabelControl, this.ValueControl };
    }

    public void Populate(Array values)
    {
        foreach (var value in values)
        {
            this.ValueControl.Items.Add(value);
        }
    }
}