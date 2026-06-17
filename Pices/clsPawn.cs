using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFA_Chess_Game
{
    public class clsPawn:ChessGame
    {
        Position _Current_Pos;
        CustomCTRL_PictureBox _TempPictureBox = null;
        public clsPawn(int row, int col)
          : base(row, col)
        {
            _Current_Pos.row = row;
            _Current_Pos.col = col;

        }
        public bool IsFirstTime = true;
        public bool isVerticalMove {  get; set; }

        public bool isCapturingMoveDiagonal = false;

        
        public override bool IsVerticalPathBlocked(CustomCTRL_PictureBox New_pos, CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {
            bool result = (New_pos.IdRow!=_Current_Pos.row&&New_pos.IdCol ==_Current_Pos.col);

            if (!result)
                return result;

            if (_PictureBoxGrid[_Current_Pos.row, _Current_Pos.col].GetCheesPieceType==CustomCTRL_PictureBox.enCheesPiecesType.White)
            {
                if (_Current_Pos.row-1==New_pos.IdRow&&(New_pos.IsTypeEmpty()))
                {
                    return true;
                }
                else
                if (_Current_Pos.row-2==New_pos.IdRow&&this.CurrentCTRL.PawnIsFirstTimeMoving)
                {
                    return true;
                }
            }
            else if(_PictureBoxGrid[_Current_Pos.row, _Current_Pos.col].GetCheesPieceType==CustomCTRL_PictureBox.enCheesPiecesType.Black)
            {
                if (_Current_Pos.row+1==New_pos.IdRow&&(New_pos.IsTypeEmpty()))
                {
                    return true;
                }
                if (_Current_Pos.row+2==New_pos.IdRow&&this.CurrentCTRL.PawnIsFirstTimeMoving)
                {
                    return true;
                }
            }

              


            return false;

        }

        private bool _CheckIsDiagonalOneStep(Position New_pos, ref CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {
            bool StepFront=New_pos.row==_Current_Pos.row-1;
            bool Stepbhined = New_pos.row==_Current_Pos.row+1;
            int ColToMove = 0;

            if (New_pos.col==_Current_Pos.col+1)
            {
                ColToMove=New_pos.col;
            }
            else if (New_pos.col==_Current_Pos.col-1)
            {
                ColToMove=New_pos.col;
            }

            if ((StepFront||Stepbhined)&&New_pos.col!=_Current_Pos.col)
            {
                if(Stepbhined)
                {
                  //  _PictureBoxGrid[]
                }

            }

            return false;
        }
        public bool IsLimetedCapturing_Move(Position New_pos,  CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {
          
                RowToMove=New_pos.row;



            if (_PictureBoxGrid[_Current_Pos.row, _Current_Pos.col].GetCheesPieceType==CustomCTRL_PictureBox.enCheesPiecesType.White)
            {
                if (New_pos.row==_Current_Pos.row-1&&New_pos.col!=_Current_Pos.col)
                {
                    ColToMove=New_pos.col;
                }

            }
            else if (_PictureBoxGrid[_Current_Pos.row, _Current_Pos.col].GetCheesPieceType==CustomCTRL_PictureBox.enCheesPiecesType.Black)
            {

                if (New_pos.row==_Current_Pos.row+1&&New_pos.col!=_Current_Pos.col)
                {
                    ColToMove=New_pos.col;
                }
            }
            if(RowToMove!=-1&& ColToMove!=-1)
            {
                if (_PictureBoxGrid[RowToMove, ColToMove].IsEmpty()||_PictureBoxGrid[RowToMove, ColToMove].IsTypeEmpty())
                {
                    isCapturingMoveDiagonal=false;
                }
                else
                    isCapturingMoveDiagonal=true;
            }
            return isCapturingMoveDiagonal;
        }
        public int ColToMove = -1;
        public int RowToMove = -1;
        public bool Capturing_Move_Diagonal(CustomCTRL_PictureBox New_pos, ref CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {

            if (isVerticalMove)
            {
               return PushingPawn(New_pos, ref _PictureBoxGrid);
            }
            if (isCapturingMoveDiagonal)
            {




                _TempPictureBox=new CustomCTRL_PictureBox();

                _TempPictureBox.SetCheesPieceName=(int)_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].GetCheesPieceName;
                _TempPictureBox.BackgroundImage=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].BackgroundImage;
                _TempPictureBox.SetCheesPieceType=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].GetCheesPieceType;
                _TempPictureBox._Pawn=new clsPawn(this.GetPosition().row, this.GetPosition().col);
                _TempPictureBox.PawnIsFirstTimeMoving=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].PawnIsFirstTimeMoving;

                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].SetCheesPieceName=(int)_TempPictureBox.GetCheesPieceName;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].BackgroundImage =_TempPictureBox.BackgroundImage;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].SetCheesPieceType=_TempPictureBox.GetCheesPieceType;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol]._Pawn=new clsPawn(New_pos.IdRow, New_pos.IdCol);
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].PawnIsFirstTimeMoving=_TempPictureBox.PawnIsFirstTimeMoving;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol]._Pawn.CurrentCTRL=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col]._Pawn.CurrentCTRL;

                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].SetCheesPieceName=(int)CustomCTRL_PictureBox.enCheesPieces.Empty;
                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].SetCheesPieceType=CustomCTRL_PictureBox.enCheesPiecesType.Empty;
                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].BackgroundImage=null;


                this.SetPosition(New_pos.IdRow, New_pos.IdCol);
                return true;
            }
           
            return false;
        }

        public Position GetPosition() { return _Current_Pos; }
        public void SetPosition(int row, int col) { _Current_Pos.row=row; _Current_Pos.col=col; }
        private bool PushingPawn(CustomCTRL_PictureBox New_pos, ref CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {

            if (this.isVerticalMove)
            {
                //if (_PictureBoxGrid[New_pos.col, New_pos.row].GetCheesPieceType!=_PictureBoxGrid[this.GetPosition().col, this.GetPosition().row].GetCheesPieceType &&
                //     _PictureBoxGrid[New_pos.col, New_pos.row].GetCheesPieceName!=CustomCTRL_PictureBox.enCheesPieces.Empty)
                //{
                //    return ProcessAvailableCaptures(ref _PictureBoxGrid[this.GetPosition().col, this.GetPosition().row], ref _PictureBoxGrid[New_pos.col, New_pos.row]);
                //    //   Execute Function Prosses Available Captures
                //}




                _TempPictureBox=new CustomCTRL_PictureBox();

                _TempPictureBox.SetCheesPieceName=(int)_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].GetCheesPieceName;
                _TempPictureBox.BackgroundImage=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].BackgroundImage;
                _TempPictureBox.SetCheesPieceType=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].GetCheesPieceType;
                _TempPictureBox._Pawn=new clsPawn(this.GetPosition().row, this.GetPosition().col);
                _TempPictureBox.PawnIsFirstTimeMoving=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].PawnIsFirstTimeMoving;

                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].SetCheesPieceName=(int)_TempPictureBox.GetCheesPieceName;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].BackgroundImage =_TempPictureBox.BackgroundImage;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].SetCheesPieceType=_TempPictureBox.GetCheesPieceType;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol]._Pawn=new clsPawn(New_pos.IdRow, New_pos.IdCol);
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].PawnIsFirstTimeMoving=_TempPictureBox.PawnIsFirstTimeMoving;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol]._Pawn.CurrentCTRL=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col]._Pawn.CurrentCTRL;

                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].SetCheesPieceName=(int)CustomCTRL_PictureBox.enCheesPieces.Empty;
                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].SetCheesPieceType=CustomCTRL_PictureBox.enCheesPiecesType.Empty;
                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].BackgroundImage=null;


                this.SetPosition(New_pos.IdRow, New_pos.IdCol);
                this.IsFirstTime=false;
                return true;
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

            if (_Current_Pos.row<0||_Current_Pos.row>7||
               _Current_Pos.col<0||_Current_Pos.col>7)
            {
                return false;
            }

          
            if (this.CurrentCTRL.PawnIsFirstTimeMoving&&!this.isCapturingMoveDiagonal)
            {
                if ((_Current_Pos.row>New_pos.IdRow)&&(_Current_Pos.row-2==New_pos.IdRow)||(_Current_Pos.row>New_pos.IdRow)&&(_Current_Pos.row-1==New_pos.IdRow))
                {
                    return PushingPawn(New_pos, ref _PictureBoxGrid);
                }
                else if ((_Current_Pos.row<New_pos.IdRow)&&(_Current_Pos.row+2==New_pos.IdRow)||(_Current_Pos.row<New_pos.IdRow)&&(_Current_Pos.row+1==New_pos.IdRow))
                {
                    return PushingPawn(New_pos, ref _PictureBoxGrid);
                }


            }
            else
            {
                if ((_Current_Pos.row>New_pos.IdRow)&&(_Current_Pos.row-1==New_pos.IdRow))
                {
                    return Capturing_Move_Diagonal(New_pos, ref _PictureBoxGrid);
                }else if ((_Current_Pos.row<New_pos.IdRow)&&(_Current_Pos.row+1==New_pos.IdRow))
                {
                    return Capturing_Move_Diagonal(New_pos, ref _PictureBoxGrid);
                }
            }

                return false;

        }
    }
}
