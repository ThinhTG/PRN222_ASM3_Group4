using BusinessObject.Models;
namespace DataAccess.InterfaceRepo;
public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetMembersAsync();
    Task<Member> GetMemberByIdAsync(int id);
    Task AddMemberAsync(Member member);
    Task UpdateMemberAsync(Member member);
    Task DeleteMemberAsync(int id);
    Task<Member> GetMemberByEmailAsync(string email);
}