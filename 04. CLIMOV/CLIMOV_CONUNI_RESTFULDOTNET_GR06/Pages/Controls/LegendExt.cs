using Syncfusion.Maui.Toolkit.Charts;

namespace CLIMOV_CONUNI_RESTFULDOTNET_GR06.Pages.Controls
{
    public class LegendExt : ChartLegend
    {
        protected override double GetMaximumSizeCoefficient()
        {
            return 0.5;
        }
    }
}
