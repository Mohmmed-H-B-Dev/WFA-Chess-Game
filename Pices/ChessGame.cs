using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFA_Chess_Game
{
    public abstract class ChessGame
    {
        public struct Position
        {
            public Position(int row,int col)
            {
                this.row=row;
                this.col=col;
            }
            public int row;
            public int col;
        }
        public ChessGame(int row,int col)
        {
            Current_Pos.row=row ;
            Current_Pos.col=col ;
        }

          Position Current_Pos =new Position();

        public CustomCTRL_PictureBox CurrentCTRL;

        //Check is valid to move Vertical and check if there a piece in A way.
        //here we check if the path between start and end positions is clear for sliding pieces (rook, bishop, queen)
        public bool IsPathClearBetween(CustomCTRL_PictureBox[,] grid, Position start, Position end)
        {
            //here we calculate the distance between start and end positions 
            int DeltaR = end.row-start.row;
            int DeltaC = end.col-start.col;

            //here we determine the step direction(-1,0,1)
            int stepR = Math.Sign(DeltaR);
            int stepC = Math.Sign(DeltaC);

            //here we determine if the move is straight or diagonal
            bool isStraight = (stepC==0||stepR==0);
            bool isDiagonal = (Math.Abs(DeltaR)==Math.Abs(DeltaC));


            if (!isDiagonal&&!isStraight)
                return false;

            //here we start checking the path from the next square after start to the square before end
            int CurrentR = start.row+ stepR;
            int CurrentC = start.col+ stepC;

            while (CurrentR!=end.row||CurrentC!=end.col)
            {

                //here we check if there is a piece in the current square
                if (grid[CurrentR, CurrentC].CheckIsPiece())
                {
                    return false;
                }
                //here we move to the next square in the path
                CurrentR += stepR;
                CurrentC += stepC;
            }

            return true;

        }

        //here we check if the move is legal for a rook piece
        public bool IsRookMoveLegal(CustomCTRL_PictureBox[,] grid, Position start, Position end)
        {
            //here we calculate the distance between start and end positions
            int deltaR = Math.Abs(end.row - start.row);
            int deltaC = Math.Abs(end.col - start.col);

             bool isStraightPattern = (deltaR == 0 && deltaC > 0) || (deltaC == 0 && deltaR > 0);
            if (!isStraightPattern) return false;

             return IsPathClearBetween(grid, start, end) && CanCaptureOrMoveToTarget(grid, start, end);
        }
        //here we check if the move is legal for a bishop piece
        public bool IsBishopMoveLegal(CustomCTRL_PictureBox[,] grid, Position start, Position end)
        {
            //here we calculate the distance between start and end positions
            int deltaR = Math.Abs(end.row - start.row);
            int deltaC = Math.Abs(end.col - start.col);
            bool isDiagonalPattern = (deltaR == deltaC && deltaR > 0);
            if (!isDiagonalPattern) return false;
            return IsPathClearBetween(grid, start, end) && CanCaptureOrMoveToTarget(grid, start, end);
        }
        //here we check if the move is legal for a queen piece
        public bool IsQueenMoveLegal(CustomCTRL_PictureBox[,] grid, Position start, Position end)
        {
            //here we calculate the distance between start and end positions for queen move as it can move like rook or bishop
            if (IsRookMoveLegal(grid, start, end) || IsBishopMoveLegal(grid, start, end))
            {
                return true;
            }
            return false;
        }


        //here we check if the target square is empty or has an enemy piece that can be captured
        private bool CanCaptureOrMoveToTarget(CustomCTRL_PictureBox[,] grid, Position start, Position end)
        {
          var startTile =grid[start.row, start.col];
          var targetTile =grid[end.row, end.col];

            if (!targetTile.CheckIsPiece()) return true;

            bool isEnemy =(startTile.PieceColor==Color.White&&targetTile.PieceColor==Color.Brown) ||
                           (startTile.PieceColor == Color.Brown && targetTile.PieceColor == Color.White);

            return isEnemy;
        }


        public bool IsSlidingPieceMoveValid(CustomCTRL_PictureBox[,] grid, Position start, Position end)
        {
            var movingPiece = grid[start.row, start.col];

            if (movingPiece.IsRook())
            {
                return IsRookMoveLegal(grid, start, end);
            }
            if (movingPiece.IsBishop())
            {
                return IsBishopMoveLegal(grid, start, end);
            }
            if (movingPiece.IsQueen())
            {
                return IsQueenMoveLegal(grid, start, end);
            }

            return false;
        }






        public abstract bool _IsValidMove(CustomCTRL_PictureBox New_pos, ref CustomCTRL_PictureBox[,] _PictureBoxGrid);
       
   




        public bool _IsNumberBetweenBoard(int n)
        {
            if ((n<0||n>7))
            {
                return false;
            }

            return true;
        }

        public bool _IsNotTypeColorSame(CustomCTRL_PictureBox New_pos)
        {

            if (!New_pos.IsTypeEmpty()&&!this.CurrentCTRL.IsTypeEmpty())
            {
                if (this.CurrentCTRL.GetCheesPieceType!=New_pos.GetCheesPieceType)
                {
                    return true;
                }
            }
            return false;
        }

        public bool _IsTypeColorSame(CustomCTRL_PictureBox New_pos)
        {

            if (!New_pos.IsTypeEmpty()&&!this.CurrentCTRL.IsTypeEmpty())
            {
                if (this.CurrentCTRL.GetCheesPieceType==New_pos.GetCheesPieceType)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsSquareUnderAttack(CustomCTRL_PictureBox[,] grid, Position targetPos, bool isWhiteKing)
        {
            // 1. الاتجاهات المستقيمة (رخ / ملكة)
            int[,] straightDirections = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
            if (_CheckSlidingAttack(grid, targetPos, straightDirections, isRookOrQueen: true, isWhiteKing))
                return true;

            // 2. الاتجاهات المائلة (فيل / ملكة)
            int[,] diagonalDirections = { { -1, -1 }, { -1, 1 }, { 1, -1 }, { 1, 1 } };
            if (_CheckSlidingAttack(grid, targetPos, diagonalDirections, isRookOrQueen: false, isWhiteKing))
                return true;

            // 3. هجوم الحصان (8 إزاحات)
            int[,] knightMoves = { { -2, -1 }, { -2, 1 }, { -1, -2 }, { -1, 2 }, { 1, -2 }, { 1, 2 }, { 2, -1 }, { 2, 1 } };
            if (_CheckLeaperAttack(grid, targetPos, knightMoves, tile => tile.IsKnight(), isWhiteKing))
                return true;

            // 4. هجوم البيدق (متوافق 100% مع صفوف رقعتك: 0 بالأعلى و 7 بالأسفل)
            int pawnRowDir = isWhiteKing ? -1 : 1;
            int[,] pawnAttacks = { { pawnRowDir, -1 }, { pawnRowDir, 1 } };
            if (_CheckLeaperAttack(grid, targetPos, pawnAttacks, tile => tile.IsPawn(), isWhiteKing))
                return true;

            // 5. هجوم ملك الخصم
            int[,] kingMoves = { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };
            if (_CheckLeaperAttack(grid, targetPos, kingMoves, tile => tile.IsKing(), isWhiteKing))
                return true;

            return false; // المربع آمن
        }

        // دالة فحص القطع ذات المدى الطويل
        private static bool _CheckSlidingAttack(CustomCTRL_PictureBox[,] grid, Position start, int[,] directions, bool isRookOrQueen, bool isWhiteTarget)
        {
            int dirCount = directions.GetLength(0);

            for (int d = 0; d < dirCount; d++)
            {
                int r = start.row + directions[d, 0];
                int c = start.col + directions[d, 1];

                while (r >= 0 && r <= 7 && c >= 0 && c <= 7)
                {
                    var tile = grid[r, c];

                    if (tile.CheckIsPiece())
                    {
                        // المقارنة المباشرة: هل لون القطعة التي وجدناها يختلف عن لون الهدف المراد حمايته؟
                        bool isEnemy = (isWhiteTarget && tile.PieceColor == Color.Brown) ||
                                       (!isWhiteTarget && tile.PieceColor == Color.White);

                        if (isEnemy)
                        {
                            bool isThreat = isRookOrQueen
                                ? (tile.IsRook() || tile.IsQueen())
                                : (tile.IsBishop() || tile.IsQueen());

                            if (isThreat) return true;
                        }

                        // الاصطدام بأي قطعة (سواء صديقة أو خصم لا يهدد) يقطع المسار
                        break;
                    }

                    r += directions[d, 0];
                    c += directions[d, 1];
                }
            }
            return false;
        }

        // دالة فحص القطع التي تقفز لمربعات محددة
        private static bool _CheckLeaperAttack(CustomCTRL_PictureBox[,] grid, Position start, int[,] moves, Predicate<CustomCTRL_PictureBox> isTargetPiece, bool isWhiteTarget)
        {
            int moveCount = moves.GetLength(0);

            for (int i = 0; i < moveCount; i++)
            {
                int r = start.row + moves[i, 0];
                int c = start.col + moves[i, 1];

                if (r >= 0 && r <= 7 && c >= 0 && c <= 7)
                {
                    var tile = grid[r, c];

                    bool isEnemy = (isWhiteTarget && tile.PieceColor == Color.Brown) ||
                                   (!isWhiteTarget && tile.PieceColor == Color.White);

                    if (tile.CheckIsPiece() && isEnemy && isTargetPiece(tile))
                    {
                        return true;
                    }
                }
            }
            return false;
        }






    }
}
