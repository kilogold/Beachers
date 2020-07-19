using System;
using System.Collections.Generic;
using System.Text;

namespace Beachers.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Booking,
        Login
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
