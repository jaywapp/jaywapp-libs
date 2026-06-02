using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Jaywapp.Algorithm.Points.Convex
{
    /// <summary>
    /// Convex Hull 생성을 위한 정적함수
    /// </summary>
    /// <see cref="https://en.wikipedia.org/wiki/Convex_hull"/>
    public static class ConvexHull
    {
        /// <summary>
        /// 좌표 목록에 대해 ConvexHull 정보를 생성합니다.
        /// </summary>
        /// <param name="pts"></param>
        /// <returns></returns>
        public static List<Point> CreateConvexHull(this IEnumerable<Point> pts) => CreateConvexHull(pts, pt => pt);

        /// <summary>
        /// 좌표를 갖는 대상에 대해 ConvexHull 정보를 생성합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static List<T> CreateConvexHull<T>(this IEnumerable<T> items, Func<T, Point> selector)
        {
            if (items.Count() <= 2)
                return items.ToList();

            var sorted = Sort(items, selector);
            var stack = new Stack<T>(sorted.Count);

            stack.Push(sorted[0]);
            stack.Push(sorted[1]);

            int idx = 2;
            while (idx < sorted.Count)
            {
                var item1 = stack.Pop();
                var item2 = stack.Pop();
                var nextItem = sorted[idx];

                // * 정상 단계
                // pt1, pt2와 이루는 각도가 좌측 방향인 경우 
                // pt1, pt2, next를 모두 stack에 넣고 다음 단계 진행
                if (IsCCW(item2, item1, nextItem, selector))
                {
                    stack.Push(item2);
                    stack.Push(item1);
                    stack.Push(nextItem);
                    idx++;
                }
                // * 비정상 단계
                // 좌측 방향이 아니며, Stack이 비어있지 않을 경우
                // pt1로부터 다음 pt2에 대한 단계 진행
                else if (stack.Any())
                {
                    stack.Push(item2);
                }
                // * 특별 케이스
                // sorted[0]와 sorted[1] 사이를 잇는 선 위에 next가 존재하는 경우
                // ex) sorted[0] : (0, 0), sorted[1] : (3, 0), next : (1, 0)
                // (Sort 함수 특성상 탐색 첫 단계에서만 해당 경우가 발생할 수 있기 때문에 Stack이 비어있음)
                else
                {
                    stack.Push(item2);
                    stack.Push(item1);
                    idx++;
                }
            }

            return stack.Reverse().ToList();
        }

        /// <summary>
        /// ConvexHull을 생성하기 위해 좌하단 item을 기준으로 시계반대방향으로 목록을 정렬합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static List<T> Sort<T>(IEnumerable<T> items, Func<T, Point> selector)
        {
            // 좌하단 기준점 수집
            var first = items
                .MinItems(item => selector(item).Y)
                .MinItem(item => selector(item).X);

            return items
                .OrderBy(i => GetDegree(selector(i) - selector(first)))
                .ThenBy(i => GetDistance(selector(i), selector(first)))
                .ToList();
        }

        /// <summary>
        /// item1, item2, item3의 위치에 따라 세 대상이 이루는 각도가 CCW인지 확인합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <param name="item3"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static bool IsCCW<T>(T item1, T item2, T item3, Func<T, Point> selector)
            => IsCCW(selector(item1), selector(item2), selector(item3));

        /// <summary>
        /// pt1, pt2, pt3의 위치에 따라 세 대상이 이루는 각도가 CCW인지 확인합니다.
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="pt3"></param>
        /// <returns></returns>
        private static bool IsCCW(Point pt1, Point pt2, Point pt3)
        {
            return (pt1.X * pt2.Y + pt2.X * pt3.Y + pt3.X * pt1.Y)
                - (pt2.X * pt1.Y + pt3.X * pt2.Y + pt1.X * pt3.Y) > 0;
        }

        /// <summary>
        /// 가장 작은 값을 가진 Item을 반환합니다.
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="items"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static TItem MinItem<TItem>(this IEnumerable<TItem> items, Func<TItem, IComparable> selector)
            => MinItems(items, selector).FirstOrDefault();

        /// <summary>
        /// 가장 작은 값을 가진 Item 목록을 반환합니다.
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="items"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static IEnumerable<TItem> MinItems<TItem>(this IEnumerable<TItem> items, Func<TItem, IComparable> selector)
            => items.Where(i => selector(i) == items.Min(o => selector(o)));

        /// <summary>
        /// 두 좌표간 거리를 계산합니다.
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        private static double GetDistance(this Point pt1, Point pt2)
        {
            var xDistance = Math.Abs(pt1.X - pt2.X);
            var yDistance = Math.Abs(pt1.Y - pt2.Y);

            return Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));
        }

        /// <summary>
        /// Vector가 이루는 각도를 계산합니다.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        private static double GetDegree(Vector vector)
        {
            var tan = vector.Y / vector.X;
            return Math.Atan(tan);
        }
    }
}
