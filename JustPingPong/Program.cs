namespace JustPingPong
{
    internal class Program
    {
        static int firstPlayerPadSize = 6;
        static int secondPlayerPadSize = 6;
        static int ballPositionX = 0;
        static int ballPositionY = 0;
        static bool ballDirectionUp = true;
        static bool ballDirectionRight = true;
        static int firstPlayerPosition = 0;
        static int secondPlayerPosition = 0; 
        static int firstPlayerResult = 0; 
        static int secondPlayerResult = 0;
        static Random randomGenerator = new Random();

        static void RemoveScrollBars()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }

        static void DrawFirstPlayer()
        {
            for (int y = firstPlayerPosition; y < firstPlayerPosition + firstPlayerPadSize; y++)
            {
                PrintAtPosition(0, y, '|');
                PrintAtPosition(1, y, '|');
            }
        }

        static void PrintAtPosition(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        static void DrawSecondPlayer()
        {
            for (int y = secondPlayerPosition; y < secondPlayerPosition + secondPlayerPadSize; y++)
            {
                PrintAtPosition(Console.WindowWidth - 1, y, '|');
                PrintAtPosition(Console.WindowWidth - 2, y, '|');
            }
        }

        static void SetInitialPositions()
        {
            firstPlayerPosition = Console.WindowHeight / 2 - firstPlayerPadSize / 2 ;
            secondPlayerPosition = Console.WindowHeight / 2 - secondPlayerPadSize / 2;
            SetBallAtTheMiddleOfTheGameField();
        }

        static void DrawBall()
        {
            PrintAtPosition(ballPositionX, ballPositionY, '@');
        }

        static void PrintResult()
        {
            Console.SetCursorPosition( Console.WindowWidth / 2 - 1, 0);
            Console.WriteLine("{0}-{1}", firstPlayerResult, secondPlayerResult);
        }

        static void MoveFirstPlayerUp()
        {
            // TODO: move first player up
            if (firstPlayerPosition > 0)
            {
                firstPlayerPosition--;
            }
        }

        static void MoveFirstPlayerDown()
        {
            // TODO: move first player down
            if (firstPlayerPosition < Console.WindowHeight - firstPlayerPadSize)
            {
                firstPlayerPosition++;
            }
        }

        static void MoveSecondPlayerUp()
        {
            if (secondPlayerPosition > 0)
            {
                secondPlayerPosition--;
            }
        }

        static void MoveSecondPlayerDown()
        {
            if (secondPlayerPosition < Console.WindowHeight - secondPlayerPadSize)
            {
                secondPlayerPosition++;
            }
        }

        static void SecondPlayerAIMove()
        {
            int randomNumber = randomGenerator.Next(1, 101);
            //if (randomNumber == 0)
            //{
            //    MoveSecondPlayerUp();
            //}

           // if (randomNumber == 1)
            //{
            //    MoveSecondPlayerDown();
            //}
            if (randomNumber < 70)
            {
                if (ballDirectionUp == true)
                {
                    MoveSecondPlayerUp();
                }
                else
                {
                    MoveSecondPlayerDown();
                }
            }
        }
        static void MoveBall()
        {
            if (ballPositionY == 0)
            {
                ballDirectionUp = false;
            }

            if (ballPositionY == Console.WindowHeight - 1)
            {
                ballDirectionUp = true;
            }

            if (ballPositionX == Console.WindowWidth - 1)
            {
                SetBallAtTheMiddleOfTheGameField();
                ballDirectionRight = false;
                ballDirectionUp = true;
                firstPlayerResult++;
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.WriteLine("First player wins!");
                Console.ReadKey();
            }

            if (ballPositionX == 0)
            {
                SetBallAtTheMiddleOfTheGameField();
                ballDirectionRight = true;
                ballDirectionUp = true;
                secondPlayerResult++;
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.WriteLine("Second player wins!");
                Console.ReadKey();
            }

            if (ballPositionX < 3)
            {
                if (ballPositionY >= firstPlayerPosition 
                    && ballPositionY < firstPlayerPosition + firstPlayerPadSize)
                {
                    ballDirectionRight = true;
                }

            }

            if (ballPositionX >= Console.WindowWidth - 3 - 1)
            {
                if (ballPositionY >= secondPlayerPosition
                    && ballPositionY < secondPlayerPosition + secondPlayerPadSize)
                {
                    ballDirectionRight = false;
                }

            }

            if (ballDirectionUp)
            {
                ballPositionY--;
            }
            else
            {
                ballPositionY++;
            }

            if (ballDirectionRight)
            {
                ballPositionX++;
            }
            else
            {
                ballPositionX--;
            }
        }

        static void SetBallAtTheMiddleOfTheGameField()
        {
            ballPositionX = Console.WindowWidth / 2;
            ballPositionY = Console.WindowHeight / 2;
        }

        static void Main(string[] args)
        {
            RemoveScrollBars();
            SetInitialPositions();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keytInfo = Console.ReadKey();
                    if (keytInfo.Key == ConsoleKey.UpArrow)
                    {
                        MoveFirstPlayerUp();
                    }

                    if (keytInfo.Key == ConsoleKey.DownArrow)
                    {
                        MoveFirstPlayerDown();

                    }
                }

                SecondPlayerAIMove();
                MoveBall();
                Console.Clear();
                DrawFirstPlayer();
                DrawSecondPlayer();
                DrawBall();
                PrintResult();
                Thread.Sleep(60);
            }
        }
    }
}
