using System;
using BenchmarkDotNet.Attributes;
using Moq;
using NSubstitute;

namespace BenchmarkMockNet
{
    public class ReturnOnly : IMockingBenchmark<int>
    {
        private readonly IThingy stub;
        private readonly Mock<IThingy> mock;
        private readonly IThingy sub;

        public ReturnOnly()
        {
            stub = new ThingStub();

            mock = new Mock<IThingy>();
            mock.Setup(m => m.One()).Returns(1);

            sub = Substitute.For<IThingy>();
            sub.One().Returns(1);
        }

        [Benchmark(Baseline = true)]
        public int Stub() => stub.One();

        [Benchmark]
        public int Moq() => mock.Object.One();

        [Benchmark]
        public int NSubstitute() => sub.One();

        public int FakeItEasy() => throw new NotImplementedException("Never completes, probably a memory leak");
    }
}