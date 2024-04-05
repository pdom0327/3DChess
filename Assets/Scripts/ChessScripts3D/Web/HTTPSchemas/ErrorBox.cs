using System;

namespace ChessScripts3D.Web.HTTPSchemas
{
    [Serializable]
    public class ErrorBox
    {
        public int code;
        public string errorMessage;
    }
}