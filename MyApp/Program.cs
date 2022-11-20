using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.Clear();
            // //var data = new DateTime(2022,11, 12, 13,25,12);
            // var data = DateTime.Now; 
            // Console.WriteLine(data);
            // Console.WriteLine(data.Year);
            // Console.WriteLine(data.Month);
            // Console.WriteLine(data.Day);
            // Console.WriteLine(data.Hour);
            // Console.WriteLine(data.Minute);
            // Console.WriteLine(data.Second); 
            // Console.WriteLine(data.DayOfWeek);
            // Console.WriteLine(data.DayOfYear);

            // Console.WriteLine(data.ToString("dd-MM-yyyy"));

            Console.WriteLine("Port number you want to clear");
                var input = Console.ReadLine();
                //var port = int.Parse(input);
                var prc = new ProcManager();
                prc.KillByPort(7107); //prc.KillbyPort(port);


        }
    }      

} 

public class PRC
     {
            public int PID { get; set; }
            public int Port { get; set; }
            public string Protocol { get; set; }
     }
        public class ProcManager
        {
            public void KillByPort(int port)
            {
                var processes = GetAllProcesses();
                if (processes.Any(p => p.Port == port))
                 try{
                    Process.GetProcessById(processes.First(p => p.Port == port).PID).Kill();
                    }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                else
                {
                    Console.WriteLine("No process to kill!");
                }
            }
    
            public List<PRC> GetAllProcesses()
            {
                var pStartInfo = new ProcessStartInfo();
                pStartInfo.FileName = "netstat.exe";
                pStartInfo.Arguments = "-a -n -o";
                pStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                pStartInfo.UseShellExecute = false;
                pStartInfo.RedirectStandardInput = true;
                pStartInfo.RedirectStandardOutput = true;
                pStartInfo.RedirectStandardError = true;
    
                var process = new Process()
                {
                    StartInfo = pStartInfo
                };
                process.Start();
    
                var soStream = process.StandardOutput;
                
                var output = soStream.ReadToEnd();
                if(process.ExitCode != 0)
                    throw new Exception("somethign broke");
    
                var result = new List<PRC>(); 
                    
               var lines = Regex.Split(output, "\r\n");
                foreach (var line in lines)
                {
                    if(line.Trim().StartsWith("Proto"))
                        continue;
                    
                    var parts = line.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
    
                    var len = parts.Length;
                    if(len > 2)
                        result.Add(new PRC
                        {
                            Protocol = parts[0],
                            Port = int.Parse(parts[1].Split(':').Last()),
                            PID = int.Parse(parts[len - 1])
                        });
                   
                 
                }
                return result;
            }
        }
