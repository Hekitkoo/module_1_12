using System;
using System.Linq;
using ORM.ADO;

namespace ORM.Console
{
    static class Program
    {
        const string ConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SqlModule;Integrated Security=True";
        private static Random randomGenerator = new();
        
        static void Main(string[] args)
        {
            AdoTest();
        }

        private static void AdoTest()
        {
            ConnectedAdoTest();
            DisconnectedAdoTest();
        }

        private static void ConnectedAdoTest()
        {
            TestConnectedWarehouse();
            TestConnectedRoute();
        }

        private static void DisconnectedAdoTest()
        {
            using var unitOfWork = new ADOUnitOfWork(ConnectionString);
            var allRoutes = unitOfWork.DisconnectedRoutes.GetAll().ToList();
            
            const double testdisntance = 666666666;
            const double testdisntance2 = 777777777;
            
            // GetAll
            var firstRoute = allRoutes.First();
            // GetById
            var findRoute = unitOfWork.DisconnectedRoutes.GetById(firstRoute.Id);
            findRoute.Distance = testdisntance;
            findRoute.Id = randomGenerator.Next(100000, 20000000);
            // Create
            unitOfWork.DisconnectedRoutes.Create(findRoute);
            var allRoutes2 = unitOfWork.DisconnectedRoutes.GetAll();
            var createdRoute = allRoutes2.First(x => x.Id == findRoute.Id);
            // Update
            createdRoute.Distance = testdisntance2;
            unitOfWork.DisconnectedRoutes.Update(createdRoute);
            var testCity2Warehouse = unitOfWork.Routes.GetById(firstRoute.Id);
            // Delete
            unitOfWork.DisconnectedRoutes.Delete(createdRoute.Id);
            unitOfWork.RollBack();
        }
        private static void TestConnectedWarehouse()
        {
            const string testCity = "testtestest";
            const string testCity2 = "aa1231111";

            using var unitOfWork = new ADOUnitOfWork(ConnectionString);

            // GetAll
            var allWarehouses = unitOfWork.Warehouses.GetAll();
            var firstWarehouse = allWarehouses.First();
            // GetById
            var findWarehouse = unitOfWork.Warehouses.GetById(firstWarehouse.Id);
            firstWarehouse.City = testCity;
            // Create
            unitOfWork.Warehouses.Create(firstWarehouse);
            var allWarehouses2 = unitOfWork.Warehouses.GetAll();
            var createdWarehouse = allWarehouses2.First(x => x.City == testCity);
            // Update
            createdWarehouse.City = testCity2;
            unitOfWork.Warehouses.Update(createdWarehouse);
            var testCity2Warehouse = unitOfWork.Warehouses.GetById(createdWarehouse.Id);
            // Delete
            unitOfWork.Warehouses.Delete(createdWarehouse.Id);
            unitOfWork.RollBack();
        }

        private static void TestConnectedRoute()
        {
            const double testdisntance = 1000000333334;
            const double testdisntance2 = 6667777112332;

            using var unitOfWork = new ADOUnitOfWork(ConnectionString);

            // GetAll
            var allRoutes = unitOfWork.Routes.GetAll();
            var firstRoute = allRoutes.First();
            // GetById
            var findRoute = unitOfWork.Routes.GetById(firstRoute.Id);
            findRoute.Distance = testdisntance;
            // Create
            unitOfWork.Routes.Create(findRoute);
            var allRoutes2 = unitOfWork.Routes.GetAll();
            var createdRoute = allRoutes2.First(x => x.Distance == testdisntance);
            // Update
            createdRoute.Distance = testdisntance2;
            unitOfWork.Routes.Update(createdRoute);
            var testCity2Warehouse = unitOfWork.Routes.GetById(createdRoute.Id);
            // Delete
            unitOfWork.Routes.Delete(createdRoute.Id);
            unitOfWork.RollBack();
        }
    }
}