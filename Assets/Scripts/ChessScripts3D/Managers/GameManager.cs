using ChessScripts3D.Web.HTTPSchemas;
using WarpSquareEngine;

namespace ChessScripts3D.Managers
{
    public class GameManager : SingleTon<GameManager>
    {
        public bool startGame { get; set; } = false;

        public Color myColor;

        public bool myTurn;

        public Game game;

        public UserInfoDto myInfo;
        public UserInfoDto opponentInfo;
    }
}