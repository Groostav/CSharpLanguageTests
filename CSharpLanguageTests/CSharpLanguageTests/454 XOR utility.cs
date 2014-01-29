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
        /* Question A
         * A RAID 4 system consists of four data disks and one check disk.  
         * Assume that a file is to be striped across the disk, and that each 
         * block consists of 8 bits (!).  The first 8 blocks of the file are: 
         * 00111001 11010110 01010110 00011101 10110101 01100011 01101010 10001011. 
         * Show the contents of the first two blocks of each of the 5 disks in 
         * this RAID system.*/
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

        /* QUestion E
         * Consider a 7 disk RAID 6 system with 4 data disks and 3 redundant disks 
         * (as described in class). If the blocks of the four data disks contain 
         * 0011 1100, 1100 0111, 0101 0101 and 1000 0100 what are the corresponding 
         * blocks of the redundant disks?
         * 
         * Question F:
         * In the same RAID 6 system described in part (e) what steps must be taken 
         * to change the other disks if the third data disk is rewritten to 1000 0000?
         */
        [TestCase("00111100", "11000111", "01010101", "10000100")]
        [TestCase("00111100", "11000111", "10000000", "10000100")]
        public void do_some_XORing_with_multi_parity(string disk1literal, string disk2literal, string disk3literal, string disk4literal)
        {
            int disk1 = Convert.ToInt32(disk1literal, 2);
            int disk2 = Convert.ToInt32(disk2literal, 2);
            int disk3 = Convert.ToInt32(disk3literal, 2);
            int disk4 = Convert.ToInt32(disk4literal, 2);

            int p123 = disk1 ^ disk2 ^ disk3;
            int p124 = disk1 ^ disk2 ^ disk4;
            int p134 = disk1 ^ disk3 ^ disk4;

            String printableP123 = Convert.ToString(p123, 2);
            String printableP124 = Convert.ToString(p124, 2);
            String printableP134 = Convert.ToString(p134, 2);
        }
    }
}
