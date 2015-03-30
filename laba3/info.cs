using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace laba3
{
   public static class info
    {
       public static string Path = "opt.dat";
       public static string Load()
       {
           try
           {
               return File.ReadAllLines(Path)[0];
           }
           catch
           {
               return null;
           }
       }

       public static void Save(string Color)
       {
           string[] m=new string[1];
           try
           {
               m = File.ReadAllLines(Path);
           }
           catch { }
           m[0] = Color;
           File.WriteAllLines(Path, m);
       }

         
    }
}
