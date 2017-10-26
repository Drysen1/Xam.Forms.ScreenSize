using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DialPad.ViewModels
{
    public class DialPadViewModel : BaseViewModel
    {
        private string _phoneNumber;

        //Properties
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged("PhoneNumber"); }
        }

        public ICommand BtnClickCmd { get; set; }
        public ICommand DeleteCmd { get; set; }

        public DialPadViewModel()
        {
            BtnClickCmd = new Command<string>(BtnClicked);
            DeleteCmd = new Command(Delete);
            PhoneNumber = string.Empty;
        }

        private void BtnClicked(string cmdParameter)
        {
            PhoneNumber += cmdParameter;
        }    

        private void Delete()
        {
            if(PhoneNumber.Length > 0)
            {
                PhoneNumber = PhoneNumber.Remove(PhoneNumber.Length - 1);
            }            
        }
    }
}
