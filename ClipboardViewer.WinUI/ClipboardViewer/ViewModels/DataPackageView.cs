using System.Collections.Generic;
using DataTransfer = Windows.ApplicationModel.DataTransfer;
namespace ClipboardViewer.ViewModels
{
    public class DataPackageView : BindableBase
    {
        DataTransfer.DataPackageView View;
        public DataPackagePropertySetView Properties { get; }
        public IReadOnlyList<string> AvailableFormats { get; }
        public DataPackageView(DataTransfer.DataPackageView View)
        {
            AvailableFormats = View.AvailableFormats;
            this.View = View;
            Properties = new DataPackagePropertySetView(View.Properties);
        }
    }
}
