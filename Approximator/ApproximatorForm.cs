using System.ComponentModel;
using System.Text;
using Enums;
using Data;
using Point = System.Drawing.Point;

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
    
    // #region Buttons
    //
    // private Button StartButton { get; } = new Button();
    // private Button StopButton { get; } = new Button();
    //
    // #endregion
    //
    // #region Decorations
    //
    // private Label HorizontalLine { get; } = new Label();
    // private Label LeftVerticalLine { get; } = new Label();
    // private Label RightVerticalLine { get; } = new Label();
    //
    // #endregion
    //
    // #region Input Controls
    //
    // private NumericInputControl ContestantsControl { get; } = new NumericInputControl();
    // private NumericInputControl DominantParentGeneStrengthControl { get; } = new NumericInputControl();
    // private ItemInputControl ErrorMetricControl { get; } = new ItemInputControl();
    // private ItemInputControl GeneTypeControl { get; } = new ItemInputControl();
    // private NumericInputControl MaxPolynomialDegreeControl { get; } = new NumericInputControl();
    // private NumericInputControl MutationProbabilityControl { get; } = new NumericInputControl();
    // private ItemInputControl PointFunctionControl { get; } = new ItemInputControl();
    // private NumericInputControl PopulationSizeControl { get; } = new NumericInputControl();
    // private NumericInputControl PrecisionDigitsControl { get; } = new NumericInputControl();
    //
    // #endregion
    //
    // #region Output Controls
    //
    // public OutputControl AverageErrorControl { get; } = new OutputControl();
    // private Label BestFunctionControl { get; } = new Label();
    // public RichTextBox BestFunctionOutputControl { get; } = new RichTextBox();
    //
    // private Label ControlTableControl { get; } = new Label();
    // public RichTextBox ControlTableOutputControl { get; } = new RichTextBox();
    // public OutputControl ElapsedTimeControl { get; } = new OutputControl();
    // public OutputControl LastImprovementControl { get; } = new OutputControl();
    // public OutputControl PopulationsCreatedControl { get; } = new OutputControl();
    //
    // #endregion

    
    
    #region Decoration Controls

    private Label TopHorizontalLine { get; set; }
    private Label MiddleHorizontalLine { get; set; }
    private Label BottomHorizontalLine { get; set; }
    
    private Label VerticalLine { get; set; }

    #endregion

    #region Input/Output Controls

    private Label PointNumberLabelControl { get; set; }
    private NumericUpDown PointNumberValueControl { get; set; }
    private Button GeneratePointsButton { get; set; }
    
    private Label GeneTypeLabelControl { get; set; }
    private ComboBox GeneTypeValueControl { get; set; }
    private Label ErrorMetricLabelControl { get; set; }
    private ComboBox ErrorMetricValueControl { get; set; }
    
    private Label MaxPolynomialDegreeLabelControl { get; set; }
    private NumericUpDown MaxPolynomialDegreeValueControl { get; set; }
    private Label PrecisionDigitsLabelControl { get; set; }
    private NumericUpDown PrecisionDigitsValueControl { get; set; }
    private Label PopulationSizeLabelControl { get; set; }
    private NumericUpDown PopulationSizeValueControl { get; set; }
    private Label ParentPoolSizeLabelControl { get; set; }
    private NumericUpDown ParentPoolSizeValueControl { get; set; }
    private Label DominantParentGeneStrengthLabelControl { get; set; }
    private NumericUpDown DominantParentGeneStrengthValueControl { get; set; }
    private Label MutationProbabilityLabelControl { get; set; }
    private NumericUpDown MutationProbabilityValueControl { get; set; }
    
    private Label ElapsedTimeLabelControl { get; set; }
    private Label ElapsedTimeValueControl { get; set; }
    private Label AverageErrorLabelControl { get; set; }
    private Label AverageErrorValueControl { get; set; }
    private Label GenerationsEvaluatedLabelControl { get; set; }
    private Label GenerationsEvaluatedValueControl { get; set; }
    private Label LastImprovementLabelControl { get; set; }
    private Label LastImprovementValueControl { get; set; }
    
    private Button StartEngineButton { get; set; }
    private Button StopEngineButton { get; set; }
    
    private RichTextBox PointTableControl { get; set; }

    #endregion
    
    #region Visualisation Controls

    // public PictureBox Plot { get; } = new PictureBox();
    // public HScrollBar XScrollBar { get; } = new HScrollBar();
    // public VScrollBar YScrollBar { get; } = new VScrollBar();
    private VScrollBar ScaleYScrollBar { get; set; }
    private HScrollBar ScaleXScrollBar { get; set; }
    private PictureBox Plot3DPictureBox { get; set; }

    #endregion
    
    private Font ControlFont { get; } = new Font(FontFamily.GenericMonospace, 16F);
    private int ControlHeight { get; } = 30;
    private int ControlWidth { get; } = 400;
    private Engine Engine { get; }

    public ApproximatorForm()
    {
        this.Engine = new Engine(this);
        this.InitializeDecorationControls();
        this.InitializeInputOutputControls();
        this.InitializeVisualizationControls();
        
        this.ConfigureForm();
    }
    
    private void InitializeDecorationControls()
    {
        this.TopHorizontalLine = new Label();
        this.TopHorizontalLine.BackColor = Color.DarkGray;
        this.TopHorizontalLine.Location = new Point(10, 138);
        this.TopHorizontalLine.Size = new Size(400, 5);
        this.Controls.Add(this.TopHorizontalLine);

        this.MiddleHorizontalLine = new Label();
        this.MiddleHorizontalLine.BackColor = Color.DarkGray;
        this.MiddleHorizontalLine.Location = new Point(10, 638);
        this.MiddleHorizontalLine.Size = new Size(400, 5);
        this.Controls.Add(this.MiddleHorizontalLine);

        this.BottomHorizontalLine = new Label();
        this.BottomHorizontalLine.BackColor = Color.DarkGray;
        this.BottomHorizontalLine.Location = new Point(10, 898);
        this.BottomHorizontalLine.Size = new Size(400, 5);
        this.Controls.Add(this.BottomHorizontalLine);

        this.VerticalLine = new Label();
        this.VerticalLine.BackColor = Color.DarkGray;
        this.VerticalLine.Location = new Point(878, 10);
        this.VerticalLine.Size = new Size(5, 1020);
        this.Controls.Add(this.VerticalLine);
    }

    private void InitializeInputOutputControls()
    {
        this.PointNumberLabelControl = this.NewLabelControl();
        this.PointNumberLabelControl.Location = new Point(10, 10);
        this.PointNumberLabelControl.Text = "POINT NUMBER";
        this.Controls.Add(this.PointNumberLabelControl);

        this.PointNumberValueControl = this.NewNumericInputControl();
        this.PointNumberValueControl.Increment = 3;
        this.PointNumberValueControl.Location = new Point(10, 40);
        this.PointNumberValueControl.Maximum = 100;
        this.PointNumberValueControl.Minimum = 1;
        this.PointNumberValueControl.Value = 25;
        this.Controls.Add(this.PointNumberValueControl);

        this.GeneratePointsButton = this.NewButton();
        this.GeneratePointsButton.Click += this.GeneratePointsButtonClick;
        this.GeneratePointsButton.Location = new Point(10, 70);
        this.GeneratePointsButton.Text = "GENERATE POINTS";
        this.Controls.Add(this.GeneratePointsButton);
        
        // line at y = 140

        this.GeneTypeLabelControl = this.NewLabelControl();
        this.GeneTypeLabelControl.Location = new Point(10, 150);
        this.GeneTypeLabelControl.Text = "GENE TYPE";
        this.Controls.Add(this.GeneTypeLabelControl);

        this.ErrorMetricLabelControl = this.NewLabelControl();
        this.ErrorMetricLabelControl.Location = new Point(10, 210);
        this.ErrorMetricLabelControl.Text = "ERROR METRIC";
        this.Controls.Add(this.ErrorMetricLabelControl);

        this.MaxPolynomialDegreeLabelControl = this.NewLabelControl();
        this.MaxPolynomialDegreeLabelControl.Location = new Point(10, 270);
        this.MaxPolynomialDegreeLabelControl.Text = "MAX POLYNOMIAL DEGREE";
        this.Controls.Add(this.MaxPolynomialDegreeLabelControl);

        this.MaxPolynomialDegreeValueControl = this.NewNumericInputControl();
        this.MaxPolynomialDegreeValueControl.Increment = 1;
        this.MaxPolynomialDegreeValueControl.Location = new Point(10, 300);
        this.MaxPolynomialDegreeValueControl.Maximum = 5;
        this.MaxPolynomialDegreeValueControl.Minimum = 0;
        this.MaxPolynomialDegreeValueControl.Value = 2;
        this.Controls.Add(this.MaxPolynomialDegreeValueControl);

        this.PrecisionDigitsLabelControl = this.NewLabelControl();
        this.PrecisionDigitsLabelControl.Location = new Point(10, 330);
        this.PrecisionDigitsLabelControl.Text = "PRECISION DIGITS";
        this.Controls.Add(this.PrecisionDigitsLabelControl);

        this.PrecisionDigitsValueControl = this.NewNumericInputControl();
        this.PrecisionDigitsValueControl.Increment = 1;
        this.PrecisionDigitsValueControl.Location = new Point(10, 360);
        this.PrecisionDigitsValueControl.Maximum = 5;
        this.PrecisionDigitsValueControl.Minimum = 0;
        this.PrecisionDigitsValueControl.Value = 3;
        this.Controls.Add(this.PrecisionDigitsValueControl);

        this.PopulationSizeLabelControl = this.NewLabelControl();
        this.PopulationSizeLabelControl.Location = new Point(10, 390);
        this.PopulationSizeLabelControl.Text = "POPULATION SIZE";
        this.Controls.Add(this.PopulationSizeLabelControl);

        this.PopulationSizeValueControl = this.NewNumericInputControl();
        this.PopulationSizeValueControl.Increment = 25;
        this.PopulationSizeValueControl.Location = new Point(10, 420);
        this.PopulationSizeValueControl.Maximum = 800;
        this.PopulationSizeValueControl.Minimum = 200;
        this.PopulationSizeValueControl.Value = 400;
        this.Controls.Add(this.PopulationSizeValueControl);

        this.ParentPoolSizeLabelControl = this.NewLabelControl();
        this.ParentPoolSizeLabelControl.Location = new Point(10, 450);
        this.ParentPoolSizeLabelControl.Text = "PARENT POOL SIZE";
        this.Controls.Add(this.ParentPoolSizeLabelControl);

        this.ParentPoolSizeValueControl = this.NewNumericInputControl();
        this.ParentPoolSizeValueControl.Increment = 20;
        this.ParentPoolSizeValueControl.Location = new Point(10, 480);
        this.ParentPoolSizeValueControl.Maximum = 200;
        this.ParentPoolSizeValueControl.Minimum = 20;
        this.ParentPoolSizeValueControl.Value = 80;
        this.Controls.Add(this.ParentPoolSizeValueControl);

        this.DominantParentGeneStrengthLabelControl = this.NewLabelControl();
        this.DominantParentGeneStrengthLabelControl.Location = new Point(10, 510);
        this.DominantParentGeneStrengthLabelControl.Text = "DOMINANT PARENT GENE STRENGTH";
        this.Controls.Add(this.DominantParentGeneStrengthLabelControl);

        this.DominantParentGeneStrengthValueControl = this.NewNumericInputControl();
        this.DominantParentGeneStrengthValueControl.Increment = 20;
        this.DominantParentGeneStrengthValueControl.Location = new Point(10, 540);
        this.DominantParentGeneStrengthValueControl.Maximum = 1000;
        this.DominantParentGeneStrengthValueControl.Minimum = 0;
        this.DominantParentGeneStrengthValueControl.Value = 600;
        this.Controls.Add(this.DominantParentGeneStrengthValueControl);

        this.MutationProbabilityLabelControl = this.NewLabelControl();
        this.MutationProbabilityLabelControl.Location = new Point(10, 570);
        this.MutationProbabilityLabelControl.Text = "MUTATION PROBABILITY";
        this.Controls.Add(this.MutationProbabilityLabelControl);

        this.MutationProbabilityValueControl = this.NewNumericInputControl();
        this.MutationProbabilityValueControl.Increment = 20;
        this.MutationProbabilityValueControl.Location = new Point(10, 600);
        this.MutationProbabilityValueControl.Maximum = 1000;
        this.MutationProbabilityValueControl.Minimum = 0;
        this.MutationProbabilityValueControl.Value = 100;
        this.Controls.Add(this.MutationProbabilityValueControl);
        
        // line at y = 640

        this.ElapsedTimeLabelControl = this.NewLabelControl();
        this.ElapsedTimeLabelControl.Location = new Point(10, 650);
        this.ElapsedTimeLabelControl.Text = "ELAPSED TIME";
        this.Controls.Add(this.ElapsedTimeLabelControl);

        this.ElapsedTimeValueControl = this.NewLabelControl();
        this.ElapsedTimeValueControl.Location = new Point(10, 680);
        this.ElapsedTimeValueControl.Text = "";
        this.Controls.Add(this.ElapsedTimeValueControl);

        this.AverageErrorLabelControl = this.NewLabelControl();
        this.AverageErrorLabelControl.Location = new Point(10, 710);
        this.AverageErrorLabelControl.Text = "AVERAGE ERROR";
        this.Controls.Add(this.AverageErrorLabelControl);

        this.AverageErrorValueControl = this.NewLabelControl();
        this.AverageErrorValueControl.Location = new Point(10, 740);
        this.AverageErrorValueControl.Text = "";
        this.Controls.Add(this.AverageErrorValueControl);

        this.GenerationsEvaluatedLabelControl = this.NewLabelControl();
        this.GenerationsEvaluatedLabelControl.Location = new Point(10, 770);
        this.GenerationsEvaluatedLabelControl.Text = "GENERATIONS EVALUATED";
        this.Controls.Add(this.GenerationsEvaluatedLabelControl);

        this.GenerationsEvaluatedValueControl = this.NewLabelControl();
        this.GenerationsEvaluatedValueControl.Location = new Point(10, 800);
        this.GenerationsEvaluatedValueControl.Text = "";
        this.Controls.Add(this.GenerationsEvaluatedValueControl);

        this.LastImprovementLabelControl = this.NewLabelControl();
        this.LastImprovementLabelControl.Location = new Point(10, 830);
        this.LastImprovementLabelControl.Text = "LAST IMPROVEMENT";
        this.Controls.Add(this.LastImprovementLabelControl);

        this.LastImprovementValueControl = this.NewLabelControl();
        this.LastImprovementValueControl.Location = new Point(10, 860);
        this.LastImprovementValueControl.Text = "";
        this.Controls.Add(this.LastImprovementValueControl);
        
        // line at y = 900

        this.StartEngineButton = this.NewButton();
        this.StartEngineButton.Click += this.StartEngineButtonClick;
        this.StartEngineButton.Location = new Point(10, 910);
        this.StartEngineButton.Text = "START";
        this.Controls.Add(this.StartEngineButton);

        this.StopEngineButton = this.NewButton();
        this.StopEngineButton.Click += this.StopEngineButtonClick;
        this.StopEngineButton.Location = new Point(10, 970);
        this.StopEngineButton.Text = "STOP";
        this.Controls.Add(this.StopEngineButton);

        this.PointTableControl = new RichTextBox();
        this.PointTableControl.BackColor = Color.White;
        this.PointTableControl.Font = this.ControlFont;
        this.PointTableControl.Location = new Point(420, 10);
        this.PointTableControl.ReadOnly = true;
        this.PointTableControl.Size = new Size(450, 1020);
        this.Controls.Add(this.PointTableControl);
    }

    private void InitializeVisualizationControls()
    {
        this.ScaleYScrollBar = new VScrollBar();
        this.ScaleYScrollBar.LargeChange = 1;
        this.ScaleYScrollBar.Location = new Point(890, 10);
        this.ScaleYScrollBar.Maximum = 25;
        this.ScaleYScrollBar.Minimum = 1;
        this.ScaleYScrollBar.Size = new Size(60, 950);
        this.ScaleYScrollBar.SmallChange = 1;
        this.ScaleYScrollBar.Value = 20;
        this.Controls.Add(this.ScaleYScrollBar);

        this.ScaleXScrollBar = new HScrollBar();
        this.ScaleXScrollBar.LargeChange = 1;
        this.ScaleXScrollBar.Location = new Point(960, 970);
        this.ScaleXScrollBar.Maximum = 25;
        this.ScaleXScrollBar.Minimum = 1;
        this.ScaleXScrollBar.Size = new Size(950, 60);
        this.ScaleXScrollBar.SmallChange = 1;
        this.ScaleXScrollBar.Value = 5;
        this.Controls.Add(this.ScaleXScrollBar);

        this.Plot3DPictureBox = new PictureBox();
        this.Plot3DPictureBox.BackColor = Color.Black;
        this.Plot3DPictureBox.Image = new Bitmap(950, 950);
        this.Plot3DPictureBox.Location = new Point(960, 10);
        this.Plot3DPictureBox.Size = new Size(950, 950);
        this.Controls.Add(this.Plot3DPictureBox);
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
        this.Engine.VisualiseSolution();
    }

    private void GeneratePointsButtonClick(object? sender, EventArgs args)
    {
        var pointNumber = (int) this.PointNumberValueControl.Value;
        var precisionDigits = (int) this.PrecisionDigitsValueControl.Value;
        var points = this.Engine.GeneratePoints(pointNumber, precisionDigits);
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
        // var job = new ApproximatorJob
        // (
        //     this.ContestantsControl.GetValue(),
        //     this.DominantParentGeneStrengthControl.GetValue(),
        //     (ErrorMetric) this.ErrorMetricControl.GetValue(),
        //     (GeneType) this.GeneTypeControl.GetValue(),
        //     this.MaxPolynomialDegreeControl.GetValue(),
        //     this.MutationProbabilityControl.GetValue(),
        //     (PointFunction) this.PointFunctionControl.GetValue(),
        //     this.PopulationSizeControl.GetValue(),
        //     this.PrecisionDigitsControl.GetValue()
        // );
        // this.Engine.Start(job);
    }

    private void StopEngineButtonClick(object? sender, EventArgs args)
    {
        this.Engine.Stop();
    }

    private Button NewButton()
    {
        var button = new Button();
        button.Font = this.ControlFont;
        button.Size = new Size(this.ControlWidth, 2 * this.ControlHeight);
        button.TextAlign = ContentAlignment.MiddleCenter;
        return button;
    }
    
    private ComboBox NewEnumInputControl()
    {
        var comboBox = new ComboBox();
        comboBox.Font = this.ControlFont;
        comboBox.Size = new Size(this.ControlWidth, this.ControlHeight);
        return comboBox;
    }
    
    private Label NewLabelControl()
    {
        var label = new Label();
        label.Font = this.ControlFont;
        label.Size = new Size(this.ControlWidth, this.ControlHeight);
        label.TextAlign = ContentAlignment.MiddleCenter;
        return label;
    }

    private NumericUpDown NewNumericInputControl()
    {
        var numericUpDown = new NumericUpDown();
        numericUpDown.Font = this.ControlFont;
        numericUpDown.Size = new Size(this.ControlWidth, this.ControlHeight);
        numericUpDown.TextAlign = HorizontalAlignment.Center;
        return numericUpDown;
    }
}