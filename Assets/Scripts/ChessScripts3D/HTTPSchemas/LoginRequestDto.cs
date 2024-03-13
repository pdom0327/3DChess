using System;

namespace ChessScripts3D.HTTPSchemas
{
    [Serializable]
    public class LoginRequestDto
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}