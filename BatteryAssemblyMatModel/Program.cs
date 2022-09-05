using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BatteryAssemblyMatModel
{
    class Program
    {
        class BatteryAssembly
        {
            const int MaxLoad = 6000;
            const int BatteryCount = 6;
            const int BatteryMaxConsumption = 1500;
            int AssemblyMaxConsumption
            {
                get => BatteryMaxConsumption * BatteryCount;
            }

            public int CalculateAssemblyConsumptionRate(int currentLoad)
            {
                double aux = MaxLoad - currentLoad;
                aux /= BatteryCount;
                aux /= (BatteryMaxConsumption / 10);
                aux = Math.Floor(aux);
                aux *= 10;
                aux = aux < 0 ? 0 : aux;  
                return (int)aux;
            }
            public int ConsumptionToKW(int cons) => (int)((double)cons / 100 * BatteryMaxConsumption * BatteryCount);
             
            
        }
        class Reactor
        {
            BatteryAssembly assembly;
            int subLoad = 2000;
            int load = 0;
            int prevAssemblyConsumptionKW = 0; //DA SOLUTION
            public Reactor(int initLoad)
            {
                subLoad = initLoad;
                load = subLoad;
                assembly = new BatteryAssembly();
                Start();
            }
            void Start()
            {
                while (true)
                {
                    Thread.Sleep(500);
                    int assemblyConsumptionPercentage = assembly.CalculateAssemblyConsumptionRate(load - prevAssemblyConsumptionKW);
                    int assemblyConsumptionKW = assembly.ConsumptionToKW(assemblyConsumptionPercentage);
                    prevAssemblyConsumptionKW = assemblyConsumptionKW;
                    load = subLoad + assemblyConsumptionKW;
                    Console.WriteLine($"Load:{load} | AssemblyCosumption:{assemblyConsumptionPercentage} | AssemblyConsumtionKW:{assemblyConsumptionKW}");
                    // subLoad = subLoad > 6000 ? subLoad - 200 : subLoad + 200;
                }
            }


        }
        static void Main(string[] args)
        {
           var aux = new Reactor(2000);
        }
    }
}
