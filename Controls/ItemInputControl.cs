namespace Controls;

public class ItemInputControl
{
    private Label LabelControl { get; } = new Label();
    private ComboBox ValueControl { get; } = new ComboBox();

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
        this.ValueControl.DropDownStyle = ComboBoxStyle.DropDownList;
        this.ValueControl.Font = new Font(FontFamily.GenericMonospace, fontSize);
        this.ValueControl.Location = new Point(xPosition, yPosition + controlHeight);
        this.ValueControl.Size = new Size(controlWidth, controlHeight);
    }

    public void Populate(Array values)
    {
        foreach (var value in values)
        {
            
        }
    }
    
    private class EnumItem
    {
        public string Name { get; }
        public object Value { get; }
    }
}