using System;

namespace ChessScripts3D.Web
{
    [Serializable]
    public static class WebAPIData
    {
        private const string _url = "https://3dchess.shop";
        public static string Url => _url;
        
        private const string AuthKey = "Authorization";
        private const string RefreshKey = "refreshToken";

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