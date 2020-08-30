using System;

namespace ClipboardViewer.ViewModels
{
    public class ClipboardHistoryItem : BindableBase
    { 
        public string Id { get; }
        public DateTimeOffset Timestamp { get; }
        public DataPackageView Content { get; }
        public ClipboardHistoryItem(Windows.ApplicationModel.DataTransfer.ClipboardHistoryItem Item)
        {
            (Id, Timestamp) = (Item.Id, Item.Timestamp);
            Content = new DataPackageView(Item.Content);
        }

    }
}
