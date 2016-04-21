using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;

namespace TAiMStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //Mock<ITAiMRepository> mock = new Mock<ITAiMRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product { Name = "Ступица", Price = 10 },
            //    new Product { Name = "Вал", Price=19 },
            //    new Product { Name = "Подшипник", Price=899.4M }
            //});
            //kernel.Bind<ITAiMRepository>().ToConstant(mock.Object);
        }
    }
}