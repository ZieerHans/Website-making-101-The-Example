using System.Collections.Concurrent;

namespace EchoesOfGrace.Data;

public class SubmittedReflection
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Reflection { get; set; } = "";
    public string? Lesson { get; set; }
    public DateTime SubmittedAtUtc { get; set; }
}

public class ReflectionStore
{
    private readonly ConcurrentQueue<SubmittedReflection> _entries = new();
    private int _nextId = 0;

    public SubmittedReflection Add(string name, string reflection, string? lesson)
    {
        var entry = new SubmittedReflection
        {
            Id = Interlocked.Increment(ref _nextId),
            Name = name,
            Reflection = reflection,
            Lesson = lesson,
            SubmittedAtUtc = DateTime.UtcNow
        };
        _entries.Enqueue(entry);
        return entry;
    }

    public int Count => _entries.Count;

    public IReadOnlyList<SubmittedReflection> GetAll() =>
        _entries.OrderByDescending(e => e.SubmittedAtUtc).ToList();
}
