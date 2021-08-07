﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace PureFlow
{
    public class InvoiceReportViewModel : ViewModelBase
    {

        private readonly InvoiceTable invoiceTable;
         public InvoiceReportViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            invoiceTable = new InvoiceTable();
        }

        public List<InvoiceGridDto> Grid
        {
            get => invoiceTable.Grid;
            set => OnPropertyChanged("Grid");
        }


        public override void SetDefaults()
        {
            throw new NotImplementedException();
        }

       
    }
}
