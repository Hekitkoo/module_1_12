using System;
using System.Linq;
using ORM.ADO;
using ORM.Dapper;

namespace ORM.Console
{
    static class Program
    {
        const string ConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SqlModule;Integrated Security=True";
        private static Random randomGenerator = new();
        
        static void Main(string[] args)
        {
            //ConnectedAdoTest();
            //DisconnectedAdoTest();
            DapperTest();
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
            var testCity2Warehouse = unitOfWork.Routes.GetById(findRoute.Id);
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
            var testCity2Warehouse = unitOfWork.Routes.GetById(findRoute.Id);
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
            findRoute.Id = randomGenerator.Next(100000, 20000000);
            // Update
            unitOfWork.DisconnectedRoutes.Update(findRoute);
            var testCity2Warehouse = unitOfWork.Routes.GetById(findRoute.Id);
        }
    }
}