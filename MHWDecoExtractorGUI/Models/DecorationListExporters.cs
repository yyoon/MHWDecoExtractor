namespace MHWDecoExtractorGUI.Models
{
    using System.Collections.Immutable;

    class DecorationListExporters
    {
        private static readonly ImmutableList<IDecorationListExporter> all =
            ImmutableList<IDecorationListExporter>.Empty.AddRange(new IDecorationListExporter[]{
                new SummonedMeDecorationListExporter(),
                new MHWWikiDBDecorationListExporter()
            });

        public static IImmutableList<IDecorationListExporter> All => all;
    }
}
