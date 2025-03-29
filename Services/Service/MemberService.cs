using BusinessObject.Models;
using DataAccess.InterfaceRepo;
using Services.InterfaceService;

namespace Services.Service
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public IEnumerable<Member> GetAllMembers() => _memberRepository.GetMembers();

        public Member GetMemberById(int id) => _memberRepository.GetMemberById(id);

        public void AddMember(Member member) => _memberRepository.AddMember(member);

        public void UpdateMember(Member member) => _memberRepository.UpdateMember(member);

        public void DeleteMember(int id) => _memberRepository.DeleteMember(id);
    }
}
