using System;

namespace _3dChess.Schemas
{
    [Serializable]
    public class SignUpRequestDto
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}