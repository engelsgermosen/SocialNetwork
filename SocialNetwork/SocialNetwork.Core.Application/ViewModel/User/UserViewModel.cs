﻿namespace SocialNetwork.Core.Application.ViewModel.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ImagePath { get; set; }

        public bool IsActive { get; set; }
    }
}
