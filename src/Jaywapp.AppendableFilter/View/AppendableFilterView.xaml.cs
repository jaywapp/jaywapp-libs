using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Jaywapp.AppendableFilter.View
{
    /// <summary>
    /// AppendableFilter.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AppendableFilterView : UserControl, INotifyPropertyChanged
    {
        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Internal Field
        private string _beforeText = "";
        #endregion

        #region Dependency Properties
        public IEnumerable<object> Sources
        {
            get => (IEnumerable<object>)GetValue(SourcesProperty);
            set => SetValue(SourcesProperty, value);
        }

        public IEnumerable<object> Filtered
        {
            get => (IEnumerable<object>)GetValue(FilteredProperty);
            set => SetValue(FilteredProperty, value);
        }

        public static readonly DependencyProperty SourcesProperty = DependencyProperty.Register(
            nameof(Sources), typeof(IEnumerable<object>), typeof(AppendableFilterView),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDependencyPropertyChanged));

        public static readonly DependencyProperty FilteredProperty = DependencyProperty.Register(
            nameof(Filtered), typeof(IEnumerable<object>), typeof(AppendableFilterView));
        #endregion

        #region Properties
        public Service.AppendableFilter Filter { get; private set; }
        #endregion

        #region Constructor
        public AppendableFilterView()
        {
            InitializeComponent();
        }
        #endregion

        #region Functions
        private static void OnDependencyPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is AppendableFilterView view))
                return;

            if(e.Property == SourcesProperty)
                view.OnSourcesPropertyChanged(e);
        }

        private void OnSourcesPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IEnumerable<object> sources) 
            {
                Filter = new Service.AppendableFilter(sources);
                Filter.FilteringChanged += OnFilteringChanged;
                Filtered = Filter.Filtered.ToList();
            }
        }

        private void OnFilteringChanged(object sender, System.EventArgs e)
        {
            if(sender is Service.AppendableFilter filter)
                Filtered = filter.Filtered.ToList();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender is TextBox textBox) || Filter == null)
                return;

            var newText = textBox.Text;

            if(newText.Length > _beforeText.Length)
                Filter.Append(newText.LastOrDefault());
            else if(newText.Length < _beforeText.Length)
                Filter.Remove();
            else if(newText.Length == 0)
                Filter.Clear();

            _beforeText = textBox.Text;
        }

        private void OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox) || textBox.SelectionStart == textBox.Text.Length)
                return;

            textBox.SelectionStart = textBox.Text.Length;
        }
        #endregion
    }
}
