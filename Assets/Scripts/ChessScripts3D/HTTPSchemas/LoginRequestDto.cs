using System;

namespace ChessScripts3D.HTTPSchemas
{
    [Serializable]
    public class LoginRequestDto
    {
        public string email;
        public string password;
    }
}