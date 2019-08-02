using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Cudafy;
using Cudafy.Translator;
using Cudafy.Host;

//HSD GPU basic utilization
//Copyright 2019 ChronomeDev
//and Cudafy
namespace ConsoleApp1
{
    class gpu_calculation
    {
       
        public static int KONSTANTA_THREAD = 1000;
        public static void eksekusi()
        {
            CudafyModule kernel_modul = CudafyTranslator.Cudafy();
            GPGPU vga = CudafyHost.GetDevice(CudafyModes.Target, CudafyModes.DeviceId);
            vga.LoadModule(kernel_modul);
            Stopwatch waktu = new Stopwatch();
            waktu.Start();
            int[] array_vga = vga.Allocate<int>(KONSTANTA_THREAD);
            int[] array_hasil = new int[KONSTANTA_THREAD];

            //long[] matriks1 = vga.Allocate<long>(KONSTANTA_THREAD);
            //long[] matriks2 = vga.Allocate<long>(KONSTANTA_THREAD);//new int[KONSTANTA_THREAD];
            //long[] matriks3 = vga.Allocate<long>(KONSTANTA_THREAD); //[KONSTANTA_THREAD];

            vga.Launch(KONSTANTA_THREAD, 1).fungsiAtomic(array_vga);
            vga.CopyFromDevice(array_vga, array_hasil);
            vga.FreeAll();

            //for(int z = 0; z < array_hasil.Length; z++)
            //{
            //    Console.WriteLine("Hasil Ekstrak----" + array_hasil[z]);
            //}
            vga.FreeAll();
            waktu.Stop();
            TimeSpan ts = waktu.Elapsed;
            String total = ts.Milliseconds.ToString();
            Console.WriteLine("Total VGA ------ > " + total);


        }


        [Cudafy]
        public static void fungsiAtomic(GThread thread, int[] azz)
        {
            int Threadid = thread.blockIdx.x;
            int batas = (Threadid * 2);
            azz[Threadid] = batas;
            if (azz[Threadid] == 1)
            {
                batas = 1000;
            }
            //for(int z= 0; z < batas; z++)
            //{
            //    azz[Threadid] = z;
            //}


        }
    }
}
