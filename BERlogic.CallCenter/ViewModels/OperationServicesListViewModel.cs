using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BERlogic.CallCenter.Models;
using BERlogic.CallCenter.Models.Enums;

namespace BERlogic.CallCenter.ViewModels
{
    public class OperationServicesListViewModel
    {
        [DisplayName("OperationServicesListViewModel.Operations.DisplayName")] public ServiceOperations Operations { get; set; } = ServiceOperations.All;
        public IQueryable<ServiceOperationsModel> Services { get; set; }
        [DisplayName("OperationServicesListViewModel.StartSearchDate.DisplayName")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartSearchDate { get; set; } = DateTime.Now.AddDays(-1);
        [DisplayName("OperationServicesListViewModel.EndSearchDate.DisplayName")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndSearchDate { get; set; } = DateTime.Now;
        [DisplayName("OperationServicesListViewModel.PageSizeList.DisplayName")]
        public IList<int> PageSizeList => new List<int> { 5, 10, 15, 20, 50, 100 };

        public int SelectedPageSize { get; set; } = 20;
        public PagingInfo PagingInfo { get; set; }
    }
}
