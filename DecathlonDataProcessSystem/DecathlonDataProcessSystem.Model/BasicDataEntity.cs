using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecathlonDataProcessSystem.Model
{
    /// <summary>
    /// T_BasicData:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class BasicDataEntity
    {
        public BasicDataEntity( )
        { }
        #region Model
        private int _modelcodeid;
        private string _modelcode;
        private string _hscodeincat;
        //private string _modelcodedescription;
        private string _englishproductname;
        private string _localproductname;
        private string _quantityunit;
        private string _sentialfactor;
        private string _localcomposition;
        private string _supervisioncondition;
        private string _doubleorset;
        private string _description;
        private string _examiningreport;
        private string _size;
        /// <summary>
        /// 
        /// </summary>
        public int ModelCodeID
        {
            set { _modelcodeid=value; }
            get { return _modelcodeid; }
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
        public string HSCodeInCat
        {
            set { _hscodeincat=value; }
            get { return _hscodeincat; }
        }
        /// <summary>
        /// 
        /// </summary>
        //public string ModelCodeDescription
        //{
        //    set { _modelcodedescription=value; }
        //    get { return _modelcodedescription; }
        //}
        /// <summary>
        /// 
        /// </summary>
        public string EnglishProductName
        {
            set { _englishproductname=value; }
            get { return _englishproductname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LocalProductName
        {
            set { _localproductname=value; }
            get { return _localproductname; }
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
        public string SentialFactor
        {
            set { _sentialfactor=value; }
            get { return _sentialfactor; }
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
        public string SupervisionCondition
        {
            set { _supervisioncondition=value; }
            get { return _supervisioncondition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DoubleOrSet
        {
            set { _doubleorset=value; }
            get { return _doubleorset; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description=value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExaminingReport
        {
            set { _examiningreport=value; }
            get { return _examiningreport; }
        }
        public string Size
        {
            set { _size=value; }
            get { return _size; }
        }
        #endregion Model

    }
}
