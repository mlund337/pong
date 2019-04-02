using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NewPongCity
{
    public class GameObjects
    {
        public Paddle Paddle { get; set; }
        public TouchInput TouchInput { get; set; }
        public Ball Ball { get; set; }
    }

    public class TouchInput
    {
        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Tapped { get; set; }
    }
}