using Graphic.Component.Geometry.Interface;
using Infra.Enumerable;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphic.Component.Geometry.Model
{
    /// <summary>
    /// 다각형
    /// </summary>
    public class Polygon : IGraphicFigure
    {
        #region Properties
        /// <summary>
        /// 다각형의 점
        /// </summary>
        public List<PolygonElement> Elements { get; set; }
        /// <summary>
        /// 내부가 채워진 다각형 여부
        /// </summary>
        public bool IsFilled { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="elements"></param>
        public Polygon(
            IEnumerable<PolygonElement> elements, bool isFilled = false)
        {
            Elements = elements.ToList();
            IsFilled = isFilled;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Polygon의 외곽선을 반환합니다.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGraphicTrace> GetTraces()
            => Elements.ChainPairing().Select(p => p.Item1.CreateTrace(p.Item2));
        #endregion
    }
}
