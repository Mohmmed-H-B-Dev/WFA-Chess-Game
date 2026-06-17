using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_Chess_Game
{

    public partial class CustomCTRL_PictureBox : Control
    {
        public CustomCTRL_PictureBox()
        {
            InitializeComponent();
        }
        public class PbLocationClass
        {
            public int ID { set; get; }

            //public int IdLocationXrow { set; get; }
            //public int IdLocationRrow { set; get; }
            //public int IdLocationXcol { set; get; }
            //public int IdLocationRcol { set; get; }

          


        }
        public  PbLocationClass pbLocationClass = new PbLocationClass();
        public bool IsKingInCheck { set; get; }
        public enum enCheesPieces {enIsChosen=0, Pawn = 1, Knight = 2, Bishop = 3, Rook = 4, Queen = 5, King = 6 , Empty =7};
        public enum enCheesPiecesType { Black=1,White=2,Empty}

        public bool PawnIsFirstTimeMoving = true;
        enCheesPieces _CheesPieceName = enCheesPieces.Empty;
        enCheesPiecesType _CheesPieceType = enCheesPiecesType.Empty;
        int _IdRow = 0;
        int _IdCol = 0;

        public Color PieceColor { set; get; }
        public int IdRow { set { _IdRow=value; } get { return _IdRow; } }

        public int IdCol { set { _IdCol=value; } get { return _IdCol; } }
        public int pbID { set; get; }
 
        public int IdPbLocationInCols { set; get; }
        public int IdPbLocationInRows { set; get; }
        public int SetCheesPieceName { set { _CheesPieceName=(enCheesPieces)value; } }
        public enCheesPieces GetCheesPieceName { get { return _CheesPieceName; } }

        public enCheesPiecesType SetCheesPieceType { set { _CheesPieceType=value; } }
        public enCheesPiecesType GetCheesPieceType { get { return _CheesPieceType; } }


        public clsRook _Rook { set; get; }
        public clsBishop _Bishop { set; get; }
        public clsKing _King { set; get; }
        public clsKnight _Knight { set; get; }
        public clsPawn _Pawn { set; get; }
        public clsQueen _Queen { set; get; }
        public clsPieceEmpty _PieceEmpty { set; get; }
        public bool StatusMove {  set; get; }
        public void AlertAllPieces_KingInCheck(CustomCTRL_PictureBox[,] _PictureBoxGrid, CustomCTRL_PictureBox.enCheesPiecesType type)
        {
            
        }
        public bool IsRookAndTypeWhite()
        {
            return this.GetCheesPieceName==enCheesPieces.Rook && this.GetCheesPieceType==enCheesPiecesType.White;
        }
        public bool IsRookAndTypeBlack()
        {
            return this.GetCheesPieceName==enCheesPieces.Rook && this.GetCheesPieceType==enCheesPiecesType.Black;
        }
        public bool enIsChosen(enCheesPieces enChees)
        {
            return enCheesPieces.enIsChosen==enChees;
        }
        public bool IsRook()
        {
            return this.GetCheesPieceName==enCheesPieces.Rook;
        }
        public bool IsKing()
        {
            return this.GetCheesPieceName==enCheesPieces.King;
        }
        public bool IsQueen()
        {
            return this.GetCheesPieceName==enCheesPieces.Queen;
        }
        public bool IsEmpty()
        {
            return this.GetCheesPieceName==enCheesPieces.Empty;
        }
        public bool CheckIsPiece()
        {
            return (this.GetCheesPieceName!=enCheesPieces.Empty&&this.GetCheesPieceName!=enCheesPieces.enIsChosen);
        }
        public bool IsTypeBlack()
        {
            return (this.GetCheesPieceType==enCheesPiecesType.Black);
        }
        public bool IsTypeWhite()
        {
            return (this.GetCheesPieceType==enCheesPiecesType.White);
        }
        public bool IsTypeWhiteOrBlack()
        {


            return (this.GetCheesPieceType==enCheesPiecesType.White||this.GetCheesPieceType==enCheesPiecesType.Black);
        }

        public bool IsTypeEmpty()
        {
            return (this.GetCheesPieceType==enCheesPiecesType.Empty);
        }
        public bool IsEqualsType(CustomCTRL_PictureBox t)
        { 
            return this.GetCheesPieceType==t.GetCheesPieceType;
        }
        ChessGame.Position _position = new ChessGame.Position();
        public void ExecuteMoving(CustomCTRL_PictureBox New_CTRL_Position,ref CustomCTRL_PictureBox[,] _PictureBoxGrid,ref CustomCTRL_PictureBox.enCheesPiecesType Turn)
        {
            _position.row=New_CTRL_Position.IdRow;
            _position.col=New_CTRL_Position.IdCol;
            switch (this.GetCheesPieceName)
            {
                case CustomCTRL_PictureBox.enCheesPieces.Pawn:

                    if (Turn==this.GetCheesPieceType)
                    {
                        _Pawn.isVerticalMove=_Pawn.IsVerticalPathBlocked(New_CTRL_Position, _PictureBoxGrid);
                       
                        if ((_Pawn.isVerticalMove))
                        {



                            if (this._Pawn._IsValidMove(New_CTRL_Position, ref _PictureBoxGrid))
                            {
                                if (Turn==enCheesPiecesType.White)
                                {
                                    Turn=enCheesPiecesType.Black;

                                }
                                else if (Turn==enCheesPiecesType.Black)
                                {
                                    Turn=enCheesPiecesType.White;

                                }
                            }


                        }
                    }

                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Queen:
                    this._Queen._IsValidMove(New_CTRL_Position, ref _PictureBoxGrid);

                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Rook:
                    if (Turn==this.GetCheesPieceType)
                    {
                        _Rook.isVerticalMove=_Rook.IsVerticalPathBlocked(New_CTRL_Position, _PictureBoxGrid);
                        _Rook.isHorizontalMove=_Rook.IsHorizontalPathBlacked(New_CTRL_Position, _PictureBoxGrid);
                        if ((_Rook.isVerticalMove)||
                            (_Rook.isHorizontalMove))
                         {

                        

                        if (this._Rook._IsValidMove(New_CTRL_Position, ref _PictureBoxGrid))
                        {
                            if (Turn==enCheesPiecesType.White)
                            {
                                Turn=enCheesPiecesType.Black;

                            }
                            else if (Turn==enCheesPiecesType.Black)
                            {
                                Turn=enCheesPiecesType.White;

                            }
                        }


                    }
                    }

                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Knight:
                    this._Knight._IsValidMove(New_CTRL_Position, ref _PictureBoxGrid);

                    break;
                case CustomCTRL_PictureBox.enCheesPieces.King:
                    this._King._IsValidMove(New_CTRL_Position, ref _PictureBoxGrid);


                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Bishop:
                    this._Bishop._IsValidMove(New_CTRL_Position, ref _PictureBoxGrid);
                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Empty:



                     break;
            }


            //switch (this.GetCheesPieceName)
            //{
            //    case CustomCTRL_PictureBox.enCheesPieces.Pawn:
            //        if (New_CTRL_Position.GetCheesPieceName==enCheesPieces.Pawn)
            //        {
            //            // this._Rook.ProcessAvailableCaptures();
            //            return;
            //        }

            //        break;
            //    case CustomCTRL_PictureBox.enCheesPieces.Queen:
            //        if (New_CTRL_Position.GetCheesPieceName==enCheesPieces.Queen)
            //        {
            //            // this._Rook.ProcessAvailableCaptures();
            //            return;
            //        }

            //        break;
            //    case CustomCTRL_PictureBox.enCheesPieces.Rook:
            //        if (New_CTRL_Position.GetCheesPieceName==enCheesPieces.Rook)
            //        {
            //            // this._Rook.ProcessAvailableCaptures();
            //            return;
            //        }

                    

            //        break;
            //    case CustomCTRL_PictureBox.enCheesPieces.Knight:
            //        if (New_CTRL_Position.GetCheesPieceName==enCheesPieces.Knight)
            //        {
            //            // this._Rook.ProcessAvailableCaptures();
            //            return;
            //        }
            //        break;
            //    case CustomCTRL_PictureBox.enCheesPieces.King:
            //        if (New_CTRL_Position.GetCheesPieceName==enCheesPieces.King)
            //        {
            //            // this._Rook.ProcessAvailableCaptures();
            //            return;

            //        }
            //        break;
            //    case CustomCTRL_PictureBox.enCheesPieces.Bishop:
            //        if (New_CTRL_Position.GetCheesPieceName==enCheesPieces.Bishop)
            //        {
            //            // this._Rook.ProcessAvailableCaptures();
            //            return;
            //        }
            //        break;
            //    case CustomCTRL_PictureBox.enCheesPieces.Empty:
            //        if (New_CTRL_Position.GetCheesPieceName==enCheesPieces.Empty)
            //        {

            //        }
            //        break;
            //}
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
