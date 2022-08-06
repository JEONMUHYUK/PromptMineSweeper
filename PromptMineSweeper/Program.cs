using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptMineSweeper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLoop gameLoop = new GameLoop();
            StartScene startScene = new StartScene();

            startScene.Start();

            gameLoop.Awake();
            gameLoop.Start();

            while (true)
            {
                gameLoop.Update();
                gameLoop.Render();
                // 게임오버가 되면 반복문 종료.
                if (gameLoop.GetGameOverIsCheck())
                    break;

            }

        }
    }
}
