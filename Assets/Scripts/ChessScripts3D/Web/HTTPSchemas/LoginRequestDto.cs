using System;

namespace ChessScripts3D.Web.HTTPSchemas
{
    [Serializable]
    public class LoginRequestDto
    {
        public string email;
        public string password;
    }
}