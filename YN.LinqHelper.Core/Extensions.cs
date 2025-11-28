using System;
using System.Collections.Generic;
using System.Linq;

namespace YN.LinqHelper.Core
{
    public static class Extensions
    {
        extension<T>(IEnumerable<T> source)
        {
            public void Enumerate()
            {
                using var enumerator = source.GetEnumerator();
                while (enumerator.MoveNext()) { }
            }

            public IEnumerable<T> Iterate(Action<T> action) =>
                source.Select(x => { action(x); return x; });

            public void ApplyAction(Action<T> action) =>
                source.Iterate(action).Enumerate();

            public IEnumerable<(T Left, T Right)> SelectPairs()
            {
                using var enumerator = source.GetEnumerator();
                enumerator.MoveNext();
                var previous = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    yield return (previous, enumerator.Current);
                    previous = enumerator.Current;
                }
            }

            public bool TryFirst(Func<T, bool> predicate, out T result)
            {
                result = source.FirstOrDefault(predicate);
                return !EqualityComparer<T>.Default.Equals(result, default);
            }
        }
    }
}
