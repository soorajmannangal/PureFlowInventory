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
        private CustomerTable customerTable;
        private ServiceRequestTable serviceRequestTable;
        private int CustomerId;
        public NewInvoiceViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            customerTable = new CustomerTable();
            serviceRequestTable = new ServiceRequestTable();
            SetDefaults();
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
        private void SetCustomerFieldsDefaults()
        {
            CustomerName = "";
            CustomerEmail = "";
            CustomerAddress = "";
        }

        public override void SetDefaults()
        {
            InvoiceDate = DateTime.Now;
            CustomerMobile = "";
            SetCustomerFieldsDefaults();
            CustomerId = 0;
            MakeCustomerFieldsReadonly = true;
        }

        private ICommand fetchCommand;
        public ICommand FetchCommand => fetchCommand ?? (fetchCommand = new RelayCommand(FetchCustomerDetails, ()=>true));
        private void FetchCustomerDetails()
        {
            if (String.IsNullOrEmpty(CustomerMobile)) return;
            CustomerGridDto customerGridDto = customerTable.GetCustomerByPhone(CustomerMobile);
            if (customerGridDto == null)
            {
                //Enable customer textboxes
                CustomerId = 0;
                MakeCustomerFieldsReadonly = false;
                SetCustomerFieldsDefaults();
                return;
            }

            MakeCustomerFieldsReadonly = true;

            //Disable Customer details text boxes
            CustomerEmail = customerGridDto.Email;
            CustomerAddress = customerGridDto.Address;
            CustomerName = customerGridDto.Name;
            CustomerId = customerGridDto.ID;

            //Fetch service request details for CustomerID
            ServiceRequestCombo = serviceRequestTable.GetServiceRequestListForCustomerId(CustomerId);        

        }


        private List<ServiceRequestGridDto> serviceRequestCombo;
        public List<ServiceRequestGridDto> ServiceRequestCombo
        {
            get
            {
                return serviceRequestCombo;
            }
            set
            {
                serviceRequestCombo = value;
                OnPropertyChanged("ServiceRequestCombo");
            }
        }

        private ServiceRequestGridDto selectedServiceRequest;
        public ServiceRequestGridDto SelectedServiceRequest
        {
            get => selectedServiceRequest;
            set
            {
                selectedServiceRequest = value;
                //Models = modelTable.GetModelNames(selectedBrand.ID);
                //if (Models.Count > 0)
                //{
                //    SelectedModel = Models[0];
                //}


                OnPropertyChanged("SelectedServiceRequest");            
            }
        }


        private bool makeCustomerFieldsReadonly;
        public bool MakeCustomerFieldsReadonly
        {
            get => makeCustomerFieldsReadonly;
            set
            {
                makeCustomerFieldsReadonly = value;
                OnPropertyChanged("MakeCustomerFieldsReadonly");
            }
        }

        public String CustomerName
        {
            get => customerTable.Name;
            set { customerTable.Name = value; OnPropertyChanged("CustomerName"); }
        }

        public String CustomerMobile
        {
            get => customerTable.Phone;
            set { customerTable.Phone = value; OnPropertyChanged("CustomerMobile"); }
        }

        public String CustomerEmail
        {
            get => customerTable.Email;
            set { customerTable.Email = value; OnPropertyChanged("CustomerEmail"); }
        }

        public String CustomerAddress
        {
            get => customerTable.Address;
            set { customerTable.Address = value; OnPropertyChanged("CustomerAddress"); }
        }

        private DateTime invoiceDate;
        public DateTime InvoiceDate
        {
            get { return invoiceDate; }
            set { invoiceDate = value; OnPropertyChanged("InvoiceDate"); }
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
