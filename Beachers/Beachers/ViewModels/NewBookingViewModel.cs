using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Beachers.ViewModels
{
    class NewBookingViewModel : BaseViewModel
    {
        private TimeSpan sessionEndTime;
        private TimeSpan selectedTime;
        private string sessionLengthMinutes;

        public DateTime MinDate { get; private set; }
        public DateTime MaxDate { get; private set; }
        public DateTime SelectedDate { get; set; }
        public TimeSpan SelectedTime
        {
            get => selectedTime;
            set
            {
                selectedTime = value;

                double newSessionMins = (SessionEndTime - SelectedTime).TotalMinutes;
                newSessionMins = Math.Max(newSessionMins, 0);
                SessionLengthMinutes = newSessionMins.ToString("F0");
            }
        }
        public TimeSpan SessionEndTime
        {
            get => sessionEndTime;
            set
            {
                sessionEndTime = value < SelectedTime ? SelectedTime : value;
                double dblMins = (SessionEndTime - SelectedTime).TotalMinutes;
                sessionLengthMinutes = dblMins.ToString("F0");
                OnPropertyChanged("SessionLengthMinutes");
                OnPropertyChanged();
            }
        }
        public string SessionLengthMinutes
        {
            get => sessionLengthMinutes;
            set
            {
                sessionLengthMinutes = value.Trim(new char[] { '-' });
                double dblMins = string.IsNullOrWhiteSpace(sessionLengthMinutes) ? 0 : double.Parse(sessionLengthMinutes);
                sessionEndTime = SelectedTime.Add(TimeSpan.FromMinutes(dblMins));
                OnPropertyChanged();
                OnPropertyChanged("SessionEndTime");
            }
        }


        public NewBookingViewModel()
        {
            MinDate = DateTime.Today;
            MaxDate = DateTime.Today.AddYears(1);
        }
    }
}
