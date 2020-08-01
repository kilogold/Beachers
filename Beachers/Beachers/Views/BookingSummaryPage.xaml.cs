using Beachers.Models;
using Beachers.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Beachers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingSummaryPage : ContentPage, INotifyPropertyChanged
    {
        public BookingSummaryPage(string timestamp)
        {
            InitializeComponent();
            BindingContext = new BookingSummaryViewModel(timestamp);
        }
    }
}