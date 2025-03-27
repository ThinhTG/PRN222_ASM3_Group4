using BusinessObject.Models;
namespace DataAccess.InterfaceRepo;
public interface IMemberRepository
{
    IEnumerable<Member> GetMembers();
    Member GetMemberById(int id);
    void AddMember(Member member);
    void UpdateMember(Member member);
    void DeleteMember(int id);

}