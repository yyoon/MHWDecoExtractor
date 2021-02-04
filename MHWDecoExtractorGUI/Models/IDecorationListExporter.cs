namespace MHWDecoExtractorGUI.Models
{
    using System.Collections.Generic;

    interface IDecorationListExporter
    {
        string DisplayName { get; }
        string ExportAsString(IReadOnlyDictionary<uint, uint> decorations);
    }
}
