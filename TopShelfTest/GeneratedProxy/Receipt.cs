﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace TopShelfTest
{

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Receipt", Namespace = "http://schemas.datacontract.org/2004/07/WCFService")]
    public class Receipt : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string[] ArticlesField;

        private decimal DiscountField;

        private System.Guid IdField;

        private string NumberField;

        private decimal SummField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] Articles
        {
            get
            {
                return this.ArticlesField;
            }
            set
            {
                this.ArticlesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Discount
        {
            get
            {
                return this.DiscountField;
            }
            set
            {
                this.DiscountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Number
        {
            get
            {
                return this.NumberField;
            }
            set
            {
                this.NumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Summ
        {
            get
            {
                return this.SummField;
            }
            set
            {
                this.SummField = value;
            }
        }
    }

}