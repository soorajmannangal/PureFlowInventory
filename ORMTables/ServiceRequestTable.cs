using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class ServiceRequestTable : ORMBase
    {
        public override eTableNames TableName => eTableNames.ServiceRequestTable;

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

        public void UpdateRequest(int id, string status, DateTime resovedDate)
        {
            _dataSource.UpdateServiceRequest(eGenericColumnName.ID.ToString(), id, nameof(Status), status, nameof(ResovedDate), resovedDate);
        }

        public void CloseRequest(int id)
        {
            UpdateRequest(id, ServiceRequestStatus.RequestResolved.ToString(), DateTime.Now);
        }

        public List<ServiceRequestDto> Grid => _dataSource.GetServiceRequestList(
                                                eGenericColumnName.ID.ToString(),   
                                                nameof(CustomerID),
                                                nameof(Details),
                                                nameof(IsUnderWarranty),
                                                nameof(DateOfEntry),
                                                nameof(Status),
                                                nameof(BrandID),
                                                nameof(ModelID),
                                                nameof(RequestDate),
                                                nameof(ResovedDate),
                                                nameof(RequestDate));
            
      

        public List<ServiceRequestDto> GetServiceRequestListForCustomerId(int customerId)
        {
            return _dataSource.GetServiceRequestListForCustomerId(
                                                eGenericColumnName.ID.ToString(),
                                                nameof(CustomerID),
                                                nameof(Details),
                                                nameof(IsUnderWarranty),
                                                nameof(DateOfEntry),
                                                nameof(Status),
                                                nameof(BrandID),
                                                nameof(ModelID),
                                                nameof(RequestDate),
                                                nameof(ResovedDate),
                                                nameof(RequestDate),
                                                customerId);
        }

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
