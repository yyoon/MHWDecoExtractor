namespace MHWDecoExtractorGUITest.Models
{
    using MHWDecoExtractorGUI.Models;
    using MHWSaveUtils;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class MHWWikiDBDecorationListExporterTest
    {
        private MHWWikiDBDecorationListExporter exporter;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            await MasterData.Load("kor");
            this.exporter = new MHWWikiDBDecorationListExporter();
        }

        [Test]
        public void TestGetNormalizedDecorationName()
        {
            var referenceSet = new HashSet<string>(File.ReadAllLines(Path.Join(
                    TestContext.CurrentContext.TestDirectory,
                    "TestData",
                    "MHWWikiDBReferenceList.txt")));

            Assert.That(
                MasterData.Jewels.Select(x => exporter.GetNormalizedDecorationName(x.ItemId)),
                Is.EquivalentTo(referenceSet));
        }
    }
}
