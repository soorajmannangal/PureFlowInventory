using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using PureFlow;

namespace PureFlow
{
    public class MainWindowViewModel:ViewModelBase
    {
        private MainWindow mainWindow;
        private ICommand disableMainWindowCommand;
        private ICommand enableMainWindowCommand;

        public MainWindowViewModel(MainWindow mainWindow) : base(null)
        {
            
            this.mainWindow = mainWindow;
            CreateCustomCommands();
        }

        private void CreateCustomCommands()
        {
            disableMainWindowCommand = new DisableMainWindowCommand(mainWindow);
            enableMainWindowCommand = new EnableMainWindowCommand(mainWindow);
        }

        private ICommand showAddNewBrandWindowCommand;      
        public ICommand ShowAddNewBrandWindowCommand => showAddNewBrandWindowCommand ?? (showAddNewBrandWindowCommand = new RelayCommand(ShowAddNewBrandWindow, CanShowAddNewBrandWindow));
        private bool CanShowAddNewBrandWindow() => true;  
        private void ShowAddNewBrandWindow()
        {
            IWindowViewModel contextViewModel = new AddBrandViewModel(enableMainWindowCommand);
            var contextView = new AddBrandView(contextViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();
        }

        private ICommand showAddNewModelWindowCommand;
        public ICommand ShowAddNewModelWindowCommand => showAddNewModelWindowCommand ?? (showAddNewModelWindowCommand = new RelayCommand(ShowAddNewModelWindow, CanShowAddNewModelWindow));

        private bool CanShowAddNewModelWindow() => true;     
        private void ShowAddNewModelWindow()
        {
            IWindowViewModel contextViewModel = new AddModelViewModel(enableMainWindowCommand);
            var contextView = new AddModelView(contextViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();
        }

        private ICommand showAddInvoiceWorkTypeViewCommand;
        public ICommand ShowAddInvoiceWorkTypeViewCommand => showAddInvoiceWorkTypeViewCommand ?? (showAddInvoiceWorkTypeViewCommand = new RelayCommand(ShowAddInvoiceWorkTypeView, CanShowAddInvoiceWorkTypeView));

        private bool CanShowAddInvoiceWorkTypeView() => true;
        private void ShowAddInvoiceWorkTypeView()
        {
            IWindowViewModel contextViewModel = new AddInvoiceWorkTypeViewModel(enableMainWindowCommand);
            var contextView = new AddInvoiceWorkTypeView(contextViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();
        }

        private ICommand showAddServiceManViewCommand;
        public ICommand ShowAddServiceManViewCommand => showAddServiceManViewCommand ?? (showAddServiceManViewCommand = new RelayCommand(ShowAddServiceManView, CanShowAddServiceManView));

        private bool CanShowAddServiceManView() => true;
        private void ShowAddServiceManView()
        {
            IWindowViewModel contextViewModel = new AddServiceManViewModel(enableMainWindowCommand);
            var contextView = new AddServiceManView(contextViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();
        }

        private ICommand showAddSpareInventoryViewCommand;
        public ICommand ShowAddSpareInventoryViewCommand => showAddSpareInventoryViewCommand ?? (showAddSpareInventoryViewCommand = new RelayCommand(ShowAddSpareInventoryView, CanShowAddSpareInventoryView));

        private bool CanShowAddSpareInventoryView() => true;
        private void ShowAddSpareInventoryView()
        {
            IWindowViewModel contextViewModel = new AddSpareInventoryViewModel(enableMainWindowCommand);
            var contextView = new AddSpareInventoryView(contextViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();
        }


        private ICommand showInventoryReportViewCommand;
        public ICommand ShowInventoryReportViewCommand => showInventoryReportViewCommand ?? 
            (showInventoryReportViewCommand = new RelayCommand(ShowInventoryReportView, CanShowInventoryReportView));

        private bool CanShowInventoryReportView() => true;
        private void ShowInventoryReportView()
        {
            IWindowViewModel contextViewModel = new InventoryReportViewModel(enableMainWindowCommand);
            var contextView = new InventoryReportView(contextViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();
        }

        private ICommand showInventoryTransactionReportViewCommand;
        public ICommand ShowInventoryTransactionReportViewCommand => showInventoryTransactionReportViewCommand 
            ?? (showInventoryTransactionReportViewCommand = new RelayCommand(ShowInventoryTransactionReportView, CanShowInventoryTransactionReportView));

        private bool CanShowInventoryTransactionReportView() => true;
        private void ShowInventoryTransactionReportView()
        {
            IWindowViewModel contextViewModel = new InventoryTransactionReportViewModel(enableMainWindowCommand);
            var contextView = new InventoryTransactionReportView(contextViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();
        }

        private ICommand showInvoiceReportViewCommand;
        public ICommand ShowInvoiceReportViewCommand => showInvoiceReportViewCommand ??
            (showInvoiceReportViewCommand = new RelayCommand(ShowInvoiceReportView, CanShowInvoiceReportView));

        private bool CanShowInvoiceReportView() => true;
        private void ShowInvoiceReportView()
        {
            IWindowViewModel contextViewModel = new InvoiceReportViewModel(enableMainWindowCommand);
            var contextView = new InvoiceReportView(contextViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();
        }

        private ICommand showNewInvoiceViewCommand;
        public ICommand ShowNewInvoiceViewCommand => showNewInvoiceViewCommand
            ?? (showNewInvoiceViewCommand = new RelayCommand(ShowNewInvoiceView, CanShowNewInvoiceView));

        private bool CanShowNewInvoiceView() => true;
        private void ShowNewInvoiceView()
        {
            NewInvoiceViewModel newInvoiceViewModel = new NewInvoiceViewModel(enableMainWindowCommand);
            var contextView = new NewInvoiceView(newInvoiceViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();
        }

        private ICommand showNewServiceRequestViewCommand;
        public ICommand ShowNewServiceRequestViewCommand => showNewServiceRequestViewCommand 
            ?? (showNewServiceRequestViewCommand = new RelayCommand(ShowAddNewServiceRequestView, CanShowNewServiceRequestView));

        private bool CanShowNewServiceRequestView() => true;
        private void ShowAddNewServiceRequestView()
        {
            IWindowViewModel contextViewModel = new NewServiceRequestViewModel(enableMainWindowCommand);
            var contextView = new NewServiceRequestView(contextViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();
        }

        private ICommand showServiceRequestReportViewCommand;
        public ICommand ShowServiceRequestReportViewCommand => showServiceRequestReportViewCommand
            ?? (showServiceRequestReportViewCommand = new RelayCommand(ShowServiceRequestReportView, CanShowServiceRequestReportView));

        private bool CanShowServiceRequestReportView() => true;
        private void ShowServiceRequestReportView()
        {
            IWindowViewModel contextViewModel = new ServiceRequestReportViewModel(enableMainWindowCommand);
            var contextView = new ServiceRequestReportView(contextViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();
        }

        private ICommand showUpdateSpareInventoryViewCommand;
        public ICommand ShowUpdateSpareInventoryViewCommand => showUpdateSpareInventoryViewCommand 
            ?? (showUpdateSpareInventoryViewCommand = new RelayCommand(ShowUpdateSpareInventoryView, CanShowUpdateSpareInventoryView));

        private bool CanShowUpdateSpareInventoryView() => true;
        private void ShowUpdateSpareInventoryView()
        {
            IWindowViewModel contextViewModel = new UpdateSpareInventoryViewModel(enableMainWindowCommand);
            var contextView = new UpdateSpareInventoryView(contextViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();
        }

        private ICommand showCustomerReportViewCommand;
        public ICommand ShowCustomerReportViewCommand => showCustomerReportViewCommand ??
            (showCustomerReportViewCommand = new RelayCommand(ShowCustomerReportView, CanShowCustomerReportView));

        private bool CanShowCustomerReportView() => true;
        private void ShowCustomerReportView()
        {
            IWindowViewModel contextViewModel = new CustomerReportViewModel(enableMainWindowCommand);
            var contextView = new CustomerReportView(contextViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();
        }

        public override void SetDefaults()
        {
            throw new NotImplementedException();
        }


        private DateTime fromDate;
        public DateTime FromDate
        {
            get { return fromDate; }
            set { fromDate = value; OnPropertyChanged("FromDate"); }
        }

        private DateTime toDate;
        public DateTime ToDate
        {
            get { return toDate; }
            set { toDate = value; OnPropertyChanged("ToDate"); }
        }

        public List<BrandGridDto> Grid
        {
            get => new BrandTable().Grid;
            set => OnPropertyChanged("Grid");
        }

    }
}
