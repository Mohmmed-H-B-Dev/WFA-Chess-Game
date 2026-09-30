using System;
using System.Collections.Generic;
using System.Drawing;

namespace WFA_Chess_Game
{
    public class clsKing : ChessGame
    {
        Position _Current_Pos;
        Position Current_Pos;
        public clsKing(int row, int col)
        : base(row, col)
        {
            _Current_Pos.row = row;
            _Current_Pos.col = col;
            Current_Pos=_Current_Pos;
        }

        

        CustomCTRL_PictureBox _TempPictureBox = null;
        public CustomCTRL_PictureBox T = null;
        public Position GetPosition() { return _Current_Pos; }

        public void SetPosition(int row, int col) { _Current_Pos.row=row; _Current_Pos.col=col; }
        /// <summary>
        /// Move One Step : Lift , Right,Forward, Backwards
        /// 
        /// </summary>
        /// <param name="New_pos"></param>
        /// <returns></returns>
        private bool _MoveOneStep(CustomCTRL_PictureBox New_pos)
        {
            if (_Current_Pos.row==New_pos.IdRow)
            {
                if (_Current_Pos.col==New_pos.IdCol+1||_Current_Pos.col==New_pos.IdCol-1)
                {
                    return true;
                }
                else
                    return false;
            }
            else if (_Current_Pos.col==New_pos.IdCol)
            {
                if (_Current_Pos.row==New_pos.IdRow+1||_Current_Pos.row==New_pos.IdRow-1)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (_Current_Pos.row!=New_pos.IdRow&&_Current_Pos.col!=New_pos.IdCol)
                {
                    if ((_Current_Pos.col+1==New_pos.IdCol&&_Current_Pos.row-1==New_pos.IdRow)||(_Current_Pos.col-1==New_pos.IdCol&&_Current_Pos.row-1==New_pos.IdRow))
                    {
                        return true;
                    }
                    else if ((_Current_Pos.col+1==New_pos.IdCol&&_Current_Pos.row+1==New_pos.IdRow)||(_Current_Pos.col-1==New_pos.IdCol&&_Current_Pos.row+1==New_pos.IdRow))
                    {
                        return true;
                    }else 
                        return false;
                }
            }

            return false;

        }

        /// <summary>
        /// Check is valid for move or not .
        /// and it will get change position pieces if is valid.
        /// </summary>
        /// <param name="New_pos"></param>
        /// <param name="_PictureBoxGrid"></param>
        /// <returns></returns>
        public override bool _IsValidMove(CustomCTRL_PictureBox New_pos, ref CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {

            if (_MoveOneStep(New_pos))
            {
             




                _TempPictureBox=new CustomCTRL_PictureBox();

                _TempPictureBox.SetCheesPieceName=(int)_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].GetCheesPieceName;
                _TempPictureBox.BackgroundImage=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].BackgroundImage;
                _TempPictureBox.SetCheesPieceType=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].GetCheesPieceType;
                _TempPictureBox.PieceColor=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].PieceColor;
                _TempPictureBox._King=new clsKing(this.GetPosition().row, this.GetPosition().col);


                //    /      المشكلة انه لما احرك الملك و اخليه في مكان فيه قطعة من نفس اللون و اضغط على القطعة اللي جنبها عشان اخدها الملك يروح مكانها و يختفي و ما يرجعش تاني
                //   / ايضا لما احرك اي قطعة اللون حق القطعة مايتغير يعني لما اغير القطعة الى مكان ثاني في الرقعة لازم
                //    اغير اللون حق القطعة في المكان الثاني واخلي اللون حق المكان الاو اللي كان للقعة الاةه فاضي يعني اسوي تبديل
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].SetCheesPieceName=(int)_TempPictureBox.GetCheesPieceName;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].PieceColor=_TempPictureBox.PieceColor;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].BackgroundImage =_TempPictureBox.BackgroundImage;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].SetCheesPieceType=_TempPictureBox.GetCheesPieceType;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol]._King=new clsKing(New_pos.IdRow, New_pos.IdCol);
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol]._King.CurrentCTRL=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col]._King.CurrentCTRL;

                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].SetCheesPieceName=(int)CustomCTRL_PictureBox.enCheesPieces.Empty;
                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].SetCheesPieceType=CustomCTRL_PictureBox.enCheesPiecesType.Empty;
                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].BackgroundImage=null;
                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].PieceColor=Color.Empty;


                this.SetPosition(New_pos.IdRow, New_pos.IdCol);
           
                return true;
            }
            return false;

        }

    


 


        // <summary>
        /// Checks if the current player's king is in check.
        /// </summary>
        public static bool IsCurrentKingInCheck(CustomCTRL_PictureBox[,] _PictureBoxGrid, bool isWhiteTurn)
        {

            Position kingPos = _FindKingPosition(_PictureBoxGrid, isWhiteTurn);

            return IsSquareUnderAttack(_PictureBoxGrid, kingPos, isWhiteTurn);
        }
        // <summary>
        /// Finds the position of the king on the board.
        /// </summary>
        private static Position  _FindKingPosition(CustomCTRL_PictureBox[,] _PictureBoxGrid, bool isWhite)
        {
            Color kingColor = isWhite ? Color.White : Color.Brown;
            //here we iterate through the board to find the king's position 
            for (int r = 0; r < 8; r++)
            {
               
                for (int c = 0; c < 8; c++)
                {
                    
                    var tile = _PictureBoxGrid[r, c];
                    if (tile.CheckIsPiece() && tile.IsKing() && tile.PieceColor == kingColor)
                    {
                        return new Position(r, c); 
                    }
                }
            }
            return new Position(0, 0); 
        }
    }
}





