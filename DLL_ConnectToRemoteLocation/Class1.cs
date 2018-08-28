using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL_ConnectToRemoteLocation
{
    public class NetworkFunctionality
    {
        public string ConnectToRemoteServerLocation(string rutaDeConexion, string username, string password)
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
                    return "El sistema no pudo encontrar la ruta especificada";
                    break;
                default:
                    return "no ejecutado";
                    break;
            }

        }

        public string EraseAllActiveConnections()
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
                    return "El sistema no pudo encontrar la ruta especificada";
                    break;
                default:
                    return "no ejecutado";
                    break;
            }


        }


        private int ExecuteCommand(string command)
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


    }
}
