﻿namespace IdentityService.Application.Features.Auth.Command.Login
{
    public class LoginCommandResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
