using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndoorPositionApp.Values
{
    public interface WifiSSIDs
    {
        Task<List<String>> SSIDsAsync();
        Task<int> SingleRssi(string ssid);
        Task<List<int>> RSSIs(List<string> ssids);
    }
}
