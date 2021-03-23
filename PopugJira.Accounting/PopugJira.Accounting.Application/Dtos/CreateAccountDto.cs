namespace PopugJira.Accounting.Application.Dtos
{
    public record CreateAccountDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
    }
}