using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoRequestModel
{
    public class CompareRatesListRequestModel
    {
        public Search RequestSearch { get; set; }
        public FilterSearch RequestFilterSearch { get; set; }
    }

    public class Search
    {
        public bool IsResidential { get; set; }
        public bool IsCommercial { get; set; }
        public bool IsElectricity { get; set; }
        public bool IsGas { get; set; }
        public string ZipCode { get; set; }
        public string RetailerName_CompareRatesRetailerOnly { get; set; }
    }

    public class FilterSearch
    {
        public int Terms { get; set; }
        public string SupplierName { get; set; }
    }

}
