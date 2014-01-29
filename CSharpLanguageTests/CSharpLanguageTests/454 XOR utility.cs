using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSharpLanguageTests
{
    [TestFixture]
    class _454_XOR_utility
    {
        [TestCase("00111001", "11010110", "01010110", "00011101")]
        [TestCase("10110101", "01100011", "01101010", "10001011")]
        public void do_some_XORing(string disk1literal, string disk2literal, string disk3literal, string disk4literal)
        {
            int disk1 = Convert.ToInt32(disk1literal, 2);
            int disk2 = Convert.ToInt32(disk2literal, 2);
            int disk3 = Convert.ToInt32(disk3literal, 2);
            int disk4 = Convert.ToInt32(disk4literal, 2);

            int result = disk1 ^ disk2 ^ disk3 ^ disk4;

            String printableResult = Convert.ToString(result, 2);

            Assert.That(result, Is.GreaterThanOrEqualTo(0).And.LessThanOrEqualTo(255));
            Assert.That(printableResult, Has.Length.LessThanOrEqualTo(8));
        }
    }
}
