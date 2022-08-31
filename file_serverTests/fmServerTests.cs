using Microsoft.VisualStudio.TestTools.UnitTesting;
using file_server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace file_server.Tests
{
    [TestClass()]
    public class fmServerTests
    {
        [TestMethod()]
        public void isFileExistTest()
        {
            fmServer fmServer = new fmServer();
            string sFolder = @"C:\source\repos\Zealogics_hw\TestFileDownloading";// TO revise it for your testing
            string sFile = "test.txt";
            string sFind = sFolder + "\\" + sFile;
            bool expected = true;
            bool actual;

            actual = fmServer.isFileExist(sFind);

            Assert.AreEqual(expected, actual);
        }
    }
}