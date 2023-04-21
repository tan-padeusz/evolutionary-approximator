using System.ComponentModel;
using Controls;

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

    #region InputControls

    private NumericInputControl ContestantsControl { get; init; }
    private ItemInputControl ErrorMetricControl { get; init; }
    private ItemInputControl GeneTypeControl { get; init; }
    private NumericInputControl MaxPolynomialDegreeControl { get; init; }
    private NumericInputControl MutationProbabilityControl { get; init; }
    private ItemInputControl PointFunctionControl { get; init; }
    private NumericInputControl populationSizeControl { get; init; }
    private NumericInputControl PrecisionDigitsControl { get; init; }

    #endregion

    public ApproximatorForm()
    {
        
    }
}