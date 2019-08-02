using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cudafy;
using Cudafy.Host;
using Cudafy.Translator;


namespace ConsoleApp1
{
    class Class1
    {
        //pakai hitung pakai VGA 
        public const int N = 10;
        //public static Random asal;

        public static void Execute()
        {
            //asal = new Random();
            CudafyModule modul_kernel = CudafyTranslator.Cudafy(); // ambil tipe API dari public

            GPGPU gpu = CudafyHost.GetDevice(CudafyModes.Target, CudafyModes.DeviceId);
            gpu.LoadModule(modul_kernel);

            int[] a = new int[N];
            int[] b = new int[N];
            int[] c = new int[N];

            // allocate the memory on the GPU
            int[] dev_a = gpu.Allocate<int>(a);
            int[] dev_b = gpu.Allocate<int>(b);
            int[] dev_c = gpu.Allocate<int>(c);//Console.WriteLine("asalzzz ----" + asal.Next(20, 70));
            // fill the arrays 'a' and 'b' on the CPU
            for (int i = 0; i < N; i++)
            {
                a[i] = -i;
                b[i] = i * i;
            }

            // copy the arrays 'a' and 'b' to the GPU
            gpu.CopyToDevice(a, dev_a);
            gpu.CopyToDevice(b, dev_b);

            // launch add on N threads
            gpu.Launch(N, 1).adder(dev_a, dev_b, dev_c);

            // copy the array 'c' back from the GPU to the CPU
            gpu.CopyFromDevice(dev_c, c);

            // display the results
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("{0} + {1} = {2}", a[i], b[i], c[i]);
            }

            // free the memory allocated on the GPU
            gpu.Free(dev_a);
            gpu.Free(dev_b);
            gpu.Free(dev_c);
        }

        [Cudafy] //GThread itu ID dari setiap kita mau buat ini dijalanin di berapa core gpu nya jadi nanti nytesuain 
        public static void adder(GThread thread, int[] a, int[] b, int[] c)
        {
            int id_thread = thread.blockIdx.x;
            //Console.WriteLine("PRINT DARI VGA" + id_thread);
            if (id_thread < N) //buat mastiin id threadnya aja (parent id)
                c[id_thread] = a[id_thread]+b[id_thread];
        }
    }
}
