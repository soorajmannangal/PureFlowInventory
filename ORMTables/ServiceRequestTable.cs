﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class ServiceRequestTable : ORMBase
    {
        public override eTableNames TableName => eTableNames.ServiceRequest;

        private int customerId;
        public int CustomerID { get => customerId; set => customerId = value; }

        private string details;
        public string Details { get => details; set => details = value; }

        private bool isUnderWarranty;
        public bool IsUnderWarranty { get => isUnderWarranty; set => isUnderWarranty = value; }

        private DateTime dateOfEntry;
        public DateTime DateOfEntry { get => dateOfEntry; set => dateOfEntry = value; }

        private string status;
        public string Status { get => status; set => status = value; }

        private int brandID;
        public int BrandID { get => brandID; set => brandID = value; }

        private int modelId;
        public int ModelID { get => modelId; set => modelId = value; }

        private DateTime requestDate;
        public DateTime RequestDate { get => requestDate; set => requestDate = value; }
        private DateTime resolvedDate;
        public DateTime ResovedDate { get => resolvedDate; set => resolvedDate = value; }

        public List<ServiceRequestGridDto> Grid => _dataSource.GetServiceRequestList(
            eGenericColumnName.ID.ToString(),   nameof(CustomerID),
                                                nameof(Details),
                                                nameof(IsUnderWarranty),
                                                nameof(DateOfEntry),
                                                nameof(Status),
                                                nameof(BrandID),
                                                nameof(ModelID),
                                                nameof(RequestDate),
                                                nameof(RequestDate));
            
        //public InventoryTransactionTable(intnameof(ResolvedDate), spareInvId, int qty, int userId)
        //{
        //    transactionDate = DateTime.Now;
        //    this.spareInventoryID = spareInvId;
        //    this.qty = qty;
        //    this.userID = userId;
        //}

        public void InsertAll()
        {
            DateOfEntry = DateTime.Now;
            ResovedDate = DateTime.MinValue;
            _dataSource.Insert(TableName, 
                nameof(CustomerID), CustomerID, 
                nameof(Details), Details, 
                nameof(IsUnderWarranty), IsUnderWarranty, 
                nameof(DateOfEntry), DateOfEntry,
                nameof(Status), Status,
                nameof(BrandID), BrandID,
                nameof(ModelID), ModelID,
                nameof(RequestDate), RequestDate,
                nameof(ResovedDate), ResovedDate);
        }
    }
}
