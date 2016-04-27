using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecathlonDataProcessSystem.Model
{
    /// <summary>
    /// T_OriginalCLP:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class OriginalCLPEntity
    {
        public OriginalCLPEntity( )
        { }
        #region Model
        private int _clpid;
        private string _itemcode;
        private string _shippingnumber;
        private string _ordernumber;
        private string _originalponumber;
        private string _palletnumber;
        private string _parcelnumber;
        private string _modelcode;
        private string _origin;
        private string _quantity;
        private string _quantityunit;
        private string _dispatchingkey;
        private string _englishcomposition;
        private string _localcomposition;
        private string _size;
        private string _englishdescription;
        private string _localdescription;
        private string _brand;
        private string _typeofgoods;
        private string _price;
        private string _currency;
        private string _hscode;
        private string _totalvalue;
        private string _unit;
        private string _netweight;
        private string _grossweight;
        private string _commercialinvoiceno;
        private string _storeno;
        private string _storename;
        private string _filename;
        private DateTime _createtime;
        private string _creator;
        /// <summary>
        /// 
        /// </summary>
        public int CLPID
        {
            set { _clpid=value; }
            get { return _clpid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ItemCode
        {
            set { _itemcode=value; }
            get { return _itemcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShippingNumber
        {
            set { _shippingnumber=value; }
            get { return _shippingnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderNumber
        {
            set { _ordernumber=value; }
            get { return _ordernumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OriginalPONumber
        {
            set { _originalponumber=value; }
            get { return _originalponumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PalletNumber
        {
            set { _palletnumber=value; }
            get { return _palletnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ParcelNumber
        {
            set { _parcelnumber=value; }
            get { return _parcelnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModelCode
        {
            set { _modelcode=value; }
            get { return _modelcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Origin
        {
            set { _origin=value; }
            get { return _origin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Quantity
        {
            set { _quantity=value; }
            get { return _quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QuantityUnit
        {
            set { _quantityunit=value; }
            get { return _quantityunit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DispatchingKey
        {
            set { _dispatchingkey=value; }
            get { return _dispatchingkey; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EnglishComposition
        {
            set { _englishcomposition=value; }
            get { return _englishcomposition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LocalComposition
        {
            set { _localcomposition=value; }
            get { return _localcomposition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Size
        {
            set { _size=value; }
            get { return _size; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EnglishDescription
        {
            set { _englishdescription=value; }
            get { return _englishdescription; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LocalDescription
        {
            set { _localdescription=value; }
            get { return _localdescription; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Brand
        {
            set { _brand=value; }
            get { return _brand; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeOfGoods
        {
            set { _typeofgoods=value; }
            get { return _typeofgoods; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Price
        {
            set { _price=value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Currency
        {
            set { _currency=value; }
            get { return _currency; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HSCode
        {
            set { _hscode=value; }
            get { return _hscode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TotalValue
        {
            set { _totalvalue=value; }
            get { return _totalvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Unit
        {
            set { _unit=value; }
            get { return _unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NETWeight
        {
            set { _netweight=value; }
            get { return _netweight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GrossWeight
        {
            set { _grossweight=value; }
            get { return _grossweight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CommercialInvoiceNO
        {
            set { _commercialinvoiceno=value; }
            get { return _commercialinvoiceno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StoreNO
        {
            set { _storeno=value; }
            get { return _storeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StoreName
        {
            set { _storename=value; }
            get { return _storename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            set { _filename=value; }
            get { return _filename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime=value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Creator
        {
            set { _creator=value; }
            get { return _creator; }
        }
        #endregion Model

    }
}
