using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace PureFlow
{
    public class NewServiceRequestViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ModelTable modelTable;
        public NewServiceRequestViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            modelTable = new ModelTable();
            SetDefaults();
        }

        public override void SetDefaults()
        {
            RequestDate = DateTime.Now;
            //CustomerName = "Sooraj";
            //CustomerMobile = " 88";
            //CustomerEmail = "email";
            //CustomerAddress = "NA";
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

        private String customerName;
        public String CustomerName
        {
            get => customerName;
            set { customerName = value; OnPropertyChanged("CustomerName"); }
        }

        private String customerMobile;
        public String CustomerMobile
        {
            get => customerMobile;
            set { customerMobile = value; OnPropertyChanged("CustomerMobile"); }
        }

        private String customerEmail;
        public String CustomerEmail
        {
            get => customerEmail;
            set { customerEmail = value; OnPropertyChanged("CustomerEmail"); }
        }

        private String customerAddress;
        public String CustomerAddress
        {
            get => customerAddress;
            set { customerAddress = value; OnPropertyChanged("CustomerAddress"); }
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
