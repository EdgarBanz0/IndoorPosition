using Android.Content;
using Android.Net.Wifi;
using IndoorPositionApp.Droid;
using IndoorPositionApp.Values;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SignalDroid))]

namespace IndoorPositionApp.Droid
{
    public class SignalDroid : WifiSSIDs
    {
        private WifiManager wifiManager;
        private IList<ScanResult> results;

        public SignalDroid()
        {
            Context context = Android.App.Application.Context;
            wifiManager = (WifiManager)context.GetSystemService(Context.WifiService);
            wifiManager.StartScan();

        }

        public async Task<int> SingleRssi(string ssid)
        {
            results = wifiManager.ScanResults;

            int rssi = 0;


            await Task.Run(() =>
            {
                foreach (ScanResult item in results)
                {
                    if (item.Ssid == ssid)
                    {
                        rssi = item.Level;
                        wifiManager.StartScan();
                        return rssi;

                    }
                }
                wifiManager.StartScan();
                return rssi = -100;
            });
            return rssi;
        }

        public async Task<List<String>> SSIDsAsync()
        {
            results = wifiManager.ScanResults;
            List<String> arrayList = new List<string>();

            await Task.Run(() =>
            {
                foreach (ScanResult item in results)
                {
                    arrayList.Add(item.Ssid);
                }
            });

            return arrayList;
        }

        public async Task<List<int>> RSSIs(List<string> ssids)
        {
            List<int> rssis = new List<int>();
            results = wifiManager.ScanResults;

            await Task.Run(() =>
            {
                for (int i = 0; i < ssids.Count; i++)
                {
                    foreach (ScanResult item in results)
                    {
                        if (item.Ssid == ssids[i])
                        {
                            rssis.Add(item.Level * (-1));//transforma RSSI a signo positivo
                            wifiManager.StartScan();
                            break;
                        }
                    }

                    if (rssis.Count <= i)
                        rssis.Add(100);//senal del router perdida
                }
            });
            return rssis;
        }
    }
}