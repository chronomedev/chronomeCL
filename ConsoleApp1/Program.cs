using System;
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

            String komen;
            CudafyModes.Target = eGPUType.OpenCL;
            CudafyModes.DeviceId = 0; 
            CudafyTranslator.Language = CudafyModes.Target == eGPUType.OpenCL ? eLanguage.OpenCL : eLanguage.Cuda;
            try
            {
                int deviceCount = CudafyHost.GetDeviceCount(CudafyModes.Target);
                if (deviceCount == 0)
                {
                    Console.WriteLine("Tidak ada device ditemukan {0}", CudafyModes.Target);
                }
                else
                {
                    Console.WriteLine("--------------Program Processing VGA ------------");
                    Console.WriteLine("-------------- C h r o n o M E Dev ------------");
                    GPGPU gpu = CudafyHost.GetDevice(CudafyModes.Target, CudafyModes.DeviceId);
                    Console.WriteLine("Radeon RX 570/580 Series");
                    Console.WriteLine("Code name: " + gpu.GetDeviceProperties(false).Name);
                    Console.WriteLine("Memori VGA: " + gpu.GetDeviceProperties(false).TotalMemory);
                    Console.WriteLine("Jumlah Pipeline " + gpu.GetDeviceProperties(false).MultiProcessorCount); 

                    do
                    {
                        gpu_calculation.primaGPU();
                        //gpu_calculation.eksekusi();
                        //cpu_tes.eksekusi();
                        cpu_tes.prima();
                        Console.WriteLine("Done!");

                        Console.Write("Press q to quit > ");
                        komen = Console.ReadLine().ToString();
                    } while (komen != "q");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
