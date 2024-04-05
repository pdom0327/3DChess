using System;

namespace ChessScripts3D.Web.HTTPSchemas
{
    [Serializable]
    public class SignUpRequestDto
    {
        public string userName;
        public string password;
        public string email;
    }
}