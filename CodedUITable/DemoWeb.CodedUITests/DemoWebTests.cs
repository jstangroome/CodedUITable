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
            // by default the Assert and Find methods on the Table will animate the mouse pointer and highlight matching rows
            // this can be disabled for all Tables:
            //  Table.DefaultAnimate = false;
            // or per table instance:
            //  var table = new Table<SomeRow>(htmlTable); table.Animate = false;

            var defaultPage = new Pages.DefaultClasses.Default();
            defaultPage.UIDemoWebWindowsInternWindow.LaunchUrl(new Uri(TestUrl));

            var jimRow = defaultPage.FindPersonRowByName("jim");
            Assert.AreEqual(new DateTime(1978,8,7), jimRow.DateOfBirth, "Jim's birthday is wrong.");

            defaultPage.AssertAllPersonsAreAdults();

            defaultPage.UIDemoWebWindowsInternWindow.Close();
        }
    }
}
