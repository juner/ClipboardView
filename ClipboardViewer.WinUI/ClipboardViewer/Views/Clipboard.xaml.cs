using Microsoft.UI.Xaml;
using VM = ClipboardViewer.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClipboardViewer.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Clipboard : Window
    {
        public VM.Clipboard ViewModel { get; } = new VM.Clipboard();
        public Clipboard()
        {
            this.InitializeComponent();
        }
    }
}
