using System;

namespace _3dChess.Schemas
{
    [Serializable]
    public class SignUpRequestDto
    {
        public string userName;
        public string password;
        public string email;
    }
}