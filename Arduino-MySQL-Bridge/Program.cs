using System;
using System.Runtime.CompilerServices;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace Ardunio_MySQL_Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var dbCon = DbConnection.Instance();
            dbCon.DatabaseName = "weather_station";
            if (dbCon.IsConnect())
            {
                string query = "select * from measurements;";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetInt16("measurement_id"));
                    Console.WriteLine(reader.GetDouble("s1_humidity"));
                    Console.WriteLine(reader.GetDouble("s2_humidity"));
                }

                reader.Close();

                string query2 =
                    string.Format(
                        "Insert Into measurements (s1_humidity, s2_humidity, s1_temperature, s2_temperature, light_sensor_1, light_sensor_2, time) values ({0},{1},{2},{3},{4},{5},{6});",
                        20, 20, 20, 30, 40, 50, "now()");
                var cmd2 = new MySqlCommand(query2, dbCon.Connection);
                cmd2.ExecuteNonQuery();
            }

            Console.ReadKey();
        }
    }

    public class DbConnection
    {
        private DbConnection()
        {
        }

        public string DatabaseName { get; set; } = string.Empty;
        private string Password { get; set; }
        public MySqlConnection Connection { get; set; }
        private static DbConnection _instance = null;

        public static DbConnection Instance()
        {
            return _instance ?? (_instance = new DbConnection());
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(DatabaseName))
                    return false;
                string connstring =
                    string.Format(
                        "Server=192.168.0.101;database={0};Port=3306;Uid=ArduinoRemote;password=arduinoremote;SslMode=none;",
                        DatabaseName);
                Connection = new MySqlConnection(connstring);
                Connection.Open();
            }

            return true;
        }

        public void Close()
        {
            if (this.IsConnect())
            {
                Connection.Close();
                Console.WriteLine("Connection closed successfully.");
            }
            else
            {
                Console.WriteLine("Connection is not open to be closed.");
            }
        }
    }


    class Measurement
    {
        public double DigitalTemperatureOne { get; set; }
        public double DigitalTemperatureTwo { get; set; }
        public double AnalogTemperatureOne { get; set; }
        public double AnalogTemperatueTwo { get; set; }
        public double DigitalHumidityOne { get; set; }
        public double DigitalHumidityTwo { get; set; }
        public int LightLevelOne { get; set; }
        public int LightLevelTwo { get; set; }
        public double AnalogGroundMoistureLevel { get; set; }
    }
}