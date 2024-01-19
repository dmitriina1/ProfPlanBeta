using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Man man = new Man();
            Man2 man2 = new Man2();
            ObservableCollection<Emplee> list = new ObservableCollection<Emplee>();
            list.Add(man);
            list.Add(man2);
            for(int i = 0;i < list.Count; i++)
            {
                Console.WriteLine(list[i].ToString());
            }
            Console.ReadLine();
        }
    }

    public class Man: Emplee
    {
        public Man() { }
        public int Age { get; set; }
    }
    public class Man2:Emplee
    {
        public Man2() { }
        public int Sex { get; set; }
    }
    public class Emplee
    {
        public Emplee() { }
    }
}
