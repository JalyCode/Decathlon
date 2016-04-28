using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecathlonDataProcessSystem.DBUtility;
using System.Data.SqlClient;
using System.Data;
using DecathlonDataProcessSystem.Model;

namespace DecathlonDataProcessSystem.DAL
{
    /// <summary>
    /// 数据访问类:T_OriginalCLP
    /// </summary>
    public partial class OriginalCLPDAL
    {
        public OriginalCLPDAL( )
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId( )
        {
            return SqlHelper.GetMaxID(SqlHelper.LocalSqlServer, "CLPID" , "T_OriginalCLP" );
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists( int CLPID )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select count(1) from T_OriginalCLP" );
            strSql.Append( " where CLPID=@CLPID " );
            SqlParameter[] parameters = {
					new SqlParameter("@CLPID", SqlDbType.Int,4)			};
            parameters[0].Value = CLPID;

            return SqlHelper.Exists(SqlHelper.LocalSqlServer, strSql.ToString( ) , parameters );
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add( OriginalCLPEntity model )
        {
            //StringBuilder strSql=new StringBuilder( );
            //strSql.Append( "insert into T_OriginalCLP(" );
            //strSql.Append( "CLPID,ItemCode,ShippingNumber,OrderNumber,OriginalPONumber,PalletNumber,ParcelNumber,ModelCode,Origin,Quantity,QuantityUnit,DispatchingKey,EnglishComposition,LocalComposition,Size,EnglishDescription,LocalDescription,Brand,TypeOfGoods,Price,Currency,HSCode,TotalValue,Unit,NETWeight,GrossWeight,CommercialInvoiceNO,StoreNO,StoreName,FileName,CreateTime,Creator)" );
            //strSql.Append( " values (" );
            //strSql.Append( "@CLPID,@ItemCode,@ShippingNumber,@OrderNumber,@OriginalPONumber,@PalletNumber,@ParcelNumber,@ModelCode,@Origin,@Quantity,@QuantityUnit,@DispatchingKey,@EnglishComposition,@LocalComposition,@Size,@EnglishDescription,@LocalDescription,@Brand,@TypeOfGoods,@Price,@Currency,@HSCode,@TotalValue,@Unit,@NETWeight,@GrossWeight,@CommercialInvoiceNO,@StoreNO,@StoreName,@FileName,@CreateTime,@Creator)" );
            //SqlParameter[] parameters = {
            //        new SqlParameter("@CLPID", SqlDbType.Int,4),
            //        new SqlParameter("@ItemCode", SqlDbType.VarChar,50),
            //        new SqlParameter("@ShippingNumber", SqlDbType.VarChar,50),
            //        new SqlParameter("@OrderNumber", SqlDbType.VarChar,50),
            //        new SqlParameter("@OriginalPONumber", SqlDbType.VarChar,20),
            //        new SqlParameter("@PalletNumber", SqlDbType.VarChar,50),
            //        new SqlParameter("@ParcelNumber", SqlDbType.VarChar,50),
            //        new SqlParameter("@ModelCode", SqlDbType.VarChar,50),
            //        new SqlParameter("@Origin", SqlDbType.VarChar,20),
            //        new SqlParameter("@Quantity", SqlDbType.VarChar,50),
            //        new SqlParameter("@QuantityUnit", SqlDbType.VarChar,50),
            //        new SqlParameter("@DispatchingKey", SqlDbType.VarChar,50),
            //        new SqlParameter("@EnglishComposition", SqlDbType.VarChar,50),
            //        new SqlParameter("@LocalComposition", SqlDbType.VarChar,50),
            //        new SqlParameter("@Size", SqlDbType.VarChar,50),
            //        new SqlParameter("@EnglishDescription", SqlDbType.VarChar,50),
            //        new SqlParameter("@LocalDescription", SqlDbType.VarChar,50),
            //        new SqlParameter("@Brand", SqlDbType.VarChar,50),
            //        new SqlParameter("@TypeOfGoods", SqlDbType.VarChar,50),
            //        new SqlParameter("@Price", SqlDbType.VarChar,50),
            //        new SqlParameter("@Currency", SqlDbType.VarChar,20),
            //        new SqlParameter("@HSCode", SqlDbType.VarChar,20),
            //        new SqlParameter("@TotalValue", SqlDbType.VarChar,50),
            //        new SqlParameter("@Unit", SqlDbType.VarChar,20),
            //        new SqlParameter("@NETWeight", SqlDbType.VarChar,50),
            //        new SqlParameter("@GrossWeight", SqlDbType.VarChar,50),
            //        new SqlParameter("@CommercialInvoiceNO", SqlDbType.VarChar,20),
            //        new SqlParameter("@StoreNO", SqlDbType.VarChar,20),
            //        new SqlParameter("@StoreName", SqlDbType.VarChar,50),
            //        new SqlParameter("@FileName", SqlDbType.VarChar,50),
            //        new SqlParameter("@CreateTime", SqlDbType.SmallDateTime),
            //        new SqlParameter("@Creator", SqlDbType.VarChar,20)};
            //parameters[0].Value = GetMaxId()+1;
            //parameters[1].Value = model.ItemCode;
            //parameters[2].Value = model.ShippingNumber;
            //parameters[3].Value = model.OrderNumber;
            //parameters[4].Value = model.OriginalPONumber;
            //parameters[5].Value = model.PalletNumber;
            //parameters[6].Value = model.ParcelNumber;
            //parameters[7].Value = model.ModelCode;
            //parameters[8].Value = model.Origin;
            //parameters[9].Value = model.Quantity;
            //parameters[10].Value = model.QuantityUnit;
            //parameters[11].Value = model.DispatchingKey;
            //parameters[12].Value = model.EnglishComposition;
            //parameters[13].Value = model.LocalComposition;
            //parameters[14].Value = model.Size;
            //parameters[15].Value = model.EnglishDescription;
            //parameters[16].Value = model.LocalDescription;
            //parameters[17].Value = model.Brand;
            //parameters[18].Value = model.TypeOfGoods;
            //parameters[19].Value = model.Price;
            //parameters[20].Value = model.Currency;
            //parameters[21].Value = model.HSCode;
            //parameters[22].Value = model.TotalValue;
            //parameters[23].Value = model.Unit;
            //parameters[24].Value = model.NETWeight;
            //parameters[25].Value = model.GrossWeight;
            //parameters[26].Value = model.CommercialInvoiceNO;
            //parameters[27].Value = model.StoreNO;
            //parameters[28].Value = model.StoreName;
            //parameters[29].Value = model.FileName;
            //parameters[30].Value = model.CreateTime;
            //parameters[31].Value = model.Creator;
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "insert into T_OriginalCLP(" );
            strSql.Append( "ItemCode,ShippingNumber,OrderNumber,OriginalPONumber,PalletNumber,ParcelNumber,ModelCode,Origin,Quantity,QuantityUnit,DispatchingKey,EnglishComposition,LocalComposition,Size,EnglishDescription,LocalDescription,Brand,TypeOfGoods,Price,Currency,HSCode,TotalValue,Unit,NETWeight,GrossWeight,CommercialInvoiceNO,StoreNO,StoreName,FileName,CreateTime,Creator)" );
            strSql.Append( " values (" );
            strSql.Append( "@ItemCode,@ShippingNumber,@OrderNumber,@OriginalPONumber,@PalletNumber,@ParcelNumber,@ModelCode,@Origin,@Quantity,@QuantityUnit,@DispatchingKey,@EnglishComposition,@LocalComposition,@Size,@EnglishDescription,@LocalDescription,@Brand,@TypeOfGoods,@Price,@Currency,@HSCode,@TotalValue,@Unit,@NETWeight,@GrossWeight,@CommercialInvoiceNO,@StoreNO,@StoreName,@FileName,@CreateTime,@Creator)" );
            strSql.Append( ";select @@IDENTITY" );
            SqlParameter[] parameters = {
					new SqlParameter("@ItemCode", SqlDbType.VarChar,50),
					new SqlParameter("@ShippingNumber", SqlDbType.VarChar,50),
					new SqlParameter("@OrderNumber", SqlDbType.VarChar,50),
					new SqlParameter("@OriginalPONumber", SqlDbType.VarChar,20),
					new SqlParameter("@PalletNumber", SqlDbType.VarChar,50),
					new SqlParameter("@ParcelNumber", SqlDbType.VarChar,50),
					new SqlParameter("@ModelCode", SqlDbType.VarChar,50),
					new SqlParameter("@Origin", SqlDbType.VarChar,20),
					new SqlParameter("@Quantity", SqlDbType.VarChar,50),
					new SqlParameter("@QuantityUnit", SqlDbType.VarChar,50),
					new SqlParameter("@DispatchingKey", SqlDbType.VarChar,50),
					new SqlParameter("@EnglishComposition", SqlDbType.VarChar,1024),
					new SqlParameter("@LocalComposition", SqlDbType.VarChar),
					new SqlParameter("@Size", SqlDbType.VarChar,256),
					new SqlParameter("@EnglishDescription", SqlDbType.VarChar,1024),
					new SqlParameter("@LocalDescription", SqlDbType.VarChar,1024),
					new SqlParameter("@Brand", SqlDbType.VarChar,50),
					new SqlParameter("@TypeOfGoods", SqlDbType.VarChar,50),
					new SqlParameter("@Price", SqlDbType.VarChar,50),
					new SqlParameter("@Currency", SqlDbType.VarChar,20),
					new SqlParameter("@HSCode", SqlDbType.VarChar,20),
					new SqlParameter("@TotalValue", SqlDbType.VarChar,50),
					new SqlParameter("@Unit", SqlDbType.VarChar,20),
					new SqlParameter("@NETWeight", SqlDbType.VarChar,50),
					new SqlParameter("@GrossWeight", SqlDbType.VarChar,50),
					new SqlParameter("@CommercialInvoiceNO", SqlDbType.VarChar,20),
					new SqlParameter("@StoreNO", SqlDbType.VarChar,20),
					new SqlParameter("@StoreName", SqlDbType.VarChar,50),
					new SqlParameter("@FileName", SqlDbType.VarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Creator", SqlDbType.VarChar,20)};
            parameters[0].Value = model.ItemCode;
            parameters[1].Value = model.ShippingNumber;
            parameters[2].Value = model.OrderNumber;
            parameters[3].Value = model.OriginalPONumber;
            parameters[4].Value = model.PalletNumber;
            parameters[5].Value = model.ParcelNumber;
            parameters[6].Value = model.ModelCode;
            parameters[7].Value = model.Origin;
            parameters[8].Value = model.Quantity;
            parameters[9].Value = model.QuantityUnit;
            parameters[10].Value = model.DispatchingKey;
            parameters[11].Value = model.EnglishComposition;
            parameters[12].Value = model.LocalComposition;
            parameters[13].Value = model.Size;
            parameters[14].Value = model.EnglishDescription;
            parameters[15].Value = model.LocalDescription;
            parameters[16].Value = model.Brand;
            parameters[17].Value = model.TypeOfGoods;
            parameters[18].Value = model.Price;
            parameters[19].Value = model.Currency;
            parameters[20].Value = model.HSCode;
            parameters[21].Value = model.TotalValue;
            parameters[22].Value = model.Unit;
            parameters[23].Value = model.NETWeight;
            parameters[24].Value = model.GrossWeight;
            parameters[25].Value = model.CommercialInvoiceNO;
            parameters[26].Value = model.StoreNO;
            parameters[27].Value = model.StoreName;
            parameters[28].Value = model.FileName;
            parameters[29].Value = model.CreateTime;
            parameters[30].Value = model.Creator;

            int rows=SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
            if ( rows > 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 将数据批量插入到数据库中。
        /// </summary>
        /// <param name="dataTable">要批量插入的 <see cref="DataTable"/>。</param>
        /// <param name="batchSize">每批次写入的数据量。</param>
        public void BulkOriginalCLPInsert( DataTable dataTable , int batchSize = 10000 )
        {
            if ( dataTable.Rows.Count == 0 )
            {
                return;
            }
            using ( SqlConnection connection = new SqlConnection( SqlHelper.LocalSqlServer ) )
            {
                try
                {
                    connection.Open( );
                    using ( var bulk = new SqlBulkCopy( connection , SqlBulkCopyOptions.KeepIdentity , null )
                    {
                        DestinationTableName = "T_OriginalCLP" ,
                        BatchSize = batchSize
                    } )
                    {
                        bulk.ColumnMappings.Add( "ITEM_CODE" , "ItemCode" );
                        bulk.ColumnMappings.Add( "SHIPPING_NUMBER" , "ShippingNumber" );
                        bulk.ColumnMappings.Add( "ORDER_NUMBER" , "OrderNumber" );
                        bulk.ColumnMappings.Add( "ORIGINAL_PO_NUMBER" , "OriginalPONumber" );
                        bulk.ColumnMappings.Add( "PALLET_NUMBER" , "PalletNumber" );
                        bulk.ColumnMappings.Add( "PARCEL_NUMBER" , "ParcelNumber" );
                        bulk.ColumnMappings.Add( "MODEL_CODE" , "ModelCode" );
                        bulk.ColumnMappings.Add( "ORIGIN" , "Origin" );
                        bulk.ColumnMappings.Add( "QUANTITY" , "Quantity" );
                        bulk.ColumnMappings.Add( "QUANTITY_UNIT" , "QuantityUnit" );
                        bulk.ColumnMappings.Add( "DISPATCHING_KEY" , "DispatchingKey" );
                        bulk.ColumnMappings.Add( "English_Composition" , "EnglishComposition" );
                        bulk.ColumnMappings.Add( "Local_Composition" , "LocalComposition" );
                        bulk.ColumnMappings.Add( "SIZE" , "Size" );
                        bulk.ColumnMappings.Add( "English_Description" , "EnglishDescription" );
                        bulk.ColumnMappings.Add( "Local_Description" , "LocalDescription" );
                        bulk.ColumnMappings.Add( "BRAND" , "Brand" );
                        bulk.ColumnMappings.Add( "TYPE_OF_GOODS" , "TypeOfGoods" );
                        bulk.ColumnMappings.Add( "PRICE" , "Price" );
                        bulk.ColumnMappings.Add( "CURRENCY" , "Currency" );
                        bulk.ColumnMappings.Add( "HS_CODE" , "HSCode" );
                        bulk.ColumnMappings.Add( "UNIT" , "Unit" );
                        bulk.ColumnMappings.Add( "TOTAL_VALUE" , "TotalValue" );
                        bulk.ColumnMappings.Add( "NET_WEIGHT" , "NETWeight" );
                        bulk.ColumnMappings.Add( "GROSS_WEIGHT" , "GrossWeight" );
                        bulk.ColumnMappings.Add( "COMMERCIAL_INVOICE_NO" , "CommercialInvoiceNO" );
                        bulk.ColumnMappings.Add( "STORE_NO" , "StoreNO" );
                        bulk.ColumnMappings.Add( "STORE_NAME" , "StoreName" );
                        bulk.ColumnMappings.Add( "FileName" , "FileName" );
                        bulk.ColumnMappings.Add( "CreateTime" , "CreateTime" );
                        bulk.ColumnMappings.Add( "Creator" , "Creator" );
                        bulk.WriteToServer( dataTable );
                        bulk.Close( );
                    }
                    dataTable.Dispose( );
                }
                catch ( Exception exp )
                {
                    throw new Exception( exp.Message );
                }
                finally
                {
                    connection.Close( );

                }
            }
        }

        public DataTable GetDiffrenceOriginalCLPRecord( string filename )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select ItemCode,ModelCode,HSCode" );
            strSql.Append( " FROM T_OriginalCLP where FileName='"+filename+"' AND ModelCode NOT IN (" );
            strSql.Append( " select distinct modelcode from T_BasicData) ");
            return SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( )).Tables[0];
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update( OriginalCLPEntity model )
        {
            //StringBuilder strSql=new StringBuilder( );
            //strSql.Append( "update T_OriginalCLP set " );
            //strSql.Append( "ItemCode=@ItemCode," );
            //strSql.Append( "ShippingNumber=@ShippingNumber," );
            //strSql.Append( "OrderNumber=@OrderNumber," );
            //strSql.Append( "OriginalPONumber=@OriginalPONumber," );
            //strSql.Append( "PalletNumber=@PalletNumber," );
            //strSql.Append( "ParcelNumber=@ParcelNumber," );
            //strSql.Append( "ModelCode=@ModelCode," );
            //strSql.Append( "Origin=@Origin," );
            //strSql.Append( "Quantity=@Quantity," );
            //strSql.Append( "QuantityUnit=@QuantityUnit," );
            //strSql.Append( "DispatchingKey=@DispatchingKey," );
            //strSql.Append( "EnglishComposition=@EnglishComposition," );
            //strSql.Append( "LocalComposition=@LocalComposition," );
            //strSql.Append( "Size=@Size," );
            //strSql.Append( "EnglishDescription=@EnglishDescription," );
            //strSql.Append( "LocalDescription=@LocalDescription," );
            //strSql.Append( "Brand=@Brand," );
            //strSql.Append( "TypeOfGoods=@TypeOfGoods," );
            //strSql.Append( "Price=@Price," );
            //strSql.Append( "Currency=@Currency," );
            //strSql.Append( "HSCode=@HSCode," );
            //strSql.Append( "TotalValue=@TotalValue," );
            //strSql.Append( "Unit=@Unit," );
            //strSql.Append( "NETWeight=@NETWeight," );
            //strSql.Append( "GrossWeight=@GrossWeight," );
            //strSql.Append( "CommercialInvoiceNO=@CommercialInvoiceNO," );
            //strSql.Append( "StoreNO=@StoreNO," );
            //strSql.Append( "StoreName=@StoreName," );
            //strSql.Append( "FileName=@FileName," );
            //strSql.Append( "CreateTime=@CreateTime," );
            //strSql.Append( "Creator=@Creator" );
            //strSql.Append( " where CLPID=@CLPID " );
            //SqlParameter[] parameters = {
            //        new SqlParameter("@ItemCode", SqlDbType.VarChar,50),
            //        new SqlParameter("@ShippingNumber", SqlDbType.VarChar,50),
            //        new SqlParameter("@OrderNumber", SqlDbType.VarChar,50),
            //        new SqlParameter("@OriginalPONumber", SqlDbType.VarChar,20),
            //        new SqlParameter("@PalletNumber", SqlDbType.VarChar,50),
            //        new SqlParameter("@ParcelNumber", SqlDbType.VarChar,50),
            //        new SqlParameter("@ModelCode", SqlDbType.VarChar,50),
            //        new SqlParameter("@Origin", SqlDbType.VarChar,20),
            //        new SqlParameter("@Quantity", SqlDbType.VarChar,50),
            //        new SqlParameter("@QuantityUnit", SqlDbType.VarChar,50),
            //        new SqlParameter("@DispatchingKey", SqlDbType.VarChar,50),
            //        new SqlParameter("@EnglishComposition", SqlDbType.VarChar,50),
            //        new SqlParameter("@LocalComposition", SqlDbType.VarChar,50),
            //        new SqlParameter("@Size", SqlDbType.VarChar,50),
            //        new SqlParameter("@EnglishDescription", SqlDbType.VarChar,50),
            //        new SqlParameter("@LocalDescription", SqlDbType.VarChar,50),
            //        new SqlParameter("@Brand", SqlDbType.VarChar,50),
            //        new SqlParameter("@TypeOfGoods", SqlDbType.VarChar,50),
            //        new SqlParameter("@Price", SqlDbType.VarChar,50),
            //        new SqlParameter("@Currency", SqlDbType.VarChar,20),
            //        new SqlParameter("@HSCode", SqlDbType.VarChar,20),
            //        new SqlParameter("@TotalValue", SqlDbType.VarChar,50),
            //        new SqlParameter("@Unit", SqlDbType.VarChar,20),
            //        new SqlParameter("@NETWeight", SqlDbType.VarChar,50),
            //        new SqlParameter("@GrossWeight", SqlDbType.VarChar,50),
            //        new SqlParameter("@CommercialInvoiceNO", SqlDbType.VarChar,20),
            //        new SqlParameter("@StoreNO", SqlDbType.VarChar,20),
            //        new SqlParameter("@StoreName", SqlDbType.VarChar,50),
            //        new SqlParameter("@FileName", SqlDbType.VarChar,50),
            //        new SqlParameter("@CreateTime", SqlDbType.SmallDateTime),
            //        new SqlParameter("@Creator", SqlDbType.VarChar,20),
            //        new SqlParameter("@CLPID", SqlDbType.Int,4)};
            //parameters[0].Value = model.ItemCode;
            //parameters[1].Value = model.ShippingNumber;
            //parameters[2].Value = model.OrderNumber;
            //parameters[3].Value = model.OriginalPONumber;
            //parameters[4].Value = model.PalletNumber;
            //parameters[5].Value = model.ParcelNumber;
            //parameters[6].Value = model.ModelCode;
            //parameters[7].Value = model.Origin;
            //parameters[8].Value = model.Quantity;
            //parameters[9].Value = model.QuantityUnit;
            //parameters[10].Value = model.DispatchingKey;
            //parameters[11].Value = model.EnglishComposition;
            //parameters[12].Value = model.LocalComposition;
            //parameters[13].Value = model.Size;
            //parameters[14].Value = model.EnglishDescription;
            //parameters[15].Value = model.LocalDescription;
            //parameters[16].Value = model.Brand;
            //parameters[17].Value = model.TypeOfGoods;
            //parameters[18].Value = model.Price;
            //parameters[19].Value = model.Currency;
            //parameters[20].Value = model.HSCode;
            //parameters[21].Value = model.TotalValue;
            //parameters[22].Value = model.Unit;
            //parameters[23].Value = model.NETWeight;
            //parameters[24].Value = model.GrossWeight;
            //parameters[25].Value = model.CommercialInvoiceNO;
            //parameters[26].Value = model.StoreNO;
            //parameters[27].Value = model.StoreName;
            //parameters[28].Value = model.FileName;
            //parameters[29].Value = model.CreateTime;
            //parameters[30].Value = model.Creator;
            //parameters[31].Value = model.CLPID;
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "update T_OriginalCLP set " );
            strSql.Append( "ItemCode=@ItemCode," );
            strSql.Append( "ShippingNumber=@ShippingNumber," );
            strSql.Append( "OrderNumber=@OrderNumber," );
            strSql.Append( "OriginalPONumber=@OriginalPONumber," );
            strSql.Append( "PalletNumber=@PalletNumber," );
            strSql.Append( "ParcelNumber=@ParcelNumber," );
            strSql.Append( "ModelCode=@ModelCode," );
            strSql.Append( "Origin=@Origin," );
            strSql.Append( "Quantity=@Quantity," );
            strSql.Append( "QuantityUnit=@QuantityUnit," );
            strSql.Append( "DispatchingKey=@DispatchingKey," );
            strSql.Append( "EnglishComposition=@EnglishComposition," );
            strSql.Append( "LocalComposition=@LocalComposition," );
            strSql.Append( "Size=@Size," );
            strSql.Append( "EnglishDescription=@EnglishDescription," );
            strSql.Append( "LocalDescription=@LocalDescription," );
            strSql.Append( "Brand=@Brand," );
            strSql.Append( "TypeOfGoods=@TypeOfGoods," );
            strSql.Append( "Price=@Price," );
            strSql.Append( "Currency=@Currency," );
            strSql.Append( "HSCode=@HSCode," );
            strSql.Append( "TotalValue=@TotalValue," );
            strSql.Append( "Unit=@Unit," );
            strSql.Append( "NETWeight=@NETWeight," );
            strSql.Append( "GrossWeight=@GrossWeight," );
            strSql.Append( "CommercialInvoiceNO=@CommercialInvoiceNO," );
            strSql.Append( "StoreNO=@StoreNO," );
            strSql.Append( "StoreName=@StoreName," );
            strSql.Append( "FileName=@FileName," );
            strSql.Append( "CreateTime=@CreateTime," );
            strSql.Append( "Creator=@Creator" );
            strSql.Append( " where CLPID=@CLPID" );
            SqlParameter[] parameters = {
					new SqlParameter("@ItemCode", SqlDbType.VarChar,50),
					new SqlParameter("@ShippingNumber", SqlDbType.VarChar,50),
					new SqlParameter("@OrderNumber", SqlDbType.VarChar,50),
					new SqlParameter("@OriginalPONumber", SqlDbType.VarChar,20),
					new SqlParameter("@PalletNumber", SqlDbType.VarChar,50),
					new SqlParameter("@ParcelNumber", SqlDbType.VarChar,50),
					new SqlParameter("@ModelCode", SqlDbType.VarChar,50),
					new SqlParameter("@Origin", SqlDbType.VarChar,20),
					new SqlParameter("@Quantity", SqlDbType.VarChar,50),
					new SqlParameter("@QuantityUnit", SqlDbType.VarChar,50),
					new SqlParameter("@DispatchingKey", SqlDbType.VarChar,50),
					new SqlParameter("@EnglishComposition", SqlDbType.VarChar,1024),
					new SqlParameter("@LocalComposition", SqlDbType.VarChar),
					new SqlParameter("@Size", SqlDbType.VarChar,256),
					new SqlParameter("@EnglishDescription", SqlDbType.VarChar,1024),
					new SqlParameter("@LocalDescription", SqlDbType.VarChar,1024),
					new SqlParameter("@Brand", SqlDbType.VarChar,50),
					new SqlParameter("@TypeOfGoods", SqlDbType.VarChar,50),
					new SqlParameter("@Price", SqlDbType.VarChar,50),
					new SqlParameter("@Currency", SqlDbType.VarChar,20),
					new SqlParameter("@HSCode", SqlDbType.VarChar,20),
					new SqlParameter("@TotalValue", SqlDbType.VarChar,50),
					new SqlParameter("@Unit", SqlDbType.VarChar,20),
					new SqlParameter("@NETWeight", SqlDbType.VarChar,50),
					new SqlParameter("@GrossWeight", SqlDbType.VarChar,50),
					new SqlParameter("@CommercialInvoiceNO", SqlDbType.VarChar,20),
					new SqlParameter("@StoreNO", SqlDbType.VarChar,20),
					new SqlParameter("@StoreName", SqlDbType.VarChar,50),
					new SqlParameter("@FileName", SqlDbType.VarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Creator", SqlDbType.VarChar,20),
					new SqlParameter("@CLPID", SqlDbType.Int,4)};
            parameters[0].Value = model.ItemCode;
            parameters[1].Value = model.ShippingNumber;
            parameters[2].Value = model.OrderNumber;
            parameters[3].Value = model.OriginalPONumber;
            parameters[4].Value = model.PalletNumber;
            parameters[5].Value = model.ParcelNumber;
            parameters[6].Value = model.ModelCode;
            parameters[7].Value = model.Origin;
            parameters[8].Value = model.Quantity;
            parameters[9].Value = model.QuantityUnit;
            parameters[10].Value = model.DispatchingKey;
            parameters[11].Value = model.EnglishComposition;
            parameters[12].Value = model.LocalComposition;
            parameters[13].Value = model.Size;
            parameters[14].Value = model.EnglishDescription;
            parameters[15].Value = model.LocalDescription;
            parameters[16].Value = model.Brand;
            parameters[17].Value = model.TypeOfGoods;
            parameters[18].Value = model.Price;
            parameters[19].Value = model.Currency;
            parameters[20].Value = model.HSCode;
            parameters[21].Value = model.TotalValue;
            parameters[22].Value = model.Unit;
            parameters[23].Value = model.NETWeight;
            parameters[24].Value = model.GrossWeight;
            parameters[25].Value = model.CommercialInvoiceNO;
            parameters[26].Value = model.StoreNO;
            parameters[27].Value = model.StoreName;
            parameters[28].Value = model.FileName;
            parameters[29].Value = model.CreateTime;
            parameters[30].Value = model.Creator;
            parameters[31].Value = model.CLPID;

            int rows=SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
            if ( rows > 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete( int CLPID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_OriginalCLP " );
            strSql.Append( " where CLPID=@CLPID " );
            SqlParameter[] parameters = {
					new SqlParameter("@CLPID", SqlDbType.Int,4)			};
            parameters[0].Value = CLPID;

            int rows=SqlHelper.ExecuteSql(SqlHelper.LocalSqlServer, strSql.ToString( ) , parameters );
            if ( rows > 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DeleteByFileName( string filename )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_OriginalCLP " );
            strSql.Append( " where FileName=@FileName " );
            SqlParameter[] parameters = {
					new SqlParameter("@FileName", SqlDbType.VarChar,50)			};
            parameters[0].Value = filename;

            SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList( string CLPIDlist )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_OriginalCLP " );
            strSql.Append( " where CLPID in ("+CLPIDlist + ")  " );
            int rows=SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ) );
            if ( rows > 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OriginalCLPEntity GetModel( int CLPID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select  top 1 CLPID,ItemCode,ShippingNumber,OrderNumber,OriginalPONumber,PalletNumber,ParcelNumber,ModelCode,Origin,Quantity,QuantityUnit,DispatchingKey,EnglishComposition,LocalComposition,Size,EnglishDescription,LocalDescription,Brand,TypeOfGoods,Price,Currency,HSCode,TotalValue,Unit,NETWeight,GrossWeight,CommercialInvoiceNO,StoreNO,StoreName,FileName,CreateTime,Creator from T_OriginalCLP " );
            strSql.Append( " where CLPID=@CLPID " );
            SqlParameter[] parameters = {
					new SqlParameter("@CLPID", SqlDbType.Int,4)			};
            parameters[0].Value = CLPID;

            OriginalCLPEntity model=new OriginalCLPEntity( );
            DataSet ds=SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
            if ( ds.Tables[0].Rows.Count>0 )
            {
                if ( ds.Tables[0].Rows[0]["CLPID"]!=null && ds.Tables[0].Rows[0]["CLPID"].ToString( )!="" )
                {
                    model.CLPID=int.Parse( ds.Tables[0].Rows[0]["CLPID"].ToString( ) );
                }
                if ( ds.Tables[0].Rows[0]["ItemCode"]!=null && ds.Tables[0].Rows[0]["ItemCode"].ToString( )!="" )
                {
                    model.ItemCode=ds.Tables[0].Rows[0]["ItemCode"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["ShippingNumber"]!=null && ds.Tables[0].Rows[0]["ShippingNumber"].ToString( )!="" )
                {
                    model.ShippingNumber=ds.Tables[0].Rows[0]["ShippingNumber"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["OrderNumber"]!=null && ds.Tables[0].Rows[0]["OrderNumber"].ToString( )!="" )
                {
                    model.OrderNumber=ds.Tables[0].Rows[0]["OrderNumber"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["OriginalPONumber"]!=null && ds.Tables[0].Rows[0]["OriginalPONumber"].ToString( )!="" )
                {
                    model.OriginalPONumber=ds.Tables[0].Rows[0]["OriginalPONumber"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["PalletNumber"]!=null && ds.Tables[0].Rows[0]["PalletNumber"].ToString( )!="" )
                {
                    model.PalletNumber=ds.Tables[0].Rows[0]["PalletNumber"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["ParcelNumber"]!=null && ds.Tables[0].Rows[0]["ParcelNumber"].ToString( )!="" )
                {
                    model.ParcelNumber=ds.Tables[0].Rows[0]["ParcelNumber"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["ModelCode"]!=null && ds.Tables[0].Rows[0]["ModelCode"].ToString( )!="" )
                {
                    model.ModelCode=ds.Tables[0].Rows[0]["ModelCode"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["Origin"]!=null && ds.Tables[0].Rows[0]["Origin"].ToString( )!="" )
                {
                    model.Origin=ds.Tables[0].Rows[0]["Origin"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["Quantity"]!=null && ds.Tables[0].Rows[0]["Quantity"].ToString( )!="" )
                {
                    model.Quantity=ds.Tables[0].Rows[0]["Quantity"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["QuantityUnit"]!=null && ds.Tables[0].Rows[0]["QuantityUnit"].ToString( )!="" )
                {
                    model.QuantityUnit=ds.Tables[0].Rows[0]["QuantityUnit"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["DispatchingKey"]!=null && ds.Tables[0].Rows[0]["DispatchingKey"].ToString( )!="" )
                {
                    model.DispatchingKey=ds.Tables[0].Rows[0]["DispatchingKey"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["EnglishComposition"]!=null && ds.Tables[0].Rows[0]["EnglishComposition"].ToString( )!="" )
                {
                    model.EnglishComposition=ds.Tables[0].Rows[0]["EnglishComposition"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["LocalComposition"]!=null && ds.Tables[0].Rows[0]["LocalComposition"].ToString( )!="" )
                {
                    model.LocalComposition=ds.Tables[0].Rows[0]["LocalComposition"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["Size"]!=null && ds.Tables[0].Rows[0]["Size"].ToString( )!="" )
                {
                    model.Size=ds.Tables[0].Rows[0]["Size"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["EnglishDescription"]!=null && ds.Tables[0].Rows[0]["EnglishDescription"].ToString( )!="" )
                {
                    model.EnglishDescription=ds.Tables[0].Rows[0]["EnglishDescription"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["LocalDescription"]!=null && ds.Tables[0].Rows[0]["LocalDescription"].ToString( )!="" )
                {
                    model.LocalDescription=ds.Tables[0].Rows[0]["LocalDescription"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["Brand"]!=null && ds.Tables[0].Rows[0]["Brand"].ToString( )!="" )
                {
                    model.Brand=ds.Tables[0].Rows[0]["Brand"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["TypeOfGoods"]!=null && ds.Tables[0].Rows[0]["TypeOfGoods"].ToString( )!="" )
                {
                    model.TypeOfGoods=ds.Tables[0].Rows[0]["TypeOfGoods"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["Price"]!=null && ds.Tables[0].Rows[0]["Price"].ToString( )!="" )
                {
                    model.Price=ds.Tables[0].Rows[0]["Price"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["Currency"]!=null && ds.Tables[0].Rows[0]["Currency"].ToString( )!="" )
                {
                    model.Currency=ds.Tables[0].Rows[0]["Currency"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["HSCode"]!=null && ds.Tables[0].Rows[0]["HSCode"].ToString( )!="" )
                {
                    model.HSCode=ds.Tables[0].Rows[0]["HSCode"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["TotalValue"]!=null && ds.Tables[0].Rows[0]["TotalValue"].ToString( )!="" )
                {
                    model.TotalValue=ds.Tables[0].Rows[0]["TotalValue"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["Unit"]!=null && ds.Tables[0].Rows[0]["Unit"].ToString( )!="" )
                {
                    model.Unit=ds.Tables[0].Rows[0]["Unit"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["NETWeight"]!=null && ds.Tables[0].Rows[0]["NETWeight"].ToString( )!="" )
                {
                    model.NETWeight=ds.Tables[0].Rows[0]["NETWeight"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["GrossWeight"]!=null && ds.Tables[0].Rows[0]["GrossWeight"].ToString( )!="" )
                {
                    model.GrossWeight=ds.Tables[0].Rows[0]["GrossWeight"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["CommercialInvoiceNO"]!=null && ds.Tables[0].Rows[0]["CommercialInvoiceNO"].ToString( )!="" )
                {
                    model.CommercialInvoiceNO=ds.Tables[0].Rows[0]["CommercialInvoiceNO"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["StoreNO"]!=null && ds.Tables[0].Rows[0]["StoreNO"].ToString( )!="" )
                {
                    model.StoreNO=ds.Tables[0].Rows[0]["StoreNO"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["StoreName"]!=null && ds.Tables[0].Rows[0]["StoreName"].ToString( )!="" )
                {
                    model.StoreName=ds.Tables[0].Rows[0]["StoreName"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["FileName"]!=null && ds.Tables[0].Rows[0]["FileName"].ToString( )!="" )
                {
                    model.FileName=ds.Tables[0].Rows[0]["FileName"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["CreateTime"]!=null && ds.Tables[0].Rows[0]["CreateTime"].ToString( )!="" )
                {
                    model.CreateTime=DateTime.Parse( ds.Tables[0].Rows[0]["CreateTime"].ToString( ) );
                }
                if ( ds.Tables[0].Rows[0]["Creator"]!=null && ds.Tables[0].Rows[0]["Creator"].ToString( )!="" )
                {
                    model.Creator=ds.Tables[0].Rows[0]["Creator"].ToString( );
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList( string strWhere )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select CLPID,ItemCode,ShippingNumber,OrderNumber,OriginalPONumber,PalletNumber,ParcelNumber,ModelCode,Origin,Quantity,QuantityUnit,DispatchingKey,EnglishComposition,LocalComposition,Size,EnglishDescription,LocalDescription,Brand,TypeOfGoods,Price,Currency,HSCode,TotalValue,Unit,NETWeight,GrossWeight,CommercialInvoiceNO,StoreNO,StoreName,FileName,CreateTime,Creator " );
            strSql.Append( " FROM T_OriginalCLP " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            return SqlHelper.Query(SqlHelper.LocalSqlServer, strSql.ToString( ) );
        }
        public DataSet GetDeleteColumnCLPList( string filename , string userid )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select ItemCode ITEM_CODE,ShippingNumber SHIPPING_NUMBER,OrderNumber ORDER_NUMBER,PalletNumber PALLET_NUMBER,ParcelNumber PARCEL_NUMBER,ModelCode MODEL_CODE,Origin ORIGIN,Quantity QUANTITY,QuantityUnit QUANTITY_UNIT,DispatchingKey DISPATCHING_KEY,EnglishComposition English_Composition,Size SIZE,Brand BRAND,TypeOfGoods TYPE_OF_GOODS,Price PRICE,Currency CURRENCY,TotalValue TOTAL_VALUE,NETWeight NET_WEIGHT,GrossWeight GROSS_WEIGHT,CommercialInvoiceNO COMMERCIAL_INVOICE_NO,StoreNO STORE_NO,StoreName STORE_NAME" );
            strSql.Append( " FROM T_OriginalCLP where FileName=@FileName and Creator=@Creator Order by CreateTime DESC;" );
            SqlParameter[] parameters = {
					new SqlParameter("@FileName", SqlDbType.VarChar,50),
					new SqlParameter("@Creator", SqlDbType.VarChar,20)};
            parameters[0].Value =filename;
            parameters[1].Value = userid;
            return SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
        }
        public DataSet GetCLPDataUnitBasicDataList( string strWhere )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select CLPID,ItemCode,ShippingNumber,OrderNumber,OriginalPONumber,PalletNumber,ParcelNumber,ModelCode,Origin,Quantity,QuantityUnit,DispatchingKey,EnglishComposition,LocalComposition,Size,EnglishDescription,LocalDescription,Brand,TypeOfGoods,Price,Currency,HSCode,TotalValue,Unit,NETWeight,GrossWeight,CommercialInvoiceNO,StoreNO,StoreName,FileName,CreateTime,Creator " );
            strSql.Append( " FROM T_OriginalCLP " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            return SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList( int Top , string strWhere , string filedOrder )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select " );
            if ( Top>0 )
            {
                strSql.Append( " top "+Top.ToString( ) );
            }
            strSql.Append( " CLPID,ItemCode,ShippingNumber,OrderNumber,OriginalPONumber,PalletNumber,ParcelNumber,ModelCode,Origin,Quantity,QuantityUnit,DispatchingKey,EnglishComposition,LocalComposition,Size,EnglishDescription,LocalDescription,Brand,TypeOfGoods,Price,Currency,HSCode,TotalValue,Unit,NETWeight,GrossWeight,CommercialInvoiceNO,StoreNO,StoreName,FileName,CreateTime,Creator " );
            strSql.Append( " FROM T_OriginalCLP " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            strSql.Append( " order by " + filedOrder );
            return SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount( string strWhere )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select count(1) FROM T_OriginalCLP " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            object obj = SqlHelper.GetSingle(SqlHelper.LocalSqlServer, strSql.ToString( ) );
            if ( obj == null )
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32( obj );
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage( string strWhere , string orderby , int startIndex , int endIndex )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "SELECT * FROM ( " );
            strSql.Append( " SELECT ROW_NUMBER() OVER (" );
            if ( !string.IsNullOrEmpty( orderby.Trim( ) ) )
            {
                strSql.Append( "order by T." + orderby );
            }
            else
            {
                strSql.Append( "order by T.CLPID desc" );
            }
            strSql.Append( ")AS Row, T.*  from T_OriginalCLP T " );
            if ( !string.IsNullOrEmpty( strWhere.Trim( ) ) )
            {
                strSql.Append( " WHERE " + strWhere );
            }
            strSql.Append( " ) TT" );
            strSql.AppendFormat( " WHERE TT.Row between {0} and {1}" , startIndex , endIndex );
            return SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }

        #endregion  Method
    }
}
