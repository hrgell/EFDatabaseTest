using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FirstDatabaseTestCreate
{
    class Util
    {
        public static string connection_string = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;MultipleActiveResultSets=True;Database=CompanyDB;";

        public static int WriteLine(string txt) {
            System.Diagnostics.Debug.WriteLine(txt);
            if(Environment.UserInteractive)
                Console.WriteLine(txt);
            string path = Environment.GetEnvironmentVariable("LOCALAPPDATA") + @"\Temp\FirstDatabaseTestCreate.txt";
            System.IO.File.AppendAllText(path, txt + Environment.NewLine);
            return 0;
        } // method

        public static string GetConnectionString()
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
            //Util.WriteLine("Connection string: " + (String.IsNullOrEmpty(cs) ? "None" : cs));
            
            //if (string.IsNullOrEmpty(cs))
            //{
            //    ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            //    map.ExeConfigFilename = "App.config"; // "FirstDatabaseTestCreate.dll.config"
            //    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            //    cs = config.ConnectionStrings["DefaultConnection"]?.ConnectionString;
            //    Util.WriteLine("Connection String2: " + config.FilePath + ": " + (String.IsNullOrEmpty(cs) ? "None" : cs));
            //}

            if (!string.IsNullOrEmpty(cs))
                return cs;
            //Util.WriteLine("Using hard coded connection string: " + connection_string);
            return connection_string;
        } // method
    }
}
