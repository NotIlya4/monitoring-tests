namespace Service.Domain;

public record User(Guid Id, string Login, string PasswordHash, decimal Money)
{
    protected User() : this(default!, default!, default!, default) { }
}