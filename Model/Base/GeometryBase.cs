using Jaywapp.Graphic.Geometry.Event;
using Jaywapp.Graphic.Geometry.Helper;
using Jaywapp.Graphic.Geometry.Interface;
using SkiaSharp;
using System.Windows.Media;

namespace Jaywapp.Graphic.Geometry.Model.Base
{
    public abstract class GeometryBase : IGeometry
    {
        #region Properties
        public SKPaint Paint { get; } = new SKPaint();
        private SKPaint SelectedPaint { get; } = new SKPaint();

        public bool IsVisible { get; set; }
        public bool IsSelected { get; set; }

        public Color Color
        {
            get => Paint.Color.Convert();
            set => Paint.Color = value.Convert();
        }
        #endregion

        #region Constructor
        public GeometryBase(Color color)
        {
            SelectedPaint.Color = SKColors.Red;
            Color = color;
        }
        #endregion

        #region Functions
        public abstract void Draw(SKCanvas canvas);

        protected SKPaint GetPaint() => IsSelected ? SelectedPaint : Paint;

        public void UpdateVisible(object sender, IsVisibleChangeEventArgs args) => IsVisible = args.IsVisible;

        public void UpdateColor(object sender, ColorChangeEventArgs args) => Color = args.Color;
        #endregion
    }
}
