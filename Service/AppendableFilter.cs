using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaywapp.AppendableFilter.Service
{
    public class AppendableFilter
    {
        #region Internal Field
        private IReadOnlyList<object> _sources;
        private Stack<char> _patternStack = new Stack<char>();
        private Stack<List<object>> _resultStack = new Stack<List<object>>();
        #endregion

        #region Event
        public EventHandler FilteringChanged;
        #endregion

        #region Properties
        public List<object> Filtered
        {
            get
            {
                if (_resultStack.Count == 0)
                    return _sources.ToList();

                return _resultStack.Peek();
            }
        }
        #endregion

        #region Constructor
        public AppendableFilter(IEnumerable<object> sources)
        {
            _sources = sources.ToList();
        }
        #endregion

        #region Functions
        public void Append(char word)
        {
            _patternStack.Push(word);

            var filtered = Filtered
                .Where(i =>
                {
                    var target = i.ToString();
                    var pattern = GetPattern();

                    return target.Contains(pattern);
                })
                .ToList();

            _resultStack.Push(filtered);
            FilteringChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Remove()
        {
            _patternStack.Pop();
            _resultStack.Pop();
            FilteringChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Clear()
        {
            _patternStack.Clear();
            _resultStack.Clear();
            FilteringChanged?.Invoke(this, EventArgs.Empty);
        }

        public string GetPattern()
        {
            var builder = new StringBuilder();

            foreach (var word in _patternStack.Reverse())
                builder.Append(word);

            return builder.ToString();
        }
        #endregion
    }
}
