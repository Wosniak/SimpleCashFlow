using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SimpleCashFlow.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCashFlow.Tests.Application
{
    public class DependencyInjectionTests
    {

        [Fact]
        public void DependencyInjection_Should_ReturnRegisteredServices()
        {
            
            //Arrange
            var services = new ServiceCollection();

            
            //Act
            var registeredServices = DependecyInjection.AddApplication(services);
            
            //Assert
            Assert.NotNull(registeredServices);

        }


    }
}
