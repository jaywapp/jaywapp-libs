using System;
using System.Windows.Media;

namespace Jaywapp.Graphic.Geometry.Event
{
    public class ColorChangeEventArgs : EventArgs
    {
        public Color Color { get; }

        public ColorChangeEventArgs(Color color)
        {   
            Color = color;
        }
    }
}
