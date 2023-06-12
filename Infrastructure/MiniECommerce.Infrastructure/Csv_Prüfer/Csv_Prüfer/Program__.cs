using System;
using System.Globalization;

namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            string DateFormat1 = "MM/dd/yyyy";
            string DateFormat2 = "dd/MM/yyyy";

            //string MyDate = "30.02.2000";
            //var CurrentDate = DateTime.Now;

            //var converted = DateTime.Parse(MyDate);

            //Console.WriteLine(converted.ToString(DateFormat1));
            //Console.ReadKey();


            // Convert a null string.
            Console.WriteLine("CurrentCulture is {0}.", CultureInfo.CurrentCulture.Name);


            //var culture = new CultureInfo(CultureInfo.CurrentCulture.Name);
            var cultureInfoDe = new CultureInfo("de-DE");
            var cultureInfoUs = new CultureInfo("en-US");

            string dateString = "22.10.2015";
            CultureInfo provider = CultureInfo.InvariantCulture;
            // It throws Argument null exception
            DateTime dateTime10 = DateTime.Parse(dateString, cultureInfoDe);


            Console.WriteLine(dateTime10.ToString(CultureInfo.CurrentCulture));
            Console.ReadKey();

        }
    }
}
