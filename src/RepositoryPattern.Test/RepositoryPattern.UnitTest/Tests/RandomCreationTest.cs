using RepositoryPattern.UnitTest.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RepositoryPattern.UnitTest
{
    public class RandomCreationTest
    {
        private readonly IRepositoryPattern<PopulationTest, string> _test;
        public RandomCreationTest(IRepositoryPattern<PopulationTest, string> test)
        {
            _test = test;
        }
        [Fact]
        public async Task TestAsync()
        {
            var all = await _test.ToListAsync();
            var theFirst = all.First();
            Assert.NotEqual(0, theFirst.A);
            Assert.NotNull(theFirst.AA);
            Assert.NotEqual((uint)0, theFirst.B);
            Assert.NotNull(theFirst.BB);
            Assert.NotEqual((byte)0, theFirst.C);
            Assert.NotNull(theFirst.CC);
            Assert.NotEqual((sbyte)0, theFirst.D);
            Assert.NotNull(theFirst.DD);
            Assert.NotEqual((short)0, theFirst.E);
            Assert.NotNull(theFirst.EE);
            Assert.NotEqual((ushort)0, theFirst.F);
            Assert.NotNull(theFirst.FF);
            Assert.NotEqual((long)0, theFirst.G);
            Assert.NotNull(theFirst.GG);
            Assert.NotEqual((nint)0, theFirst.H);
            Assert.NotNull(theFirst.HH);
            Assert.NotEqual((nuint)0, theFirst.L);
            Assert.NotNull(theFirst.LL);
            Assert.NotEqual((float)0, theFirst.M);
            Assert.NotNull(theFirst.MM);
            Assert.NotEqual((double)0, theFirst.N);
            Assert.NotNull(theFirst.NN);
            Assert.NotEqual((decimal)0, theFirst.O);
            Assert.NotNull(theFirst.OO);
            Assert.NotNull(theFirst.P);
            Assert.NotNull(theFirst.QQ);
            Assert.NotEqual((char)0, theFirst.R);
            Assert.NotNull(theFirst.RR);
            Assert.NotEqual(new Guid("00000000-0000-0000-0000-000000000000"), theFirst.S);
            Assert.NotNull(theFirst.SS);
            Assert.NotEqual(new DateTime(), theFirst.T);
            Assert.NotNull(theFirst.TT);
            Assert.NotEqual(TimeSpan.FromTicks(0), theFirst.U);
            Assert.NotNull(theFirst.UU);
            Assert.NotEqual(new DateTimeOffset(), theFirst.V);
            Assert.NotNull(theFirst.VV);
            Assert.NotEqual(new Range(), theFirst.Z);
            Assert.NotNull(theFirst.ZZ);
        }
    }
}