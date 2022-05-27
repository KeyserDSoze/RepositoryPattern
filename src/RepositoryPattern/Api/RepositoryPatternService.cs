namespace RepositoryPattern
{
    internal class RepositoryPatternService
    {
        public Type RepositoryType { get; set; }
        public Type CommandType { get; set; }
        public Type QueryType { get; set; }
        public Type EntityType { get; set; }
    }
}