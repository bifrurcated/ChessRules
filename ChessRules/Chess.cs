using System;
using System.Collections.Generic;

namespace ChessRules
{
    public class Chess
    {
        public string fen { get; private set; }
        public bool IsCheck { get; private set; }
        public bool IsCheckmate { get; private set; }
        public bool IsStalemate { get; private set; }

        Board board;
        Moves moves;
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.fen = fen;
            board = new Board(fen);
            moves = new Moves(board);
            SetCheckFlags();
        }

        Chess(Board board)
        {
            this.board = board;
            this.fen = board.fen;
            moves = new Moves(board);
            SetCheckFlags();
        }

        private void SetCheckFlags()
        {
            IsCheck = board.IsCheck();
            IsCheckmate = false;
            IsStalemate = false;
            foreach (string moves in YieldValidMoves())
            {
                return;
            }
            if (IsCheck)
            {
                IsCheckmate = true;
            }
            else 
            {
                IsStalemate = true;
            }
        }

        public bool IsValidMove(string move)
        {
            FigureMoving fm = new FigureMoving(move);
            if (!moves.CanMove(fm) || board.IsCheckAfter(fm))
            {
                return false;
            }
            return true;
        }

        public Chess Move(string move)
        {
            if (!IsValidMove(move))
            {
                return this;
            }
            FigureMoving fm = new FigureMoving(move);
            Board nextBoard = board.Move(fm);
            Chess nextChess = new Chess(nextBoard);
            return nextChess;
        }

        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x, y);
            Figure f = board.GetFigureAt(square);
            return f == Figure.none ? '.': (char)f;
        }

        public char GetFigureAt(string xy)
        {
            Square square = new Square(xy);
            Figure f = board.GetFigureAt(square);
            return f == Figure.none ? '.' : (char)f;
        }

        public IEnumerable<string> YieldValidMoves()
        {
            foreach (FigureOnSquare fs in board.YieldMyFigureOnSquares())
            { 
                foreach (Square to in Square.YieldBoardSquares())
                { 
                    foreach(Figure promotion in fs.figure.YieldPromotions(to))
                    {
                        FigureMoving fm = new FigureMoving(fs, to, promotion);
                        if (moves.CanMove(fm) && !board.IsCheckAfter(fm))
                        {
                            yield return fm.ToString();
                        }
                    }
                }
            }
        }

        public IEnumerable<string> YieldValidMovesFigure(string figure)
        {
            foreach (FigureOnSquare fs in board.YieldMyFigureOnSquares())
            {
                foreach (Square to in Square.YieldBoardSquares())
                {
                    foreach (Figure promotion in fs.figure.YieldPromotions(to))
                    {
                        FigureMoving fm = new FigureMoving(fs, to, promotion);
                        if (moves.CanMove(fm) && !board.IsCheckAfter(fm))
                        {
                            yield return fm.ToString();
                        }
                    }
                }
            }
        }
    }
}
