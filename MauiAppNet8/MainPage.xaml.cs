using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiAppNet8;

public partial class MainPage : ContentPage
{
    public ObservableCollection<ListViewItem> ItemSource { get; set; }

    private ListViewItem _selectedItem;
    public ListViewItem SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            if (_selectedItem != value)
            {
                _selectedItem = value;
                ItemSelected(_selectedItem);
            }
        }
    }

    public string Header { get; set; }

    public MainPage()
    {
        InitializeComponent();

        ItemSource = new ObservableCollection<ListViewItem>()
        {
            new ListViewItem("Item 1" , Colors.Blue) {Value ="ABC"},
            new ListViewItem("Item 2" , Colors.Red) {Value ="GKL"},
            new ListViewItem("Item 3" , Colors.Green) {Value ="PTR"},
            new ListViewItem("Item 4" , Colors.Purple) {Value ="ENG"},
        };

        Header = "My List:";

        BindingContext = this;
    }

    private async void ItemSelected(ListViewItem selection)
    {
        if (selection == null)
            return;

        await App.Current.MainPage.DisplayAlert("List View", string.Format("{0} was selected", selection.Name), "OK");
        SelectedItem = null;
        return;
    }
}


public class ListViewItem : INotifyPropertyChanged
{
    private string _name;
    private Color _color;
    private object _value;
    private string _image;
    private bool _isEnabled;

    public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); } }
    public Color Color { get => _color; set { _color = value; OnPropertyChanged(nameof(Color)); } }
    public object Value { get => _value; set { _value = value; OnPropertyChanged(nameof(Value)); } }
    public string Image { get => _image; set { _image = value; OnPropertyChanged(nameof(Image)); } }
    public bool IsEnabled { get => _isEnabled; set { _isEnabled = value; OnPropertyChanged(nameof(IsEnabled)); } }
    public ListViewItem(string name, Color color)
    {
        Name = name;
        Color = color;
        IsEnabled = true;
    }

    public ListViewItem(string name, object value, bool isEnabled = true)
    {
        Name = name;
        Value = value;
        IsEnabled = isEnabled;
        if (IsEnabled == false)
        {
            Color = Colors.DarkGray;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
