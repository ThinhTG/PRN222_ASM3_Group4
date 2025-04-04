﻿using BusinessObject.Models;

namespace Services.InterfaceService
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllMembersAsync();
        Task<Member> GetMemberByIdAsync(int id);
        Task AddMemberAsync(Member member);
        Task UpdateMemberAsync(Member member);
        Task DeleteMemberAsync(int id);
        Task ChangePasswordAsync(int memberId, string currentPassword, string newPassword);
        Task<Member> GetMemberByEmailAsync(string email);
    }
}
