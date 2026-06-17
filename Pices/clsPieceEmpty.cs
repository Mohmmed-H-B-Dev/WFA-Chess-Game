using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFA_Chess_Game
{
    public class clsPieceEmpty:ChessGame
    {
        Position _Current_Pos;

        public clsPieceEmpty(int row, int col) : base(row, col)
        {
            _Current_Pos.row = row;
            _Current_Pos.col = col;
        }
        public Position GetPosition() { return _Current_Pos; }
        public void SetPosition(int row, int col) { _Current_Pos.row=row; _Current_Pos.col=col; }

        public override bool _IsValidMove(CustomCTRL_PictureBox New_pos , ref CustomCTRL_PictureBox[,] _PictureBoxGrid)
        {
            if (_Current_Pos.row<0||_Current_Pos.row>7||
                _Current_Pos.col<0||_Current_Pos.col>7)
            {
                return false;
            }
  
               
          
            return false;

        }
        //public bool IsValidMove(int row, int col )
        //{
        //    Position p = new Position();
        //    p.row = row;
        //    p.col = col;

        //    return _IsValidMove(p  );
        //}
    }
}
