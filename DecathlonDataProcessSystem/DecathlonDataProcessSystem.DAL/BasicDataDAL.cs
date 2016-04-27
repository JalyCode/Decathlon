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
    /// 数据访问类:T_BasicData
    /// </summary>
    public class BasicDataDAL
    {
        public BasicDataDAL( )
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId( )
        {
            return SqlHelper.GetMaxID(SqlHelper.LocalSqlServer, "ModelCodeID" , "T_BasicData" );
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists( int ModeCodeID )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select count(1) from T_BasicData" );
            strSql.Append( " where ModelCodeID=@ModelCodeID " );
            SqlParameter[] parameters = {
					new SqlParameter("@ModelCodeID", SqlDbType.Int,4)			};
            parameters[0].Value = ModeCodeID;

            return SqlHelper.Exists(SqlHelper.LocalSqlServer, strSql.ToString( ) , parameters );
        }
        public bool ModelCodeExists( string  ModeCode )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select count(1) from T_BasicData" );
            strSql.Append( " where ModelCode=@ModelCode " );
            SqlParameter[] parameters = {
					new SqlParameter("@ModelCode", SqlDbType.VarChar,50)			};
            parameters[0].Value = ModeCode;

            return SqlHelper.Exists( SqlHelper.LocalSqlServer , strSql.ToString( ) , parameters );
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add( BasicDataEntity model )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "insert into T_BasicData(" );
            strSql.Append( "ModelCodeID,ModelCode,HSCodeInCat,EnglishProductName,LocalProductName,QuantityUnit,SentialFactor,LocalComposition,SupervisionCondition,DoubleOrSet,Description,ExaminingReport,Size)" );
            strSql.Append( " values (" );
            strSql.Append( "@ModelCodeID,@ModelCode,@HSCodeInCat,@EnglishProductName,@LocalProductName,@QuantityUnit,@SentialFactor,@LocalComposition,@SupervisionCondition,@DoubleOrSet,@Description,@ExaminingReport,@Size)" );
            SqlParameter[] parameters = {
					new SqlParameter("@ModelCodeID", SqlDbType.Int,4),
					new SqlParameter("@ModelCode", SqlDbType.VarChar,50),
					new SqlParameter("@HSCodeInCat", SqlDbType.VarChar,50),
					new SqlParameter("@EnglishProductName", SqlDbType.VarChar,255),
					new SqlParameter("@LocalProductName", SqlDbType.VarChar,255),
					new SqlParameter("@QuantityUnit", SqlDbType.VarChar,50),
					new SqlParameter("@SentialFactor", SqlDbType.VarChar,2048),
					new SqlParameter("@LocalComposition", SqlDbType.VarChar,255),
					new SqlParameter("@SupervisionCondition", SqlDbType.VarChar,255),
					new SqlParameter("@DoubleOrSet", SqlDbType.VarChar,255),
                    new SqlParameter("@Description", SqlDbType.VarChar,2048),
					new SqlParameter("@ExaminingReport", SqlDbType.VarChar,255),
                    new SqlParameter("@Size", SqlDbType.VarChar,255)};
            parameters[0].Value = GetMaxId()+1;
            parameters[1].Value = model.ModelCode;
            parameters[2].Value = model.HSCodeInCat;
            //parameters[3].Value = model.ModelCodeDescription;
            parameters[3].Value = model.EnglishProductName;
            parameters[4].Value = model.LocalProductName;
            parameters[5].Value = model.QuantityUnit;
            parameters[6].Value = model.SentialFactor;
            parameters[7].Value = model.LocalComposition;
            parameters[8].Value = model.SupervisionCondition;
            parameters[9].Value = model.DoubleOrSet;
            parameters[10].Value = model.Description;
            parameters[11].Value = model.ExaminingReport;
            parameters[12].Value = model.Size;
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

        /// <summary>
        /// 将数据批量插入到数据库中。
        /// </summary>
        /// <param name="dataTable">要批量插入的 <see cref="DataTable"/>。</param>
        /// <param name="batchSize">每批次写入的数据量。</param>
        public void BulkBasicDataInsert( DataTable dataTable , int batchSize = 10000 )
        {
            if (dataTable.Rows.Count == 0)
            {
                return;
            }
            using (SqlConnection connection = new SqlConnection(SqlHelper.LocalSqlServer))
            {
                try
                {
                    connection.Open( );
                    using (var bulk = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, null)
                        {
                            DestinationTableName = "T_BasicData" , 
                            BatchSize = batchSize
                        })
                    {
                        bulk.ColumnMappings.Add( "Model_code" , "ModelCode" );
                        bulk.ColumnMappings.Add( "法定计量单位" , "QuantityUnit" );
                        bulk.ColumnMappings.Add( "HS_CODE_(IN_CAT)" , "HSCodeInCat" );
                        bulk.ColumnMappings.Add( "中文品名" , "LocalProductName" );
                        bulk.ColumnMappings.Add( "英文品名" , "EnglishProductName" );
                        bulk.ColumnMappings.Add( "申报要素" , "SentialFactor" );
                        bulk.ColumnMappings.Add( "监管条件" , "SupervisionCondition" );
                        bulk.ColumnMappings.Add( "Double_or_Set" , "DoubleOrSet" );
                        bulk.ColumnMappings.Add( "备注" , "Description" );
                        bulk.ColumnMappings.Add( "现检测报告号" , "ExaminingReport" );
                        bulk.ColumnMappings.Add( "SIZE" , "Size" );
                        bulk.WriteToServer(dataTable);
                        bulk.Close();
                    }
                    dataTable.Dispose( );
                }
                catch (Exception exp)
                {
                    throw new Exception(exp.Message);
                }
                finally
                {
                    connection.Close( );                                    

                }
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update( BasicDataEntity model )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "update T_BasicData set " );
            strSql.Append( "ModelCode=@ModelCode," );
            strSql.Append( "HSCodeInCat=@HSCodeInCat," );
            //strSql.Append( "ModelCodeDescription=@ModelCodeDescription," );
            strSql.Append( "EnglishProductName=@EnglishProductName," );
            strSql.Append( "LocalProductName=@LocalProductName," );
            strSql.Append( "QuantityUnit=@QuantityUnit," );
            strSql.Append( "SentialFactor=@SentialFactor," );
            strSql.Append( "LocalComposition=@LocalComposition," );
            strSql.Append( "SupervisionCondition=@SupervisionCondition," );
            strSql.Append( "DoubleOrSet=@DoubleOrSet," );
            strSql.Append( "Description=@Description," );
            strSql.Append( "ExaminingReport=@ExaminingReport," );
            strSql.Append( "Size=@Size" );
            strSql.Append( " where ModelCodeID=@ModelCodeID " );
            SqlParameter[] parameters = {
					new SqlParameter("@ModelCode", SqlDbType.VarChar,50),
					new SqlParameter("@HSCodeInCat", SqlDbType.VarChar,50),
					//new SqlParameter("@ModelCodeDescription", SqlDbType.VarChar,250),
					new SqlParameter("@EnglishProductName", SqlDbType.VarChar,255),
					new SqlParameter("@LocalProductName", SqlDbType.VarChar,255),
					new SqlParameter("@QuantityUnit", SqlDbType.VarChar,50),
					new SqlParameter("@SentialFactor", SqlDbType.VarChar,2048),
					new SqlParameter("@LocalComposition", SqlDbType.VarChar,255),
					new SqlParameter("@SupervisionCondition", SqlDbType.VarChar,255),
					new SqlParameter("@DoubleOrSet", SqlDbType.VarChar,255),
                    new SqlParameter("@Description", SqlDbType.VarChar,2048),
					new SqlParameter("@ExaminingReport", SqlDbType.VarChar,255),
                    new SqlParameter("@Size", SqlDbType.VarChar,255),
					new SqlParameter("@ModelCodeID", SqlDbType.Int,4)};
            parameters[0].Value = model.ModelCode;
            parameters[1].Value = model.HSCodeInCat;
            //parameters[2].Value = model.ModelCodeDescription;
            parameters[2].Value = model.EnglishProductName;
            parameters[3].Value = model.LocalProductName;
            parameters[4].Value = model.QuantityUnit;
            parameters[5].Value = model.SentialFactor;
            parameters[6].Value = model.LocalComposition;
            parameters[7].Value = model.SupervisionCondition;
            parameters[8].Value = model.DoubleOrSet;
            parameters[9].Value = model.Description;
            parameters[10].Value = model.ExaminingReport;
            parameters[11].Value = model.Size;
            parameters[12].Value = model.ModelCodeID;

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

        /// <summary>
        /// 更新一条数据
        /// </summary>
        //public bool UpdateSizeFlagBySize( )
        //{
        //    StringBuilder strSql=new StringBuilder( );
        //    strSql.Append( "update T_BasicData set " );
        //    strSql.Append( "SizeFlag=case Size when instr(Size,'≦2岁')<>0 then 0 when instr(Size','≧3岁')<>0 then 1 when Size is null then 1 END" );

        //    int rows=SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ));
        //    if ( rows > 0 )
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        /// <summary>
        /// 删除所有数据
        /// </summary>
        public void DeleteAll()
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_BasicData; " );
            SqlHelper.ExecuteSql( SqlHelper.LocalSqlServer , strSql.ToString( ));
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete( int ModelCodeID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_BasicData " );
            strSql.Append( " where ModelCodeID=@ModelCodeID " );
            SqlParameter[] parameters = {
					new SqlParameter("@ModelCodeID", SqlDbType.Int,4)			};
            parameters[0].Value = ModelCodeID;

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
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList( string ModeCodeIDlist )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "delete from T_BasicData " );
            strSql.Append( " where ModelCodeID in ("+ModeCodeIDlist + ")  " );
            int rows=SqlHelper.ExecuteSql(SqlHelper.LocalSqlServer, strSql.ToString( ) );
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
        public BasicDataEntity GetModel( int ModelCodeID )
        {

            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select  top 1 ModelCodeID,ModelCode,HSCodeInCat,EnglishProductName,LocalProductName,QuantityUnit,SentialFactor,LocalComposition,SupervisionCondition,DoubleOrSet,Description,ExaminingReport,Size from T_BasicData " );
            strSql.Append( " where ModelCodeID=@ModelCodeID " );
            SqlParameter[] parameters = {
					new SqlParameter("@ModelCodeID", SqlDbType.Int,4)			};
            parameters[0].Value = ModelCodeID;

            BasicDataEntity model=new BasicDataEntity( );
            DataSet ds=SqlHelper.Query(SqlHelper.LocalSqlServer, strSql.ToString( ) , parameters );
            if ( ds.Tables[0].Rows.Count>0 )
            {
                if ( ds.Tables[0].Rows[0]["ModelCodeID"]!=null && ds.Tables[0].Rows[0]["ModelCodeID"].ToString( )!="" )
                {
                    model.ModelCodeID=int.Parse( ds.Tables[0].Rows[0]["ModelCodeID"].ToString( ) );
                }
                if ( ds.Tables[0].Rows[0]["ModelCode"]!=null && ds.Tables[0].Rows[0]["ModelCode"].ToString( )!="" )
                {
                    model.ModelCode=ds.Tables[0].Rows[0]["ModelCode"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["HSCodeInCat"]!=null && ds.Tables[0].Rows[0]["HSCodeInCat"].ToString( )!="" )
                {
                    model.HSCodeInCat=ds.Tables[0].Rows[0]["HSCodeInCat"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["EnglishProductName"]!=null && ds.Tables[0].Rows[0]["EnglishProductName"].ToString( )!="" )
                {
                    model.EnglishProductName=ds.Tables[0].Rows[0]["EnglishProductName"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["LocalProductName"]!=null && ds.Tables[0].Rows[0]["LocalProductName"].ToString( )!="" )
                {
                    model.LocalProductName=ds.Tables[0].Rows[0]["LocalProductName"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["QuantityUnit"]!=null && ds.Tables[0].Rows[0]["QuantityUnit"].ToString( )!="" )
                {
                    model.QuantityUnit=ds.Tables[0].Rows[0]["QuantityUnit"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["SentialFactor"]!=null && ds.Tables[0].Rows[0]["SentialFactor"].ToString( )!="" )
                {
                    model.SentialFactor=ds.Tables[0].Rows[0]["SentialFactor"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["LocalComposition"]!=null && ds.Tables[0].Rows[0]["LocalComposition"].ToString( )!="" )
                {
                    model.LocalComposition=ds.Tables[0].Rows[0]["LocalComposition"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["SupervisionCondition"]!=null && ds.Tables[0].Rows[0]["SupervisionCondition"].ToString( )!="" )
                {
                    model.SupervisionCondition=ds.Tables[0].Rows[0]["SupervisionCondition"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["DoubleOrSet"]!=null && ds.Tables[0].Rows[0]["DoubleOrSet"].ToString( )!="" )
                {
                    model.DoubleOrSet=ds.Tables[0].Rows[0]["DoubleOrSet"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["Description"]!=null && ds.Tables[0].Rows[0]["Description"].ToString( )!="" )
                {
                    model.Description=ds.Tables[0].Rows[0]["Description"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["ExaminingReport"]!=null && ds.Tables[0].Rows[0]["ExaminingReport"].ToString( )!="" )
                {
                    model.ExaminingReport=ds.Tables[0].Rows[0]["ExaminingReport"].ToString( );
                }
                if ( ds.Tables[0].Rows[0]["Size"]!=null && ds.Tables[0].Rows[0]["Size"].ToString( )!="" )
                {
                    model.Size=ds.Tables[0].Rows[0]["Size"].ToString( );
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
            strSql.Append( "select ModelCode as 'Model_code',HSCodeInCat as 'HS_CODE_(IN_CAT)',EnglishProductName as '英文品名',LocalProductName as '中文品名',QuantityUnit as '法定计量单位',SentialFactor as '申报要素',SupervisionCondition as '监管条件',DoubleOrSet as 'Double_or_Set',Description as '备注',ExaminingReport as '现检测报告号',SIZE " );
            strSql.Append( " FROM T_BasicData " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            return SqlHelper.Query(SqlHelper.LocalSqlServer, strSql.ToString( ) );
        }
        public DataSet GetDistinctModeCodeList()
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select ModelCodeID,ModelCode,HSCodeInCat,EnglishProductName,LocalProductName,QuantityUnit,SentialFactor,LocalComposition,SupervisionCondition,DoubleOrSet,Description,ExaminingReport,Size " );
            strSql.Append( " FROM T_BasicData t where not exists( " );
            strSql.Append( " select 1 from T_BasicData where ModelCode=t.ModelCode and ModelCodeID<t.ModelCodeID) " );
            return SqlHelper.Query( SqlHelper.LocalSqlServer , strSql.ToString( ) );
        }
        public DataSet GetRepetitiveModeCodeList( )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select ModelCodeID,ModelCode,HSCodeInCat,EnglishProductName,LocalProductName,QuantityUnit,SentialFactor,LocalComposition,SupervisionCondition,DoubleOrSet,Description,ExaminingReport,Size " );
            strSql.Append( " FROM T_BasicData where ModelCode in ( " );
            strSql.Append( " select  ModelCode  from  T_BasicData  group  by  ModelCode  having  count(ModelCode) > 1) " );
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
            strSql.Append( " ModelCodeID,ModelCode,HSCodeInCat,EnglishProductName,LocalProductName,QuantityUnit,SentialFactor,LocalComposition,SupervisionCondition,DoubleOrSet,Description,ExaminingReport,Size " );
            strSql.Append( " FROM T_BasicData " );
            if ( strWhere.Trim( )!="" )
            {
                strSql.Append( " where "+strWhere );
            }
            strSql.Append( " order by " + filedOrder );
            return SqlHelper.Query(SqlHelper.LocalSqlServer, strSql.ToString( ) );
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount( string strWhere )
        {
            StringBuilder strSql=new StringBuilder( );
            strSql.Append( "select count(1) FROM T_BasicData " );
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
                strSql.Append( "order by T.ModeCodeID desc" );
            }
            strSql.Append( ")AS Row, T.*  from T_BasicData T " );
            if ( !string.IsNullOrEmpty( strWhere.Trim( ) ) )
            {
                strSql.Append( " WHERE " + strWhere );
            }
            strSql.Append( " ) TT" );
            strSql.AppendFormat( " WHERE TT.Row between {0} and {1}" , startIndex , endIndex );
            return SqlHelper.Query(SqlHelper.LocalSqlServer, strSql.ToString( ) );
        }

        #endregion  Method
    }
}
