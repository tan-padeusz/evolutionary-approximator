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

    private Label EngineStatusControl { get; } = ApproximatorForm.NewLabel();
    private Button StartEngineButton { get; } = ApproximatorForm.NewButton();
    private Button StopEngineButton { get; } = ApproximatorForm.NewButton();
    private Button PauseEngineButton { get; } = ApproximatorForm.NewButton();
    private Button ResumeEngineButton { get; } = ApproximatorForm.NewButton();

    private RichTextBox PointTableControl { get; } = new RichTextBox();

    #endregion
    
    #region Visualisation Controls

    private VScrollBar ScaleYScrollBar { get; } = new VScrollBar();
    private HScrollBar ScaleXScrollBar { get; } = new HScrollBar();
    private PictureBox Plot3DPictureBox { get; } = new PictureBox();

    #endregion
    private ApproximatorEngine ApproximatorEngine { get; }
    private Timer UpdateInterfaceTimer { get; } = new Timer();

    public ApproximatorForm()
    {
        this.ApproximatorEngine = new ApproximatorEngine();
        this.ConfigureDecorationControls();
        this.ConfigurePointControls();
        this.ConfigureInputControls();
        this.ConfigureOutputControls();
        this.ConfigureEngineStateControls();
        this.ConfigureVisualizationControls();
        this.ConfigureUpdateInterfaceTimer();
        this.ConfigureForm();
    }
    
    private void ConfigureDecorationControls()
    {
        this.TopHorizontalLine.BackColor = Color.DarkGray;
        this.TopHorizontalLine.Location = new Point(10, 138);
        this.TopHorizontalLine.Size = new Size(300, 5);
        this.Controls.Add(this.TopHorizontalLine);

        this.MiddleHorizontalLine.BackColor = Color.DarkGray;
        this.MiddleHorizontalLine.Location = new Point(10, 578);
        this.MiddleHorizontalLine.Size = new Size(300, 5);
        this.Controls.Add(this.MiddleHorizontalLine);

        this.BottomHorizontalLine.BackColor = Color.DarkGray;
        this.BottomHorizontalLine.Location = new Point(10, 838);
        this.BottomHorizontalLine.Size = new Size(300, 5);
        this.Controls.Add(this.BottomHorizontalLine);

        this.VerticalLine.BackColor = Color.DarkGray;
        this.VerticalLine.Location = new Point(318, 10);
        this.VerticalLine.Size = new Size(5, 990);
        this.Controls.Add(this.VerticalLine);
    }

    private void ConfigurePointControls()
    {
        this.PointNumberLabelControl.Location = new Point(10, 10);
        this.PointNumberLabelControl.Text = "POINT NUMBER";
        this.Controls.Add(this.PointNumberLabelControl);
        
        this.PointNumberValueControl.Increment = 1;
        this.PointNumberValueControl.Location = new Point(10, 40);
        this.PointNumberValueControl.Maximum = 25;
        this.PointNumberValueControl.Minimum = 1;
        this.PointNumberValueControl.Value = 15;
        this.Controls.Add(this.PointNumberValueControl);
        
        this.GeneratePointsButton.Click += this.GeneratePointsButtonClick;
        this.GeneratePointsButton.Location = new Point(10, 80);
        this.GeneratePointsButton.Text = "GENERATE POINTS";
        this.Controls.Add(this.GeneratePointsButton);
    }
    
    // line at y = 140

    private void ConfigureInputControls()
    {
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
        
        this.PopulationSizeLabelControl.Location = new Point(10, 330);
        this.PopulationSizeLabelControl.Text = "POPULATION SIZE";
        this.Controls.Add(this.PopulationSizeLabelControl);

        this.PopulationSizeValueControl.Increment = 25;
        this.PopulationSizeValueControl.Location = new Point(10, 360);
        this.PopulationSizeValueControl.Maximum = 800;
        this.PopulationSizeValueControl.Minimum = 200;
        this.PopulationSizeValueControl.Value = 400;
        this.Controls.Add(this.PopulationSizeValueControl);
        
        this.ParentPoolSizeLabelControl.Location = new Point(10, 390);
        this.ParentPoolSizeLabelControl.Text = "PARENT POOL SIZE";
        this.Controls.Add(this.ParentPoolSizeLabelControl);

        this.ParentPoolSizeValueControl.Increment = 20;
        this.ParentPoolSizeValueControl.Location = new Point(10, 420);
        this.ParentPoolSizeValueControl.Maximum = 200;
        this.ParentPoolSizeValueControl.Minimum = 20;
        this.ParentPoolSizeValueControl.Value = 80;
        this.Controls.Add(this.ParentPoolSizeValueControl);
        
        this.DominantParentGeneStrengthLabelControl.Location = new Point(10, 450);
        this.DominantParentGeneStrengthLabelControl.Text = "DOMINANT PARENT GENE STRENGTH";
        this.Controls.Add(this.DominantParentGeneStrengthLabelControl);

        this.DominantParentGeneStrengthValueControl.Increment = 20;
        this.DominantParentGeneStrengthValueControl.Location = new Point(10, 480);
        this.DominantParentGeneStrengthValueControl.Maximum = 1000;
        this.DominantParentGeneStrengthValueControl.Minimum = 0;
        this.DominantParentGeneStrengthValueControl.Value = 600;
        this.Controls.Add(this.DominantParentGeneStrengthValueControl);

        this.MutationProbabilityLabelControl.Location = new Point(10, 510);
        this.MutationProbabilityLabelControl.Text = "MUTATION PROBABILITY";
        this.Controls.Add(this.MutationProbabilityLabelControl);

        this.MutationProbabilityValueControl.Increment = 20;
        this.MutationProbabilityValueControl.Location = new Point(10, 540);
        this.MutationProbabilityValueControl.Maximum = 1000;
        this.MutationProbabilityValueControl.Minimum = 0;
        this.MutationProbabilityValueControl.Value = 100;
        this.Controls.Add(this.MutationProbabilityValueControl);
    }
    
    // line at y = 580

    private void ConfigureOutputControls()
    {
        this.ElapsedTimeLabelControl.Location = new Point(10, 590);
        this.ElapsedTimeLabelControl.Text = "ELAPSED TIME";
        this.Controls.Add(this.ElapsedTimeLabelControl);

        this.ElapsedTimeValueControl.Location = new Point(10, 620);
        this.ElapsedTimeValueControl.Text = "";
        this.Controls.Add(this.ElapsedTimeValueControl);

        this.AverageErrorLabelControl.Location = new Point(10, 650);
        this.AverageErrorLabelControl.Text = "AVERAGE ERROR";
        this.Controls.Add(this.AverageErrorLabelControl);

        this.AverageErrorValueControl.Location = new Point(10, 680);
        this.AverageErrorValueControl.Text = "";
        this.Controls.Add(this.AverageErrorValueControl);

        this.PopulationsCreatedLabelControl.Location = new Point(10, 710);
        this.PopulationsCreatedLabelControl.Text = "GENERATIONS EVALUATED";
        this.Controls.Add(this.PopulationsCreatedLabelControl);

        this.PopulationsCreatedValueControl.Location = new Point(10, 740);
        this.PopulationsCreatedValueControl.Text = "";
        this.Controls.Add(this.PopulationsCreatedValueControl);

        this.LastImprovementLabelControl.Location = new Point(10, 770);
        this.LastImprovementLabelControl.Text = "LAST IMPROVEMENT";
        this.Controls.Add(this.LastImprovementLabelControl);

        this.LastImprovementValueControl.Location = new Point(10, 800);
        this.LastImprovementValueControl.Text = "";
        this.Controls.Add(this.LastImprovementValueControl);
    }
    
    // line at y = 840

    private void ConfigureEngineStateControls()
    {
        this.EngineStatusControl.Location = new Point(10, 850);
        this.EngineStatusControl.Text = "STATUS : STOPPED";
        this.Controls.Add(this.EngineStatusControl);
        
        this.StartEngineButton.Click += this.StartEngineButtonClick;
        this.StartEngineButton.Location = new Point(10, 890);
        this.StartEngineButton.Text = "START";
        this.StartEngineButton.Width = 145;
        this.Controls.Add(this.StartEngineButton);

        this.StopEngineButton.Click += this.StopEngineButtonClick;
        this.StopEngineButton.Location = new Point(165, 890);
        this.StopEngineButton.Text = "STOP";
        this.StopEngineButton.Width = 145;
        this.Controls.Add(this.StopEngineButton);
        
        this.PauseEngineButton.Click += this.PauseEngineButtonClick;
        this.PauseEngineButton.Location = new Point(10, 950);
        this.PauseEngineButton.Text = "PAUSE";
        this.PauseEngineButton.Width = 145;
        this.Controls.Add(this.PauseEngineButton);
        
        this.ResumeEngineButton.Click += this.ResumeEngineButtonClick;
        this.ResumeEngineButton.Location = new Point(165, 950);
        this.ResumeEngineButton.Text = "RESUME";
        this.ResumeEngineButton.Width = 145;
        this.Controls.Add(this.ResumeEngineButton);
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
        this.ClientSize = new Size(1770, 1010);
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
        this.GeneratePointsButton.Text = "RE-GENERATE POINTS";
        var pointNumber = (int) this.PointNumberValueControl.Value;
        var points = this.ApproximatorEngine.GeneratePoints(pointNumber);
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
        if (this.ApproximatorEngine.Points == null) MessageBox.Show("Points not generated!", "Error!");
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
            if (this.ApproximatorEngine.Start(job)) this.ChangeDisplayedStatus(EngineStatus.RUNNING);
        }
    }

    private void StopEngineButtonClick(object? sender, EventArgs args)
    {
        this.ApproximatorEngine.Stop();
         this.ChangeDisplayedStatus(EngineStatus.STOPPED);
    }

    private void PauseEngineButtonClick(object? sender, EventArgs args)
    {
        if (this.ApproximatorEngine.Pause()) this.ChangeDisplayedStatus(EngineStatus.PAUSED);
    }
    
    private void ResumeEngineButtonClick(object? sender, EventArgs args)
    {
        if (this.ApproximatorEngine.Resume()) this.ChangeDisplayedStatus(EngineStatus.RUNNING);
    }

    private static Button NewButton()
    {
        var button = new Button();
        button.Font = new Font(FontFamily.GenericMonospace, 13F);
        button.Size = new Size(300, 50);
        button.TextAlign = ContentAlignment.MiddleCenter;
        return button;
    }
    
    private static ComboBox NewComboBox()
    {
        var comboBox = new ComboBox();
        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBox.Font = new Font(FontFamily.GenericMonospace, 13F);
        comboBox.Size = new Size(300, 30);
        return comboBox;
    }
    
    private static Label NewLabel()
    {
        var label = new Label();
        label.Font = new Font(FontFamily.GenericMonospace, 13F);
        label.Size = new Size(300, 30);
        label.TextAlign = ContentAlignment.MiddleCenter;
        return label;
    }

    private static NumericUpDown NewNumericUpDown()
    {
        var numericUpDown = new NumericUpDown();
        numericUpDown.Font = new Font(FontFamily.GenericMonospace, 13F);
        numericUpDown.Size = new Size(300, 30);
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

    private void ChangeDisplayedStatus(EngineStatus status)
    {
        this.EngineStatusControl.Text = $"STATUS : {status}";
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