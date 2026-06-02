namespace Jaywapp.Graphic.Geometry.Model
{
    public class Square : Rectangle
    {
        #region Properties
        public float Length { get; set; }
        #endregion

        #region Constructor
        public Square(float x, float y, float length) : base(x, y, length, length)
        {
            Length = length;
        }
        #endregion
    }
}
