using SimpleCashFlow.Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleCashFlow.Tests.Domain.Results
{
    public class ResultTests
    {

        public class TestResult : Result
        {
            internal TestResult(bool isSuccess, Error error) : base(isSuccess, error) {}
        }

        [Fact]
        public void Result_Should_ReturExceptionOnContructorWithSuccessAndError() {

            var exception = Assert.Throws<ArgumentException>(() => new TestResult(true, new Error("Test.Code", "Test.Message")));

            Assert.Equal("Return Cannot be sucessfull with an Error", exception.Message);

        }

        [Fact]
        public void Result_Should_ReturExceptionOnContructorWithouySuccessAndNoError()
        {

            var exception = Assert.Throws<ArgumentException>(() => new TestResult(false, Error.NoError));

            Assert.Equal("All uncessfull return must have an Error", exception.Message);

        }


    }


   
}
