using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ORM.ADO;
using ORM.Dapper;
using ORM.EF;

namespace ORM.Console
{
    static class Program
    {
        const string ConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SqlModule;Integrated Security=True";
        private static readonly Random randomGenerator = new();
        
        static void Main(string[] args)
        {
            var functions = new List<Action>
            {
                ConnectedAdoTest,
                DisconnectedAdoTest,
                DapperTest,
                EfTest
            };
            
            foreach (var function in functions)
            {
                var methodName = function.Method.Name;
                System.Console.WriteLine(methodName);
                var stopwatch = Stopwatch.StartNew();
                for (var i = 0; i < 100 ; i++)
                {
                    function();
                }
                stopwatch.Stop();
                System.Console.WriteLine($"Performance for {methodName} approach is {stopwatch.ElapsedMilliseconds} s");
            }
        }

        private static void EfTest()
        {
            double testdisntance = randomGenerator.Next(100000, 20000000);
            using var unitOfWork = new EfUnitOfWork(ConnectionString);
            // GetAll
            var allRoutes = unitOfWork.Routes.GetAll();
            var firstRoute = allRoutes.First(x=>x.Id == 11100);
            // GetById
            var findRoute = unitOfWork.Routes.GetById(firstRoute.Id);
            // Update
            findRoute.Distance = testdisntance;
            unitOfWork.Routes.Update(findRoute);
        }
        
        private static void DapperTest()
        {
            double testdisntance = randomGenerator.Next(100000, 20000000);
            using var unitOfWork = new DapperUnitOfWork(ConnectionString);
            // GetAll
            var allRoutes = unitOfWork.Routes.GetAll();
            var firstRoute = allRoutes.First(x=>x.Id == 11100);
            // GetById
            var findRoute = unitOfWork.Routes.GetById(firstRoute.Id);
            // Update
            findRoute.Distance = testdisntance;
            unitOfWork.Routes.Update(findRoute);
        }
        
        private static void ConnectedAdoTest()
        {
            double testdisntance = randomGenerator.Next(100000, 20000000);
            using var unitOfWork = new ADOUnitOfWork(ConnectionString);
            // GetAll
            var allRoutes = unitOfWork.Routes.GetAll();
            var firstRoute = allRoutes.First();
            // GetById
            var findRoute = unitOfWork.Routes.GetById(firstRoute.Id);
            // Update
            findRoute.Distance = testdisntance;
            unitOfWork.Routes.Update(findRoute);
        }

        private static void DisconnectedAdoTest()
        {
            using var unitOfWork = new ADOUnitOfWork(ConnectionString);
            var allRoutes = unitOfWork.DisconnectedRoutes.GetAll();
            double testdisntance = randomGenerator.Next(100000, 20000000);
            // GetAll
            var firstRoute = allRoutes.First();
            // GetById
            var findRoute = unitOfWork.DisconnectedRoutes.GetById(firstRoute.Id);
            findRoute.Distance = testdisntance;
            // Update
            unitOfWork.DisconnectedRoutes.Update(findRoute);
        }
    }
}