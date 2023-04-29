using System;
using System.Threading;

namespace cengcheckerss
{
    struct MoveTable//struct which helps us keep all the necessary info in computer moves
    {
        public int gain;
        public int loc_x;
        public int loc_y;
        public int new_x;
        public int new_y;
    }
    class Program
    {

        static int cursorX = 2;
        static int cursorY = 2;
        static int round = 1;
        static int remarkX = 0; static int remarkY = 0;
        static string turn = "X";
        static char[,] board = new char[,] {{'O','O','O','.','.','.','.','.'},
                                            { 'O','O','O','.','.','.','.','.'},
                                            { 'O','O','O','.','.','.','.','.'},
                                            { '.','.','.','.','.','.','.','.'},
                                            { '.','.','.','.','.','.','.','.'},
                                            { '.','.','.','.','.','X','X','X'},
                                            { '.','.','.','.','.','X','X','X'},
                                            { '.','.','.','.','.','X','X','X'} };
        static void GameBoard()//to print the game board
        {
            cursorX = 2; cursorY = 2;
            Console.SetCursorPosition(15, 1);
            Console.Write("Round: " + round);//tells which round
            Console.SetCursorPosition(15, 2);
            Console.Write("Turn : " + turn);//tells who's turn
            Console.SetCursorPosition(2, 2);
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j]);
                }
                cursorY++;
                Console.SetCursorPosition(cursorX, cursorY);
            }
            cursorX = 2; cursorY = 2;
        }
        static void Main(string[] args)
        {
            int first_x = 0;
            int first_y = 0;
            int count = 0;
            bool border_down = false;
            bool border_up = false;
            bool border_right = false;
            bool border_left = false;
            MoveTable[] element = new MoveTable[9];
            bool piece_chosen = false;
            int previous_locX = 0;
            int previous_locY = 0;
            ConsoleKeyInfo cki;
            cursorX = 2; cursorY = 2;
            Console.SetCursorPosition(2, 0);
            for (int i = 1; i <= 8; i++)
            {
                Console.Write(i);
            }
            Console.SetCursorPosition(15, 1);
            Console.Write("Round: " + round);//tells which round
            Console.SetCursorPosition(15, 2);
            Console.Write("Turn : " + turn);//tells who's turn
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("+--------+");
            for (int i = 1; i <= 8; i++)
            {
                Console.WriteLine(i + "|        |");
            }
            Console.SetCursorPosition(1, 10);
            Console.WriteLine("+--------+");
            GameBoard();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    cki = Console.ReadKey(true);
                    //cursor movement if piece is not chosen
                    if (cki.Key == ConsoleKey.RightArrow && cursorX < 9 && !piece_chosen)
                    {
                        Console.SetCursorPosition(cursorX, cursorY);
                        cursorX++;

                    }
                    if (cki.Key == ConsoleKey.LeftArrow && cursorX > 2 && !piece_chosen)
                    {
                        Console.SetCursorPosition(cursorX, cursorY);
                        cursorX--;

                    }
                    if (cki.Key == ConsoleKey.UpArrow && cursorY > 2 && !piece_chosen)
                    {
                        Console.SetCursorPosition(cursorX, cursorY);
                        cursorY--;

                    }
                    if (cki.Key == ConsoleKey.DownArrow && cursorY < 9 && !piece_chosen)
                    {
                        Console.SetCursorPosition(cursorX, cursorY);
                        cursorY++;

                    }
                    //cursor movement if piece is chosen
                    if (cki.Key == ConsoleKey.RightArrow && cursorX < 9 && piece_chosen && !border_right)
                    {
                        if (cursorX < 8 && board[cursorY - 2, cursorX - 1] != '.' && board[cursorY - 2, cursorX] == '.')//right jump
                        {
                            border_left = false;//if player can move the piece to right, it means it can move to the left too
                            board[previous_locY, previous_locX] = '.';//replacing the old spot with '.'
                            Console.SetCursorPosition(cursorX, cursorY);
                            cursorX += 2;//we are adding +2 because it is a jump move 
                            board[cursorY - 2, cursorX - 2] = 'X';//make the new spot x
                            remarkX = cursorX;
                            remarkY = cursorY;
                            GameBoard();//print the game board
                            cursorY = remarkY;
                            cursorX = remarkX;
                            Console.SetCursorPosition(remarkX, remarkY);
                            previous_locX = cursorX - 2; previous_locY = cursorY - 2;//updateing the variables
                        }
                        else if (((cursorY - 2 == first_y && cursorX - 2 == first_x) || (cursorY - 2 == first_y && cursorX - 2 == first_x - 1)) && cursorX - 1 >= 0 && board[cursorY - 2, cursorX - 1] == '.')//right step
                        {
                            count++;//to follow if the x is in the same spot as first
                            border_left = false;//if player can move the piece to right, it means it can move to the left too
                            board[previous_locY, previous_locX] = '.';//replacing the old spot with '.'
                            Console.SetCursorPosition(cursorX, cursorY);
                            cursorX++;//we are adding +1 because it is a step move 
                            board[cursorY - 2, cursorX - 2] = 'X';//make the new spot x
                            remarkX = cursorX;
                            remarkY = cursorY;
                            GameBoard();//print the game board
                            cursorY = remarkY;
                            cursorX = remarkX;
                            Console.SetCursorPosition(remarkX, remarkY);
                            previous_locX = cursorX - 2; previous_locY = cursorY - 2;//updating the variables
                            if (cursorY - 2 != first_y && cursorX - 2 != first_x)
                            {
                                border_right = true;
                            }
                        }
                        if (count == 1)//this means the piece is only stepped right 
                        {
                            border_right = true;
                        }
                    }
                    if (cki.Key == ConsoleKey.LeftArrow && cursorX > 2 && piece_chosen && !border_left)
                    {
                        if (cursorX - 4 >= 0 && board[cursorY - 2, cursorX - 3] != '.' && board[cursorY - 2, cursorX - 4] == '.')//left jump
                        {
                            border_right = false;//if player can move the piece to left, it means it can move to the right too
                            board[previous_locY, previous_locX] = '.';//replacing the old spot with '.'
                            Console.SetCursorPosition(cursorX, cursorY);
                            cursorX -= 2;
                            board[cursorY - 2, cursorX - 2] = 'X';//make the new spot x
                            remarkX = cursorX;
                            remarkY = cursorY;
                            GameBoard();//print the game board
                            cursorY = remarkY;
                            cursorX = remarkX;
                            Console.SetCursorPosition(remarkX, remarkY);
                            previous_locX = cursorX - 2; previous_locY = cursorY - 2;//updating the variables
                        }
                        else if (((cursorY - 2 == first_y && cursorX - 2 == first_x) || (cursorY - 2 == first_y && cursorX - 2 == first_x + 1)) && cursorX - 3 >= 0 && board[cursorY - 2, cursorX - 3] == '.')//left step
                        {
                            count--;//to follow if the x is in the same spot as first
                            border_right = false;//if player can move the piece to left, it means it can move to the right too
                            board[previous_locY, previous_locX] = '.';//replacing the old spot with '.'
                            Console.SetCursorPosition(cursorX, cursorY);
                            cursorX--;//we are adding -1 because it is a step move 
                            board[cursorY - 2, cursorX - 2] = 'X';//make the new spot x
                            remarkX = cursorX;
                            remarkY = cursorY;
                            GameBoard();//print the game board
                            cursorY = remarkY;
                            cursorX = remarkX;
                            Console.SetCursorPosition(remarkX, remarkY);
                            previous_locX = cursorX - 2; previous_locY = cursorY - 2;//updating the variables
                            if (cursorY - 2 != first_y && cursorX - 2 != first_x)
                            {
                                border_left = true;
                            }
                        }
                        if (count == -1)//this means the piece is only stepped left 
                        {
                            border_left = true;
                        }
                    }
                    if (cki.Key == ConsoleKey.UpArrow && cursorY > 2 && piece_chosen && !border_up)
                    {
                        if (cursorY - 4 >= 0 && board[cursorY - 3, cursorX - 2] != '.' && board[cursorY - 4, cursorX - 2] == '.')//up jump
                        {
                            border_down = false;//if player can move the piece to up, it means it can move to the down too
                            board[previous_locY, previous_locX] = '.'; //replacing the old spot with '.'
                            Console.SetCursorPosition(cursorX, cursorY);
                            cursorY -= 2;
                            board[cursorY - 2, cursorX - 2] = 'X';//make the new spot x
                            remarkX = cursorX;
                            remarkY = cursorY;
                            GameBoard();//print the game board
                            cursorY = remarkY;
                            cursorX = remarkX;
                            Console.SetCursorPosition(remarkX, remarkY);
                            previous_locX = cursorX - 2; previous_locY = cursorY - 2;//updating the variables
                        }
                        else if (((cursorY - 2 == first_y && cursorX - 2 == first_x) || (cursorY - 2 == first_y + 1 && cursorX - 2 == first_x)) && cursorY - 3 >= 0 && board[cursorY - 3, cursorX - 2] == '.')//up step
                        {
                            count++;//to follow if the x is in the same spot as first
                            border_down = false;//if player can move the piece to up, it means it can move to the down too
                            board[previous_locY, previous_locX] = '.';//replacing the old spot with '.'
                            Console.SetCursorPosition(cursorX, cursorY);
                            cursorY--;//we are adding -1 because it is a step move 
                            board[cursorY - 2, cursorX - 2] = 'X';//make the new spot x
                            remarkX = cursorX;
                            remarkY = cursorY;
                            GameBoard();//print the game board
                            cursorY = remarkY;
                            cursorX = remarkX;
                            Console.SetCursorPosition(remarkX, remarkY);
                            previous_locX = cursorX - 2; previous_locY = cursorY - 2;//updating the variables
                            if (cursorY - 2 != first_y && cursorX - 2 != first_x)//this means the piece is only stepped up 
                            {
                                border_up = true;
                            }
                        }
                        if (count == 1)//this means the piece is only stepped up 
                        {
                            border_up = true;
                        }
                    }
                    if (cki.Key == ConsoleKey.DownArrow && cursorY < 9 && piece_chosen && !border_down)
                    {
                        if (cursorY < 8 && cursorY >= 0 && board[cursorY - 1, cursorX - 2] != '.' && board[cursorY, cursorX - 2] == '.')//down jump
                        {
                            border_up = false;//if player can move the piece to down, it means it can move to the up too
                            board[previous_locY, previous_locX] = '.';//replacing the old spot with '.'
                            Console.SetCursorPosition(cursorX, cursorY);
                            cursorY += 2;
                            board[cursorY - 2, cursorX - 2] = 'X';//make the new spot x
                            remarkX = cursorX;
                            remarkY = cursorY;
                            GameBoard();//print the game board
                            cursorY = remarkY;
                            cursorX = remarkX;
                            Console.SetCursorPosition(remarkX, remarkY);
                            previous_locX = cursorX - 2; previous_locY = cursorY - 2;//updating the variables
                        }
                        else if (((cursorY - 2 == first_y && cursorX - 2 == first_x) || (cursorY - 2 == first_y - 1 && cursorX - 2 == first_x)) && cursorY - 1 >= 0 && board[cursorY - 1, cursorX - 2] == '.') //down step
                        {
                            count--;//to follow if the x is in the same spot as first
                            border_up = false;//if player can move the piece to down, it means it can move to the up too
                            board[previous_locY, previous_locX] = '.';//replacing the old spot with '.'
                            Console.SetCursorPosition(cursorX, cursorY);
                            cursorY++;//we are adding +1 because it is a step move 
                            board[cursorY - 2, cursorX - 2] = 'X';//make the new spot x
                            remarkX = cursorX;
                            remarkY = cursorY;
                            GameBoard();//print the game board
                            cursorY = remarkY;
                            cursorX = remarkX;
                            Console.SetCursorPosition(remarkX, remarkY);
                            previous_locX = cursorX - 2; previous_locY = cursorY - 2;//updating the variables
                            if (cursorY - 2 != first_y && cursorX - 2 != first_x)
                            {
                                border_down = true;
                            }
                        }
                        if (count == -1)//this means the piece is only stepped down 
                        {
                            border_down = true;
                        }
                    }

                    if (cki.Key == ConsoleKey.Z && turn == "X" && !piece_chosen)  //if player presses 'Z' for the first time in that round.
                    {
                        if (board[cursorY - 2, cursorX - 2] != '.' && board[cursorY - 2, cursorX - 2] != 'O')//controls if player choosed 'X' piece or not
                        {
                            first_x = cursorX - 2;
                            first_y = cursorY - 2;
                            previous_locX = cursorX - 2;//holds the old GameBoard[-, x] location
                            previous_locY = cursorY - 2;//holds the old GameBoard[y,-] location
                            piece_chosen = true;
                        }
                    }
                    if (cki.Key == ConsoleKey.X && turn == "X" && piece_chosen && (cursorX - 2 != first_x || cursorY - 2 != first_y))//if player presses 'X' and it is his/her turn and also checks if player changed the pieces spot
                    {
                        border_up = false; border_right = false; border_left = false; border_down = false;//to set the values as default
                        count = 0;//to set the values as default
                        turn = "O";//switches the turn
                        remarkX = cursorX;
                        remarkY = cursorY;//holds the old values
                        GameBoard();//prints the game board
                        cursorY = remarkY;
                        cursorX = remarkX;//returns them
                        Console.SetCursorPosition(remarkX, remarkY);//makes it seem like the cursor is not moved
                        piece_chosen = false;
                    }
                    if (cki.Key == ConsoleKey.C && turn == "O")//if player presses 'C' and its computers turn
                    {
                        int b = 7;//board.GetLength()
                        int count_7th_row = 0;//the number of pieces at the 7th row
                        int count_last_row = 0;//the number of pieces at the last row
                        for (int i = 0; i < 8; i++)//counts the last row
                        {
                            if (board[7, i] == 'O')
                            {
                                count_last_row++;
                            }
                        }
                        if (count_last_row == 3)//if last row parameter equals to 3 changes the boundry and this way pieces are not accumulating
                        {
                            b = 6;//board.GetLength()-1
                        }
                        for (int i = 0; i < 8; i++)//counts the 7th row
                        {
                            if (board[6, i] == 'O')
                            {
                                count_7th_row++;
                            }
                        }
                        if (count_last_row == 3 && count_7th_row == 3)//if there are 6 pieces at the last two row
                        {
                            b = 5;//board.GetLength()-2
                        }

                        int g = 0;
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if (board[j, i] == 'O')//if the place is 'O'
                                {
                                    previous_locX = i;
                                    previous_locY = j;
                                    element[g].loc_x = i;
                                    element[g].loc_y = j;
                                    if ((previous_locY + 4 < b + 1 && board[previous_locY + 1, previous_locX] != '.' && board[previous_locY + 2, previous_locX] == '.' && board[previous_locY + 3, previous_locX] != '.' && board[j + 4, i] == '.'))  //down double jump
                                    {
                                        element[g].gain = 10;
                                        element[g].new_x = previous_locX;
                                        element[g].new_y = previous_locY + 4;
                                    }
                                    else if ((previous_locX + 4 < 8 && board[previous_locY, previous_locX + 1] != '.' && board[previous_locY, previous_locX + 2] == '.' && board[previous_locY, previous_locX + 3] != '.' && board[j, i + 4] == '.'))//right double jump
                                    {
                                        element[g].gain = 9;
                                        element[g].new_x = previous_locX + 4;
                                        element[g].new_y = previous_locY;
                                    }
                                    else if ((previous_locY + 2 < b + 1 && previous_locX + 2 < 8 && board[previous_locY + 1, previous_locX] != '.' && board[previous_locY + 2, previous_locX] == '.' && board[previous_locY + 2, previous_locX + 1] != '.' && board[j + 2, i + 2] == '.') ||
                                             (previous_locX + 2 < b + 1 && previous_locY + 2 < 8 && board[previous_locY, previous_locX + 1] != '.' && board[previous_locY, previous_locX + 2] == '.' && board[previous_locY + 1, previous_locX + 2] != '.' && board[j + 2, i + 2] == '.'))//down and right
                                                                                                                                                                                                                                                                                          //rotated jump
                                    {
                                        element[g].gain = 8;
                                        element[g].new_x = previous_locX + 2;
                                        element[g].new_y = previous_locY + 2;
                                    }
                                    else if ((previous_locY + 2 < b + 1 && board[previous_locY + 1, previous_locX] != '.' && board[j + 2, i] == '.'))//down jump
                                    {
                                        element[g].gain = 7;
                                        element[g].new_x = previous_locX;
                                        element[g].new_y = previous_locY + 2;
                                    }
                                    else if ((previous_locX + 2 < 8 && board[previous_locY, previous_locX + 1] != '.' && board[j, i + 2] == '.'))//right jump
                                    {
                                        element[g].gain = 6;
                                        element[g].new_x = previous_locX + 2;
                                        element[g].new_y = previous_locY;
                                    }
                                    else if ((previous_locY + 1 < b + 1 && board[previous_locY + 1, previous_locX] == '.' && board[j + 1, i] == '.'))//down step
                                    {
                                        element[g].gain = 5;
                                        element[g].new_x = previous_locX;
                                        element[g].new_y = previous_locY + 1;
                                    }
                                    else if ((previous_locX + 1 < 8 && board[previous_locY, previous_locX + 1] == '.' && board[j, i + 1] == '.'))//right step
                                    {
                                        element[g].gain = 4;
                                        element[g].new_x = previous_locX + 1;
                                        element[g].new_y = previous_locY;
                                    }
                                    else if ((previous_locX + 2 < b + 1 && previous_locY - 2 > 0 && board[previous_locY, previous_locX + 1] != '.' && board[previous_locY, previous_locX + 2] == '.' && board[previous_locY - 1, previous_locX + 2] != '.' && board[j - 2, i + 2] == '.') ||
                                             (previous_locY - 2 >= 0 && previous_locX + 2 < 8 && board[previous_locY - 1, previous_locX] != '.' && board[previous_locY - 2, previous_locX] == '.' && board[previous_locY - 2, previous_locX + 1] != '.' && board[j - 2, i + 2] == '.'))  //up and right
                                                                                                                                                                                                                                                                                         //double jump

                                    {
                                        element[g].gain = 3;
                                        element[g].new_x = previous_locX + 2;
                                        element[g].new_y = previous_locY - 2;
                                    }
                                    else if ((previous_locX - 2 >= 0 && previous_locY + 2 < b + 1 && board[previous_locY, previous_locX - 1] != '.' && board[previous_locY, previous_locX - 2] == '.' && board[previous_locY + 1, previous_locX - 2] != '.' && board[j + 2, i - 2] == '.') ||
                                             (previous_locY + 2 < 8 && previous_locX - 2 > 0 && board[previous_locY + 1, previous_locX] != '.' && board[previous_locY + 2, previous_locX] == '.' && board[previous_locY + 2, previous_locX - 1] != '.' && board[j + 2, i - 2] == '.'))
                                    {//down and left double jump
                                        element[g].gain = 2;
                                        element[g].new_x = previous_locX - 2;
                                        element[g].new_y = previous_locY + 2;
                                    }
                                    else if ((previous_locX - 1 >= 0 && board[previous_locY, previous_locX - 1] == '.' && board[j, i - 1] == '.'))//left step
                                    {
                                        element[g].gain = 1;
                                        element[g].new_x = previous_locX - 1;
                                        element[g].new_y = previous_locY;
                                    }
                                    else
                                    {
                                        element[g].gain = 0;
                                    }
                                    g++;
                                }

                            }
                        }
                        int piece = 0;
                        int max = element[0].gain;
                        for (int i = 0; i < 9; i++)//to keep the max value
                        {
                            if (element[i].gain >= max)
                            {
                                max = element[i].gain;
                                piece = i;
                            }
                        }
                        board[element[piece].loc_y, element[piece].loc_x] = '.';//makes the old spot '.'
                        board[element[piece].new_y, element[piece].new_x] = 'O';//makes the new spot 'O'
                        round++;//adds +1 to the round
                        turn = "X";//switches the turn
                        GameBoard();//prints the game board
                        cursorX = remarkX;
                        cursorY = remarkY;
                        Console.SetCursorPosition(cursorX, cursorY);
                        for (int i = 0; i < 9; i++)//setting the values as 0
                        {
                            element[i].gain = 0;
                        }

                    }
                    int count_final_game_X = 0;
                    for (int i = 0; i < 3; i++)//checks if X win
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (board[i, j] == 'X')
                            {
                                count_final_game_X++;
                            }
                        }
                    }
                    int count_final_game_O = 0;
                    for (int i = 5; i < 8; i++)//check if O win
                    {
                        for (int j = 5; j < 8; j++)
                        {
                            if (board[i, j] == 'O')
                            {
                                count_final_game_O++;
                            }
                        }
                    }
                    if (count_final_game_O == 9 || count_final_game_X == 9)
                    {
                        Console.SetCursorPosition(15, 4);
                        if (count_final_game_O == 9)
                        {
                            Console.WriteLine("The winner: O");
                            Console.SetCursorPosition(0, 13);

                            break;
                        }
                        else
                        {
                            Console.WriteLine("The winner: X");
                            Console.SetCursorPosition(0, 13);
                            break;
                        }

                    }
                    if (cki.Key == ConsoleKey.Escape) break;//if player presses ESC, closes the game
                }
                Console.SetCursorPosition(cursorX, cursorY);
                Thread.Sleep(20);//to prevent sorted transaction
            }
        }
    }
}
