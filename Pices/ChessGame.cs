using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFA_Chess_Game
{
    public abstract class ChessGame
    {
        public struct Position
        {
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

        public virtual bool IsVerticalPathBlocked(CustomCTRL_PictureBox New_pos, CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {

           bool result = (New_pos.IdCol==Current_Pos.col&&New_pos.IdRow!=Current_Pos.row);

            if (!result)
                return result;


            if (Current_Pos.row>New_pos.IdRow)
            {
                for (int i= Current_Pos.row-1; i>=New_pos.IdRow; i--)
                {
                    if (_PictureBoxGrid[i, Current_Pos.col].CheckIsPiece())
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = Current_Pos.row+1; i<=New_pos.IdRow; i++)
                {
                    if (_PictureBoxGrid[i, Current_Pos.col].CheckIsPiece())
                    {
                        return false;
                    }
                }

            }
            

            return result;
        }
        //افقي
        //Check is valid to move Horizontal and check if there a piece in A way.
        public virtual bool IsHorizontalPathBlacked(CustomCTRL_PictureBox New_pos, CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {
            bool result = (New_pos.IdRow==Current_Pos.row&&New_pos.IdCol !=Current_Pos.col);

            if (!result)
                return result;


            if (Current_Pos.col>New_pos.IdCol)
            {
                for (int i = Current_Pos.col-1; i>=New_pos.IdCol; i--)
                {
                    if (_PictureBoxGrid[Current_Pos.row, i].CheckIsPiece())
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = Current_Pos.col+1; i<=New_pos.IdCol; i++)
                {
                    if (_PictureBoxGrid[Current_Pos.row, i].CheckIsPiece())
                    {
                        return false;
                    }
                }
            }

          
            return result;

        }
        
        public abstract bool _IsValidMove(CustomCTRL_PictureBox New_pos, ref CustomCTRL_PictureBox[,] _PictureBoxGrid);
        //Check is valid to move Diagonal and check if there a piece in A way.
        public bool IsPathBlockedDiagonal(CustomCTRL_PictureBox New_pos,  CustomCTRL_PictureBox[,] _PictureBoxGrid,ref CustomCTRL_PictureBox T)
        {

            if((New_pos.IdCol==Current_Pos.col&&New_pos.IdRow!=Current_Pos.row)
            ||
             (New_pos.IdRow==Current_Pos.row&&New_pos.IdCol !=Current_Pos.col))
            {
                return false;
            }


            if (New_pos.PieceColor!=_PictureBoxGrid[Current_Pos.row, Current_Pos.col].PieceColor)
                return false;


            int TempRow = Current_Pos.row;
            int TempCol = Current_Pos.col;
       
                if (Current_Pos.col<New_pos.IdCol)
              {
                for (int i = Current_Pos.col+1; i<=New_pos.IdCol; i++)
                {
                    if(Current_Pos.row<New_pos.IdRow)
                    TempRow+=1;
                    else
                        TempRow-=1;
                    if (!_PictureBoxGrid[TempRow, i].IsEmpty())
                    {
                       T= _PictureBoxGrid[TempRow, i];
                        return false;
                    }

                }
            }
            else
            {
                for (int i = Current_Pos.col-1; i>=New_pos.IdCol; i--)
                {
                    if (Current_Pos.row<New_pos.IdRow)
                        TempRow+=1;
                    else
                        TempRow-=1;
                    if (!_PictureBoxGrid[TempRow, i].IsEmpty())
                    {

                        T = _PictureBoxGrid[TempRow, i]; return false;
                        

                    }
                }

            }


            return true ;
        }
        //افقي


     
        public bool IsPathBlhockedDiagonal(CustomCTRL_PictureBox New_pos, CustomCTRL_PictureBox[,] _PictureBoxGrid, ref CustomCTRL_PictureBox T)
        {

            //if ((New_pos.IdCol==Current_Pos.col&&New_pos.IdRow!=Current_Pos.row)
            //||
            // (New_pos.IdRow==Current_Pos.row&&New_pos.IdCol !=Current_Pos.col))
            //{
            //    return false;
            //}

            //if (New_pos.PieceColor!=_PictureBoxGrid[Current_Pos.row, Current_Pos.col].PieceColor)
            //    return false;

            int rcol = New_pos.IdCol-Current_Pos.col;
            int rrow = New_pos.IdRow-Current_Pos.row;
            if(Math.Abs(rrow)==0&&Math.Abs(rcol)==0||(Math.Abs(rrow)!=Math.Abs(rcol))) 
                { return false; }
            
            

            int TempRow = Current_Pos.row;
            int TempCol = Current_Pos.col;

            if (Current_Pos.col<New_pos.IdCol)
            {
                for (int i = Current_Pos.col+1; i<=New_pos.IdCol; i++)
                {
                    if (Current_Pos.row<New_pos.IdRow)
                        TempRow+=1;
                    else
                        TempRow-=1;
                    if (!_PictureBoxGrid[TempRow, i].IsEmpty())
                    {
                        T= _PictureBoxGrid[TempRow, i];
                        return false;
                    }

                }
            }
            else
            {
                for (int i = Current_Pos.col-1; i>=New_pos.IdCol; i--)
                {
                    if (Current_Pos.row<New_pos.IdRow)
                        TempRow+=1;
                    else
                        TempRow-=1;
                    if (!_PictureBoxGrid[TempRow, i].IsEmpty())
                    {

                        T = _PictureBoxGrid[TempRow, i]; return false;


                    }
                }

            }


            return true;
        }





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



    }
}
