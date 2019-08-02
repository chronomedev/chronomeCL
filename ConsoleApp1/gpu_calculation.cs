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

//Kalkulasi menggunakan VGA/GPU
namespace ConsoleApp1
{
    class gpu_calculation
    {

        // is prime constant/konstanta prima = 1
        // in not prime constant/konstanta bukan prima = 0
        public static int KONSTANTA_THREAD = 130000;



        public static void primaGPU()
        {
            CudafyModule modul_kernel = CudafyTranslator.Cudafy();
            GPGPU vga = CudafyHost.GetDevice(CudafyModes.Target, CudafyModes.DeviceId);
            vga.LoadModule(modul_kernel);

            Stopwatch waktu = new Stopwatch();
            waktu.Start();
            int[] list_cpu = new int[KONSTANTA_THREAD];
            int[] list_cpy = new int[KONSTANTA_THREAD];
            int[] list = vga.Allocate<int>(KONSTANTA_THREAD);
            vga.Launch(KONSTANTA_THREAD, 1).ModulAtomic(list);
            vga.CopyFromDevice(list, list_cpy);
            vga.FreeAll();

            int index = 0;
            for (int z = 0; z < list_cpy.Length; z++)
            {
                if(list_cpy[z] != -1)
                {
                    list_cpu[index] = list_cpy[z];
                    //Console.WriteLine(list_cpu[index]);
                    index++;
                }
            }
            waktu.Stop();
            TimeSpan ts = waktu.Elapsed;
            String total = ts.Seconds.ToString();
            Console.WriteLine("Total GPU ------ {0} detik> ", total);
        }

        [Cudafy]
        public static void ModulAtomic(GThread thread, int[] ls)
        {
            int id_thread = thread.blockIdx.x;
            int faktor = 0;
            if (id_thread == 0 || id_thread == 1)
            {
                ls[id_thread] = -1;
            } else
            {
                for (int z = 1; z <= id_thread; z++)
                {
                    if (id_thread % z == 0)
                    {
                        faktor++;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (faktor > 2)
                {
                    ls[id_thread] = -1;
                }
                else
                {
                    ls[id_thread] = id_thread;
                }
              
            }
            
        }
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
