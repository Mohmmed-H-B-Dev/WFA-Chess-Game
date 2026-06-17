using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFA_Chess_Game
{
    public class clsBishop: ChessGame
    {
        Position _Current_Pos;
        public clsBishop(int row, int col)
            : base(row, col)
        {
            _Current_Pos.row = row;
            _Current_Pos.col = col;

        }

        public CustomCTRL_PictureBox T = null;
        CustomCTRL_PictureBox _TempPictureBox = null;
        public bool isDiagonalMove { set; get; }
        public Position GetPosition() { return _Current_Pos; }
        public void SetPosition(int row, int col) { _Current_Pos.row=row; _Current_Pos.col=col; }

        public bool IsAvailableCaptures(CustomCTRL_PictureBox New_pos)
        {
            bool result = (T.IdRow==New_pos.IdRow)&&(T.IdCol==New_pos.IdCol)
                &&(!T.IsTypeEmpty()&&!New_pos.IsTypeEmpty())
                &&(T.GetCheesPieceType==New_pos.GetCheesPieceType);
            return result;
        }
        private bool ProcessAvailableCaptures(CustomCTRL_PictureBox New_pos, ref CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {

            _TempPictureBox=new CustomCTRL_PictureBox();

            _TempPictureBox.SetCheesPieceName=(int)_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].GetCheesPieceName;
            _TempPictureBox.BackgroundImage=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].BackgroundImage;
            _TempPictureBox.SetCheesPieceType=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].GetCheesPieceType;
            _TempPictureBox._Bishop=new clsBishop(this.GetPosition().row, this.GetPosition().col);

            _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].SetCheesPieceName=(int)_TempPictureBox.GetCheesPieceName;
            _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].BackgroundImage =_TempPictureBox.BackgroundImage;
            _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].SetCheesPieceType=_TempPictureBox.GetCheesPieceType;
            _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol]._Bishop=new clsBishop(New_pos.IdRow, New_pos.IdCol);
            _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol]._Bishop.CurrentCTRL=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col];

            _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].SetCheesPieceName=(int)CustomCTRL_PictureBox.enCheesPieces.Empty;
            _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].SetCheesPieceType=CustomCTRL_PictureBox.enCheesPiecesType.Empty;
            _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].BackgroundImage=null;

            this.SetPosition(New_pos.IdRow, New_pos.IdCol);
            return true;
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

            if (!( isDiagonalMove))
            {

                if (this.T!=null&&this.IsAvailableCaptures(New_pos))
                {

                    return ProcessAvailableCaptures(New_pos, ref _PictureBoxGrid);

                }

            }

            if ((isDiagonalMove))
            {
                //if (_PictureBoxGrid[New_pos.row, New_pos.col].GetCheesPieceType!=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].GetCheesPieceType &&
                //     _PictureBoxGrid[New_pos.row, New_pos.col].GetCheesPieceName!=CustomCTRL_PictureBox.enCheesPieces.Empty)
                //{
                //    return ProcessAvailableCaptures(ref _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col], ref _PictureBoxGrid[New_pos.row, New_pos.col]);
                //    //   Execute Function Prosses Available Captures
                //}




                _TempPictureBox=new CustomCTRL_PictureBox();

                _TempPictureBox.SetCheesPieceName=(int)_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].GetCheesPieceName;
                _TempPictureBox.BackgroundImage=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].BackgroundImage;
                _TempPictureBox.SetCheesPieceType=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].GetCheesPieceType;
                _TempPictureBox._Bishop = new clsBishop(this.GetPosition().row, this.GetPosition().col);

                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].SetCheesPieceName=(int)_TempPictureBox.GetCheesPieceName;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].BackgroundImage =_TempPictureBox.BackgroundImage;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol].SetCheesPieceType=_TempPictureBox.GetCheesPieceType;
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol]._Bishop = new clsBishop(New_pos.IdRow, New_pos.IdCol);
                _PictureBoxGrid[New_pos.IdRow, New_pos.IdCol]._Bishop.CurrentCTRL=_PictureBoxGrid[this.GetPosition().row, this.GetPosition().col];


                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].SetCheesPieceName=(int)CustomCTRL_PictureBox.enCheesPieces.Empty;
                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].SetCheesPieceType=CustomCTRL_PictureBox.enCheesPiecesType.Empty;
                _PictureBoxGrid[this.GetPosition().row, this.GetPosition().col].BackgroundImage=null;

                this.SetPosition(New_pos.IdRow, New_pos.IdCol);
                return true;
            }
            return false;

        }
    }
}
