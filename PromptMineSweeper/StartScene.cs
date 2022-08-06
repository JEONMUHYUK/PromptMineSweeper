using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptMineSweeper
{
    internal class StartScene
    {
        public void Start()
        {

            Console.Title = "Prompt_MineSweeper";
            RunMainMenu();


        }

        private void RunMainMenu()
        {
            string prompt = @"




                            __  __     _____     _   _     ______                            
                           |  \/  |   |_   _|   | \ | |   |  ____|                           
                           | \  / |     | |     |  \| |   | |__                              
                           | |\/| |     | |     | . ` |   |  __|                             
                           | |  | |    _| |_    | |\  |   | |____                            
                           |_|__|_|_  |_____| __|_|_\_|_  |______|  _____    ______   _____  
                          / ____| \ \        / / |  ____| |  ____| |  __ \  |  ____| |  __ \ 
                         | (___    \ \  /\  / /  | |__    | |__    | |__) | | |__    | |__) |
                          \___ \    \ \/  \/ /   |  __|   |  __|   |  ___/  |  __|   |  _  / 
                          ____) |    \  /\  /    | |____  | |____  | |      | |____  | | \ \ 
                         |_____/      \/  \/     |______| |______| |_|      |______| |_|  \_\
                                                                     
                                                                     

";            // 프롬프트 값
            string[] options = { "Play", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();
            switch (selectedIndex)
            {
                case 0:
                    RunFirstChoice();
                    break;
                case 1:
                    ExitGame();
                    break;
            }
        }

        private void ExitGame()
        {
            Console.WriteLine("\nPressed any Key to exit....");
            Console.ReadKey(true);
            Environment.Exit(0);
        }

        private void RunFirstChoice()
        {
            Console.Clear();
            return;
        }
    }
}
