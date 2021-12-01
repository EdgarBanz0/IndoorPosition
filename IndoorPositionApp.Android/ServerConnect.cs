using IndoorPositionApp.Droid;
using IndoorPositionApp.Model;
using IndoorPositionApp.Values;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ServerConnect))]
namespace IndoorPositionApp.Droid
{
    class ServerConnect : DBconnection
    {
        public static string entry = "insert into TestData(T,IdU,IdT,IdM,XY,RSSI,Distance) values (@T,@IdU,@IdT,@IdM,@XY,@RSSI,@Distance)";
        public static string selectAllQ = "select * from TestData order by IdT desc";
        public static string entry2 = "insert into ChartSamples(IdCT,Samples,Data,R1,R2,R3,R4,R5,R6,R7,R8,R9,R10) values (@IdCT,@Samples,@Data,@R1,@R2,@R3,@R4,@R5,@R6,@R7,@R8,@R9,@R10)";
        public static string selectAllQ2 = "select * from TestData order by IdCT desc";
        MySqlConnection ms;

        public async Task<bool> SubmitEntry(IEnumerable<TestData> data)
        {
            MySqlConnectionStringBuilder Builder = new MySqlConnectionStringBuilder();
            Builder.Port = 3306;
            // usar la ip del servidor
            Builder.Server = "sql3.freemysqlhosting.net";
            Builder.Database = "sql3407330";
            Builder.UserID = "sql3407330"; //usuario de la base de datos
            Builder.Password = "ahprawp9lP"; //La contraseña del usuario

            try
            {
                ms = new MySqlConnection(Builder.ToString());
                ms.Open(); //agregar la referencia System.Data

                await Task.Run(() =>
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = ms;

                    cmd.CommandText = entry;
                    cmd.Parameters.Add(new MySqlParameter("@T", MySqlDbType.Int32));
                    cmd.Parameters.Add(new MySqlParameter("@IdU", MySqlDbType.Int32));
                    cmd.Parameters.Add(new MySqlParameter("@IdT", MySqlDbType.Int32));
                    cmd.Parameters.Add(new MySqlParameter("@IdM", MySqlDbType.Int32));
                    cmd.Parameters.Add(new MySqlParameter("@XY", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@RSSI", MySqlDbType.Int32));
                    cmd.Parameters.Add(new MySqlParameter("@Distance", MySqlDbType.Decimal));

                    foreach (TestData item in data)
                    {

                        //cmd.Parameters["@T"].Value = item.T;
                        cmd.Parameters["@IdU"].Value = item.IdU;
                        cmd.Parameters["@IdT"].Value = item.IdT;
                        cmd.Parameters["@IdM"].Value = item.IdM;
                        cmd.Parameters["@XY"].Value = item.XY;
                        cmd.Parameters["@RSSI"].Value = item.RSSI;
                        cmd.Parameters["@Distance"].Value = item.Distance;

                        cmd.ExecuteNonQuery();
                    }

                    cmd.Parameters.Clear();
                });

            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR DE REGISTRO: " + e);
                return false;
            }

            ms.Close(); //agregar la referencia System.Data

            return true;
        }

        public async Task<bool> SubmitEntry2(List<Pages.DataCharts.ChartSamples> data)
        {
            MySqlConnectionStringBuilder Builder = new MySqlConnectionStringBuilder();
            Builder.Port = 3306;
            // usar la ip del servidor
            Builder.Server = "sql3.freemysqlhosting.net";
            Builder.Database = "sql3407330";
            Builder.UserID = "sql3407330"; //usuario de la base de datos
            Builder.Password = "ahprawp9lP"; //La contraseña del usuario

            try
            {
                ms = new MySqlConnection(Builder.ToString());
                ms.Open(); //agregar la referencia System.Data

                await Task.Run(() =>
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = ms;

                    cmd.CommandText = entry2;
                    cmd.Parameters.Add(new MySqlParameter("@IdCT", MySqlDbType.Int32));
                    cmd.Parameters.Add(new MySqlParameter("@Samples", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@Data", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@R1", MySqlDbType.Decimal));
                    cmd.Parameters.Add(new MySqlParameter("@R2", MySqlDbType.Decimal));
                    cmd.Parameters.Add(new MySqlParameter("@R3", MySqlDbType.Decimal));
                    cmd.Parameters.Add(new MySqlParameter("@R4", MySqlDbType.Decimal));
                    cmd.Parameters.Add(new MySqlParameter("@R5", MySqlDbType.Decimal));
                    cmd.Parameters.Add(new MySqlParameter("@R6", MySqlDbType.Decimal));
                    cmd.Parameters.Add(new MySqlParameter("@R7", MySqlDbType.Decimal));
                    cmd.Parameters.Add(new MySqlParameter("@R8", MySqlDbType.Decimal));
                    cmd.Parameters.Add(new MySqlParameter("@R9", MySqlDbType.Decimal));
                    cmd.Parameters.Add(new MySqlParameter("@R10", MySqlDbType.Decimal));

                    foreach (Pages.DataCharts.ChartSamples item in data)
                    {
                        cmd.Parameters["@Samples"].Value = item.Samples;
                        cmd.Parameters["@Data"].Value = item.Data;
                        cmd.Parameters["@R1"].Value = item.Router1;
                        cmd.Parameters["@R2"].Value = item.Router2;
                        cmd.Parameters["@R3"].Value = item.Router3;
                        cmd.Parameters["@R4"].Value = item.Router4;
                        cmd.Parameters["@R5"].Value = item.Router5;
                        cmd.Parameters["@R6"].Value = item.Router6;
                        cmd.Parameters["@R7"].Value = item.Router7;
                        cmd.Parameters["@R8"].Value = item.Router8;
                        cmd.Parameters["@R9"].Value = item.Router9;
                        cmd.Parameters["@R10"].Value = item.Router10;

                        cmd.ExecuteNonQuery();
                    }

                    cmd.Parameters.Clear();
                });

            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR DE REGISTRO: " + e);
                return false;
            }

            ms.Close(); //agregar la referencia System.Data

            return true;
        }

        public void LoadEntry(MySqlConnection ms)
        {
            MySqlCommand cmd = new MySqlCommand(selectAllQ, ms);
            cmd.Connection = ms;

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {

            }
        }
    }
}