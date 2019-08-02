using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Cudafy;
using Cudafy.Host; 
using Cudafy.Translator;

//Simple GPU Utilization in programming
// 2019 ChronomeDev 

namespace ConsoleApp1
{
    class Program
    {

        
        [STAThread]
        static void Main(string[] args)
        {
            CudafyModes.Target = eGPUType.OpenCL; // To use OpenCL, change this enum inisialisasi mode
            CudafyModes.DeviceId = 0; //ambil indeks pertama idnya
            CudafyTranslator.Language = CudafyModes.Target == eGPUType.OpenCL ? eLanguage.OpenCL : eLanguage.Cuda;
            try
            {
                int deviceCount = CudafyHost.GetDeviceCount(CudafyModes.Target);
                if (deviceCount == 0)
                {
                    Console.WriteLine("No suitable {0} devices found.", CudafyModes.Target);
                    goto theEnd;
                }
                Console.WriteLine("--------------Program Processing VGA ------------");
                Console.WriteLine("-------------- C h r o n o M E Dev ------------");
                GPGPU gpu = CudafyHost.GetDevice(CudafyModes.Target, CudafyModes.DeviceId); // ngambil device gpunya yang terdaftar pertama sesuai device id
                Console.WriteLine("Radeon RX 570/580 Series" );
                Console.WriteLine("Code name: " + gpu.GetDeviceProperties(false).Name);
                Console.WriteLine("Memori VGA: " + gpu.GetDeviceProperties(false).TotalMemory);
                Console.WriteLine("Jumlah Pipeline " + gpu.GetDeviceProperties(false).MultiProcessorCount); //ROPSnya diambil

                Boolean selesai = false;
                String komen = "";
                do
                {
                   gpu_calculation.eksekusi();
                    //cpu_tes.eksekusi();
                    cpu_tes.prima();
                    Console.WriteLine("Done!");

                    Console.Write("> ");
                    komen = Console.ReadLine().ToString();


                } while (komen != "q");
                
            //Console.WriteLine("Done!");
            //    Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        theEnd:
            Console.ReadKey();
        }

        //public static void kirim_webclient()
        //{
        //    WebClient klien = new WebClient();
        //    klien.Headers.Add("Content-Typeyd", "binary/octet-stream");
        //    byte[] result = klien.UploadFile("http://unicraftstore.com/wp-includes/server_pemvis/kelola_file.php", "POST", @"C:\Users\Hansrenee\Pictures\yorha-type-b-hd-wallpapers-67675-3372375.jpg");

        //    string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
        //    Console.WriteLine("HASIL VALUE DARI SERVER: " + s);
        //    Console.ReadKey();
        //}
        //static void Main(string[] args)
        //{
        //    kirim_webclient();

        //}

    }
}
