using System;
namespace DreamProperties.Common.Models
{
    public class AppleAccount
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string UserStatus { get; set; }
        public string UserId { get; set; }

    }

    public enum AppleSignInCredentialState
    {
        Authorized,
        Revoked,
        NotFound,
        Unknown
    }
}
