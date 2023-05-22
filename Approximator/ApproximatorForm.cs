using System.ComponentModel;
using System.Globalization;
using System.Text;
using Enums;
using Data;
using Point = System.Drawing.Point;
using Timer = System.Windows.Forms.Timer;

namespace Approximator;

public class ApproximatorForm: Form
{
    #region Generated

    private IContainer Components { get; } = new Container();
    protected override void Dispose(bool disposing)
    {
        if (disposing) this.Components.Dispose();
        base.Dispose(disposing);
    }

    #endregion
    
    #region Decoration Controls

    private Label TopHorizontalLine { get; } = new Label();
    private Label MiddleHorizontalLine { get; } = new Label();
    private Label BottomHorizontalLine { get; } = new Label();
    private Label VerticalLine { get; } = new Label();

    #endregion

    #region Input/Output Controls

    private Label PointNumberLabelControl { get; } = ApproximatorForm.NewLabel();
    private NumericUpDown PointNumberValueControl { get; } = ApproximatorForm.NewNumericUpDown();
    private Button GeneratePointsButton { get; } = ApproximatorForm.NewButton();

    private Label GeneTypeLabelControl { get; } = ApproximatorForm.NewLabel();
    private ComboBox GeneTypeValueControl { get; } = ApproximatorForm.NewComboBox();
    private Label ErrorMetricLabelControl { get; } = ApproximatorForm.NewLabel();
    private ComboBox ErrorMetricValueControl { get; } = ApproximatorForm.NewComboBox();

    private Label MaxPolynomialDegreeLabelControl { get; } = ApproximatorForm.NewLabel();
    private NumericUpDown MaxPolynomialDegreeValueControl { get; } = ApproximatorForm.NewNumericUpDown();
    private Label PrecisionDigitsLabelControl { get; } = ApproximatorForm.NewLabel();
    private NumericUpDown PrecisionDigitsValueControl { get; } = ApproximatorForm.NewNumericUpDown();
    private Label PopulationSizeLabelControl { get; } = ApproximatorForm.NewLabel();
    private NumericUpDown PopulationSizeValueControl { get; } = ApproximatorForm.NewNumericUpDown();
    private Label ParentPoolSizeLabelControl { get; } = ApproximatorForm.NewLabel();
    private NumericUpDown ParentPoolSizeValueControl { get; } = ApproximatorForm.NewNumericUpDown();
    private Label DominantParentGeneStrengthLabelControl { get; } = ApproximatorForm.NewLabel();
    private NumericUpDown DominantParentGeneStrengthValueControl { get; } = ApproximatorForm.NewNumericUpDown();
    private Label MutationProbabilityLabelControl { get; } = ApproximatorForm.NewLabel();
    private NumericUpDown MutationProbabilityValueControl { get; } = ApproximatorForm.NewNumericUpDown();

    private Label ElapsedTimeLabelControl { get; } = ApproximatorForm.NewLabel();
    private Label ElapsedTimeValueControl { get; } = ApproximatorForm.NewLabel();
    private Label AverageErrorLabelControl { get; } = ApproximatorForm.NewLabel();
    private Label AverageErrorValueControl { get; } = ApproximatorForm.NewLabel();
    private Label GenerationsEvaluatedLabelControl { get; } = ApproximatorForm.NewLabel();
    private Label GenerationsEvaluatedValueControl { get; } = ApproximatorForm.NewLabel();
    private Label LastImprovementLabelControl { get; } = ApproximatorForm.NewLabel();
    private Label LastImprovementValueControl { get; } = ApproximatorForm.NewLabel();

    private Button StartEngineButton { get; } = ApproximatorForm.NewButton();
    private Button StopEngineButton { get; } = ApproximatorForm.NewButton();

    private RichTextBox PointTableControl { get; } = new RichTextBox();

    #endregion
    
    #region Visualisation Controls

    private VScrollBar ScaleYScrollBar { get; } = new VScrollBar();
    private HScrollBar ScaleXScrollBar { get; } = new HScrollBar();
    private PictureBox Plot3DPictureBox { get; } = new PictureBox();

    #endregion
    
    private static Font ControlFont => new Font(FontFamily.GenericMonospace, 16F);
    private static int ControlHeight => 30;
    private static int ControlWidth => 400;
    private ApproximatorEngine ApproximatorEngine { get; }
    private Timer UpdateInterfaceTimer { get; } = new Timer();

    public ApproximatorForm()
    {
        this.ApproximatorEngine = new ApproximatorEngine(this);
        this.ConfigureDecorationControls();
        this.ConfigureInputOutputControls();
        this.ConfigureVisualizationControls();
        this.ConfigureUpdateInterfaceTimer();
        this.ConfigureForm();
    }
    
    private void ConfigureDecorationControls()
    {
        this.TopHorizontalLine.BackColor = Color.DarkGray;
        this.TopHorizontalLine.Location = new Point(10, 138);
        this.TopHorizontalLine.Size = new Size(400, 5);
        this.Controls.Add(this.TopHorizontalLine);

        this.MiddleHorizontalLine.BackColor = Color.DarkGray;
        this.MiddleHorizontalLine.Location = new Point(10, 638);
        this.MiddleHorizontalLine.Size = new Size(400, 5);
        this.Controls.Add(this.MiddleHorizontalLine);

        this.BottomHorizontalLine.BackColor = Color.DarkGray;
        this.BottomHorizontalLine.Location = new Point(10, 898);
        this.BottomHorizontalLine.Size = new Size(400, 5);
        this.Controls.Add(this.BottomHorizontalLine);

        this.VerticalLine.BackColor = Color.DarkGray;
        this.VerticalLine.Location = new Point(878, 10);
        this.VerticalLine.Size = new Size(5, 1020);
        this.Controls.Add(this.VerticalLine);
    }

    private void ConfigureInputOutputControls()
    {
        this.PointNumberLabelControl.Location = new Point(10, 10);
        this.PointNumberLabelControl.Text = "POINT NUMBER";
        this.Controls.Add(this.PointNumberLabelControl);

        this.PointNumberValueControl.Increment = 3;
        this.PointNumberValueControl.Location = new Point(10, 40);
        this.PointNumberValueControl.Maximum = 100;
        this.PointNumberValueControl.Minimum = 1;
        this.PointNumberValueControl.Value = 25;
        this.Controls.Add(this.PointNumberValueControl);

        this.GeneratePointsButton.Click += this.GeneratePointsButtonClick;
        this.GeneratePointsButton.Location = new Point(10, 70);
        this.GeneratePointsButton.Text = "GENERATE POINTS";
        this.Controls.Add(this.GeneratePointsButton);
        
        // line at y = 140

        this.GeneTypeLabelControl.Location = new Point(10, 150);
        this.GeneTypeLabelControl.Text = "GENE TYPE";
        this.Controls.Add(this.GeneTypeLabelControl);

        this.GeneTypeValueControl.DisplayMember = "Name";
        this.GeneTypeValueControl.Location = new Point(10, 180);
        this.GeneTypeValueControl.ValueMember = "Value";
        foreach (var value in Enum.GetValues<GeneType>())
            this.GeneTypeValueControl.Items.Add(new EnumItem(value));
        this.GeneTypeValueControl.SelectedIndex = 2;
        this.Controls.Add(this.GeneTypeValueControl);

        this.ErrorMetricLabelControl.Location = new Point(10, 210);
        this.ErrorMetricLabelControl.Text = "ERROR METRIC";
        this.Controls.Add(this.ErrorMetricLabelControl);

        this.ErrorMetricValueControl.DisplayMember = "Name";
        this.ErrorMetricValueControl.Location = new Point(10, 240);
        this.ErrorMetricValueControl.ValueMember = "Value";
        foreach (var value in Enum.GetValues<ErrorMetric>())
            this.ErrorMetricValueControl.Items.Add(new EnumItem(value));
        this.ErrorMetricValueControl.SelectedIndex = 1;
        this.Controls.Add(this.ErrorMetricValueControl);

        this.MaxPolynomialDegreeLabelControl.Location = new Point(10, 270);
        this.MaxPolynomialDegreeLabelControl.Text = "MAX POLYNOMIAL DEGREE";
        this.Controls.Add(this.MaxPolynomialDegreeLabelControl);

        this.MaxPolynomialDegreeValueControl.Increment = 1;
        this.MaxPolynomialDegreeValueControl.Location = new Point(10, 300);
        this.MaxPolynomialDegreeValueControl.Maximum = 5;
        this.MaxPolynomialDegreeValueControl.Minimum = 0;
        this.MaxPolynomialDegreeValueControl.Value = 2;
        this.Controls.Add(this.MaxPolynomialDegreeValueControl);

        this.PrecisionDigitsLabelControl.Location = new Point(10, 330);
        this.PrecisionDigitsLabelControl.Text = "PRECISION DIGITS";
        this.Controls.Add(this.PrecisionDigitsLabelControl);

        this.PrecisionDigitsValueControl.Increment = 1;
        this.PrecisionDigitsValueControl.Location = new Point(10, 360);
        this.PrecisionDigitsValueControl.Maximum = 5;
        this.PrecisionDigitsValueControl.Minimum = 0;
        this.PrecisionDigitsValueControl.Value = 3;
        this.Controls.Add(this.PrecisionDigitsValueControl);

        this.PopulationSizeLabelControl.Location = new Point(10, 390);
        this.PopulationSizeLabelControl.Text = "POPULATION SIZE";
        this.Controls.Add(this.PopulationSizeLabelControl);

        this.PopulationSizeValueControl.Increment = 25;
        this.PopulationSizeValueControl.Location = new Point(10, 420);
        this.PopulationSizeValueControl.Maximum = 800;
        this.PopulationSizeValueControl.Minimum = 200;
        this.PopulationSizeValueControl.Value = 400;
        this.Controls.Add(this.PopulationSizeValueControl);

        this.ParentPoolSizeLabelControl.Location = new Point(10, 450);
        this.ParentPoolSizeLabelControl.Text = "PARENT POOL SIZE";
        this.Controls.Add(this.ParentPoolSizeLabelControl);

        this.ParentPoolSizeValueControl.Increment = 20;
        this.ParentPoolSizeValueControl.Location = new Point(10, 480);
        this.ParentPoolSizeValueControl.Maximum = 200;
        this.ParentPoolSizeValueControl.Minimum = 20;
        this.ParentPoolSizeValueControl.Value = 80;
        this.Controls.Add(this.ParentPoolSizeValueControl);

        this.DominantParentGeneStrengthLabelControl.Location = new Point(10, 510);
        this.DominantParentGeneStrengthLabelControl.Text = "DOMINANT PARENT GENE STRENGTH";
        this.Controls.Add(this.DominantParentGeneStrengthLabelControl);

        this.DominantParentGeneStrengthValueControl.Increment = 20;
        this.DominantParentGeneStrengthValueControl.Location = new Point(10, 540);
        this.DominantParentGeneStrengthValueControl.Maximum = 1000;
        this.DominantParentGeneStrengthValueControl.Minimum = 0;
        this.DominantParentGeneStrengthValueControl.Value = 600;
        this.Controls.Add(this.DominantParentGeneStrengthValueControl);

        this.MutationProbabilityLabelControl.Location = new Point(10, 570);
        this.MutationProbabilityLabelControl.Text = "MUTATION PROBABILITY";
        this.Controls.Add(this.MutationProbabilityLabelControl);

        this.MutationProbabilityValueControl.Increment = 20;
        this.MutationProbabilityValueControl.Location = new Point(10, 600);
        this.MutationProbabilityValueControl.Maximum = 1000;
        this.MutationProbabilityValueControl.Minimum = 0;
        this.MutationProbabilityValueControl.Value = 100;
        this.Controls.Add(this.MutationProbabilityValueControl);
        
        // line at y = 640

        this.ElapsedTimeLabelControl.Location = new Point(10, 650);
        this.ElapsedTimeLabelControl.Text = "ELAPSED TIME";
        this.Controls.Add(this.ElapsedTimeLabelControl);

        this.ElapsedTimeValueControl.Location = new Point(10, 680);
        this.ElapsedTimeValueControl.Text = "";
        this.Controls.Add(this.ElapsedTimeValueControl);

        this.AverageErrorLabelControl.Location = new Point(10, 710);
        this.AverageErrorLabelControl.Text = "AVERAGE ERROR";
        this.Controls.Add(this.AverageErrorLabelControl);

        this.AverageErrorValueControl.Location = new Point(10, 740);
        this.AverageErrorValueControl.Text = "";
        this.Controls.Add(this.AverageErrorValueControl);

        this.GenerationsEvaluatedLabelControl.Location = new Point(10, 770);
        this.GenerationsEvaluatedLabelControl.Text = "GENERATIONS EVALUATED";
        this.Controls.Add(this.GenerationsEvaluatedLabelControl);

        this.GenerationsEvaluatedValueControl.Location = new Point(10, 800);
        this.GenerationsEvaluatedValueControl.Text = "";
        this.Controls.Add(this.GenerationsEvaluatedValueControl);

        this.LastImprovementLabelControl.Location = new Point(10, 830);
        this.LastImprovementLabelControl.Text = "LAST IMPROVEMENT";
        this.Controls.Add(this.LastImprovementLabelControl);

        this.LastImprovementValueControl.Location = new Point(10, 860);
        this.LastImprovementValueControl.Text = "";
        this.Controls.Add(this.LastImprovementValueControl);
        
        // line at y = 900

        this.StartEngineButton.Click += this.StartEngineButtonClick;
        this.StartEngineButton.Location = new Point(10, 910);
        this.StartEngineButton.Text = "START";
        this.Controls.Add(this.StartEngineButton);

        this.StopEngineButton.Click += this.StopEngineButtonClick;
        this.StopEngineButton.Location = new Point(10, 970);
        this.StopEngineButton.Text = "STOP";
        this.Controls.Add(this.StopEngineButton);

        this.PointTableControl.BackColor = Color.White;
        this.PointTableControl.Font = ApproximatorForm.ControlFont;
        this.PointTableControl.Location = new Point(420, 10);
        this.PointTableControl.ReadOnly = true;
        this.PointTableControl.Size = new Size(450, 1020);
        this.Controls.Add(this.PointTableControl);
    }

    private void ConfigureVisualizationControls()
    {
        this.ScaleYScrollBar.LargeChange = 1;
        this.ScaleYScrollBar.Location = new Point(890, 10);
        this.ScaleYScrollBar.Maximum = 25;
        this.ScaleYScrollBar.Minimum = 1;
        this.ScaleYScrollBar.Size = new Size(60, 950);
        this.ScaleYScrollBar.SmallChange = 1;
        this.ScaleYScrollBar.Value = 20;
        this.Controls.Add(this.ScaleYScrollBar);

        this.ScaleXScrollBar.LargeChange = 1;
        this.ScaleXScrollBar.Location = new Point(960, 970);
        this.ScaleXScrollBar.Maximum = 25;
        this.ScaleXScrollBar.Minimum = 1;
        this.ScaleXScrollBar.Size = new Size(950, 60);
        this.ScaleXScrollBar.SmallChange = 1;
        this.ScaleXScrollBar.Value = 5;
        this.Controls.Add(this.ScaleXScrollBar);

        this.Plot3DPictureBox.BackColor = Color.Black;
        this.Plot3DPictureBox.Image = new Bitmap(950, 950);
        this.Plot3DPictureBox.Location = new Point(960, 10);
        this.Plot3DPictureBox.Size = new Size(950, 950);
        this.Controls.Add(this.Plot3DPictureBox);
    }

    private void ConfigureUpdateInterfaceTimer()
    {
        this.UpdateInterfaceTimer.Interval = 100;
        this.UpdateInterfaceTimer.Tick += this.UpdateInterfaceTimerTick;
    }

    

    

    private void ConfigureForm()
    {
        this.ClientSize = new Size(1920, 1040);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "EVOLUTIONARY APPROXIMATOR";
    }

    private void ScaleScrollbarValueChange(object? sender, EventArgs args)
    {
        this.ApproximatorEngine.VisualiseSolution();
    }

    private void GeneratePointsButtonClick(object? sender, EventArgs args)
    {
        var pointNumber = (int) this.PointNumberValueControl.Value;
        var precisionDigits = (int) this.PrecisionDigitsValueControl.Value;
        var points = this.ApproximatorEngine.GeneratePoints(pointNumber, precisionDigits);
        var sizeDigits = pointNumber.ToString().Length;
        
        this.PointTableControl.Clear();
        var builder = new StringBuilder();
        for (var index = 0; index < pointNumber; index++)
        {
            var stringIndex = (index + 1).ToString();
            while (stringIndex.Length < sizeDigits)
                stringIndex = $"0{stringIndex}";
            builder.Append($"#{stringIndex}:{points[index]}\n");
        }
        builder.Remove(builder.Length - 1, 1);
        this.PointTableControl.Text = builder.ToString();
    }

    private void StartEngineButtonClick(object? sender, EventArgs args)
    {
        if (this.ApproximatorEngine.Points == null) this.ShowError("Points not generated!");
        else if (this.ApproximatorEngine.Running) this.ShowError("Engine is already running!");
        else
        {
            var job = new ApproximatorJob
            (
                (int) this.DominantParentGeneStrengthValueControl.Value,
                (ErrorMetric) this.ErrorMetricValueControl.SelectedItem,
                (GeneType) this.GeneTypeValueControl.SelectedItem,
                (int) this.MaxPolynomialDegreeValueControl.Value,
                (int) this.MutationProbabilityValueControl.Value,
                (int) this.ParentPoolSizeValueControl.Value,
                (int) this.PopulationSizeValueControl.Value,
                (int) this.PrecisionDigitsValueControl.Value
            );
        }
        this.ApproximatorEngine.Start(job);
    }

    private void StopEngineButtonClick(object? sender, EventArgs args)
    {
        this.ApproximatorEngine.Stop();
    }

    private static Button NewButton()
    {
        var button = new Button();
        button.Font = ApproximatorForm.ControlFont;
        button.Size = new Size(ControlWidth, 2 * ControlHeight);
        button.TextAlign = ContentAlignment.MiddleCenter;
        return button;
    }
    
    private static ComboBox NewComboBox()
    {
        var comboBox = new ComboBox();
        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBox.Font = ApproximatorForm.ControlFont;
        comboBox.Size = new Size(ControlWidth, ControlHeight);
        return comboBox;
    }
    
    private static Label NewLabel()
    {
        var label = new Label();
        label.Font = ApproximatorForm.ControlFont;
        label.Size = new Size(ControlWidth, ControlHeight);
        label.TextAlign = ContentAlignment.MiddleCenter;
        return label;
    }

    private static NumericUpDown NewNumericUpDown()
    {
        var numericUpDown = new NumericUpDown();
        numericUpDown.Font = ApproximatorForm.ControlFont;
        numericUpDown.Size = new Size(ControlWidth, ControlHeight);
        numericUpDown.TextAlign = HorizontalAlignment.Center;
        return numericUpDown;
    }
    
    private string FormatTime(long milliseconds)
    {
        var seconds = (milliseconds / 1000) % 60;
        var minutes = milliseconds / 60000;

        var minutesString = minutes < 10 ? $"0{minutes}" : minutes.ToString();
        var secondsString = seconds < 10 ? $"0{seconds}" : seconds.ToString();

        return $"{minutesString}:{secondsString}";
    }

    

    private void UpdateInterfaceTimerTick(object? sender, EventArgs args)
    {
        var state = this.ApproximatorEngine.GetCurrentState();
        if (!state.HasValue) return;
        this.ElapsedTimeValueControl.Text = this.FormatTime(state.Value.ElapsedTime);
        this.AverageErrorValueControl.Text = state.Value.GlobalBestIndividual.Error.ToString();
        this.LastImprovementValueControl.Text = state.Value.LastImprovement.ToString();
    }

    private void ShowError(string message)
    {
        const MessageBoxButtons buttons = MessageBoxButtons.OK;
        MessageBox.Show(message, "ERROR", buttons);
    }
    
    private readonly struct EnumItem
    {
        public string Name { get; }
        public Enum Value { get; }
        
        public EnumItem(Enum value)
        {
            this.Name = value.ToString().Replace("_", " ");
            this.Value = value;
        }
    }
}