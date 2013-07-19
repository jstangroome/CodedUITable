using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoWeb.CodedUITests
{
    [CodedUITest]
    public class DemoWebTests
    {
        const string TestUrl = "http://localhost:16330/default.aspx";

        [TestMethod]
        public void SampleTestMethod()
        {

            var defaultPage = new Pages.DefaultClasses.Default();
            defaultPage.UIDemoWebWindowsInternWindow.LaunchUrl(new Uri(TestUrl));

            var jimRow = defaultPage.FindPersonRowByName("jim");
            Assert.AreEqual(new DateTime(1978,8,7), jimRow.DateOfBirth, "Jim's birthday is wrong.");

            defaultPage.AssertAllPersonsAreAdults();

            defaultPage.UIDemoWebWindowsInternWindow.Close();
        }
    }
}
