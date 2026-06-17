using System;
using System.Drawing;
using System.Windows.Forms;
using WFA_Chess_Game.Properties;
using static System.Net.Mime.MediaTypeNames;
using static WFA_Chess_Game.ChessGame;
using static WFA_Chess_Game.CustomCTRL_PictureBox;

namespace WFA_Chess_Game
{
    public partial class Main : Form
    {
        private int _Rows = 8;
        private int _Cols = 8;
        bool IsFirstTimeMove = false;
        bool _HandelCreateChessBoardExecuted = false;

        private bool _PlayerWhite_Win = false;
        private bool _PlayerBlack_Win = false;
        private bool _PlayerOneWhiteIsKingInCheck = false;
        private bool _PlayerOneBlackIsKingInCheck = false;
        bool _CapturingStatus = false;
        string MoveNotation = "";
        CustomCTRL_PictureBox.enCheesPiecesType _PlayerColorWon = enCheesPiecesType.Empty;
        CustomCTRL_PictureBox.enCheesPieces _PlayerPieceNameFiled = CustomCTRL_PictureBox.enCheesPieces.Empty;



        CustomCTRL_PictureBox _PreviousCTRl_Piece = null;

        clsKing _PublicKing = null;


        CustomCTRL_PictureBox.enCheesPiecesType _CurrentTurn;
        CustomCTRL_PictureBox.enCheesPiecesType _PreviousTurn;

        int moveNumber = 1;
        Position _Sb_Pos;
        //Type Pieces
        public enum enCheesPieces { Pawn = 1, Knight = 2, Bishop = 3, Rook = 4, Queen = 5, King = 6, Empty = 7 };
        //The Baisc array for Chess Board
        CustomCTRL_PictureBox[,] _PictureBoxGrid;

        //Create Event Args Bag
        public class CheesGameEventArgs : EventArgs
        {
            public CheesGameEventArgs(CustomCTRL_PictureBox customCTRL_PictureBox)
            {

                this.customCTRL_PictureBox=customCTRL_PictureBox;
            }


            public CustomCTRL_PictureBox customCTRL_PictureBox;
        }
        public event EventHandler<CheesGameEventArgs> OnPbClick;

        private void RaiseCheesGame(object sender, CheesGameEventArgs e)
        {
            OnPbClick?.Invoke(this, e);

        }



        CustomCTRL_PictureBox _DefaultButton;

        //Position for every piece 
        int _rowX = 2, _rowY = 23;



        public Main()
        {
            InitializeComponent();


            _PictureBoxGrid = new CustomCTRL_PictureBox[_Rows, _Cols];
            _DefaultButton=customCTRL_PictureBox1;
            this.Controls.Remove(customCTRL_PictureBox1);
            _DefaultButton.Text="";
        }




        private void btnWhite_Click(object sender, EventArgs e)
        {
            _CurrentTurn=CustomCTRL_PictureBox.enCheesPiecesType.White;
            _PreviousTurn=CustomCTRL_PictureBox.enCheesPiecesType.Black;
            btnBlack.Enabled= false;
            btnWhite.Enabled= false;
            IsFirstTimeMove = true;
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            _CurrentTurn=CustomCTRL_PictureBox.enCheesPiecesType.Black;
            _PreviousTurn=CustomCTRL_PictureBox.enCheesPiecesType.White;

            btnBlack.Enabled= false;
            btnWhite.Enabled= false;
            IsFirstTimeMove = true;
        }

        void IsKingInCheckMethod()
        {
            if (_CurrentTurn==enCheesPiecesType.White)
            {
                CustomCTRL_PictureBox d = clsKing.GetKingPoistion(_PictureBoxGrid, CustomCTRL_PictureBox.enCheesPiecesType.White);
                if (d!=null)
                    _PublicKing=new clsKing(d.IdRow, d.IdCol);
                _PublicKing.CurrentCTRL=d;
                if ((_PlayerOneWhiteIsKingInCheck=_PublicKing.IsKingInCheck(_PictureBoxGrid)))
                    MessageBox.Show("Your king in Check ", "Error");



            }
            else if (_CurrentTurn==enCheesPiecesType.Black)
            {
                CustomCTRL_PictureBox d = clsKing.GetKingPoistion(_PictureBoxGrid, CustomCTRL_PictureBox.enCheesPiecesType.Black);
                if (d!=null)
                    _PublicKing=new clsKing(d.IdRow, d.IdCol);
                _PublicKing.CurrentCTRL=d;
                if ((_PlayerOneBlackIsKingInCheck=_PublicKing.IsKingInCheck(_PictureBoxGrid)))
                    MessageBox.Show("Your king in Check ", "Error");


            }
        }
        void _HendelCatching(CustomCTRL_PictureBox NewCTRlPosition)
        {
            ChessGame.Position pos = new ChessGame.Position();
            pos.row=NewCTRlPosition.IdRow;
            pos.col=NewCTRlPosition.IdCol;

            _PlayerPieceNameFiled=NewCTRlPosition.GetCheesPieceName;
            if (NewCTRlPosition.GetCheesPieceName!=CustomCTRL_PictureBox.enCheesPieces.Empty)
                MoveNotation= GetMoveNotation(_PreviousCTRl_Piece.IdRow, _PreviousCTRl_Piece.IdCol, NewCTRlPosition.IdRow, NewCTRlPosition.IdCol, GetPieceName((enCheesPieces)_PreviousCTRl_Piece.GetCheesPieceName), true);
            else
                MoveNotation= GetMoveNotation(_PreviousCTRl_Piece.IdRow, _PreviousCTRl_Piece.IdCol, NewCTRlPosition.IdRow, NewCTRlPosition.IdCol, GetPieceName((enCheesPieces)_PreviousCTRl_Piece.GetCheesPieceName), false);

            switch (_PreviousCTRl_Piece.GetCheesPieceName)
            {

                case CustomCTRL_PictureBox.enCheesPieces.Pawn:




                    _PreviousCTRl_Piece._Pawn.isVerticalMove=_PreviousCTRl_Piece._Pawn.IsVerticalPathBlocked(NewCTRlPosition, _PictureBoxGrid);

                    if (!_PreviousCTRl_Piece._Pawn.isVerticalMove)
                    {
                        _PreviousCTRl_Piece._Pawn.IsLimetedCapturing_Move(pos, _PictureBoxGrid);
                    }

                    if (_PreviousCTRl_Piece._Pawn._IsValidMove(NewCTRlPosition, ref _PictureBoxGrid))
                    {
                        _CapturingStatus= true;

                        if (_CurrentTurn==enCheesPiecesType.White)
                        {
                            _PreviousTurn = _CurrentTurn;
                            _CurrentTurn=enCheesPiecesType.Black;

                        }
                        else if (_CurrentTurn==enCheesPiecesType.Black)
                        {
                            _PreviousTurn = _CurrentTurn;
                            _CurrentTurn=enCheesPiecesType.White;

                        }
                        _PreviousCTRl_Piece._Pawn.isCapturingMoveDiagonal=false;
                        _PreviousCTRl_Piece._Pawn.IsFirstTime=false;
                        _PreviousCTRl_Piece.PawnIsFirstTimeMoving=false;
                    }


                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Queen:
                    _PreviousCTRl_Piece._Queen.isVerticalMove=_PreviousCTRl_Piece._Queen.IsVerticalPathBlocked(NewCTRlPosition, _PictureBoxGrid, ref _PreviousCTRl_Piece._Queen.T);
                    if (!_PreviousCTRl_Piece._Queen.isVerticalMove)
                    {
                        _PreviousCTRl_Piece._Queen.isHorizontalMove=_PreviousCTRl_Piece._Queen.IsHorizontalPathBlacked(NewCTRlPosition, _PictureBoxGrid,
                        ref _PreviousCTRl_Piece._Queen.T);
                    }
                    if (!_PreviousCTRl_Piece._Queen.isVerticalMove&&!_PreviousCTRl_Piece._Queen.isHorizontalMove)
                    {
                        _PreviousCTRl_Piece._Queen.isDiagonalMove=_PreviousCTRl_Piece._Queen.IsPathBlockedDiagonal(NewCTRlPosition, _PictureBoxGrid,
                           ref _PreviousCTRl_Piece._Queen.T);
                    }



                    if (_PreviousCTRl_Piece._Queen._IsValidMove(NewCTRlPosition, ref _PictureBoxGrid))
                    {
                        _CapturingStatus= true;

                        if (_CurrentTurn==enCheesPiecesType.White)
                        {
                            _PreviousTurn=_CurrentTurn;
                            _CurrentTurn=enCheesPiecesType.Black;

                        }
                        else if (_CurrentTurn==enCheesPiecesType.Black)
                        {
                            _PreviousTurn = _CurrentTurn;
                            _CurrentTurn=enCheesPiecesType.White;

                        }
                    }

                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Rook:

                    _PreviousCTRl_Piece._Rook.isVerticalMove=_PreviousCTRl_Piece._Rook.IsVerticalPathBlocked(NewCTRlPosition, _PictureBoxGrid, ref _PreviousCTRl_Piece._Rook.T);
                    if (!_PreviousCTRl_Piece._Rook.isVerticalMove)
                        _PreviousCTRl_Piece._Rook.isHorizontalMove=_PreviousCTRl_Piece._Rook.IsHorizontalPathBlacked(NewCTRlPosition, _PictureBoxGrid,

                            ref _PreviousCTRl_Piece._Rook.T);


                    if (_PreviousCTRl_Piece._Rook._IsValidMove(NewCTRlPosition, ref _PictureBoxGrid))
                    {
                        _CapturingStatus= true;

                        if (_CurrentTurn==enCheesPiecesType.White)
                        {
                            _PreviousTurn=_CurrentTurn;
                            _CurrentTurn=enCheesPiecesType.Black;

                        }
                        else if (_CurrentTurn==enCheesPiecesType.Black)
                        {
                            _PreviousTurn=_CurrentTurn;
                            _CurrentTurn=enCheesPiecesType.White;

                        }
                    }



                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Knight:


                    _PreviousCTRl_Piece._Knight.isVerticalMove=_PreviousCTRl_Piece._Knight.IsVerticalPathBlockedKnight(NewCTRlPosition, _PictureBoxGrid);
                    if (!_PreviousCTRl_Piece._Knight.isVerticalMove)
                        _PreviousCTRl_Piece._Knight.isHorizontalMove=_PreviousCTRl_Piece._Knight.IsHorizontalPathBlackedKnight(NewCTRlPosition, _PictureBoxGrid);




                    if (_PreviousCTRl_Piece._Knight._IsValidMove(NewCTRlPosition, ref _PictureBoxGrid))
                    {
                        _CapturingStatus= true;

                        if (_CurrentTurn==enCheesPiecesType.White)
                        {
                            _PreviousTurn = _CurrentTurn;
                            _CurrentTurn=enCheesPiecesType.Black;

                        }
                        else if (_CurrentTurn==enCheesPiecesType.Black)
                        {
                            _PreviousTurn = _CurrentTurn;
                            _CurrentTurn=enCheesPiecesType.White;

                        }
                    }

                    break;
                case CustomCTRL_PictureBox.enCheesPieces.King:

                    if (_PreviousCTRl_Piece._King._IsValidMove(NewCTRlPosition, ref _PictureBoxGrid))
                    {
                        if (_CurrentTurn==enCheesPiecesType.White)
                        {
                            _PreviousTurn = _CurrentTurn;
                            _CurrentTurn=enCheesPiecesType.Black;

                        }
                        else if (_CurrentTurn==enCheesPiecesType.Black)
                        {
                            _PreviousTurn = _CurrentTurn;
                            _CurrentTurn=enCheesPiecesType.White;

                        }

                    }

                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Bishop:

                    _PreviousCTRl_Piece._Bishop.isDiagonalMove=_PreviousCTRl_Piece._Bishop.IsPathBlockedDiagonal(NewCTRlPosition, _PictureBoxGrid,
                       ref _PreviousCTRl_Piece._Bishop.T);




                    if (_PreviousCTRl_Piece._Bishop._IsValidMove(NewCTRlPosition, ref _PictureBoxGrid))
                    {
                        _CapturingStatus= true;

                        if (_CurrentTurn==enCheesPiecesType.White)
                        {
                            _PreviousTurn=_CurrentTurn;
                            _CurrentTurn=enCheesPiecesType.Black;

                        }
                        else if (_CurrentTurn==enCheesPiecesType.Black)
                        {
                            _PreviousTurn=_CurrentTurn;
                            _CurrentTurn=enCheesPiecesType.White;

                        }
                    }

                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Empty:

                    break;
            }
        }

        void _ChoosePieceToMove(object sender, CheesGameEventArgs e)
        {
            ChessGame.Position pos = new ChessGame.Position();
            pos.row=e.customCTRL_PictureBox.IdRow;
            pos.col=e.customCTRL_PictureBox.IdCol;

            IsKingInCheckMethod();

            if (_CurrentTurn!=e.customCTRL_PictureBox.GetCheesPieceType)
            {
                if (_PreviousCTRl_Piece==null)
                    return;
                if (!_PreviousCTRl_Piece.IsTypeEmpty())
                {
                    _HendelCatching(e.customCTRL_PictureBox);
                    if (_CapturingStatus)
                    {
                        AddMoveToDisplay(MoveNotation);
                        MoveNotation="";
                        IsFirstTimeMove = false;

                    }
                    return;
                }
                else

                    return;
            }


            switch (e.customCTRL_PictureBox.GetCheesPieceName)
            {

                case CustomCTRL_PictureBox.enCheesPieces.Pawn:
                    _PreviousCTRl_Piece=e.customCTRL_PictureBox;
                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Queen:

                    _PreviousCTRl_Piece=e.customCTRL_PictureBox;
                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Rook:
                    _PreviousCTRl_Piece=e.customCTRL_PictureBox;

                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Knight:
                    _PreviousCTRl_Piece=e.customCTRL_PictureBox;
                    break;
                case CustomCTRL_PictureBox.enCheesPieces.King:
                    _PreviousCTRl_Piece=e.customCTRL_PictureBox;
                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Bishop:
                    _PreviousCTRl_Piece=e.customCTRL_PictureBox;
                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Empty:
                    _PreviousCTRl_Piece=e.customCTRL_PictureBox;
                    break;
            }


            //الحل يتم. عمل نسخ من كل كلاس من القطع.في في البكشر.بوكس.وعند تنفيذ الحدث.
            //    يتم تعبئه النسخ.بالبوزيشن الموقع. وارجاعها مع الحدث.هذه الاوله. ومن ثم 
            //    الثانيه.يتم عمل. اوبجيكت.نسخه.من البيكشر بوكس كلاس. وتسمى النسخه السابقه.ويتم 
            //    تخزين فيها.القطعه السابقه التي.تم ارجاعها بالحدث السابق. بحيث.حين.يتم.النقر على
            //    قطعه اخرى من النوع نفسه نوع نفس القطعه الاولى التي تم ارجاعها بالحدث السابق. 
            //    يتم.تفريغ البيانات السابقة وعدم اعتمادها لحركة اللاعب



        }


        private void CheesGame_EventHandler(object sender, CheesGameEventArgs e)
        {

            if (_CurrentTurn==enCheesPiecesType.Empty)
                return;
            if (_PlayerWhite_Win)
            {
                MessageBox.Show("Game Over (:- \nPlayer White is Win -:)...", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            else if (_PlayerBlack_Win)
            {
                MessageBox.Show("Game Over (:- \nPlayer Black is Win -:)...", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }



            _ChoosePieceToMove(sender, e);
            _PlayerWon();

            return;

        }

        private void btnResetGame_Click(object sender, EventArgs e)
        {
            _rowX = 2; _rowY = 23;

            lstHistory.Items.Clear();
            _CurrentTurn =enCheesPiecesType.Empty;
            btnBlack.Enabled=true;
            btnWhite.Enabled=true;
            _PlayerWhite_Win = false;
            _PlayerBlack_Win = false;
            _ResetChessBoard();
            DrawChessBoard();

        }

        //selected Player who Won.
        void _PlayerWon()
        {
            if (_CapturingStatus)
            {
                if (_PlayerPieceNameFiled==CustomCTRL_PictureBox.enCheesPieces.King)
                {
                    if (_PreviousTurn==enCheesPiecesType.Black)
                    {
                        MessageBox.Show("Player Black is Win -:)...", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gbGrid.Enabled=false;

                        _PlayerBlack_Win = true;
                    }
                    else if (_PreviousTurn==enCheesPiecesType.White)
                    {
                        MessageBox.Show("Player White is Win -:)...", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gbGrid.Enabled=false;
                        _PlayerWhite_Win = true;
                    }
                }
                _CapturingStatus = false;
            }
        }

        void _StartGame()
        {
            OnPbClick+=CheesGame_EventHandler;

            _CreateChessBoard();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _StartGame();





        }

        private void _Drawing_A_chess_interface(int row, int col, CustomCTRL_PictureBox NewPictureBox)
        {
            //Put piece rook black in her place
            _HandelAllPieceRooK(row, col, 0, NewPictureBox, Resources.chess_rook_black, CustomCTRL_PictureBox.enCheesPiecesType.Black);
            //Put piece rook white in her place
            _HandelAllPieceRooK(row, col, 7, NewPictureBox, Resources.chess_rook_white, CustomCTRL_PictureBox.enCheesPiecesType.White);
            //Put piece Bishop black in her place
            _HandelAllPieceBishop(row, col, 0, NewPictureBox, Resources.chess_bishop_black, CustomCTRL_PictureBox.enCheesPiecesType.Black);
            //Put piece Bishop white in her place
            _HandelAllPieceBishop(row, col, 7, NewPictureBox, Resources.chess_bishop_white, CustomCTRL_PictureBox.enCheesPiecesType.White);
            //Put piece Knight black in her place
            _HandelAllPieceKnight(row, col, 0, NewPictureBox, Resources.chess_knight_black, CustomCTRL_PictureBox.enCheesPiecesType.Black);
            //Put piece Knight white in her place
            _HandelAllPieceKnight(row, col, 7, NewPictureBox, Resources.chess_knight_white, CustomCTRL_PictureBox.enCheesPiecesType.White);
            //Put piece King black in her place
            _HandelAllPieceKing(row, col, 0, NewPictureBox, Resources.chess_king_black, CustomCTRL_PictureBox.enCheesPiecesType.Black);
            //Put piece King white in her place
            _HandelAllPieceKing(row, col, 7, NewPictureBox, Resources.chess_king_white, CustomCTRL_PictureBox.enCheesPiecesType.White);
            //Put piece Queen black in her place
            _HandelAllPieceQueen(row, col, 0, NewPictureBox, Resources.chess_queen_black, CustomCTRL_PictureBox.enCheesPiecesType.Black);
            //Put piece Queen white in her place
            _HandelAllPieceQueen(row, col, 7, NewPictureBox, Resources.chess_queen_white, CustomCTRL_PictureBox.enCheesPiecesType.White);
            //Put piece Pawn Black in her place
            _HandelAllPiecePawn(row, col, 1, NewPictureBox, Resources.chess_pawn_black, CustomCTRL_PictureBox.enCheesPiecesType.Black);
            //Put piece Pawn white in her place
            _HandelAllPiecePawn(row, col, 6, NewPictureBox, Resources.chess_pawn_white, CustomCTRL_PictureBox.enCheesPiecesType.White);

            if (NewPictureBox.GetCheesPieceName==CustomCTRL_PictureBox.enCheesPieces.Empty)
            {
                NewPictureBox._PieceEmpty=new clsPieceEmpty(row, col);
            }
        }

        void _ResetPices(CustomCTRL_PictureBox c)
        {
            switch (c.GetCheesPieceName)
            {

                case CustomCTRL_PictureBox.enCheesPieces.Pawn:
                    c.SetCheesPieceType=CustomCTRL_PictureBox.enCheesPiecesType.Empty;
                    c._Pawn=null;
                    //    c._Pawn.CurrentCTRL=null;
                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Queen:
                    c.SetCheesPieceType=CustomCTRL_PictureBox.enCheesPiecesType.Empty;
                    c._Queen=null;
                    //  c._Queen.CurrentCTRL=null
                    ;

                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Rook:
                    c.SetCheesPieceType=CustomCTRL_PictureBox.enCheesPiecesType.Empty;
                    c._Rook=null;
                    //   c._Rook.CurrentCTRL.Dispose();

                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Knight:
                    c.SetCheesPieceType=CustomCTRL_PictureBox.enCheesPiecesType.Empty;
                    c._Knight=null;
                    //  c._Knight.CurrentCTRL=null;

                    break;
                case CustomCTRL_PictureBox.enCheesPieces.King:
                    c.SetCheesPieceType=CustomCTRL_PictureBox.enCheesPiecesType.Empty;
                    c._King=null;
                    //   c._King.CurrentCTRL=null;

                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Bishop:
                    c.SetCheesPieceType=CustomCTRL_PictureBox.enCheesPiecesType.Empty;
                    c._Bishop=null;
                    //   c._Bishop.CurrentCTRL=null
                    ;
                    break;
                case CustomCTRL_PictureBox.enCheesPieces.Empty:
                    break;
            }
        }
        private void _ResetChessBoard()
        {
            for (int row = 0; row<_Rows; row++)
            {

                for (int col = 0; col<_Cols; col++)
                {


                    if (_PictureBoxGrid[row, col]!=null)
                    {

                        _PictureBoxGrid[row, col].SetCheesPieceName=(int)enCheesPieces.Rook;

                        if (_PictureBoxGrid[row, col].BackgroundImage!=null)
                        {
                            _PictureBoxGrid[row, col].BackgroundImage.Dispose();

                            _PictureBoxGrid[row, col].BackgroundImage=null;
                        }
                        _ResetPices(_PictureBoxGrid[row, col]);



                    }
                }
            }
        }
        //Create shop Chess Board
        private void _CreateChessBoard()
        {

            //_ResetChessBoard();
            if (!_HandelCreateChessBoardExecuted)
            {
                _PictureBoxGrid=new CustomCTRL_PictureBox[_Rows, _Cols];

                bool WhiteOrBlackR = true;

                int num = 1;
                for (int row = 0; row<_Rows; row++)
                {

                    for (int col = 0; col<_Cols; col++)
                    {
                        CustomCTRL_PictureBox NewPictureBox = new CustomCTRL_PictureBox();
                        NewPictureBox.IdCol=col;
                        NewPictureBox.IdRow=row;

                        NewPictureBox.Size=_DefaultButton.Size;
                        NewPictureBox.pbID=num;
                        NewPictureBox.Name="btnChees"+num.ToString();
                        NewPictureBox.Text="";
                        if (WhiteOrBlackR)
                        {
                            NewPictureBox.BackColor = Color.White;
                            NewPictureBox.PieceColor=Color.White;
                            WhiteOrBlackR =false;
                        }
                        else
                        {
                            NewPictureBox.BackColor = Color.Brown;
                            NewPictureBox.PieceColor=Color.Brown;
                            WhiteOrBlackR =true;
                        }

                        NewPictureBox.Location = new Point(_rowX, _rowY);
                        _rowX+=44;
                        NewPictureBox.BackgroundImageLayout= ImageLayout.Zoom;

                        NewPictureBox.IsKingInCheck=false;
                        NewPictureBox.StatusMove=false;

                        this.gbGrid.Controls.Add(NewPictureBox);
                        // this.Controls.Add(NewPictureBox);

                        _PictureBoxGrid[row, col]=NewPictureBox;
                        _PictureBoxGrid[row, col].Click +=PictureBox_Click;

                        _Drawing_A_chess_interface(row, col, NewPictureBox);

                        num++;


                    }
                    WhiteOrBlackR=!WhiteOrBlackR;
                    _rowY+=33;
                    _rowX=2;
                }


            }
            _HandelCreateChessBoardExecuted = true;
        }

        private void DrawChessBoard()
        {
            for (int row = 0; row<_Rows; row++)
            {
                for (int col = 0; col<_Cols; col++)
                {
                    _Drawing_A_chess_interface(row, col, _PictureBoxGrid[row, col]);
                }
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            //Safe Casting
            CustomCTRL_PictureBox custom = sender as CustomCTRL_PictureBox;

            RaiseCheesGame(this, new CheesGameEventArgs(custom));



        }
        private void _HandelAllPieceRooK(int rowReal, int col, int rowFrom, CustomCTRL_PictureBox NewPictureBox, System.Drawing.Image image, CustomCTRL_PictureBox.enCheesPiecesType Type)
        {
            if (rowReal == rowFrom &&(col==0||col==7))
            {
                NewPictureBox.SetCheesPieceName=(int)enCheesPieces.Rook;
                NewPictureBox.BackgroundImage=image; ;
                NewPictureBox.SetCheesPieceType=Type;
                NewPictureBox._Rook=new clsRook(NewPictureBox.IdRow, NewPictureBox.IdCol);
                NewPictureBox._Rook.CurrentCTRL=NewPictureBox;
            }
        }
        private void _HandelAllPieceBishop(int rowReal, int col, int rowFrom, CustomCTRL_PictureBox NewPictureBox, System.Drawing.Image image, CustomCTRL_PictureBox.enCheesPiecesType Type)
        {
            if (rowReal == rowFrom&&(col==2||col==5))
            {
                NewPictureBox.SetCheesPieceName=(int)enCheesPieces.Bishop;
                NewPictureBox.BackgroundImage=image;
                NewPictureBox.SetCheesPieceType=Type;
                NewPictureBox._Bishop=new clsBishop(NewPictureBox.IdRow, NewPictureBox.IdCol);
                NewPictureBox._Bishop.CurrentCTRL=NewPictureBox;
            }
        }
        private void _HandelAllPieceKnight(int rowReal, int col, int rowFrom, CustomCTRL_PictureBox NewPictureBox, System.Drawing.Image image, CustomCTRL_PictureBox.enCheesPiecesType Type)
        {
            if (rowReal == rowFrom&&(col==1||col==6))
            {
                NewPictureBox.SetCheesPieceName=(int)enCheesPieces.Knight;
                NewPictureBox.BackgroundImage=image;
                NewPictureBox.SetCheesPieceType=Type;
                NewPictureBox._Knight=new clsKnight(NewPictureBox.IdRow, NewPictureBox.IdCol);
                NewPictureBox._Knight.CurrentCTRL=NewPictureBox;

            }
        }

        private void _HandelAllPieceKing(int rowReal, int col, int rowFrom, CustomCTRL_PictureBox NewPictureBox, System.Drawing.Image image, CustomCTRL_PictureBox.enCheesPiecesType Type)
        {
            if (rowReal == rowFrom&&((col==4&&Type==enCheesPiecesType.White)||(col==3&&Type==enCheesPiecesType.Black)))
            {
                NewPictureBox.SetCheesPieceName=(int)enCheesPieces.King;
                NewPictureBox.BackgroundImage=image;
                NewPictureBox.SetCheesPieceType=Type;

                NewPictureBox._King=new clsKing(NewPictureBox.IdRow, NewPictureBox.IdCol);
                NewPictureBox._King.CurrentCTRL=NewPictureBox;
            }
        }

        private void _HandelAllPieceQueen(int rowReal, int col, int rowFrom, CustomCTRL_PictureBox NewPictureBox, System.Drawing.Image image, CustomCTRL_PictureBox.enCheesPiecesType Type)
        {
            if (rowReal == rowFrom&&((col==3&&Type==enCheesPiecesType.White)||(col==4&&Type==enCheesPiecesType.Black)))
            {

                NewPictureBox.SetCheesPieceName=(int)enCheesPieces.Queen;
                NewPictureBox.BackgroundImage=image;
                NewPictureBox.SetCheesPieceType=Type;
                NewPictureBox._Queen=new clsQueen(NewPictureBox.IdRow, NewPictureBox.IdCol);

                NewPictureBox._Queen.CurrentCTRL=NewPictureBox;
            }
        }



        private void _HandelAllPiecePawn(int rowReal, int col, int rowFrom, CustomCTRL_PictureBox NewPictureBox, System.Drawing.Image image, CustomCTRL_PictureBox.enCheesPiecesType Type)
        {
            if (rowReal == rowFrom&&(col>=0||col<=7))
            {
                NewPictureBox.SetCheesPieceName=(int)enCheesPieces.Pawn;
                NewPictureBox.BackgroundImage=image;
                NewPictureBox.SetCheesPieceType=Type;
                NewPictureBox._Pawn=new clsPawn(NewPictureBox.IdRow, NewPictureBox.IdCol);

                NewPictureBox._Pawn.CurrentCTRL=NewPictureBox;

            }
        }


        public string GetMoveNotation(int fromRow, int fromCol, int toRow, int toCol, string pieceName, bool isCapture)
        {
            // 1. تحويل الأعمدة من (0-7) إلى (a-h)
            char[] files = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
            char targetFile = files[toCol];

            // 2. تحويل الصفوف من (0-7) إلى (1-8) 
            // ملاحظة: الصف 0 في البرمجة هو الصف 8 في الشطرنج (الأعلى)
            int targetRank = 8 - toRow;

            string notation = "";

            // 3. تحديد حرف القطعة
            switch (pieceName.ToLower())
            {
                case "king": notation = "K"; break;
                case "queen": notation = "Q"; break;
                case "rook": notation = "R"; break;
                case "bishop": notation = "B"; break;
                case "knight": notation = "N"; break;
                case "pawn":
                    // البيدق لا يأخذ حرفاً، لكن إذا أكل قطعة نكتب اسم عموده القديم
                    if (isCapture) notation = files[fromCol].ToString();
                    break;
            }

            // 4. إضافة علامة الأكل "x" إذا لزم الأمر
            if (isCapture)
            {
                notation += "x";
            }

            // 5. دمج كل شيء (الحرف + المربع المستهدف)
            notation += targetFile.ToString() + targetRank.ToString();

            return notation;
        }

        // هذه الدالة تستدعيها في نهاية كل حركة صحيحة
        public void AddMoveToDisplay(string moveNotation)
        {
            if (_PreviousTurn==enCheesPiecesType.White||IsFirstTimeMove)
            {
                // إذا كان دور الأبيض، نفتح سطر جديد برقم النقلة
                // النتيجة المتوقعة: "1. e4"
                string entry = moveNumber + ". " + moveNotation;
                lstHistory.Items.Add(entry);


            }
            else
            {
                // إذا كان دور الأسود، نعدل آخر سطر أضفناه لنضع حركة الأسود بجانب حركة الأبيض
                // النتيجة المتوقعة: "1. e4   e5"
                int lastIndex = lstHistory.Items.Count - 1;
                string lastEntry = lstHistory.Items[lastIndex].ToString();

                lstHistory.Items[lastIndex] = lastEntry + "    " + moveNotation;

                moveNumber++; // نزيد رقم النقلة للمرة القادمة

            }

            // تجعل الـ ListBox ينزل تلقائياً لآخر حركة مضافة
            lstHistory.SelectedIndex = lstHistory.Items.Count - 1;
        }

        public string GetPieceName(enCheesPieces pieceType)
        {
            switch (pieceType)
            {
                case enCheesPieces.Knight:
                    return "king"; // نستخدم N للحصان لأن K محجوزة للملك
                case enCheesPieces.Bishop:
                    return "bishop";
                case enCheesPieces.Rook:
                    return "rook";
                case enCheesPieces.Queen:
                    return "queen";

                case enCheesPieces.King:
                    return "knight";
                case enCheesPieces.Pawn:
                    return "pawn";  // في نظام الشطرنج، البيدق لا يُكتب له حرف
                default:
                    return "";  // في حال كان المربع فارغاً
            }

        }
    }
}