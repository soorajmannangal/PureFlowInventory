using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace PureFlow
{
    public class NewInvoiceViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public NewInvoiceViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {

        }

        private NewInvoiceView invoiceView;
        public NewInvoiceView InvoiceView
        {
            get { return invoiceView; }
            set
            {
                invoiceView = value;
            }
        }

        public override void SetDefaults()
        {
            throw new NotImplementedException();
        }

        private ICommand addInvoiceItemCommand;
        public ICommand AddInvoiceItemCommand => addInvoiceItemCommand ?? (addInvoiceItemCommand = new RelayCommand(AddInvoiceItem, CanAddInvoiceItem));
        private void AddInvoiceItem()
        {
            IWindowViewModel contextViewModel = new AddInvoiceItemViewModel(new EnableInvoiceWindowCommand(InvoiceView));
            var contextView = new AddInvoiceItemView(contextViewModel);
            new DisableInvoiceWindowCommand(InvoiceView).Execute(null);
            contextView.Show();
        }

        private bool CanAddInvoiceItem() => true;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
