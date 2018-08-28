using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToRemoteLocation
{
    class Program
    {
        static void Main(string[] args)
        {

            EraseAllActiveConnections();
            ConnectToRemoteServerLocation("server", "user", "password");
            Console.ReadLine();

        }

        public static string ConnectToRemoteServerLocation(string rutaDeConexion, string username, string password)
        {
            string command = "NET USE " + rutaDeConexion + " /user:" + username + " " + password;

            int result = ExecuteCommand(command);
            switch (result)
            {

                case 0:
                    return "Comando Exitoso";
                    break;
                case 1:
                    return "Función Incorrecta";
                    break;
                case 2:
                    return "Hay un dato erróneo";
                    break;
                default:
                    return "no ejecutado";
                    break;
            }

        }



        public static string EraseAllActiveConnections()
        {
            string command = @"NET USE * /delete /y";

            int result = ExecuteCommand(command);
            switch (result)
            {
                
                case 0:
                    return "Comando Exitoso"; 
                    break;
                case 1:
                    return "Función Incorrecta"; 
                    break;
                case 2:
                    return "Hay un dato erróneo";
                    break;
                default:
                    return "no ejecutado";
                    break;
            }


        }

        public static void SaveACopyfileToServer(string filePath, string savePath)
        {
            var directory = Path.GetDirectoryName(savePath).Trim();
            var username = "loginusername";
            var password = "loginpassword";
            var filenameToSave = Path.GetFileName(savePath);

            if (!directory.EndsWith("\\"))
                filenameToSave = "\\" + filenameToSave;

            var command = "NET USE " + directory + " /delete";
            ExecuteCommand(command);

            command = "NET USE " + directory + " /user:" + username + " " + password;
            ExecuteCommand(command);

            command = " copy \"" + filePath + "\"  \"" + directory + filenameToSave + "\"";

            ExecuteCommand(command);


            command = "NET USE " + directory + " /delete";
            ExecuteCommand(command);
        }

        public static int ExecuteCommand(string command)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/C " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = "C:\\",
            };

            var process = Process.Start(processInfo);
            process.WaitForExit(8000);
            var exitCode = process.ExitCode;
            process.Close();
            return exitCode;
        }

        private static void NetCredentialusage()
        {
            NetworkCredential myCred = new NetworkCredential(
                @"username", "password", @"route");

            //CredentialCache myCache = new CredentialCache();
            //myCache.Add(new Uri(@"route"), "Basic", myCred);

            //WebRequest wr = WebRequest.Create(@"route");
            //wr.Credentials = myCache;
        }
    }
}
