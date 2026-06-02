namespace Jaywapp.Graphic.Geometry.Model
{
    public class Circle : Ellipse
    {
        #region Properties
        public float Diameter { get; set; }
        #endregion

        #region Constructor
        public Circle(float x, float y, float diameter) : base(x, y, diameter, diameter)
        {
            Diameter = diameter;
        }
        #endregion
    }
}
