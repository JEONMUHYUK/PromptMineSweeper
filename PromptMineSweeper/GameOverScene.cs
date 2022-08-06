using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptMineSweeper
{
    internal class GameOverScene
    {
        public void Render()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
 
         _____   ____    _        _____
        /  __/  /  _ \  / \__/|  /  __/
        | |  _  | / \|  | |\/||  |  \  
        | |_//  | |-||  | |  ||  |  /_ 
        \____\  \_/ \|  \_/  \|  \____\
                               
         ____    _       _____   ____  
        /  _ \  / \ |\  /  __/  /  __\ 
        | / \|  | | //  |  \    |  \/| 
        | \_/|  | \//   |  /_   |    / 
        \____/  \__/    \____\  \_/\_\ 
                               

");
        
        }
    }
}
