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
        public NewServiceRequestViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            modelTable = new ModelTable();
            customerTable = new CustomerTable();
            SetDefaults();
        }

        public override void SetDefaults()
        {
            RequestDate = DateTime.Now;
            CustomerName = " ";
            CustomerMobile = " ";
            CustomerEmail = " ";
            CustomerAddress = " ";
            if (Brands.Count > 0)
            {
                SelectedBrand = Brands[0];
            }

            Models = modelTable.GetModelNames(selectedBrand.ID);

            if (Models.Count > 0)
            {
                SelectedModel = Models[0];
            }

        
            //IsUnderWarranty = true;
        }

        private ICommand fetchCommand;
        public ICommand FetchCommand => fetchCommand ?? (fetchCommand = new RelayCommand(FetchCustomerDetails, CanFetchCustomerDetails));
        private void FetchCustomerDetails()
        {
            CustomerGridDto customerGridDto = customerTable.GetCustomerByPhone(CustomerMobile);
            if (customerGridDto == null)
            {
                //Enable customer textboxes
                return;
            }

            //Disable Customer details text boxes
            CustomerEmail = customerGridDto.Email;
            CustomerAddress = customerGridDto.Address;
            CustomerName = customerGridDto.Name;
        }

        private bool CanFetchCustomerDetails()
        {
            if (String.IsNullOrEmpty(CustomerMobile)) return false;
            return true;
        }


        private ICommand createRequestCommand;
        public ICommand CreateRequestCommand => createRequestCommand ?? (createRequestCommand = new RelayCommand(CreateRequest, CanCreateRequest));
        private void CreateRequest()
        {
            customerTable.InsertAll();
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
