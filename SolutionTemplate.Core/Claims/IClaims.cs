namespace SolutionTemplate.Core.Claims
{
    public interface IClaims
    {
        int? Id { get; }
        string Username { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
    }
}