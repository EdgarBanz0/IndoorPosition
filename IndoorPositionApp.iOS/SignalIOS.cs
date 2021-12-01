using CoreBluetooth;
using CoreFoundation;
using Foundation;
using IndoorPositionApp.iOS;
using IndoorPositionApp.Values;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SignalIOS))]
namespace IndoorPositionApp.iOS
{
    class SignalIOS : WifiSSIDs
    {
        ClassManagerDelegate dele;
        CBCentralManager manager;

        public async Task<List<String>> SSIDsAsync()
        {
            dele = new ClassManagerDelegate();
            manager = new CBCentralManager(dele, DispatchQueue.CurrentQueue);
            await Task.Delay(10000); //10 segundos de busqueda de perifericos
            manager.StopScan();
            return dele.arrayList;
        }

        public async Task<int> SingleRssi(string ssid)
        {
            int index = 0;
            int rssi = 0;

            dele = new ClassManagerDelegate();
            manager = new CBCentralManager(dele, DispatchQueue.CurrentQueue);
            await Task.Delay(2000); //2 segundos de busqueda de perifericos
            manager.StopScan();

            foreach (string item in dele.arrayList)
            {
                if (item == ssid)
                {
                    rssi = dele.rssisList[index];
                    dele.Dispose();
                    return rssi;
                }
                index++;
            }
            dele.Dispose();
            return rssi;
        }


        public async Task<List<int>> RSSIs(List<string> ssids)
        {
            List<int> rssis = new List<int>();
            int index;

            dele = new ClassManagerDelegate();
            manager = new CBCentralManager(dele, DispatchQueue.CurrentQueue);
            await Task.Delay(4000); //4 segundos de busqueda de perifericos
            manager.StopScan();

            for (int i = 0; i < ssids.Count; i++)
            {
                index = 0;
                foreach (string item in dele.arrayList)
                {

                    if (item == ssids[i])
                    {
                        rssis.Add(dele.rssisList[index] * (-1));//transforma RSSI a signo positivo
                        break;
                    }
                    index++;
                }

                if (rssis.Count <= i)
                    rssis.Add(100);//senal del router perdida
            }

            dele.Dispose();
            return rssis;
        }

    }

    public class ClassManagerDelegate : CBCentralManagerDelegate
    {
        public List<string> arrayList = new List<string>();
        public List<int> rssisList = new List<int>();

        override public void UpdatedState(CBCentralManager mgr)
        {

            Console.WriteLine($"UpdatedState: {mgr.State}");
            if (mgr.State == CBCentralManagerState.PoweredOn)
            {
                CBUUID[] cbuuids = null;
                mgr.ScanForPeripherals(cbuuids);

            }
            else
            {
                Console.WriteLine("Bluetooth no disponible");
            }
        }

        public override void DiscoveredPeripheral(CBCentralManager central, CBPeripheral peripheral, NSDictionary advertisementData, NSNumber RSSI)
        {
            if (peripheral.Name != null)
            {
                arrayList.Add(peripheral.Name);
                rssisList.Add(-Math.Abs(RSSI.Int32Value));

                Console.WriteLine("Discovered {0}, RSSI {1}", peripheral.Name, RSSI);
            }
        }

    }
}