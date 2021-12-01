using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndoorPositionApp.Values
{
    public interface DBconnection
    {
        Task<bool> SubmitEntry(IEnumerable<Model.TestData> data);
        Task<bool> SubmitEntry2(List<Pages.DataCharts.ChartSamples> data);
    }
}
