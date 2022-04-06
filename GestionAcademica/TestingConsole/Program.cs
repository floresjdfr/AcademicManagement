using BusinessLogic;
using GestionAcademica.DataAccess;
using System;


namespace TestingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            CareerService careerService = new CareerService();
            //var result = careerService.InsertCareer(new Career { Code = "123", CareerName = "Matematica", DegreeName = "Matematica para Empresas" });
            var list = careerService.ListCareer();

            list.ForEach(C =>
            {
                Console.WriteLine(C.PK);
                Console.WriteLine(C.Code);
                Console.WriteLine(C.CareerName);
                Console.WriteLine(C.DegreeName);
                Console.WriteLine("------------------");
            });
        }
    }
}
