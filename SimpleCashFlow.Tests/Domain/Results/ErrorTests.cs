using SimpleCashFlow.Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleCashFlow.Tests.Domain.Results
{
    public  class ErrorTests
    {

        [Fact]
        public void Error_Sould_ReturnErrorCodeOnStringCast()
        {
            var error = new Error("Test.Code", "This is a test");

            string code = error;


            Assert.NotNull(code);
            Assert.NotEmpty(code);
            Assert.Equal("Test.Code", code);

        }

    }
}
