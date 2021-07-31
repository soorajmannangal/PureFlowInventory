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
    public class NewServiceRequestViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ModelTable modelTable;
        private CustomerTable customerTable;
        ServiceRequestTable serviceRequestTable;
        private int CustomerId;
        public NewServiceRequestViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            modelTable = new ModelTable();
            customerTable = new CustomerTable();
            CustomerId = 0; //to check is a new customer
            serviceRequestTable = new ServiceRequestTable();
            SetDefaults();
        }

        public override void SetDefaults()
        {
            RequestDate = DateTime.Now;
            CustomerMobile = "";
            SetCustomerFieldsDefaults();
            if (Brands.Count > 0)
            {
                SelectedBrand = Brands[0];
            }

            Models = modelTable.GetModelNames(selectedBrand.ID);

            if (Models.Count > 0)
            {
                SelectedModel = Models[0];
            }

            RequestDetails = "";
            IsUnderWarranty = false;

            MakeCustomerFieldsReadonly = true;

            //IsUnderWarranty = true;
        }

        private void SetCustomerFieldsDefaults()
        {
            CustomerName = "";
            CustomerEmail = "";
            CustomerAddress = "";
        }

        private ICommand fetchCommand;
        public ICommand FetchCommand => fetchCommand ?? (fetchCommand = new RelayCommand(FetchCustomerDetails, CanFetchCustomerDetails));
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
           
        }

        private bool CanFetchCustomerDetails()
        {
           // if (String.IsNullOrEmpty(CustomerMobile)) return false;
            return true;
        }

        private bool ValidateCustomerFields()
        {
            if (String.IsNullOrEmpty(CustomerMobile) || String.IsNullOrEmpty(CustomerName)) return false;
            return true;
        }


        private ICommand createRequestCommand;
        public ICommand CreateRequestCommand => createRequestCommand ?? (createRequestCommand = new RelayCommand(CreateRequest, CanCreateRequest));
        private void CreateRequest()
        {
            if (!ValidateCustomerFields()) return;


            if (CustomerId == 0)
            {
                customerTable.InsertAll();
            }

            CustomerId = customerTable.GetIdByPhone();

            serviceRequestTable.BrandID = SelectedBrand.ID;
            serviceRequestTable.ModelID = selectedModel.ID;
            serviceRequestTable.CustomerID = CustomerId;
            serviceRequestTable.Details = RequestDetails;
            serviceRequestTable.IsUnderWarranty = IsUnderWarranty;
            serviceRequestTable.Status = ServiceRequestStatus.RequestPlaced.ToString();
            serviceRequestTable.RequestDate = RequestDate;
            serviceRequestTable.InsertAll();
            SetDefaults();
        }

        private bool CanCreateRequest()
        {
            return true;
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

        private List<ComboDto> brands;
        public List<ComboDto> Brands => brands ?? (brands = new BrandTable().GetBrandNames());

        private ComboDto selectedBrand;
        public ComboDto SelectedBrand
        {
            get => selectedBrand;
            set
            {
                selectedBrand = value;
                Models = modelTable.GetModelNames(selectedBrand.ID);
                if (Models.Count > 0)
                {
                    SelectedModel = Models[0];
                }

              
                OnPropertyChanged("SelectedBrand");
                //modelTable.BrandID = selectedBrand.ID;
              //  SetDefaults();
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
        //public List<ComboDto> Models => models ?? (models = modelTable.GetModelNames());

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


        private String requestDetails;
        public String RequestDetails
        {
            get { return requestDetails; }
            set { requestDetails = value; OnPropertyChanged("RequestDetails"); }
        }


        private DateTime requestDate;
        public DateTime RequestDate 
        { 
            get { return requestDate; } 
            set { requestDate = value; OnPropertyChanged("RequestDate"); } 
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
