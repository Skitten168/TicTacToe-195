using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe_195
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members
        /// <summary>
        /// what is in square for active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// true if it's player 1's turn (X) or Player 2's tunr (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if game has ended
        /// </summary>
        private bool mGameEnded;

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();      
        }
        #endregion
        /// <summary>
        /// start a new game and clears board
        /// </summary>
        private void NewGame()
        {   
            ///Create array for unused squares
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;
            //ensure player 1 starts the game
            mPlayer1Turn = true;

            //Iterate each button on board
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.ForestGreen;
                button.Foreground = Brushes.Purple;
            });
            mGameEnded = false;
        }

        /// <summary>
        /// handles a button click
        /// </summary>
        /// <param name="sender">button clicked</param>
        /// <param name="e"> event from the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {   //starts a new game when a game ends
            if(mGameEnded)
            {
                NewGame();
                return;
            }
            //cast sender to button
            var button = (Button)sender;
            //find buttons in array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);
            //Don't do anything if the square already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // set the square value based on whos turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Naught;
            //set the button to display the result of the click
            button.Content = mPlayer1Turn ? "X" : "O";

            //change player marktypr colors
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Yellow;
            //cool shorthand to invert values and toggle turns
            mPlayer1Turn ^= true;

            //check for a winner
            CheckForWinner();
        }
        //function to check for winner with 3 in a row
        private void CheckForWinner()
        {
            //horizontal win check

            //First Row
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;
                //show winning squares
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.HotPink;
            }
            //second Row
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;
                //show winning squares
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.HotPink;
            }
            //third row
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;
                //show winning squares
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.HotPink;
            }

            //vertical win check

            //First Column
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;
                //show winning squares
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.HotPink;
            }
            
            //Second Column
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;
                //show winning squares
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.HotPink;
            }
            
            //Third Column
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;
                //show winning squares
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.HotPink;
            }

            //Horizontal win
            //Top-left to bottom-right
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;
                //show winning squares
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.HotPink;
            }

            //Top-right to bottom-left
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;
                //show winning squares
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.HotPink;
            }


            //no winner with a full board
            if (!mResults.Any(f => f == MarkType.Free))
            {
                   //Game ended
                mGameEnded = true;
                //change all squares color to indicate end of game
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.OrangeRed;
                });

            }
        }
    }
}
