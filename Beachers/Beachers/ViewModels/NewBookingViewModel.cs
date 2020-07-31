using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Beachers.ViewModels
{
    class NewBookingViewModel : BaseViewModel
    {
        public DateTime MinDate { get; private set; }
        public DateTime MaxDate { get; private set; }
        public DateTime SelectedDate { get; set; }
        public TimeSpan SelectedTime { get; set; }


        public NewBookingViewModel()
        {
            MinDate = DateTime.Today;
            MaxDate = DateTime.Today.AddYears(1);

        }
    }
}
