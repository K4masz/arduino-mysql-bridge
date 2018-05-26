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
            Console.ReadKey();
        }
    }

    public class DbConnection
    {
        private DbConnection()
        {
        }

        private string DatabaseName { get; set; } = string.Empty;
        private string Password { get; set; }
        private MySqlConnection Connection { get; set; }
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
                    string.Format("Server=localhost;Port=3306;Uid=Arduino;password=arduino;", DatabaseName);
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