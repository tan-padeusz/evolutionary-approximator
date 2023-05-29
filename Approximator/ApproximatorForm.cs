using System.ComponentModel;
using System.Diagnostics;
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
    private Label PopulationsCreatedLabelControl { get; } = ApproximatorForm.NewLabel();
    private Label PopulationsCreatedValueControl { get; } = ApproximatorForm.NewLabel();
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
    
    private static Font ControlFont => new Font(FontFamily.GenericMonospace, 14F);
    private static int ControlHeight => 25;
    private static int ControlWidth => 350;
    private ApproximatorEngine ApproximatorEngine { get; }
    private Stopwatch Stopwatch { get; } = new Stopwatch();
    private Timer UpdateInterfaceTimer { get; } = new Timer();

    public ApproximatorForm()
    {
        this.ApproximatorEngine = new ApproximatorEngine();
        this.ConfigureDecorationControls();
        this.ConfigureInputOutputControls();
        this.ConfigureVisualizationControls();
        this.ConfigureUpdateInterfaceTimer();
        this.ConfigureForm();
    }
    
    private void ConfigureDecorationControls()
    {
        this.TopHorizontalLine.BackColor = Color.DarkGray;
        this.TopHorizontalLine.Location = new Point(10, 123);
        this.TopHorizontalLine.Size = new Size(400, 5);
        this.Controls.Add(this.TopHorizontalLine);

        this.MiddleHorizontalLine.BackColor = Color.DarkGray;
        this.MiddleHorizontalLine.Location = new Point(10, 543);
        this.MiddleHorizontalLine.Size = new Size(400, 5);
        this.Controls.Add(this.MiddleHorizontalLine);

        this.BottomHorizontalLine.BackColor = Color.DarkGray;
        this.BottomHorizontalLine.Location = new Point(10, 763);
        this.BottomHorizontalLine.Size = new Size(400, 5);
        this.Controls.Add(this.BottomHorizontalLine);

        this.VerticalLine.BackColor = Color.DarkGray;
        this.VerticalLine.Location = new Point(878, 10);
        this.VerticalLine.Size = new Size(5, 870);
        this.Controls.Add(this.VerticalLine);
    }

    private void ConfigureInputOutputControls()
    {
        this.PointNumberLabelControl.Location = new Point(10, 10);
        this.PointNumberLabelControl.Text = "POINT NUMBER";
        this.Controls.Add(this.PointNumberLabelControl);

        this.PointNumberValueControl.Increment = 1;
        this.PointNumberValueControl.Location = new Point(10, 35);
        this.PointNumberValueControl.Maximum = 25;
        this.PointNumberValueControl.Minimum = 1;
        this.PointNumberValueControl.Value = 15;
        this.Controls.Add(this.PointNumberValueControl);

        this.GeneratePointsButton.Click += this.GeneratePointsButtonClick;
        this.GeneratePointsButton.Location = new Point(10, 65);
        this.GeneratePointsButton.Text = "GENERATE POINTS";
        this.Controls.Add(this.GeneratePointsButton);
        
        // line at y = 125

        this.GeneTypeLabelControl.Location = new Point(10, 135);
        this.GeneTypeLabelControl.Text = "GENE TYPE";
        this.Controls.Add(this.GeneTypeLabelControl);

        this.GeneTypeValueControl.DisplayMember = "Name";
        this.GeneTypeValueControl.Location = new Point(10, 160);
        this.GeneTypeValueControl.ValueMember = "Value";
        foreach (var value in Enum.GetValues<GeneType>())
            this.GeneTypeValueControl.Items.Add(new EnumItem(value));
        this.GeneTypeValueControl.SelectedIndex = 2;
        this.Controls.Add(this.GeneTypeValueControl);

        this.ErrorMetricLabelControl.Location = new Point(10, 185);
        this.ErrorMetricLabelControl.Text = "ERROR METRIC";
        this.Controls.Add(this.ErrorMetricLabelControl);

        this.ErrorMetricValueControl.DisplayMember = "Name";
        this.ErrorMetricValueControl.Location = new Point(10, 210);
        this.ErrorMetricValueControl.ValueMember = "Value";
        foreach (var value in Enum.GetValues<ErrorMetric>())
            this.ErrorMetricValueControl.Items.Add(new EnumItem(value));
        this.ErrorMetricValueControl.SelectedIndex = 1;
        this.Controls.Add(this.ErrorMetricValueControl);

        this.MaxPolynomialDegreeLabelControl.Location = new Point(10, 235);
        this.MaxPolynomialDegreeLabelControl.Text = "MAX POLYNOMIAL DEGREE";
        this.Controls.Add(this.MaxPolynomialDegreeLabelControl);

        this.MaxPolynomialDegreeValueControl.Increment = 1;
        this.MaxPolynomialDegreeValueControl.Location = new Point(10, 260);
        this.MaxPolynomialDegreeValueControl.Maximum = 5;
        this.MaxPolynomialDegreeValueControl.Minimum = 0;
        this.MaxPolynomialDegreeValueControl.Value = 2;
        this.Controls.Add(this.MaxPolynomialDegreeValueControl);

        this.PrecisionDigitsLabelControl.Location = new Point(10, 285);
        this.PrecisionDigitsLabelControl.Text = "PRECISION DIGITS";
        this.Controls.Add(this.PrecisionDigitsLabelControl);

        this.PrecisionDigitsValueControl.Increment = 1;
        this.PrecisionDigitsValueControl.Location = new Point(10, 310);
        this.PrecisionDigitsValueControl.Maximum = 5;
        this.PrecisionDigitsValueControl.Minimum = 0;
        this.PrecisionDigitsValueControl.Value = 3;
        this.Controls.Add(this.PrecisionDigitsValueControl);

        this.PopulationSizeLabelControl.Location = new Point(10, 335);
        this.PopulationSizeLabelControl.Text = "POPULATION SIZE";
        this.Controls.Add(this.PopulationSizeLabelControl);

        this.PopulationSizeValueControl.Increment = 25;
        this.PopulationSizeValueControl.Location = new Point(10, 360);
        this.PopulationSizeValueControl.Maximum = 800;
        this.PopulationSizeValueControl.Minimum = 200;
        this.PopulationSizeValueControl.Value = 400;
        this.Controls.Add(this.PopulationSizeValueControl);

        this.ParentPoolSizeLabelControl.Location = new Point(10, 385);
        this.ParentPoolSizeLabelControl.Text = "PARENT POOL SIZE";
        this.Controls.Add(this.ParentPoolSizeLabelControl);

        this.ParentPoolSizeValueControl.Increment = 20;
        this.ParentPoolSizeValueControl.Location = new Point(10, 410);
        this.ParentPoolSizeValueControl.Maximum = 200;
        this.ParentPoolSizeValueControl.Minimum = 20;
        this.ParentPoolSizeValueControl.Value = 80;
        this.Controls.Add(this.ParentPoolSizeValueControl);

        this.DominantParentGeneStrengthLabelControl.Location = new Point(10, 435);
        this.DominantParentGeneStrengthLabelControl.Text = "DOMINANT PARENT GENE STRENGTH";
        this.Controls.Add(this.DominantParentGeneStrengthLabelControl);

        this.DominantParentGeneStrengthValueControl.Increment = 20;
        this.DominantParentGeneStrengthValueControl.Location = new Point(10, 460);
        this.DominantParentGeneStrengthValueControl.Maximum = 1000;
        this.DominantParentGeneStrengthValueControl.Minimum = 0;
        this.DominantParentGeneStrengthValueControl.Value = 600;
        this.Controls.Add(this.DominantParentGeneStrengthValueControl);

        this.MutationProbabilityLabelControl.Location = new Point(10, 485);
        this.MutationProbabilityLabelControl.Text = "MUTATION PROBABILITY";
        this.Controls.Add(this.MutationProbabilityLabelControl);

        this.MutationProbabilityValueControl.Increment = 20;
        this.MutationProbabilityValueControl.Location = new Point(10, 510);
        this.MutationProbabilityValueControl.Maximum = 1000;
        this.MutationProbabilityValueControl.Minimum = 0;
        this.MutationProbabilityValueControl.Value = 100;
        this.Controls.Add(this.MutationProbabilityValueControl);
        
        // line at y = 545

        this.ElapsedTimeLabelControl.Location = new Point(10, 555);
        this.ElapsedTimeLabelControl.Text = "ELAPSED TIME";
        this.Controls.Add(this.ElapsedTimeLabelControl);

        this.ElapsedTimeValueControl.Location = new Point(10, 580);
        this.ElapsedTimeValueControl.Text = "";
        this.Controls.Add(this.ElapsedTimeValueControl);

        this.AverageErrorLabelControl.Location = new Point(10, 605);
        this.AverageErrorLabelControl.Text = "AVERAGE ERROR";
        this.Controls.Add(this.AverageErrorLabelControl);

        this.AverageErrorValueControl.Location = new Point(10, 630);
        this.AverageErrorValueControl.Text = "";
        this.Controls.Add(this.AverageErrorValueControl);

        this.PopulationsCreatedLabelControl.Location = new Point(10, 655);
        this.PopulationsCreatedLabelControl.Text = "GENERATIONS EVALUATED";
        this.Controls.Add(this.PopulationsCreatedLabelControl);

        this.PopulationsCreatedValueControl.Location = new Point(10, 680);
        this.PopulationsCreatedValueControl.Text = "";
        this.Controls.Add(this.PopulationsCreatedValueControl);

        this.LastImprovementLabelControl.Location = new Point(10, 705);
        this.LastImprovementLabelControl.Text = "LAST IMPROVEMENT";
        this.Controls.Add(this.LastImprovementLabelControl);

        this.LastImprovementValueControl.Location = new Point(10, 730);
        this.LastImprovementValueControl.Text = "";
        this.Controls.Add(this.LastImprovementValueControl);
        
        // line at y = 765

        this.StartEngineButton.Click += this.StartEngineButtonClick;
        this.StartEngineButton.Location = new Point(10, 775);
        this.StartEngineButton.Text = "START";
        this.Controls.Add(this.StartEngineButton);

        this.StopEngineButton.Click += this.StopEngineButtonClick;
        this.StopEngineButton.Location = new Point(10, 830);
        this.StopEngineButton.Text = "STOP";
        this.Controls.Add(this.StopEngineButton);

        this.PointTableControl.BackColor = Color.White;
        this.PointTableControl.Font = ApproximatorForm.ControlFont;
        this.PointTableControl.Location = new Point(420, 10);
        this.PointTableControl.ReadOnly = true;
        this.PointTableControl.Size = new Size(450, 870);
        this.Controls.Add(this.PointTableControl);
    }

    private void ConfigureVisualizationControls()
    {
        this.ScaleYScrollBar.LargeChange = 1;
        this.ScaleYScrollBar.Location = new Point(890, 10);
        this.ScaleYScrollBar.Maximum = 25;
        this.ScaleYScrollBar.Minimum = 1;
        this.ScaleYScrollBar.Size = new Size(30, 830);
        this.ScaleYScrollBar.SmallChange = 1;
        this.ScaleYScrollBar.Value = 20;
        this.ScaleYScrollBar.ValueChanged += this.ScaleScrollbarValueChanged;
        this.Controls.Add(this.ScaleYScrollBar);

        this.ScaleXScrollBar.LargeChange = 1;
        this.ScaleXScrollBar.Location = new Point(930, 850);
        this.ScaleXScrollBar.Maximum = 25;
        this.ScaleXScrollBar.Minimum = 1;
        this.ScaleXScrollBar.Size = new Size(830, 30);
        this.ScaleXScrollBar.SmallChange = 1;
        this.ScaleXScrollBar.Value = 5;
        this.ScaleXScrollBar.ValueChanged += this.ScaleScrollbarValueChanged;
        this.Controls.Add(this.ScaleXScrollBar);

        this.Plot3DPictureBox.BackColor = Color.Black;
        this.Plot3DPictureBox.Image = new Bitmap(830, 830);
        this.Plot3DPictureBox.Location = new Point(930, 10);
        this.Plot3DPictureBox.Size = new Size(830, 830);
        this.Controls.Add(this.Plot3DPictureBox);
        
        this.VisualizeSolution();
    }

    private void ConfigureUpdateInterfaceTimer()
    {
        this.UpdateInterfaceTimer.Interval = 100;
        this.UpdateInterfaceTimer.Tick += this.UpdateInterfaceTimerTick;
        this.UpdateInterfaceTimer.Start();
    }

    

    

    private void ConfigureForm()
    {
        this.ClientSize = new Size(1770, 890);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "EVOLUTIONARY APPROXIMATOR";
    }

    private void ScaleScrollbarValueChanged(object? sender, EventArgs args)
    {
        this.VisualizeSolution();
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
            var emi = (EnumItem) this.ErrorMetricValueControl.SelectedItem;
            var gti = (EnumItem) this.GeneTypeValueControl.SelectedItem;
            
            var job = new ApproximatorJob
            (
                (int) this.DominantParentGeneStrengthValueControl.Value,
                (ErrorMetric) emi.Value,
                (GeneType) gti.Value,
                (int) this.MaxPolynomialDegreeValueControl.Value,
                (int) this.MutationProbabilityValueControl.Value,
                (int) this.ParentPoolSizeValueControl.Value,
                (int) this.PopulationSizeValueControl.Value,
                (int) this.PrecisionDigitsValueControl.Value
            );
            this.ApproximatorEngine.Start(job);
        }
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

    

    private void UpdateInterfaceTimerTick(object? sender, EventArgs args)
    {
        var state = this.ApproximatorEngine.GetCurrentState();
        if (state == null) return;
        this.AverageErrorValueControl.Text = state.AverageError;
        this.ElapsedTimeValueControl.Text = state.ElapsedTime;
        this.LastImprovementValueControl.Text = state.LastImprovement;
        this.PopulationsCreatedValueControl.Text = state.PopulationsCreated;
        this.VisualizeSolution();
    }

    private void VisualizeSolution()
    {
        var graphics = Graphics.FromImage(this.Plot3DPictureBox.Image);
        graphics.Clear(Color.Black);

        const int nodesCount = 45;

        const int leftXBias = 300;
        const int rightXBias = 200;
        const int topYBias = 200;
        const int bottomYBias = 100;

        const int pixelsFromLeft = 60;
        const int pixelsFromTop = 210;
        const int pixelsFromRight = pixelsFromLeft + leftXBias + rightXBias;
        const int pixelsFromBottom = pixelsFromTop + topYBias + bottomYBias;

        const int centerX = (pixelsFromLeft + pixelsFromRight) / 2;
        const int centerY = (pixelsFromTop + pixelsFromBottom) / 2;

        const double leftXStep = leftXBias / (nodesCount - 1.0);
        const double rightXStep = rightXBias / (nodesCount - 1.0);
        const double topYStep = topYBias / (nodesCount - 1.0);
        const double bottomYStep = bottomYBias / (nodesCount - 1.0);

        const double range = (nodesCount - 1.0) / 2;

        var pixelsPerOneX = this.ScaleXScrollBar.Value * 10;
        var pixelsPerOneY = this.ScaleYScrollBar.Value * 10;
        
        var invertedXScale = leftXStep / pixelsPerOneX;

        int i, j;
        int p, q;

        var (previousI, previousJ) = (centerX, centerY);
        
        var xs = new int[nodesCount];
        var ys = new int[nodesCount];
        
        for (q = 0; q < nodesCount; q++) for (p = 0; p < nodesCount; p++)
        {
            i = pixelsFromLeft + rightXBias + (int) (leftXStep * p - rightXStep * q);
            j = pixelsFromTop + (int) (topYStep * p + bottomYStep * q);
            var x = invertedXScale * (p - range);
            var y = invertedXScale * (q - range);
            var z = this.ApproximatorEngine.CalculateFunctionResult(x, y);
            var k = j - (int) (pixelsPerOneY * z);
            if (p > 0) graphics.DrawLine(Pens.Red, previousI, previousJ, i, k);
            if (q > 0) graphics.DrawLine(Pens.Red, xs[p], ys[p], i, k);
            previousI = i;
            previousJ = k;
            xs[p] = i;
            ys[p] = k;
        }
        
        this.Plot3DPictureBox.Refresh();
    }

    private void ShowError(string message)
    {
        const MessageBoxButtons buttons = MessageBoxButtons.OK;
        MessageBox.Show(message, "ERROR", buttons);
    }
    
    private readonly struct EnumItem
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string Name { get; }
        public Enum Value { get; }
        
        public EnumItem(Enum value)
        {
            this.Name = value.ToString().Replace("_", " ");
            this.Value = value;
        }
    }
}