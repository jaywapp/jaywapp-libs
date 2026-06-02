using System;

namespace Jaywapp.Graphic.Geometry.Event
{
    public  class IsVisibleChangeEventArgs : EventArgs
    {
        public bool IsVisible { get; set; }

        public IsVisibleChangeEventArgs(bool isVisible)
        {
            IsVisible = isVisible;
        }
    }
}
