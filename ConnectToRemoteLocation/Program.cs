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
            //NetCredentialusage();
            string command = @"NET USE * /delete /y";

            //command = "NET USE " + @"Full route to shared folder" + " /user:" + @"username" + " " + "password";
            int timeout = 5000;
            var processInfo = new ProcessStartInfo("cmd.exe", "/C " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = "C:\\",
            };

            //CMD RETURN CODES: 
            /*
             *       0	The operation completed successfully.
                     1	Incorrect function.
                     2	The system cannot find the file specified.
                            
             * */

            var process = Process.Start(processInfo);
            process.WaitForExit(timeout);
            var exitCode = process.ExitCode;
            process.Close();
            //return exitCode;
            if(exitCode == 0 )
                //Console.WriteLine(exitCode.ToString());
                Console.WriteLine("Exito");
            else
                Console.WriteLine("Fracaso");
            //ExecuteCommand(command, 5000);


            Console.ReadLine();

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
            ExecuteCommand(command, 5000);

            command = "NET USE " + directory + " /user:" + username + " " + password;
            ExecuteCommand(command, 5000);

            command = " copy \"" + filePath + "\"  \"" + directory + filenameToSave + "\"";

            ExecuteCommand(command, 5000);


            command = "NET USE " + directory + " /delete";
            ExecuteCommand(command, 5000);
        }

        public static int ExecuteCommand(string command, int timeout)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/C " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = "C:\\",
            };

            var process = Process.Start(processInfo);
            process.WaitForExit(timeout);
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
