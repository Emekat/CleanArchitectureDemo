using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTut
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateFileAsync("mekus").Wait();
        }
        public static async Task CreateFileAsync (string name)
        {
            using (StreamWriter writer = File.CreateText(name))
                await writer.WriteAsync("This is a Test");
        } 
    }
}
