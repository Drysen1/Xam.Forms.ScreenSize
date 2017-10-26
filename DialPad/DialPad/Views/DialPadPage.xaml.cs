using DialPad.CustomRenderers;
using DialPad.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DialPad.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialPadPage : ContentPage
    {
        private DialPadViewModel _viewModel;
        private List<RoundBtn> _btnList;
        private List<string> _btnParams;

        public DialPadPage()
        {
            InitializeComponent();
            _viewModel = new DialPadViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ScaleUI();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        /// <summary>
        /// This method add buttons to a list
        /// </summary>
        private void CreateButtons()
        {
            SetBtnParams();
            _btnList = new List<RoundBtn>();
            foreach (var item in _btnParams)
            {
                _btnList.Add(CreateRoundBtn(item));
            }
        }

        /// <summary>
        /// To be able to scale the ui we draw most of the buttons in code behind instead of
        /// xaml. We do this because round buttons need a constant value to be round.
        /// We also set the same value to the grids row and columns.
        /// </summary>
        private void ScaleUI()
        {
            CreateButtons();
            AddBtnsToUI(_btnList);

            double screenHeight = App.DisplayScreenHeight;
            int size = 0;
            if (screenHeight < 600)
            {
                size = 60;
                SetGridDefinitions(size);
                foreach (var btn in _btnList)
                {
                    SetBtnSize(btn, size);
                }
            }
            else if (screenHeight > 600 && screenHeight < 700)
            {
                size = 70;
                SetGridDefinitions(size);
                foreach (var btn in _btnList)
                {
                    SetBtnSize(btn, size);
                }
            }
            else if (screenHeight > 700)
            {
                size = 80;
                SetGridDefinitions(size);
                foreach (var btn in _btnList)
                {
                    SetBtnSize(btn, size);
                }
            }
        }

        /// <summary>
        /// This method loops through a list with buttons and calls another method
        /// to add the button to the ui.
        /// </summary>
        /// <param name="btnList">Button list.</param>
        private void AddBtnsToUI(List<RoundBtn> btnList)
        {
            foreach (var btn in btnList)
            {
                AddBtnToUI(btn);
            }
        }

        /// <summary>
        /// This method adds a button to the UI in correct row and column
        /// </summary>
        /// <param name="btn">Button.</param>
        private void AddBtnToUI(Button btn)
        {
            int row = SetRow(btn.CommandParameter.ToString());
            int col = SetCol(btn.CommandParameter.ToString());
            dialGrid.Children.Add(btn, col, row);
        }

        /// <summary>
        /// This method returns what row a button should have depending on its cmdparam
        /// </summary>
        /// <returns>The row.</returns>
        /// <param name="s">S.</param>
        private int SetRow(string s)
        {
            int row = 0;
            switch (s)
            {
                case "1":
                case "2":
                case "3":
                    row = 1;
                    break;
                case "4":
                case "5":
                case "6":
                    row = 2;
                    break;
                case "7":
                case "8":
                case "9":
                    row = 3;
                    break;
                case "*":
                case "0":
                case "#":
                    row = 4;
                    break;
            }

            return row;
        }

        /// <summary>
        /// This method returns what column a button should have depending on its cmdparam
        /// </summary>
        /// <returns>The row.</returns>
        /// <param name="s">S.</param>
        private int SetCol(string s)
        {
            int col = 0;
            switch (s)
            {
                case "1":
                case "4":
                case "7":
                case "*":
                    col = 1;
                    break;
                case "2":
                case "5":
                case "8":
                case "0":             
                    col = 2;
                    break;
                case "3":
                case "6":
                case "9":
                case "#":
                    col = 3;
                    break;
            }

            return col;
        }

        /// <summary>
        /// This method adds parameters to a list which then is used to give a button a parameter.
        /// </summary>
        private void SetBtnParams()
        {
            _btnParams = new List<string>{
                "1","2","3","4","5","6","7","8","9","*","0","#"
            };
        }

        /// <summary>
        /// This method creates a round button and adds a command and commandparameter
        /// </summary>
        /// <returns>The round button.</returns>
        /// <param name="cmdParam">Cmd parameter.</param>
        private RoundBtn CreateRoundBtn(string cmdParam)
        {

            RoundBtn btn = new RoundBtn()
            {
                BackgroundColor = Color.FromHex("#FFFFFF"),
                BorderColor = Color.Black,
                TextColor = Color.Black,
                Command = _viewModel.BtnClickCmd,
                CommandParameter = cmdParam,
                Text = cmdParam.ToString()
            };

            return btn;
        }

        /// <summary>
        /// This method sets grid row and columnsizes when scaling the ui.
        /// </summary>
        /// <param name="size">Size.</param>
        private void SetGridDefinitions(int size)
        {
            dialGrid.RowDefinitions.Clear();
            dialGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) }); //Top should always be 60.
            dialGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(size) });
            dialGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(size) });
            dialGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(size) });
            dialGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(size) });
            dialGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) }); //Bottom row also always 60

            dialGrid.ColumnDefinitions.Clear();
            dialGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); //first col always star
            dialGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(size) });
            dialGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(size) });
            dialGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(size) });
            dialGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); //last col always star
        }

        /// <summary>
        /// This method sets btn sizes when scaling the ui.
        /// </summary>
        /// <param name="btn">Button.</param>
        /// <param name="size">Size.</param>
        private void SetBtnSize(Button btn, int size)
        {
            int borderWidth = 1;
            int heightWidth = size - 1;
            int borderRadius = size / 2;

            btn.BorderWidth = borderWidth;
            btn.BorderRadius = borderRadius;
            btn.HeightRequest = heightWidth;
            btn.MinimumHeightRequest = heightWidth;
            btn.WidthRequest = heightWidth;
            btn.MinimumWidthRequest = heightWidth;
        }
    }
}