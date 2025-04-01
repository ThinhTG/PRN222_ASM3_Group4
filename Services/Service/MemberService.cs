using BCrypt.Net;
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

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _memberRepository.GetMembersAsync();
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            return await _memberRepository.GetMemberByIdAsync(id);
        }

        public async Task<Member> GetMemberByEmailAsync(string email)
        {
            return await _memberRepository.GetMemberByEmailAsync(email);
        }

        public async Task AddMemberAsync(Member member)
        {
            await _memberRepository.AddMemberAsync(member);
        }

        public async Task UpdateMemberAsync(Member member)
        {
            // Get the existing member to preserve the password
            var existingMember = await _memberRepository.GetMemberByIdAsync(member.MemberId);
            if (existingMember == null)
            {
                throw new Exception("Member not found");
            }

            // Preserve the password from the database
            member.Password = existingMember.Password;

            await _memberRepository.UpdateMemberAsync(member);
        }

        public async Task DeleteMemberAsync(int id)
        {
            await _memberRepository.DeleteMemberAsync(id);
        }

        public async Task ChangePasswordAsync(
            int memberId,
            string currentPassword,
            string newPassword
        )
        {
            // Get the member to verify current password
            var member = await _memberRepository.GetMemberByIdAsync(memberId);
            if (member == null)
            {
                throw new Exception("Member not found");
            }

            // Verify the current password using BCrypt
            if (!BCrypt.Net.BCrypt.Verify(currentPassword, member.Password))
            {
                throw new Exception("Current password is incorrect");
            }

            // Hash the new password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

            // Update the password
            await _memberRepository.UpdatePasswordAsync(memberId, hashedPassword);
        }
    }
}
