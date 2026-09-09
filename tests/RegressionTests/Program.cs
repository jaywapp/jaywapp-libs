using Jaywapp.Algorithm.Enumerables;
var enumerations = 0;
IEnumerable<int> Once() { if (++enumerations > 1) throw new Exception("Source enumerated twice"); yield return 1; yield return 2; yield return 3; }
var actual = Combination.Combinate(Once(), 2);
Check.That(enumerations == 1, "Single source enumeration");
Check.That(string.Join(";", actual.Select(x => string.Join(",", x))) == "1,2;1,3;2,1;2,3;3,1;3,2", "Order and permutations");
Check.That(Combination.Combinate(Array.Empty<int>(), 2).Count == 0, "Empty input");
Check.That(Combination.Combinate(new[] { 1, 2 }, 0).Count == 0, "Zero selection");
Check.That(Combination.Combinate(new[] { 1, 2 }, -1).Count == 0, "Negative selection");
Check.That(Combination.Combinate(new[] { 1, 1, 2 }, 2).Count == 4, "Duplicate semantics");
Check.That(Combination.Combinate(new[] { "a", "A" }, 2, StringComparer.OrdinalIgnoreCase).Count == 0, "Custom comparer");
await Check.ThrowsAsync<ArgumentNullException>(() => Task.Run(() => Combination.Combinate<int>(null!, 1)), "Null input contract");
Console.WriteLine($"PASS {Check.Count} combination regression checks");
