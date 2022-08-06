using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptMineSweeper
{
    internal class GameLoop
    {
        #region Feild (Member Valiable)
        public const int mapSize = 22;       // 맵 크기 상수 선언


        Player          player;             // 플레이어 변수 선언
        GameBoard       gameBoard;          // 게임보드 변수 선언
        GameOverScene   gameOverScene;      // 게임오버 화면 변수 선언

        int             mineCheckCount;     // 마인 개수 확인 변수

        bool[,]         openCheck;          // 열렸는지 비교하는 함수
        int[,]          CountMineArr;    // 마인개수를 담을 변수

        bool            gameOverIsCheck;    // 게임오버 확인할 bool 변수 선언
        #endregion

        public bool GetGameOverIsCheck() { return gameOverIsCheck; }    // 게임오버를 확인할 Get함수

        public void Awake() // >> 객체 생성 <<
        {
            gameBoard       = new GameBoard();      // 게임보드 객체 생성
            gameOverScene   = new GameOverScene();  // 게임오버화면 객체 생성
            player          = new Player();         // 플레이어 객체 생성

            openCheck       = new bool[22, 22];     // 열렸는지 확인할 bool 배열 생성
            CountMineArr = new int[22, 22];      // 찍은 좌표 기준 마인 개수를 담을 정수형 배열 생성

            gameBoard.Awake();                      // 게임보드의 객체 생성
        }

        public void Start() // >> 초기화 <<
        {
            // 콘솔 기본 세팅
            Console.CursorVisible = false;
            Console.BufferWidth = Console.WindowWidth = 50;     // 콘솔창 가로길이
            Console.BufferHeight = Console.BufferHeight = 50;   // 콘솔창 세로길이

            gameBoard.Start();              // 게임보드 초기화
            player.Start();                 // 플레이어 초기화

            gameOverIsCheck = false;        // 게임오버변수 초기화
            mineCheckCount  = 0;            // 마인개수 확인할 변수 초기화

            #region Assign_CountMineArr_After_MineCheck
            // 플레이어와 같은 좌표의 CountMineArr 좌표의 값이 마인이 아니면 그 주변에 마인이 있는지 검사한다.
            // 검사후 마인이 있으면 mineCheckCount를 증가시켜준다.없으면 0이다
            for (int y = 1; y < mapSize - 1; y++)
            {
                for (int x = 1; x < mapSize - 1; x++)
                {
                    if (gameBoard.tile[y, x] != GameBoard.TileType.Mine)
                    {
                        if (gameBoard.tile[y - 1, x] == GameBoard.TileType.Mine)            // 상단 타일 체크
                            mineCheckCount++;
                        if (gameBoard.tile[y - 1, x - 1] == GameBoard.TileType.Mine)        // 좌상단 타일 체크
                            mineCheckCount++;
                        if (gameBoard.tile[y, x - 1] == GameBoard.TileType.Mine)            // 좌 타일 체크
                            mineCheckCount++;
                        if (gameBoard.tile[y + 1, x - 1] == GameBoard.TileType.Mine)        // 좌 하단 타일 체크
                            mineCheckCount++;
                        if (gameBoard.tile[y + 1, x] == GameBoard.TileType.Mine)            // 하단 타일 체크
                            mineCheckCount++;
                        if (gameBoard.tile[y + 1, x + 1] == GameBoard.TileType.Mine)        // 우 하단 타일 체크
                            mineCheckCount++;
                        if (gameBoard.tile[y, x + 1] == GameBoard.TileType.Mine)            // 우 타일 체크
                            mineCheckCount++;
                        if (gameBoard.tile[y - 1, x + 1] == GameBoard.TileType.Mine)        // 우 상단 타일 체크
                            mineCheckCount++;
                    }

                    // mineCheckCount를 CountMineArr에 값을 할당한 후 mineCheckCount 초기화
                    CountMineArr[y, x] = mineCheckCount;
                    mineCheckCount = 0;

                    // x,y가 지뢰타일이라면 CountMineArr y,x 값에 8 할당
                    if (gameBoard.tile[y, x] == GameBoard.TileType.Mine)
                    {
                        CountMineArr[y, x] = 8;
                    }
                }
            }
            #endregion
        }

        public void Update() // >> 각종 계산 <<
        {

            player.Update();    // 플레이어 업데이트

            // 플레이어가 움직이지 않으면 업데이트 문을 넘긴다.
            if (player.GetIsActive() == false)
            {
                return;
            }
            
            // 게임오버 조건
            if (gameBoard.tile[player.GetPosY(), player.GetPosX()] == GameBoard.TileType.Mine && player.GetCheck())
            {
                gameOverIsCheck = true;
                return;
            }

        }

        public void CheckTile(int y, int x)
        {
            // CountMineArr의 좌표값이 0,1,2고 벽이 아닐경우에만 실행한다.
            // 값이 맞을 경우 openCheck의 좌표값을 true로 바꾸어 준다.
            // FindZero함수를 실행하여 다시 그 좌표값을 기준으로 8곳을 검사한다.
            if ((CountMineArr[y - 1, x] == 0 || CountMineArr[y - 1, x] == 1 || CountMineArr[y - 1, x] == 2) && gameBoard.tile[y - 1, x] != GameBoard.TileType.Wall)           // 상단 타일 체크
            {
                openCheck[y - 1, x] = true;
                FindZero(y - 1, x);
            }

            if ((CountMineArr[y - 1, x - 1] == 0 || CountMineArr[y - 1, x - 1] == 1 || CountMineArr[y - 1, x - 1] == 2) && gameBoard.tile[y - 1, x - 1] != GameBoard.TileType.Wall)       // 좌상단 타일 체크
            {
                openCheck[y - 1, x - 1] = true;
                FindZero(y - 1, x - 1);
            }

            if ((CountMineArr[y - 1, x + 1] == 0 || CountMineArr[y - 1, x + 1] == 1 || CountMineArr[y - 1, x + 1] == 2) && gameBoard.tile[y - 1, x + 1] != GameBoard.TileType.Wall)     // 우 상단 타일 체크
            {
                openCheck[y - 1, x + 1] = true;
                FindZero(y - 1, x + 1);
            }

            if ((CountMineArr[y, x - 1] == 0 || CountMineArr[y, x - 1] == 1 || CountMineArr[y, x - 1] == 2) && gameBoard.tile[y, x - 1] != GameBoard.TileType.Wall)          // 좌 타일 체크
            {
                openCheck[y, x - 1] = true;
                FindZero(y, x - 1);

            }
            if ((CountMineArr[y + 1, x - 1] == 0 || CountMineArr[y + 1, x - 1] == 1 || CountMineArr[y + 1, x - 1] == 2) && gameBoard.tile[y + 1, x + 1] != GameBoard.TileType.Wall)     // 좌 하단 타일 체크
            {
                openCheck[y + 1, x - 1] = true;
                FindZero(y + 1, x - 1);

            }
            if ((CountMineArr[y + 1, x] == 0 || CountMineArr[y + 1, x] == 1 || CountMineArr[y + 1, x] == 2) && gameBoard.tile[y + 1, x] != GameBoard.TileType.Wall)         // 하단 타일 체크
            {
                openCheck[y + 1, x] = true;

                FindZero(y + 1, x);

            }
            if ((CountMineArr[y + 1, x + 1] == 0 || CountMineArr[y + 1, x + 1] == 1 || CountMineArr[y + 1, x + 1] == 2) && gameBoard.tile[y + 1, x + 1] != GameBoard.TileType.Wall)     // 우 하단 타일 체크
            {
                openCheck[y + 1, x + 1] = true;

                FindZero(y + 1, x + 1);

            }
            if ((CountMineArr[y, x + 1] == 0 || CountMineArr[y, x + 1] == 1 || CountMineArr[y, x + 1] == 2) && gameBoard.tile[y, x + 1] != GameBoard.TileType.Wall)         // 우 타일 체크
            {
                openCheck[y, x + 1] = true;

                FindZero(y, x + 1);

            }


        }  // 플레이어가 확인하는 좌표기준 주위 8곳 확인.
        public void FindZero(int y, int x)
        {
            // CheckTile 에서 좌표를 넘겨 받는다.
            // 인덱스 범위 안에서 돌게 if문으로 좌표값을 제한한다.
            // 그 후 만약 CountMineArr의 좌표값이 0보다 크고
            // openCheck의 좌표값이 flase 이며, gameBoard.tile의 좌표의 타일타입이 벽이 아니라면
            // openCheck를 true로 바꾸어주고 넘긴다.

            // 만약 CountMineArr의 좌표값이 0이고
            // openCheck의 좌표값이 flase 이며, gameBoard.tile의 좌표의 타일타입이 벽이 아니라면
            // openCheck를 true로 바꾸어주고 다시 좌표를 재귀함수로 돌린다.
            if (y > 0 && y < mapSize - 1 && x > 0 && x < mapSize - 1)
            {
                // 상단 타일 체크 
                if (CountMineArr[y - 1, x] < 3 && CountMineArr[y - 1, x] > 0 && openCheck[y - 1, x] == false && gameBoard.tile[y - 1, x] != GameBoard.TileType.Wall)           
                    openCheck[y - 1, x] = true;
                else if (CountMineArr[y - 1, x] == 0 && openCheck[y - 1, x] == false && gameBoard.tile[y - 1, x] != GameBoard.TileType.Wall)
                {
                    openCheck[y - 1, x] = true;
                    FindZero(y - 1, x);
                }

                // 좌상단 타일 체크
                if (CountMineArr[y - 1, x - 1] < 3 && CountMineArr[y - 1, x - 1] > 0 && openCheck[y - 1, x - 1] == false && gameBoard.tile[y - 1, x - 1] != GameBoard.TileType.Mine && gameBoard.tile[y - 1, x - 1] != GameBoard.TileType.Wall)  
                    openCheck[y - 1, x - 1] = true;
                else if (CountMineArr[y - 1, x - 1] == 0 && openCheck[y - 1, x - 1] == false && gameBoard.tile[y - 1, x - 1] != GameBoard.TileType.Wall)       
                {
                    openCheck[y - 1, x - 1] = true;
                    FindZero(y - 1, x - 1);
                }

                // 우 상단 타일 체크
                if (CountMineArr[y - 1, x + 1] < 3 && CountMineArr[y - 1, x + 1] > 0 && openCheck[y - 1, x + 1] == false && gameBoard.tile[y - 1, x + 1] != GameBoard.TileType.Mine && gameBoard.tile[y - 1, x + 1] != GameBoard.TileType.Wall)    
                    openCheck[y - 1, x + 1] = true;
                else if (CountMineArr[y - 1, x + 1] == 0 && openCheck[y - 1, x + 1] == false && gameBoard.tile[y - 1, x + 1] != GameBoard.TileType.Wall)    
                {
                    openCheck[y - 1, x + 1] = true;
                    FindZero(y - 1, x + 1);
                }

                // 좌 타일 체크
                if (CountMineArr[y, x - 1] < 3 && CountMineArr[y, x - 1] > 0 && openCheck[y, x - 1] == false && gameBoard.tile[y, x - 1] != GameBoard.TileType.Mine && gameBoard.tile[y, x - 1] != GameBoard.TileType.Wall)
                    openCheck[y, x - 1] = true;
                else if (CountMineArr[y, x - 1] == 0 && openCheck[y, x - 1] == false && gameBoard.tile[y, x - 1] != GameBoard.TileType.Wall)          
                {
                    openCheck[y, x - 1] = true;
                    FindZero(y, x - 1);
                }

                // 좌 하단 타일 체크
                if (CountMineArr[y + 1, x - 1] < 3 && CountMineArr[y + 1, x - 1] > 0 && openCheck[y + 1, x - 1] == false && gameBoard.tile[y + 1, x - 1] != GameBoard.TileType.Mine && gameBoard.tile[y + 1, x - 1] != GameBoard.TileType.Wall)     
                    openCheck[y + 1, x - 1] = true;
                else if (CountMineArr[y + 1, x - 1] == 0 && openCheck[y + 1, x - 1] == false && gameBoard.tile[y + 1, x - 1] != GameBoard.TileType.Wall)
                {
                    openCheck[y + 1, x - 1] = true;
                    FindZero(y + 1, x - 1);
                }

                // 하단 타일 체크
                if (CountMineArr[y + 1, x] < 3 && CountMineArr[y + 1, x] > 0 && openCheck[y + 1, x] == false && gameBoard.tile[y + 1, x] != GameBoard.TileType.Mine && gameBoard.tile[y + 1, x] != GameBoard.TileType.Wall)       
                    openCheck[y + 1, x] = true;
                else if (CountMineArr[y + 1, x] == 0 && openCheck[y + 1, x] == false && gameBoard.tile[y + 1, x] != GameBoard.TileType.Wall)         
                {
                    openCheck[y + 1, x] = true;
                    FindZero(y + 1, x);
                }

                // 우 하단 타일 체크
                if (CountMineArr[y + 1, x + 1] < 3 && CountMineArr[y + 1, x + 1] > 0 && openCheck[y + 1, x + 1] == false && gameBoard.tile[y + 1, x + 1] != GameBoard.TileType.Mine && gameBoard.tile[y + 1, x + 1] != GameBoard.TileType.Wall)     
                    openCheck[y + 1, x + 1] = true;
                else if (CountMineArr[y + 1, x + 1] == 0 && openCheck[y + 1, x + 1] == false && gameBoard.tile[y + 1, x + 1] != GameBoard.TileType.Wall)     
                {
                    openCheck[y + 1, x + 1] = true;
                    FindZero(y + 1, x + 1);
                }

                // 우 타일 체크
                if (CountMineArr[y, x + 1] < 3 && CountMineArr[y, x + 1] > 0 && openCheck[y, x + 1] == false && gameBoard.tile[y, x + 1] != GameBoard.TileType.Mine && gameBoard.tile[y, x + 1] != GameBoard.TileType.Wall)
                    openCheck[y, x + 1] = true;
                else if (CountMineArr[y, x + 1] == 0 && openCheck[y, x + 1] == false && gameBoard.tile[y, x + 1] != GameBoard.TileType.Wall)         
                {
                    openCheck[y, x + 1] = true;
                    FindZero(y, x + 1);
                }
                return;

            }


        }   // 확인한 8곳의 좌표를 기준으로 각각 8 곳 확인.
        public void Render() // >> 출력 <<
        {
            Console.SetCursorPosition(0, 4);                // 출력 위치

            // 출력문
            for (int y = 0; y < mapSize; y++)
            {
                Console.Write("   ");
                for (int x = 0; x < mapSize; x++)
                {
                    // 플레이어 위치 Draw
                    if (x == player.GetPosX() && y == player.GetPosY())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("▣");
                        Console.ResetColor();

                        // 플레이어가 스페이스바를 누르면 true가 되는 것을 받는다.
                        if (player.GetCheck() == true)
                        {
                            openCheck[y, x] = true;     // 플레이어와 같은 좌표에 오픈체크 배열을 트루로 만든다
                            player.SetCheck(false);     // 플레이어 player.SetCheck(false)

                            // 플레이어가 선택한 타일이 0일경우 근접한 타일도 0이면 트루로 바꾸어준다
                            if (CountMineArr[y, x] == 0)
                                CheckTile(y, x);
                        }
                    }

                    // CountMineArr이 true 이면 CountMineArr[y,x] 값으로 바꾸어 준다.
                    else if (openCheck[y, x] == true)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        // CountMineArr == 0 이면 공백으로 출력한다.
                        if (CountMineArr[y, x] == 0)
                            Console.Write("  ");
                        else
                            Console.Write($"{CountMineArr[y, x]} ");
                        Console.ResetColor();

                    }
                    else
                    {
                        Console.ForegroundColor = gameBoard.GetTileColor(gameBoard.tile[y, x]);
                        Console.Write("■");
                    }
                }
                Console.WriteLine();
            }

            // 플레이어가 움직이지 않으면 랜더함수를 실행하지 않는다.
            if (player.GetIsActive() == false)
                return;

            // 게임 오버가 되면 게임오버 화면 출력
            if (gameOverIsCheck)
                gameOverScene.Render();        
        }




        

    }
}
