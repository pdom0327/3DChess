using System;

namespace _3dChess.Schemas
{
    [Serializable]
    public class LoginRequestDto
    {
        public string email;
        public string password;
    }
}