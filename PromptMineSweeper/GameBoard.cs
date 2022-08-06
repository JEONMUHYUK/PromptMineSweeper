using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptMineSweeper
{
    internal class GameBoard
    {
        #region Field
        public TileType[,] tile;    // 타일타입 배열 선언
        Random random;              // 랜덤 변수 선언

        // 열거형 타일타입
        public enum TileType
        {
            Mine,
            Empty,
            Wall,
        }

        #endregion

        public void Awake()
        {
            tile    = new TileType[GameLoop.mapSize, GameLoop.mapSize];     // 타일 배열 생성
            random  = new Random();                                         // 랜덤 객체 생성
        }
        public void Start()
        {
            // 초기화 할때 미리 배열에 값을 넣어 둔다.
            // 마인타입, 벽타입, 공백 처리한다.
            for (int y = 0; y < GameLoop.mapSize; y++)
            {
                for (int x = 0; x < GameLoop.mapSize; x++)
                {
                    if ((x == random.Next(2, GameLoop.mapSize-2) || y == random.Next(2, GameLoop.mapSize-2)) && x > 1 && y > 1 && x < 20 && y < 20)
                        tile[y, x] = TileType.Mine;
                    else if (x == 0 || x == GameLoop.mapSize - 1 || y == 0 || y == GameLoop.mapSize - 1)
                    {
                        tile[y, x] = TileType.Wall;
                    }
                    else
                        tile[y, x] = TileType.Empty;
                }
            }

        }

        // 타일 타입 색깔 정의
        public ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Mine:
                    return ConsoleColor.Cyan;
                case TileType.Empty:
                        return ConsoleColor.Cyan;    
                case TileType.Wall:
                    return ConsoleColor.Black;
                default:
                    return ConsoleColor.Cyan;
            }
        }
    }
}
