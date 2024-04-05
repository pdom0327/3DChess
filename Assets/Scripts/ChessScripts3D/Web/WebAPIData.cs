using System;

namespace ChessScripts3D.Web
{
    [Serializable]
    public static class WebAPIData
    {
        private const string _url = "https://3dchess.shop";
        private const string _socketUrl = "ws://3dchess.shop";
        public static string Url => _url;
        public static string SocketUrl => _socketUrl;
        
        private const string AuthKey = "Authorization";
        private const string RefreshKey = "refreshToken";
        
        public const bool matchStart = true;
        public const bool matchEnd = false;

        public static string GetAuthKey()
        {
            return AuthKey;
        }

        public static string GetRefreshKey()
        {
            return RefreshKey;
        }
    }
}