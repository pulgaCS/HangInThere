using System;

class Program
{
    //Hang In There
    //Computer generates random 3 digit number
    //Goal of the game is to guess the 3 digit number

    //Not Close:        if your guess didn't have any digits that exist in the goal number
    //Close:            if your guess HAD digits in the goal number BUT WERE NOT in the right place value
    //Hang In There:    if your guess HAD digits in the goal number AND atleast 1 was in the right place value

    // Acrescentar sistem de vida:
    //      1 - i = 5(?), caso input == "HangInThere" i++ else i--.
    //      2 - não permitir números repetidos para não have vidas infinitas.
    //          2.1 - ou não permitir "HangInThere" repetidos.
    //      3 - total tentativas.
    //          3.1 - tentativas por classe.
    //      4 - .

    // Ex goal nuber 532
    //output:
    //
    // Not Close  | Close  | Hang In There
    //------------------------------------
    //    678     |  123   |   222         
    //    999     |  299   |   682            
    //            |  399   |     

    static void Main(string[] args)
    {
        while (true)
        {
            Random rng = new Random();

            string hang = rng.Next(100, 999).ToString();

            string input = "";
            int inputCount = 0;

            List<string> NotClose = new List<string>();
            List<string> Close = new List<string>();
            List<string> HangInThere = new List<string>();

            Intro(hang);
            do
            {
                Console.SetCursorPosition(55, 2);

                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    Help(hang, input != "hlp" || input == "clr" ? true : false);

                    inputCount++;
                    if (input == hang)
                    {
                        Intro(hang);
                        Console.SetCursorPosition(0, 5);
                        Console.WriteLine($" |   Congratulation {input} is the correct number  |");
                        Console.WriteLine($" |---------------------------------------------|");
                        Console.ReadKey();
                        break;
                    }
                    else if (int.TryParse(input, out int num) && num >= 100 && num < 999)
                    {
                        ResponseCoordinator(input, hang, NotClose, Close, HangInThere);
                        Intro(hang);
                        Console.SetCursorPosition(0, 5);
                        Results(NotClose, Close, HangInThere);
                    }
                }

            } while (true);
        }
    }

    static void Intro(string hang)
    {
        Console.Clear();
        Console.WriteLine($"/-----------------------------------------------\\                 ");
        Console.WriteLine($" |    Guess the number between 100 and 999     |       ---        ");
        Console.WriteLine($" |---------------+-----------+-----------------|      |   |       ");
        Console.WriteLine($" |   Not Close   |   Close   |  Hang In There  |       ---        ");
        Console.WriteLine("\\----------------+-----------+------------------/                 ");
        Console.SetCursorPosition(55, 2);
    }

    static void Help(string hang, bool onOff)
    {
        if (!onOff)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"/-----------------------------------------------\\      {hang}    ");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($" |---------------+-----------+-----------------|      |   |       ");
        }
        else
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"/-----------------------------------------------\\                ");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($" |---------------+-----------+-----------------|      |   |       ");
        }
    }

    static int HowClose(string input, string hang)
    {
        int hotCold = 0;
        foreach (char i in input)
        {
            if (hang.Contains(i))
            {
                hotCold++;
            }
        }
        return hotCold;
    }

    static void ResponseCoordinator(string input, string hang, List<string> NotClose, List<string> Close, List<string> HangInThere)
    {
        int hotCold = HowClose(input, hang);

        if (hotCold == 0)
        {
            NotClose.Add(input);
        }
        else
        {
            int numberLocationCount = 0;
            for (int i = 0; i < hang.Length; i++)
            {
                if (hang[i] == input[i])
                {
                    numberLocationCount++;
                }
            }

            if (numberLocationCount > 0)
            {
                HangInThere.Add(input);
            }
            else
            {
                Close.Add(input);
            }
        }
    }

    static void Results(List<string> NotClose, List<string> Close, List<string> HangInThere)
    {

        for (int i = 0; i < NotClose.Count; i++)
        {
            Console.SetCursorPosition(0, 5 + i);

            int space = 5 - i.ToString().Length;
            Console.Write(" |       " + NotClose[i]);

            for (int k = 0; k < space; k++)
            {
                Console.Write(" ");
            }

            Console.Write(" |");
        }

        for (int i = 0; i < Close.Count; i++)
        {
            Console.SetCursorPosition(16, 5 + i);

            int space = 4 - i.ToString().Length;
            Console.Write(" |    " + Close[i]);

            for (int k = 0; k < space; k++)
            {
                Console.Write(" ");
            }

            Console.Write(" |");
        }

        for (int i = 0; i < HangInThere.Count; i++)
        {
            Console.SetCursorPosition(29, 5 + i);

            int space = 7 - i.ToString().Length;
            Console.Write("|       " + HangInThere[i]);

            for (int k = 0; k < space; k++)
            {
                Console.Write(" ");
            }

            Console.Write(" |");
        }
    }
}