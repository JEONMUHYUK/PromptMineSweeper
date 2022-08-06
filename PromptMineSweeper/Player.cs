using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptMineSweeper
{
    internal class Player
    {
        #region Field
        ConsoleKeyInfo cki;     // 콘솔 키 값
        int     pos_x;          // 플레이어 x값 변수 선언
        int     pos_y;          // 플레이어 y값 변수 선언      
        bool    check;          // 플레이어가 스페이스바를 누를 때 확인하기 위한 변수 선언
        bool    isActive;       // isActive가 true 일때만 Loop을 돌리기 위한 변수 선언 
        #endregion

        #region Get_Set_Function
        public int GetPosX() { return pos_x; }
        public int GetPosY() { return pos_y; }
        public bool GetCheck() { return check; }
        public void SetCheck(bool check) { this.check = check; }
        public bool GetIsActive() { return isActive; }
        public void SetIsActive(bool isMoveCheck) { this.isActive = isMoveCheck; }
        #endregion

        public void Start()
        {
            pos_x   = 1;        // 플레이어 x 값 초기화
            pos_y   = 1;        // 플레이어 y 값 초기화
            check   = false;    // bool 변수 초기화
            isActive = false;
        }

        public void Update()
        {
            // 키 입력 처리
            if (Console.KeyAvailable)
            {
                cki = Console.ReadKey(true);
                isActive = true;
                switch (cki.Key)
                {
                    case ConsoleKey.UpArrow:        // 상
                        if (pos_y - 1 > 0)
                            pos_y--;
                        break;
                    case ConsoleKey.DownArrow:      // 하
                        if (pos_y + 1 < 21)
                            pos_y++;
                        break;
                    case ConsoleKey.LeftArrow:      // 좌
                        if (pos_x - 1 > 0)
                            pos_x--;
                        break;
                    case ConsoleKey.RightArrow:     // 우
                        if (pos_x + 1 < 21)
                            pos_x++;
                        break;
                    case ConsoleKey.Spacebar:       
                        check = true;
                        break;
                }
            }
        }
    }
}
