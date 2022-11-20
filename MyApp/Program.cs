using System;
using System.Text;
namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            var data = new DateTime(2022,11, 12, 13,25,12);
            //var data = DateTime.Now; 
            Console.WriteLine(data);
            Console.WriteLine(data.Year);
            Console.WriteLine(data.Month);
            Console.WriteLine(data.Day);
            Console.WriteLine(data.Hour);
            Console.WriteLine(data.Minute);
            Console.WriteLine(data.Second); 
        }
    }      

} 