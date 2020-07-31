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
            MinDate = new DateTime(2018, 1, 1);
            MaxDate = new DateTime(2018, 12, 31);

        }
    }
}
