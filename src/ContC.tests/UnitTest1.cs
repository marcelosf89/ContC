using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContC.domain.entities.Models;
using ContC.domain.services;
using Service.Pattern;

namespace ContC.tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Empresa_Test()
        {
           
        }


        [TestMethod]
        public void Usuario_Test()
        {
          
        }

        
        [TestMethod]
        public void UnitOfWork_Dispose_Test()
        {
            //IDataContextAsync context = new DbContext();
            //IUnitOfWorkAsync unitOfWork = new UnitOfWork(context);

            //// opening connection
            //unitOfWork.BeginTransaction();
            //unitOfWork.Commit();

            //// calling dispose 1st time
            //unitOfWork.Dispose();
            //var isDisposed = (bool)GetInstanceField(typeof(UnitOfWork), unitOfWork, "_disposed");
            //Assert.IsTrue(isDisposed);

            //// calling dispose 2nd time, should not throw any excpetions
            //unitOfWork.Dispose();
            //context.Dispose();

            //// calling dispose 3rd time, should not throw any excpetions
            //context.Dispose();
            //unitOfWork.Dispose();
        }

        [TestMethod]
        public void IDataContext_Dispose_Test()
        {
            //IDataContextAsync context = new DbContext();
            //IUnitOfWorkAsync unitOfWork = new UnitOfWork(context);

            //// opening connection
            //unitOfWork.BeginTransaction();
            //unitOfWork.Commit();

            //// calling dispose 1st time
            //context.Dispose();

            //var isDisposed = (bool)GetInstanceField(typeof(DataContext), context, "_disposed");
            //Assert.IsTrue(isDisposed);

            //// calling dispose 2nd time, should not throw any excpetions
            //unitOfWork.Dispose();
            //context.Dispose();

            //// calling dispose 3rd time, should not throw any excpetions
            //unitOfWork.Dispose();
            //context.Dispose();
        }

        private static object GetInstanceField(Type type, object instance, string fieldName)
        {
            const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            var field = type.GetField(fieldName, bindFlags);
            return field != null ? field.GetValue(instance) : null;
        }

        public TestContext TestContext { get; set; }
    }
}
