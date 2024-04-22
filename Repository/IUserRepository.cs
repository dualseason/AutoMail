﻿using AutoMail.Models.Entities;

namespace AutoMail.Repository
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser?> GetAllUsers();
        ApplicationUser? GetUserById(int id);
        void AddUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        void DeleteUser(int id);
    }
}
