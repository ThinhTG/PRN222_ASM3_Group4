using BusinessObject.Models;

namespace Services.InterfaceService
{
    public interface IMemberService
    {
        IEnumerable<Member> GetAllMembers();
        Member GetMemberById(int id);
        void AddMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(int id);
    }
}
