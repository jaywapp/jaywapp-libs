using Jaywapp.Graphic.Geometry.Event;
using Jaywapp.Graphic.Geometry.Interface;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Jaywapp.Graphic.Geometry.Model
{
    public class Layer
    {
        #region Internal Field
        private List<IGeometry> _geometries;
        private bool _isVisible;
        private Color _color;
        #endregion

        #region Properties
        public IReadOnlyList<IGeometry> Geometries => _geometries;

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                IsVisibleChanged?.Invoke(this, new IsVisibleChangeEventArgs(value));
            }
        }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                ColorChanged?.Invoke(this, new ColorChangeEventArgs(value));
            }
        }
        #endregion

        #region Event
        public EventHandler<IsVisibleChangeEventArgs> IsVisibleChanged;
        public EventHandler<ColorChangeEventArgs> ColorChanged;
        #endregion

        #region Constructor
        public Layer()
        {
        }
        #endregion

        #region Functions
        public void Draw(SKCanvas canvas)
        {
            foreach (var geometry in Geometries)
            {
                if (!geometry.IsVisible)
                    continue;

                geometry.Draw(canvas);
            }
        }

        public void Add(IGeometry geometry)
        {
            _geometries.Add(geometry);
            Attach(geometry);
        }

        public void Remove(IGeometry geometry)
        {
            _geometries.Remove(geometry);
            Detach(geometry);
        }

        public void Add(IEnumerable<IGeometry> geometries)
        {
            foreach (var geometry in geometries)
                Add(geometry);
        }

        public void Remove(IEnumerable<IGeometry> geometries)
        {
            foreach (var geom in geometries)
                Remove(geom);
        }

        public void Clear()
        {
            foreach(var geometry in _geometries)
                Detach(geometry);

            _geometries.Clear();

        }

        private void Attach(IGeometry geometry)
        {
            IsVisibleChanged += geometry.UpdateVisible;
            ColorChanged += geometry.UpdateColor;
        }

        private void Detach(IGeometry geometry)
        {
            IsVisibleChanged -= geometry.UpdateVisible;
            ColorChanged -= geometry.UpdateColor;
        }
        #endregion
    }
}
