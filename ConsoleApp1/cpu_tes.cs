using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;

//Prime number calculation using CPU
//2019 ChronomeDev


namespace ConsoleApp1
{
    class cpu_tes
    {


        public static void prima()
        {
            int batas = 10000;

            Stopwatch waktu = new Stopwatch();
            waktu.Start();
            int[] list = new int[batas];

            int index = 0;
            int bilangan = 1;

            while (index < batas)
            {
                int modulus = 1;
                int faktor = 0;
                while (modulus <= bilangan)
                {
                    if (bilangan !=1)
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
                    index++;
                }
                bilangan++;
            }
            waktu.Stop();
            TimeSpan ts = waktu.Elapsed;
            String total = ts.Seconds.ToString();
            Console.WriteLine("Total CPU ------ > " + total);
        }
        public static void eksekusi()
        {
            long[] arr = new long[1000000000];
            Stopwatch waktu = new Stopwatch();
            waktu.Start();
            //setTimer();
            //waktu.Start();
            for (int z = 0; z < arr.Length; z++)
            {
                arr[z] = z * 2;
            }
            waktu.Stop();
            TimeSpan ts = waktu.Elapsed;

            Console.WriteLine("TOTAL WAKTU CPU: " + ts.Seconds.ToString());


        }
    }
}
