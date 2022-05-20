namespace WebHookBin.Database.Model {
    public enum HttpMethod
    {
        Get, Post, Put, Delete, Options, Head, Patch, Connect, Trace, Unknown
    }

    public abstract record BaseEntity
    {
        public int Id { get; init; }
    }

    public record LogEntry(
        HttpMethod Method,
        string Path,
        string RawBody,
        DateTime Timestamp
    ) : BaseEntity
    {
        public virtual ICollection<Header> Headers { get; init; } = new List<Header>();
        public virtual ICollection<QueryParameter> QueryParameters { get; init; } = new List<QueryParameter>();
    }

    public record Header(
        string Key,
        string Value
    ) : BaseEntity;

    public record QueryParameter(
        string Key,
        string? Value
    ) : BaseEntity;
}
