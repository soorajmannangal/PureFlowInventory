using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace PureFlow
{
    public class NewInvoiceViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private CustomerTable customerTable;
        private ServiceRequestTable serviceRequestTable;
        private InvoiceTable invoiceTable;
        public NewInvoiceViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            customerTable = new CustomerTable();
            serviceRequestTable = new ServiceRequestTable();
            invoiceTable = new InvoiceTable();
            SetDefaults();
           // InvoiceItemsList = new List<InvoiceItemsGridDto>();
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
            Technician = new ServiceManTable().GetItemNames();
            if(Technician.Count > 0)
            {
                SelectedTechnician = Technician[0];
            }

            NextServiceDueDate = DateTime.Now.AddMonths(6);
            TotalAmount = 0;
            FillServiceRequestDetails(0);
            Notes = "";

            InvoiceItems = new ObservableCollection<InvoiceItemsGridDto>();
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
                FillServiceRequestDetails(0);
                return;
            }

            MakeCustomerFieldsReadonly = true;

            //Disable Customer details text boxes
            CustomerEmail = customerGridDto.Email;
            CustomerAddress = customerGridDto.Address;
            CustomerName = customerGridDto.Name;
            CustomerId = customerGridDto.ID;
              
            FillServiceRequestDetails(customerGridDto.ID);
        }

        public int CustomerId
        {
            get => invoiceTable.CustomerID;
            set => invoiceTable.CustomerID = value;
        }  

        private ICommand createInvoiceCommand;
        public ICommand CreateInvoiceCommand => createInvoiceCommand ?? (createInvoiceCommand = new RelayCommand(CreateInvoice, () => true));
        private void CreateInvoice()
        {

            int serviceRequestId = 0;
            if(SelectedServiceRequest != null)
            {
                serviceRequestId = SelectedServiceRequest.ID;
            }

            //Decrease inventory count

            int serviceManID = 0;
            if (SelectedTechnician != null)
            {
                serviceManID = SelectedTechnician.ID;
            }

            invoiceTable.ServiceRequestID = serviceRequestId;
            invoiceTable.ServiceManID = serviceManID;
            invoiceTable.InsertAll();

            serviceRequestTable.CloseRequest(serviceRequestId);

            SetDefaults();
        }


        
        private ICommand removeItemCommand;
        public ICommand RemoveItemCommand => removeItemCommand ?? (removeItemCommand = new RelayCommand(RemoveItem, () => true));
        private void RemoveItem()
        {

        }

        private ICommand cancelCommand;
        public ICommand CancelCommand => cancelCommand ?? (cancelCommand = new RelayCommand(Cancel, () => true));
        private void Cancel()
        {
            SetDefaults();
        }

        private void FillServiceRequestDetails(int custId)
        {
            if (custId == 0 || (ServiceRequestCombo = serviceRequestTable.GetServiceRequestListForCustomerId(custId)).Count == 0)
            {
                ServiceRequestCombo = null;
                Brands = new BrandTable().GetBrandNames();
                if (Brands.Count > 0)
                {
                    SelectedBrand = Brands[0];
                }

                if (SelectedBrand != null)
                {
                    Models = new ModelTable().GetModelNames(SelectedBrand.ID);
                    if (Models.Count > 0)
                    {
                        SelectedModel = Models[0];
                    }
                }

                RequestDate = DateTime.Now;
                IsUnderWarranty = false;
                RequestDetails = "";
                return;
            }

            SelectedServiceRequest = ServiceRequestCombo[0];

            Brands = new BrandTable().GetNamesById(SelectedServiceRequest.BrandID);
            if(Brands.Count > 0)
            {
                SelectedBrand = Brands[0];
            }

            Models = new ModelTable().GetNamesById(SelectedServiceRequest.ModelID);
            if (Models.Count > 0)
            {
                SelectedModel = Models[0];
            }

            IsUnderWarranty = SelectedServiceRequest.IsUnderWarranty;
            RequestDate = SelectedServiceRequest.RequestDate;
            RequestDetails = SelectedServiceRequest.Details;
            MakeServiceRequestFieldsReadonly = true;


        }

        public string Notes
        {
            get => invoiceTable.Notes;
            set { invoiceTable.Notes = value; OnPropertyChanged("Note"); }
        }

        public Decimal TotalAmount
        {
            get => invoiceTable.TotalAmount;
            set { invoiceTable.TotalAmount = value; OnPropertyChanged("TotalAmount"); }
        }

        private List<ComboDto> technician;
        public List<ComboDto> Technician
        {
            get
            {
                return technician;
            }
            set
            {
                technician = value;              
                OnPropertyChanged("Technician");
            }
        }

        private ComboDto selectedTechnician;
        public ComboDto SelectedTechnician
        {
            get => selectedTechnician;
            set
            {
                selectedTechnician = value;
                OnPropertyChanged("SelectedTechnician");
            }
        }

        private List<ComboDto> brands;
        public List<ComboDto> Brands
        {
            get
            {
                return brands;
            }
            set
            {
                brands = value;
                if (SelectedBrand != null)
                {
                    Models = new ModelTable().GetModelNames(SelectedBrand.ID);
                    if (Models.Count > 0)
                    {
                        SelectedModel = Models[0];
                    }
                }
                OnPropertyChanged("Brands");
            }
        }

        private ComboDto selectedBrand;
        public ComboDto SelectedBrand
        {
            get => selectedBrand;
            set
            {
                selectedBrand = value;
                OnPropertyChanged("SelectedBrand");
            }
        }

        private List<ComboDto> models;
        public List<ComboDto> Models
        {
            get => models;
            set
            {
                models = value;
                OnPropertyChanged("Models");
            }
        }
       
        private ComboDto selectedModel;
        public ComboDto SelectedModel
        {
            get => selectedModel;
            set
            {
                selectedModel = value;
                OnPropertyChanged("SelectedModel");
            }
        }
        private DateTime requestDate;
        public DateTime RequestDate
        {
            get { return requestDate; }
            set { requestDate = value; OnPropertyChanged("RequestDate"); }
        }


        private String requestDetails;
        public String RequestDetails
        {
            get { return requestDetails; }
            set { requestDetails = value; OnPropertyChanged("RequestDetails"); }
        }

        private bool isUnderWarranty;
        public bool IsUnderWarranty
        {
            get => isUnderWarranty;
            set
            {
                isUnderWarranty = value;
                OnPropertyChanged("IsUnderWarranty");
            }
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
                OnPropertyChanged("SelectedServiceRequest");            
            }
        }

        private bool makeServiceRequestFieldsReadonly;
        public bool MakeServiceRequestFieldsReadonly
        {
            get => makeServiceRequestFieldsReadonly;
            set
            {
                makeServiceRequestFieldsReadonly = value;
                OnPropertyChanged("MakeServiceRequestFieldsReadonly");
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

        
        public DateTime NextServiceDueDate
        {
            get { return invoiceTable.NextServiceDueDate; }
            set { invoiceTable.NextServiceDueDate = value; OnPropertyChanged("NextServiceDueDate"); }
        }

        public DateTime InvoiceDate
        {
            get { return invoiceTable.InvoiceDate; }
            set { invoiceTable.InvoiceDate = value; OnPropertyChanged("InvoiceDate"); }
        }


        ObservableCollection<InvoiceItemsGridDto> invoiceItems;

        public ObservableCollection<InvoiceItemsGridDto> InvoiceItems
        {
            get { return invoiceItems; }
            set { invoiceItems = value; OnPropertyChanged("InvoiceItems"); }
        }

      

        private ICommand addInvoiceItemCommand;
        public ICommand AddInvoiceItemCommand => addInvoiceItemCommand ?? (addInvoiceItemCommand = new RelayCommand(AddInvoiceItem, CanAddInvoiceItem));
        private void AddInvoiceItem()
        {         
            IWindowViewModel contextViewModel = new AddInvoiceItemViewModel(new EnableInvoiceWindowCommand(InvoiceView), this);
            var contextView = new AddInvoiceItemView(contextViewModel,this);
            new DisableInvoiceWindowCommand(InvoiceView).Execute(null);
            contextView.Show();         
        }

        public Dictionary<int, int> usedStock = new Dictionary<int, int>();

       // public IObservable<InvoiceItemsGridDto> InvoiceItemsList; //will update from different gui
        public void UpdateInvoiceItemsGrid()
        {
            decimal total = 0;
            foreach(InvoiceItemsGridDto i in InvoiceItems)
            {
                total += i.Amount;
            }

            TotalAmount = total;
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
