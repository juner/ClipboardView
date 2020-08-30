using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using ClipboardApi = Windows.ApplicationModel.DataTransfer.Clipboard;

namespace ClipboardViewer.ViewModels
{
    [DebuggerDisplay(nameof(IsHistoryEnabled) + ":{isHistoryEnabled}," + nameof(IsRoamingEnabled) + ":{isRoamingEnabled}")]
    public class Clipboard : BindableBase, IDisposable
    {
        private bool disposedValue;

        public Clipboard()
        {
            ClipboardApi.RoamingEnabledChanged += OnRoamingEnabled;
            ClipboardApi.HistoryChanged += OnHistoryChanged;
            ClipboardApi.HistoryEnabledChanged += OnHistoryEnabled;
            ClipboardApi.ContentChanged += OnContentChanged;
            _ = GetHistoryItemAsync();
        }
        private bool isAutoReload;
        public bool IsAutoReload { get => isAutoReload; set => SetProperty(ref isAutoReload, value); }
        private bool isCompleted = false;
        public bool IsCompleted { get => isCompleted; private set => SetProperty(ref isCompleted, value); } 
        private bool isHistoryEnabled;
        public bool IsHistoryEnabled => isHistoryEnabled = ClipboardApi.IsHistoryEnabled();
        private bool isRoamingEnabled;
        public bool IsRoamingEnabled => isRoamingEnabled = ClipboardApi.IsRoamingEnabled();
        private void OnContentChanged(object obj, object args)
        {
            if (!IsAutoReload)
                return;
        }
        private void OnHistoryChanged(object obj, ClipboardHistoryChangedEventArgs args)
        {
            if (!IsAutoReload)
                return;

        }
        private void OnRoamingEnabled(object obj, object args) => OnPropertyChanged(nameof(IsRoamingEnabled));
        private void OnHistoryEnabled(object obj, object args) => OnPropertyChanged(nameof(IsHistoryEnabled));
        public async Task GetHistoryItemAsync()
        {
            IsCompleted = false;
            try
            {
                foreach (var Item in HistoryItems.ToList())
                    HistoryItems.Remove(Item);

                var Result = await ClipboardApi.GetHistoryItemsAsync();
                
                foreach (var Item in Result.Items)
                {
                    HistoryItems.Add(new ClipboardHistoryItem(Item));
                }
            }
            finally
            {
                IsCompleted = true;
            }
        }
        public ObservableCollection<ClipboardHistoryItem> HistoryItems = new ObservableCollection<ClipboardHistoryItem>();

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ClipboardApi.RoamingEnabledChanged -= OnRoamingEnabled;
                    ClipboardApi.HistoryChanged -= OnHistoryChanged;
                    ClipboardApi.HistoryEnabledChanged -= OnHistoryEnabled;
                    ClipboardApi.ContentChanged -= OnContentChanged;
                }
                disposedValue = true;
            }
        }

        ~Clipboard()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
