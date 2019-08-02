using System;
using System.Diagnostics;

//Prime number calculation using CPU
//2019 ChronomeDev


namespace ConsoleApp1
{
    class cpu_tes
    {

           
        public static void prima()
        {
            int batas = 130000;

            Stopwatch waktu = new Stopwatch();
            waktu.Start();
            int[] list = new int[batas];
            int bilangan = 1;
            //int modulus = 1;
            int faktor = 0;
            int index = 0;
            while (bilangan < batas)
            {
                int modulus = 1;
                while (modulus <= bilangan)
                {
                    if (bilangan != 1)
                    {
                        if ((bilangan % modulus) == 0)
                        {
                            faktor++;
                        }
                    }
                    modulus++;
                }
                if (faktor == 2)
                {
                    //Console.WriteLine(bilangan);
                    list[index] = bilangan;
                    index++;                
                }
                bilangan++;
                faktor = 0;
            }
           
            waktu.Stop();
            TimeSpan ts = waktu.Elapsed;
            String total = ts.Seconds.ToString();
            Console.WriteLine("Total CPU ------ {0} detik> ", total);
            Console.WriteLine("");

        }
        //public static void prima()
        //{
        //    int batas = 10000;

        //    Stopwatch waktu = new Stopwatch();
        //    waktu.Start();
        //    int[] list = new int[batas];

        //    char[] eh = new char[1];
        //    int index = 0;
        //    int bilangan = 1;

        //    while (index < batas)
        //    {
        //        int modulus = 1;
        //        int faktor = 0;
        //        while (modulus <= bilangan)
        //        {
        //            if (bilangan !=1)
        //            {
        //                if ((bilangan % modulus) == 0)
        //                {
        //                    faktor++;
        //                }
        //            }
        //            modulus++;
        //        }
        //        if (faktor > 2)
        //        {
        //            //Console.WriteLine(bilangan);
        //            index++;
        //        }
        //        bilangan++;
        //    }
        //    waktu.Stop();
        //    TimeSpan ts = waktu.Elapsed;
        //    String total = ts.Seconds.ToString();
        //    Console.WriteLine("Total CPU ------ > " + total);
        //}
    }
}
