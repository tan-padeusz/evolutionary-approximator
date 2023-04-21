using System.ComponentModel;
using Controls;
using Enums;

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

    #region Buttons

    private Button StartButton { get; } = new Button();
    private Button StopButton { get; } = new Button();

    #endregion

    #region Decorations

    private Label HorizontalLine { get; } = new Label();
    private Label LeftVerticalLine { get; } = new Label();
    private Label RightVerticalLine { get; } = new Label();

    #endregion

    #region Input Controls

    private NumericInputControl ContestantsControl { get; } = new NumericInputControl();
    private NumericInputControl DominantParentGeneStrengthControl { get; } = new NumericInputControl();
    private ItemInputControl ErrorMetricControl { get; } = new ItemInputControl();
    private ItemInputControl GeneTypeControl { get; } = new ItemInputControl();
    private NumericInputControl MaxPolynomialDegreeControl { get; } = new NumericInputControl();
    private NumericInputControl MutationProbabilityControl { get; } = new NumericInputControl();
    private ItemInputControl PointFunctionControl { get; } = new ItemInputControl();
    private NumericInputControl PopulationSizeControl { get; } = new NumericInputControl();
    private NumericInputControl PrecisionDigitsControl { get; } = new NumericInputControl();

    #endregion

    #region Output Controls

    public OutputControl AverageErrorControl { get; } = new OutputControl();
    private Label BestFunctionControl { get; } = new Label();
    public RichTextBox BestFunctionOutputControl { get; } = new RichTextBox();

    private Label ControlTableControl { get; } = new Label();
    public RichTextBox ControlTableOutputControl { get; } = new RichTextBox();
    public OutputControl ElapsedTimeControl { get; } = new OutputControl();
    public OutputControl LastImprovementControl { get; } = new OutputControl();
    public OutputControl PopulationsCreatedControl { get; } = new OutputControl();

    #endregion

    public ApproximatorForm()
    {
        this.ConfigureButtons();
        this.ConfigureDecorations();
        this.ConfigureInputControls();
        this.ConfigureOutputControls();
        
        this.ConfigureForm();
    }

    private void ConfigureButtons()
    {
        this.StartButton.Click += this.StartButtonClick;
        this.StartButton.Location = new Point(10, 640);
        this.StartButton.Size = new Size(300, 60);
        this.StartButton.Text = "START";
        this.StartButton.TextAlign = ContentAlignment.MiddleCenter;
        this.Controls.Add(this.StartButton);

        this.StopButton.Click += this.StopButtonClick;
        this.StopButton.Location = new Point(950, 10);
        this.StopButton.Size = new Size(130, 130);
        this.StopButton.Text = "STOP";
        this.StopButton.TextAlign = ContentAlignment.MiddleCenter;
        this.Controls.Add(this.StopButton);
    }

    private void ConfigureDecorations()
    {
        this.HorizontalLine.BackColor = Color.DarkGray;
        this.HorizontalLine.Location = new Point(330, 218);
        this.HorizontalLine.Size = new Size(750, 5);
        this.Controls.Add(this.HorizontalLine);
        
        this.LeftVerticalLine.BackColor = Color.DarkGray;
        this.LeftVerticalLine.Location = new Point(318, 10);
        this.LeftVerticalLine.Size = new Size(5, 690);
        this.Controls.Add(this.LeftVerticalLine);

        this.RightVerticalLine.BackColor = Color.DarkGray;
        this.RightVerticalLine.Location = new Point(1088, 10);
        this.RightVerticalLine.Size = new Size(5, 690);
        this.Controls.Add(this.RightVerticalLine);
    }

    private void ConfigureInputControls()
    {
        this.ContestantsControl.ConfigureControl("CONTESTANTS", 10, 430);
        this.ContestantsControl.ConfigureControlConstraints(20, 100, 50, 10);
        this.Controls.AddRange(this.ContestantsControl.GetControls());
        
        this.DominantParentGeneStrengthControl.ConfigureControl("DOMINANT PARENT GENE STRENGTH", 10, 500);
        this.DominantParentGeneStrengthControl.ConfigureControlConstraints(0, 1000, 600, 25);
        this.Controls.AddRange(this.DominantParentGeneStrengthControl.GetControls());
        
        this.ErrorMetricControl.ConfigureControl("ERROR METRIC", 10, 150);
        this.ErrorMetricControl.Populate(Enum.GetValues<ErrorMetric>());
        this.Controls.AddRange(this.ErrorMetricControl.GetControls());
        
        this.GeneTypeControl.ConfigureControl("GENE TYPE", 10, 10);
        this.GeneTypeControl.Populate(Enum.GetValues<GeneType>());
        this.Controls.AddRange(this.GeneTypeControl.GetControls());
        
        this.MaxPolynomialDegreeControl.ConfigureControl("MAX POLYNOMIAL DEGREE", 10, 220);
        this.MaxPolynomialDegreeControl.ConfigureControlConstraints(0, 5, 2, 1);
        this.Controls.AddRange(this.MaxPolynomialDegreeControl.GetControls());
        
        this.MutationProbabilityControl.ConfigureControl("MUTATION PROBABILITY", 10, 570);
        this.MutationProbabilityControl.ConfigureControlConstraints(0, 1000, 10, 10);
        this.Controls.AddRange(this.MutationProbabilityControl.GetControls());
        
        this.PointFunctionControl.ConfigureControl("POINT FUNCTION", 10, 80);
        this.PointFunctionControl.Populate(Enum.GetValues<PointFunction>());
        this.Controls.AddRange(this.PointFunctionControl.GetControls());
        
        this.PopulationSizeControl.ConfigureControl("POPULATION SIZE", 10, 360);
        this.PopulationSizeControl.ConfigureControlConstraints(200, 1000, 400, 40);
        this.Controls.AddRange(this.PopulationSizeControl.GetControls());
        
        this.PrecisionDigitsControl.ConfigureControl("PRECISION DIGITS", 10, 290);
        this.PrecisionDigitsControl.ConfigureControlConstraints(0, 6, 2, 1);
        this.Controls.AddRange(this.PrecisionDigitsControl.GetControls());
    }

    private void ConfigureForm()
    {
        this.ClientSize = new Size(1100, 710);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Text = "EVOLUTIONARY APPROXIMATOR";
    }

    private void ConfigureOutputControls()
    {
        this.AverageErrorControl.ConfigureControl("AVERAGE ERROR", 640, 10);
        this.Controls.AddRange(this.AverageErrorControl.GetControls());

        this.BestFunctionControl.Font = new Font(FontFamily.GenericMonospace, 16F);
        this.BestFunctionControl.Location = new Point(330, 150);
        this.BestFunctionControl.Size = new Size(750, 30);
        this.BestFunctionControl.Text = "BEST FUNCTION";
        this.BestFunctionControl.TextAlign = ContentAlignment.MiddleLeft;
        this.Controls.Add(this.BestFunctionControl);

        this.BestFunctionOutputControl.BackColor = Color.White;
        this.BestFunctionOutputControl.Font = new Font(FontFamily.GenericMonospace, 16F);
        this.BestFunctionOutputControl.Location = new Point(330, 180);
        this.BestFunctionOutputControl.Multiline = false;
        this.BestFunctionOutputControl.ReadOnly = true;
        this.BestFunctionOutputControl.Size = new Size(750, 30);
        this.Controls.Add(this.BestFunctionOutputControl);

        this.ControlTableControl.Font = new Font(FontFamily.GenericMonospace, 16F);
        this.ControlTableControl.Location = new Point(330, 230);
        this.ControlTableControl.Size = new Size(750, 30);
        this.ControlTableControl.Text = "[ X | Y | Z ] : [ RESULT | ERROR ]";
        this.ControlTableControl.TextAlign = ContentAlignment.MiddleLeft;
        this.Controls.Add(this.ControlTableControl);

        this.ControlTableOutputControl.BackColor = Color.White;
        this.ControlTableOutputControl.Font = new Font(FontFamily.GenericMonospace, 16F);
        this.ControlTableOutputControl.Location = new Point(330, 260);
        this.ControlTableOutputControl.ReadOnly = true;
        this.ControlTableOutputControl.Size = new Size(750, 440);
        this.Controls.Add(this.ControlTableOutputControl);
        
        this.ElapsedTimeControl.ConfigureControl("ELAPSED TIME", 330, 10);
        this.Controls.AddRange(this.ElapsedTimeControl.GetControls());
        
        this.LastImprovementControl.ConfigureControl("LAST IMPROVEMENT", 640, 80);
        this.Controls.AddRange(this.LastImprovementControl.GetControls());
        
        this.PopulationsCreatedControl.ConfigureControl("POPULATIONS CREATED", 330, 80);
        this.Controls.AddRange(this.PopulationsCreatedControl.GetControls());
    }

    private void StartButtonClick(object? sender, EventArgs args)
    {
        
    }

    private void StopButtonClick(object? sender, EventArgs args)
    {
        
    }
}