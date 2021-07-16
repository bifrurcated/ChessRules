using System;
using System.Text;
using ChessRules;

namespace ChessDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Chess chess = new Chess();
            while (true)
            {
                //list = chess.GetAllMoves();
                Console.WriteLine(chess.fen);
                Print(ChessToAscii(chess));
                foreach (string moves in chess.YieldValidMoves())
                {
                    Console.Write(moves + "\t");
                }
                Console.WriteLine();
                Console.Write("> ");

                string move = Console.ReadLine();
                if (move == "") break;
                //if (move == "" && list.Count != 0) move = list[random.Next(list.Count)];
                chess = chess.Move(move);
            }
        }

        static string ChessToAscii(Chess chess)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("  +-----------------+");
            for (int y = 7; y >= 0; y--)
            {
                sb.Append(y + 1);
                sb.Append(" | ");
                for (int x = 0; x < 8; x++)
                {
                    sb.Append(chess.GetFigureAt(x, y) + " ");
                }
                sb.AppendLine("|");
            }
            sb.AppendLine("  +-----------------+");
            sb.AppendLine("    a b c d e f g h  ");
            if (chess.IsCheck)
            {
                sb.AppendLine("IS CHECK");
            }
            if (chess.IsCheckmate)
            {
                sb.AppendLine("IS CHECKMATE");
            }
            if (chess.IsStalemate)
            {
                sb.AppendLine("IS STALEMATE");
            }
            return sb.ToString();
        }

        static void Print(string text)
        {
            ConsoleColor oldForceColor = Console.ForegroundColor;
            foreach (char x in text)
            {
                if (x >= 'a' && x <= 'z')
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (x >= 'A' && x <= 'Z')
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(x);
            }
            Console.ForegroundColor = oldForceColor;
        }
    }
}
