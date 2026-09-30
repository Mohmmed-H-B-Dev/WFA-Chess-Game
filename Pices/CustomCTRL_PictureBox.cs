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
        public Color TileColor { set; get; }
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
        public bool IsPawn()
        {
            return this.GetCheesPieceName==enCheesPieces.Pawn;
        }
        public bool IsKnight()
        {
            return this.GetCheesPieceName==enCheesPieces.Knight;
        }
        public bool IsBishop()
        {
            return this.GetCheesPieceName==enCheesPieces.Bishop;
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
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
