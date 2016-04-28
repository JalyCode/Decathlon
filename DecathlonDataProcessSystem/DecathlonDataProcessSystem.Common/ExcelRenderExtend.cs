using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using System.Data;
using System.IO;
using System.Collections;

namespace DecathlonDataProcessSystem.Common
{
    public class ExcelRenderExtend
    {
        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <param name="fileName">指定Excel工作表名称</param>
        /// <returns>Excel工作表</returns>
        public static void ExportInvoiceListToExcel( DataTable sourceTable , DataTable netweightTable , string modelpath , string fileName , ArrayList shippingNumbers, 
            string BuyerCN , string BuyerEN , string AddressCN , string AddressEN , string TEL , string FAX , string ShipmentDate , string ShipmentType, 
            string LoadingPort , string DeliveryPort , string PaymentTerm , string DeliveryCountryCN,string DeliveryCountryEN , string DomesticSources ,
            string PackingSpecifications , string Incoterm , string TransportMode , string ShippingMark , string Destination , string Currency )
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook( );
            DataTable dt=new DataTable( );
            CreateScheduleSheet( ref hssfworkbook , netweightTable );
            int i=1;
            sourceTable.AsEnumerable( ).GroupBy( m => m.Field<int>( "BillNO" ) , m => m ).ToList( ).ForEach( delegate( IGrouping<int , DataRow> ig )
            {
                i=ig.Key;
                DataTable tempDT=new DataTable( );
                tempDT=ig.CopyToDataTable( );
                //DataRow[] drs=tempDT.AsEnumerable( ).ToArray<DataRow>( );
                List<DataRow> lst=tempDT.AsEnumerable( ).ToList<DataRow>( );
                List<DataRow> templist=new List<DataRow>( );
                CreateSalesContractSheet( ref hssfworkbook , ref i , tempDT,
                shippingNumbers , BuyerCN , BuyerEN , AddressCN , AddressEN , TEL , FAX , ShipmentDate , ShipmentType , 
             LoadingPort , DeliveryPort ,  PaymentTerm , DeliveryCountryCN, DeliveryCountryEN ,  DomesticSources ,
             PackingSpecifications , Incoterm , TransportMode , ShippingMark , Destination , Currency );
                CreatePackingListSheet( ref hssfworkbook , ref i , netweightTable,tempDT , tempDT.AsEnumerable( ).FirstOrDefault( )["数据类型"].ToString(),
                shippingNumbers , BuyerCN , BuyerEN , AddressCN , AddressEN , TEL , FAX , ShipmentDate , ShipmentType , 
             LoadingPort , DeliveryPort ,  PaymentTerm , DeliveryCountryCN, DeliveryCountryEN ,  DomesticSources ,
             PackingSpecifications ,  Incoterm ,  TransportMode ,  ShippingMark ,  Destination);
                CreateInvoiceSheet( ref hssfworkbook , ref i , netweightTable , tempDT ,
                 shippingNumbers , BuyerCN , BuyerEN , AddressCN , AddressEN , TEL , FAX , ShipmentDate , ShipmentType , 
             LoadingPort , DeliveryPort ,  PaymentTerm , DeliveryCountryCN, DeliveryCountryEN ,  DomesticSources ,
             PackingSpecifications ,  Incoterm ,  TransportMode ,  ShippingMark ,  Destination,Currency);

                DataTable billDT=new DataTable( );
                billDT=tempDT.Clone( );
                //IEnumerable<DataRow> drs=ig.CopyToDataTable( ).AsEnumerable( ).Take( 5 );
                int x=0;
                while ( lst.Count>0 )
                {
                    x++;
                    templist=lst.Take( 5 ).ToList();
                    billDT=templist.CopyToDataTable( );
                    CreateCustomsDeclarationSheet( ref hssfworkbook , ref i , netweightTable , billDT , ref x ,
                         shippingNumbers , BuyerCN , BuyerEN , AddressCN , AddressEN , TEL , FAX , ShipmentDate , ShipmentType , 
             LoadingPort , DeliveryPort ,  PaymentTerm , DeliveryCountryCN, DeliveryCountryEN ,  DomesticSources ,
             PackingSpecifications ,  Incoterm ,  TransportMode ,  ShippingMark ,  Destination,Currency);
                    lst=lst.Except<DataRow>(templist).ToList();
                    //tempDT.AcceptChanges( );
                }

                //ig.CopyToDataTable().AsEnumerable().
                //DataRow row=_BillSmallClassNetWeightTable.NewRow( );
                //row["BillNO"]=ig.Key;
                //row["Qty"]=ig.ToList<DataRow>( ).Sum( p => int.Parse( p.Field<string>( "QUANTITY" ) ) );
                //row["Amount"]=ig.ToList<DataRow>( ).Sum( p => decimal.Parse( p.Field<string>( "TOTAL_VALUE" ) ) );
                //row["CartonQty"]=0;
                //row["GW"]=ig.ToList<DataRow>( ).Sum( p => decimal.Parse( p.Field<string>( "GROSS_WEIGHT" ) ) );
                //row["NW"]=0;
                //row["Volume"]=0;
                //row["NWRadio"]=0;
                //_BillSmallClassNetWeightTable.Rows.Add( row );
            } );

            FileStream fileS = new FileStream( modelpath + fileName + ".xls" , FileMode.Create );//保存
            hssfworkbook.Write( fileS );
            fileS.Close( );
        }
        private static void CreateScheduleSheet( ref HSSFWorkbook hssfworkbook , DataTable schDT )
        {
            int rowIndex = 0;//从第二行开始，因为前两行是模板里面的内容 
            //int colIndex = 0;
            int sumCartonQty=0;
            int sumQty=0;
            decimal sumGW=0;
            decimal sumNW=0;
            decimal sumVolume=0;
            decimal sumAmount=0;
            ISheet sheet1 = hssfworkbook.CreateSheet( "schedule" );
            HSSFRow rowHead = (HSSFRow)sheet1.CreateRow( 0 );

            rowHead.CreateCell( 0 ).SetCellValue( "序号" );
            rowHead.CreateCell( 1 ).SetCellValue( "Carton qty" );
            rowHead.CreateCell( 2 ).SetCellValue( "Qty" );
            rowHead.CreateCell( 3 ).SetCellValue( "GW" );
            rowHead.CreateCell( 4 ).SetCellValue( "NW" );
            rowHead.CreateCell( 5 ).SetCellValue( "Volume" );
            rowHead.CreateCell( 6 ).SetCellValue( "金额（USD)" );
            foreach ( DataRow row in schDT.Rows )
            {   //双循环写入sourceTable中的数据
                //rowIndex++;
                //colIndex = 0;
                HSSFRow xlsrow = (HSSFRow)sheet1.CreateRow( ++rowIndex );
                xlsrow.CreateCell( 0 ).SetCellValue( row["BillNO"].ToString( ) );
                xlsrow.CreateCell( 1 ).SetCellValue( row["CartonQty"].ToString( ) );
                sumCartonQty+=int.Parse( row["CartonQty"].ToString( ) );
                xlsrow.CreateCell( 2 ).SetCellValue( row["Qty"].ToString( ) );
                sumQty+=int.Parse( row["Qty"].ToString( ) );
                xlsrow.CreateCell( 3 ).SetCellValue( row["GW"].ToString( ) );
                sumGW+=decimal.Parse( row["GW"].ToString( ) );
                xlsrow.CreateCell( 4 ).SetCellValue( row["NW"].ToString( ) );
                sumNW+=decimal.Parse( row["NW"].ToString( ) );
                xlsrow.CreateCell( 5 ).SetCellValue( row["Volume"].ToString( ) );
                sumVolume+=decimal.Parse( row["Volume"].ToString( ) );
                xlsrow.CreateCell( 6 ).SetCellValue( row["Amount"].ToString( ) );
                sumAmount+=decimal.Parse( row["Amount"].ToString( ) );
            }
            HSSFRow xlsrow1 = (HSSFRow)sheet1.CreateRow( ++rowIndex );
            xlsrow1.CreateCell( 0 ).SetCellValue( "汇总" );
            xlsrow1.CreateCell( 1 ).SetCellValue( sumCartonQty.ToString( ) );
            xlsrow1.CreateCell( 2 ).SetCellValue( sumQty.ToString( ) );
            xlsrow1.CreateCell( 3 ).SetCellValue( sumGW.ToString() );
            xlsrow1.CreateCell( 4 ).SetCellValue( sumNW.ToString() );
            xlsrow1.CreateCell( 5 ).SetCellValue( sumVolume.ToString());
            xlsrow1.CreateCell( 6 ).SetCellValue( sumAmount.ToString());
            sheet1.ForceFormulaRecalculation = true;
            for ( int i = 0 ; i < schDT.Columns.Count ; i++ )
            {
                sheet1.AutoSizeColumn( i );
            }
        }
        private static void CreateSalesContractSheet( ref HSSFWorkbook hssfworkbook , ref int i ,  DataTable schDT , ArrayList shippingNumbers , 
            string BuyerCN , string BuyerEN , string AddressCN , string AddressEN , string TEL , string FAX , string ShipmentDate , string ShipmentType, 
            string LoadingPort , string DeliveryPort , string PaymentTerm , string DeliveryCountryCN,string DeliveryCountryEN , string DomesticSources ,
            string PackingSpecifications , string Incoterm , string TransportMode , string ShippingMark , string Destination , string Currency)
        {
            //DataTable schCopyDT=new DataTable( );
            //schCopyDT=schDT.Copy( );
            //schCopyDT.Columns.Remove("")
            int rowIndex = 1;//从第二行开始，因为前两行是模板里面的内容 
            //int colIndex = 0;
            string sheetname="P"+i.ToString( )+"合同";
            ISheet sheet1 = hssfworkbook.CreateSheet( sheetname );
            HSSFRow rowHead = (HSSFRow)sheet1.CreateRow( 0 );
            rowHead.Height=30 * 20;            
            sheet1.AddMergedRegion( new CellRangeAddress( 0 , 0 , 0 , 10 ) );
            ICell cell = rowHead.CreateCell( 0 );//在行中添加一列
            
            cell.SetCellValue( "DECATHLON" );//设置列的内容
            cell.CellStyle=GetCellStyle( hssfworkbook , GetFontStyle( hssfworkbook , "华文彩云" , HSSFColor.SKY_BLUE.index , 16 ) ,HSSFColor.WHITE.index, FillPatternType.SOLID_FOREGROUND, HSSFColor.SKY_BLUE.index , HorizontalAlignment.CENTER , VerticalAlignment.CENTER );
            //setCellStyle( hssfworkbook , cell );
            HSSFRow xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.Height=30 * 20;
            sheet1.AddMergedRegion( new CellRangeAddress( 1 , 1 , 0 , 10 ) );
            cell = xlsrow.CreateCell( 0 );//在行中添加一列
            cell.SetCellValue( "销　售　合　同" );//设置列的内容
            //setCellStyle( hssfworkbook , cell );
            cell.CellStyle=GetCellStyle( hssfworkbook , GetFontStyle( hssfworkbook , "宋体" , HSSFColor.BLACK.index , 22 ) , HSSFColor.WHITE.index , FillPatternType.SOLID_FOREGROUND , HSSFColor.WHITE.index , HorizontalAlignment.CENTER , VerticalAlignment.CENTER );
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 0 , 0 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 1 , 4 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 6 , 6 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 7 , 10 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "卖方：" );
            xlsrow.CreateCell( 1 ).SetCellValue( "迪卡侬（昆山)仓储有限公司" );
            xlsrow.CreateCell( 6 ).SetCellValue( "买方：" );
            xlsrow.CreateCell( 7 ).SetCellValue( BuyerEN.ToString() );


            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            //xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            sheet1.AddMergedRegion( new CellRangeAddress( 4 , 4 , 0 , 0 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( 4 , 4 , 1 , 4 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( 4 , 4 , 6 , 6 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( 4 , 4 , 7 , 10 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "所在地：" );
            xlsrow.CreateCell( 1 ).SetCellValue( "江苏昆山花桥花集花溪路667号" );
            xlsrow.CreateCell( 6 ).SetCellValue( "所在地：" );
            xlsrow.CreateCell( 7 ).SetCellValue( AddressEN.ToString());
            rowIndex++;
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            sheet1.AddMergedRegion( new CellRangeAddress( 7 , 7 , 0 , 0 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( 7 , 7 , 1 , 10 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "合同编号：" );
            cell = xlsrow.CreateCell( 1 );
            string ContractNumber=string.Empty;
            foreach ( string strname in shippingNumbers )
            {

                ContractNumber+=strname+"/";
            }
            cell.SetCellValue( ContractNumber.Substring( 0 , ContractNumber.Length-1 )+"-"+i.ToString( ) );
            //setCellStyle( hssfworkbook , cell );
            //rowHead.CreateCell( 1 ).SetCellValue( "江苏昆山花桥花集花溪路667号" );
            rowIndex++;
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            sheet1.AddMergedRegion( new CellRangeAddress( 10 , 10 , 0 , 10 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "双方同意按照本销售合同所列条款由卖方出售，买方购进下列货物：" );
            //rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "" );
            xlsrow.CreateCell( 1 ).SetCellValue( "" );
            xlsrow.CreateCell( 2 ).SetCellValue( "" );
            xlsrow.CreateCell( 3 ).SetCellValue( "" );
            xlsrow.CreateCell( 4 ).SetCellValue( "" );
            xlsrow.CreateCell( 5 ).SetCellValue( "" );
            xlsrow.CreateCell( 6 ).SetCellValue( "" );
            xlsrow.CreateCell( 7 ).SetCellValue( "" );
            xlsrow.CreateCell( 8 ).SetCellValue( "" );
            xlsrow.CreateCell( 9 ).SetCellValue( Incoterm );
            xlsrow.CreateCell( 10 ).SetCellValue( LoadingPort );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            sheet1.AddMergedRegion( new CellRangeAddress( 12 , 12 , 0 , 0 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( 12 , 12 , 1 , 4 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( 12 , 12 , 5 , 5 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( 12 , 12 , 6 , 6 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( 12 , 12 , 7 , 7 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( 12 , 12 , 8 , 8 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( 12 , 12 , 9 , 9 ) );
            //xlsrow.CreateCell( 0 ).SetCellValue( "PO号" );
            xlsrow.CreateCell( 0 ).SetCellValue( "" );
            xlsrow.CreateCell( 1 ).SetCellValue( "品名及面料" );
            xlsrow.CreateCell( 5 ).SetCellValue( "数量" );
            xlsrow.CreateCell( 6 ).SetCellValue( "单价" );
            xlsrow.CreateCell( 7 ).SetCellValue( "毛重" );
            xlsrow.CreateCell( 8 ).SetCellValue( "金额" );
            xlsrow.CreateCell( 9 ).SetCellValue( "币制" );
            int beginIndex=rowIndex;
            int endIndex=0;
            int sumquantity=0;
            decimal sumprice=0;
            //decimal sumnetweight=0;
            decimal sumgrossweight=0;
            decimal sumtotalvalue=0;
            foreach ( DataRow row in schDT.Rows )
            {   //双循环写入sourceTable中的数据
                //beginIndex=rowIndex++;
                //colIndex = 0;
                xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex );
                sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 0 , 0 ) );
                sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 1 , 4 ) );
                sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 5 , 5 ) );
                sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 6 , 6 ) );
                sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 7 , 7 ) );
                sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 8 , 8 ) );
                sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 9 , 9 ) );
                xlsrow.CreateCell( 0 ).SetCellValue( "" );                
                xlsrow.CreateCell( 1 ).SetCellValue( row["中文品名"].ToString( ) );
                sumquantity+=int.Parse( row["QUANTITY"].ToString( ) );
                xlsrow.CreateCell( 5 ).SetCellValue( row["QUANTITY"].ToString( ) );
                //sumprice+=decimal.Parse( row["TOTAL_VALUE"].ToString( ) )/decimal.Parse( row["QUANTITY"].ToString( ) );
                xlsrow.CreateCell( 6 ).SetCellValue( decimal.Round( decimal.Parse( row["TOTAL_VALUE"].ToString( ) )/decimal.Parse( row["QUANTITY"].ToString( ) ) , 2 ).ToString( ) );
                sumgrossweight+=decimal.Parse(row["GROSS_WEIGHT"].ToString( ));
                xlsrow.CreateCell( 7 ).SetCellValue( row["GROSS_WEIGHT"].ToString( ) );
                sumtotalvalue+=decimal.Parse( row["TOTAL_VALUE"].ToString( ) );
                xlsrow.CreateCell( 8 ).SetCellValue( row["TOTAL_VALUE"].ToString( ) );
                xlsrow.CreateCell( 9 ).SetCellValue( Currency );
                rowIndex++;
            }
            endIndex=rowIndex-1;
            //sheet1.AddMergedRegion( new CellRangeAddress( beginIndex , endIndex , 0 , 0 ) );
            //xlsrow.CreateCell( 0 ).SetCellValue( ContractNumber.Substring( 0 , ContractNumber.Length-1 )+"-"+i.ToString( ));
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 0 , 0 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 1 , 4 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 5 , 5 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 6 , 6 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 7 , 7 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 8 , 8 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 9 , 9 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 0 , 0 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 1 , 4 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 5 , 5 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 6 , 6 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 7 , 7 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 8 , 8 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 9 , 9 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "" );
            xlsrow.CreateCell( 1 ).SetCellValue("TOTAL");
            xlsrow.CreateCell( 5 ).SetCellValue(sumquantity.ToString());
            //xlsrow.CreateCell( 6 ).SetCellValue( sumprice.ToString());
            xlsrow.CreateCell( 6 ).SetCellValue("");
            xlsrow.CreateCell( 7 ).SetCellValue( sumgrossweight.ToString() );
            xlsrow.CreateCell( 8 ).SetCellValue( sumtotalvalue.ToString() );
            xlsrow.CreateCell( 9 ).SetCellValue( Currency );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 0 , 0 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 1 , 4 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "装运期：" );
            xlsrow.CreateCell( 1 ).SetCellValue( ShipmentDate );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 0 , 0 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 1 , 4 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "装运方式：：" );
            xlsrow.CreateCell( 1 ).SetCellValue( ShipmentType );

            //xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 0 , 0 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 1 , 4 ) );
            //xlsrow.CreateCell( 0 ).SetCellValue( "装运方式：：" );
            //xlsrow.CreateCell( 1 ).SetCellValue( "BY VESSEL" );
            //xlsrow.CreateCell( 0 ).SetCellValue( "双方同意按照本销售合同所列条款由卖方出售，买方购进下列货物：" );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 0 , 0 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 1 , 4 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "装运港口：" );
            xlsrow.CreateCell( 1 ).SetCellValue( LoadingPort );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 0 , 0 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 1 , 4 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "目的港口：" );
            xlsrow.CreateCell( 1 ).SetCellValue( DeliveryPort );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 0 , 0 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 1 , 4 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "付款方式：" );
            xlsrow.CreateCell( 1 ).SetCellValue( PaymentTerm );

            //xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 0 , 0 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 1 , 4 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 5 , 5 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 6 , 6 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 7 , 7 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 8 , 8 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 9 , 9 ) );
            //xlsrow.CreateCell( 0 ).SetCellValue( "" );
            //xlsrow.CreateCell( 1 ).SetCellValue( "" );
            //xlsrow.CreateCell( 2 ).SetCellValue( "" );
            //xlsrow.CreateCell( 3 ).SetCellValue( "" );
            //xlsrow.CreateCell( 4 ).SetCellValue( "" );
            //xlsrow.CreateCell( 5 ).SetCellValue( "" );
            //xlsrow.CreateCell( 6 ).SetCellValue( "" );
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "" );
            //xlsrow.CreateCell( 0 ).SetCellValue( "DPSH" );
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "" );
            xlsrow.CreateCell( 1 ).SetCellValue( "买　　方" );
            xlsrow.CreateCell( 2 ).SetCellValue( "" );
            xlsrow.CreateCell( 3 ).SetCellValue( "" );
            xlsrow.CreateCell( 4 ).SetCellValue( "" );
            xlsrow.CreateCell( 5 ).SetCellValue( "" );
            xlsrow.CreateCell( 6 ).SetCellValue( "" );
            xlsrow.CreateCell( 7 ).SetCellValue( "卖　方" );
            xlsrow.CreateCell( 8 ).SetCellValue( "" );
            xlsrow.CreateCell( 9 ).SetCellValue( "" );
            xlsrow.CreateCell( 10 ).SetCellValue( "" );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "" );
            xlsrow.CreateCell( 1 ).SetCellValue( BuyerEN );
            xlsrow.CreateCell( 2 ).SetCellValue( "" );
            xlsrow.CreateCell( 3 ).SetCellValue( "" );
            xlsrow.CreateCell( 4 ).SetCellValue( "" );
            xlsrow.CreateCell( 5 ).SetCellValue( "" );
            xlsrow.CreateCell( 6 ).SetCellValue( "" );
            xlsrow.CreateCell( 7 ).SetCellValue( "迪卡侬（昆山)仓储有限公司" );
            xlsrow.CreateCell( 8 ).SetCellValue( "" );
            xlsrow.CreateCell( 9 ).SetCellValue( "" );
            xlsrow.CreateCell( 10 ).SetCellValue( "" );
        }
        private static void CreatePackingListSheet( ref HSSFWorkbook hssfworkbook , ref int i , DataTable sDT , DataTable schDT , string typeName , ArrayList shippingNumbers , 
            string BuyerCN , string BuyerEN , string AddressCN , string AddressEN , string TEL , string FAX , string ShipmentDate , string ShipmentType, 
            string LoadingPort , string DeliveryPort , string PaymentTerm , string DeliveryCountryCN,string DeliveryCountryEN , string DomesticSources ,
            string PackingSpecifications , string Incoterm , string TransportMode , string ShippingMark , string Destination)
        {
            int rowIndex = 1;//从第二行开始，因为前两行是模板里面的内容 
            int colIndex = 0;
            string sheetname="P"+i.ToString( )+typeName;
            ISheet sheet1 = hssfworkbook.CreateSheet( sheetname );
            //sheet1.AutoSizeColumn( 1 , true );
            //sheet1.AutoSizeColumn( 2 , true );
            //sheet1.AutoSizeColumn( 3 , true ); 
            //sheet1.AutoSizeColumn( 1 , true ); 
            sheet1.SetColumnWidth( 1 , 30 * 256 );
            sheet1.SetColumnWidth( 2 , 20 * 256 );
            //sheet1.SetColumnWidth( 3 , 100 * 256 );
            HSSFRow rowHead = (HSSFRow)sheet1.CreateRow( 0 );
            rowHead.Height=30 * 20;
            sheet1.AddMergedRegion( new CellRangeAddress( 0 , 0 , 0 , 8 ) );
            ICell cell = rowHead.CreateCell( 0 );//在行中添加一列
            cell.SetCellValue( "PACKING LIST" );//设置列的内容
            cell.CellStyle=GetCellStyle( hssfworkbook , GetFontStyle( hssfworkbook , "华文彩云" , HSSFColor.SKY_BLUE.index , 16 ) , HSSFColor.WHITE.index , FillPatternType.SOLID_FOREGROUND , HSSFColor.SKY_BLUE.index , HorizontalAlignment.CENTER , VerticalAlignment.CENTER );
            //setCellStyle( hssfworkbook , cell );
            HSSFRow xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.Height=30 * 20;
            sheet1.AddMergedRegion( new CellRangeAddress( 1 , 1 , 0 , 8 ) );
            cell = xlsrow.CreateCell( 0 );//在行中添加一列
            cell.SetCellValue( "装箱单" );//设置列的内容
            cell.CellStyle=GetCellStyle( hssfworkbook , GetFontStyle( hssfworkbook , "宋体" , HSSFColor.BLACK.index , 22 ) , HSSFColor.WHITE.index , FillPatternType.SOLID_FOREGROUND , HSSFColor.WHITE.index , HorizontalAlignment.CENTER , VerticalAlignment.CENTER );
            //setCellStyle( hssfworkbook , cell );
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 0 , 0 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 1 , 4 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 0 , 6 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 7 , 10 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "出售单位：" );
            xlsrow.CreateCell( 1 ).SetCellValue( "迪卡侬（昆山）仓储有限公司" );
            xlsrow.CreateCell( 8 ).SetCellValue( "FOR EXPORT" );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 0 , 0 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 1 , 4 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 0 , 6 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 7 , 10 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "地址：" );
            xlsrow.CreateCell( 1 ).SetCellValue( "江苏省昆山市花桥镇顺陈路东侧、花集路南侧" );
            xlsrow.CreateCell( 8 ).SetCellValue( "出口专用" );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "Address: " );
            xlsrow.CreateCell( 1 ).SetCellValue( "No.667 huaji Road, Kunshan,China" );
            //xlsrow.CreateCell( 8 ).SetCellValue( "出口专用" );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "Register: " );
            xlsrow.CreateCell( 1 ).SetCellValue( "No.3204604052" );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "注册号: " );
            xlsrow.CreateCell( 1 ).SetCellValue( "3204604052" );

            rowIndex++;

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "购货单位: " );
            xlsrow.CreateCell( 1 ).SetCellValue( BuyerCN );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "地址: " );
            xlsrow.CreateCell( 1 ).SetCellValue( AddressCN );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "Address: " );
            xlsrow.CreateCell( 1 ).SetCellValue( AddressEN );

            //xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            //xlsrow.CreateCell( 0 ).SetCellValue(BuyerCN );
            //xlsrow.CreateCell( 1 ).SetCellValue( "812 YUNG CHUN EAST 1ST ROAD, - NAN TUN DISTRICT,TAICHUNG，TAIWAN." );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( BuyerEN );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "TEL: " );
            xlsrow.CreateCell( 1 ).SetCellValue( TEL );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "FAX:" );
            xlsrow.CreateCell( 1 ).SetCellValue( FAX );
            rowIndex++;
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 4 ).SetCellValue( "发票号码: " );
            string ContractNumber=string.Empty;
            foreach ( string strname in shippingNumbers )
            {
                ContractNumber+=strname+"/";
            }
            xlsrow.CreateCell( 5 ).SetCellValue( ContractNumber.Substring( 0 , ContractNumber.Length-1 )+"-"+i.ToString());
            rowIndex++;
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 4 ).SetCellValue( "SHIPMENT DATE  装运日期: " );
            xlsrow.CreateCell( 7 ).SetCellValue( ShipmentDate );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 4 ).SetCellValue( "LOADING PORT 装船口岸: " );
            xlsrow.CreateCell( 7 ).SetCellValue( LoadingPort );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 4 ).SetCellValue( "DELIVERY PORT 目的港: " );
            xlsrow.CreateCell( 7 ).SetCellValue( DeliveryPort );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 4 ).SetCellValue( "INCOTERM  价格条款:" );
            xlsrow.CreateCell( 7 ).SetCellValue( Incoterm );
            rowIndex++;

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.Height=30*20;
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 0 , 0 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 1 , 1 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 2 , 3 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 4 , 4 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 5 , 5 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 6 , 6 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "" );
            xlsrow.CreateCell( 1 ).SetCellValue( "SPECIFICATION 品名:" );
            xlsrow.CreateCell( 2 ).SetCellValue( "ENGLISH SPECIFICATION" );
            xlsrow.CreateCell( 4 ).SetCellValue( "QTY 数量(PCS)" );
            xlsrow.CreateCell( 5 ).SetCellValue( "G.W. 毛重(KG)" );
            xlsrow.CreateCell( 6 ).SetCellValue( "N.W. 净重(KG)" );
            int index=0;
            int sumQUANTITY=0;
            decimal sumGROSS_WEIGHT=0;
            decimal sumNET_WEIGHT=0;
            int sumParcel_Number=0;
            decimal sumVolume=0;
            foreach ( DataRow row in schDT.Rows )
            {   //双循环写入sourceTable中的数据
                //rowIndex++;
                //colIndex = 0;
                index++;
                xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
                //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 2 , 3 ) );
                sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 0 , 0 ) );
                sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 1 , 1 ) );
                sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 2 , 3 ) );
                sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 4 , 4 ) );
                sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 5 , 5 ) );
                sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 6 , 6 ) );
                xlsrow.CreateCell( 0 ).SetCellValue( index.ToString( )+"#" );                
                xlsrow.CreateCell( 1 ).SetCellValue( row["中文品名"].ToString( ) );
                xlsrow.CreateCell( 2 ).SetCellValue( row["英文品名"].ToString( ) );
                xlsrow.CreateCell( 4 ).SetCellValue( row["QUANTITY"].ToString( ));
                sumQUANTITY+=int.Parse( row["QUANTITY"].ToString( ) );
                xlsrow.CreateCell( 5 ).SetCellValue( row["GROSS_WEIGHT"].ToString( ) );
                sumGROSS_WEIGHT+=decimal.Parse( row["GROSS_WEIGHT"].ToString( ) );
                xlsrow.CreateCell( 6 ).SetCellValue( row["NET_WEIGHT"].ToString( ) );
                sumNET_WEIGHT+=decimal.Parse( row["NET_WEIGHT"].ToString( ) );
            }
            sumParcel_Number=sDT.Select( "BillNO="+i ).AsEnumerable( ).Sum( p => p.Field<int>( "CartonQty" ) );
            sumVolume=sDT.Select( "BillNO="+i ).AsEnumerable( ).Sum( p => p.Field<decimal>( "Volume" ) );            
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 2 , 3 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 0 , 0 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 1 , 1 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 2 , 3 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 4 , 4 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 5 , 5 ) );
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 6 , 6 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "" );
            xlsrow.CreateCell( 1 ).SetCellValue( "" );
            xlsrow.CreateCell( 2 ).SetCellValue( "TOTAL" );
            xlsrow.CreateCell( 4 ).SetCellValue( sumQUANTITY.ToString());
            xlsrow.CreateCell( 5 ).SetCellValue( sumGROSS_WEIGHT.ToString());
            xlsrow.CreateCell( 6 ).SetCellValue( sumNET_WEIGHT.ToString());
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "G. WEIGHT(KG)毛重:" );
            xlsrow.CreateCell( 1 ).SetCellValue( sumGROSS_WEIGHT.ToString( )+"KG" );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "N. WEIGHT(KG)净重:" );
            xlsrow.CreateCell( 1 ).SetCellValue( sumNET_WEIGHT.ToString( )+"KG" );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "TOTAL CARTONS总箱数:" );
            xlsrow.CreateCell( 1 ).SetCellValue( sumParcel_Number.ToString()+" parcels箱" );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "TOTAL VOLUME总体积:" );
            xlsrow.CreateCell( 1 ).SetCellValue( sumVolume.ToString( )+"M3" );
            rowIndex++;
            rowIndex++;
            rowIndex++;
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 5 ).SetCellValue( "Etienne Callafe" );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 5 ).SetCellValue( "经理" );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 5 ).SetCellValue( "MANAGER" );

        }
        private static void CreateInvoiceSheet( ref HSSFWorkbook hssfworkbook , ref int i , DataTable sDT, DataTable schDT , ArrayList shippingNumbers, 
            string BuyerCN , string BuyerEN , string AddressCN , string AddressEN , string TEL , string FAX , string ShipmentDate , string ShipmentType, 
            string LoadingPort , string DeliveryPort , string PaymentTerm , string DeliveryCountryCN,string DeliveryCountryEN , string DomesticSources ,
            string PackingSpecifications , string Incoterm , string TransportMode , string ShippingMark , string Destination , string Currency )
        {
            int rowIndex = 1;//从第二行开始，因为前两行是模板里面的内容 
            //int colIndex = 0;
            string sheetname="INV"+i.ToString( );
            ISheet sheet1 = hssfworkbook.CreateSheet( sheetname );
            sheet1.SetColumnWidth( 0 , 30 * 256 );
            sheet1.SetColumnWidth( 2 , 20 * 256 );
            sheet1.SetColumnWidth( 3 , 30 * 256 );
            HSSFRow rowHead = (HSSFRow)sheet1.CreateRow( 0 );
            rowHead.Height=30 * 20;  
            sheet1.AddMergedRegion( new CellRangeAddress( 0 , 0 , 0 , 8 ) );
            ICell cell = rowHead.CreateCell( 0 );//在行中添加一列
            cell.SetCellValue( "INVOICE" );//设置列的内容
            cell.CellStyle=GetCellStyle( hssfworkbook , GetFontStyle( hssfworkbook , "华文彩云" , HSSFColor.SKY_BLUE.index , 16 ) , HSSFColor.WHITE.index , FillPatternType.SOLID_FOREGROUND , HSSFColor.SKY_BLUE.index , HorizontalAlignment.CENTER , VerticalAlignment.CENTER );
            //setCellStyle( hssfworkbook , cell );
            HSSFRow xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            sheet1.AddMergedRegion( new CellRangeAddress( 1 , 1 , 0 , 8 ) );
            xlsrow.Height=30 * 20;
            cell = xlsrow.CreateCell( 0 );//在行中添加一列
            cell.SetCellValue( "发票" );//设置列的内容
            cell.CellStyle=GetCellStyle( hssfworkbook , GetFontStyle( hssfworkbook , "宋体" , HSSFColor.BLACK.index , 22 ) , HSSFColor.WHITE.index , FillPatternType.SOLID_FOREGROUND , HSSFColor.WHITE.index , HorizontalAlignment.CENTER , VerticalAlignment.CENTER );
            //setCellStyle( hssfworkbook , cell );
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 0 , 0 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 1 , 4 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 0 , 6 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 7 , 10 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "出售单位：" );
            xlsrow.CreateCell( 1 ).SetCellValue( "迪卡侬（昆山）仓储有限公司" );
            xlsrow.CreateCell( 8 ).SetCellValue( "FOR EXPORT" );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 0 , 0 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 1 , 4 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 0 , 6 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 7 , 10 ) );
            xlsrow.CreateCell( 0 ).SetCellValue( "地址：" );
            xlsrow.CreateCell( 1 ).SetCellValue( "江苏省昆山市花桥镇顺陈路东侧、花集路南侧" );
            xlsrow.CreateCell( 8 ).SetCellValue( "出口专用" );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "Address: " );
            xlsrow.CreateCell( 1 ).SetCellValue( "No.667 huaji Road, Kunshan,China" );
            //xlsrow.CreateCell( 8 ).SetCellValue( "出口专用" );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "Register: " );
            xlsrow.CreateCell( 1 ).SetCellValue( "No.3204604052" );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "注册号: " );
            xlsrow.CreateCell( 1 ).SetCellValue( "3204604052" );

            rowIndex++;

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "购货单位: " );
            xlsrow.CreateCell( 1 ).SetCellValue( BuyerCN );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "地址: " );
            xlsrow.CreateCell( 1 ).SetCellValue( AddressCN );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "Address: " );
            xlsrow.CreateCell( 1 ).SetCellValue( AddressEN );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( BuyerEN );
            //xlsrow.CreateCell( 1 ).SetCellValue( "812 YUNG CHUN EAST 1ST ROAD, - NAN TUN DISTRICT,TAICHUNG，TAIWAN." );
            //xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            //xlsrow.CreateCell( 0 ).SetCellValue( ". - TAICHUNG" );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "TEL: " );
            xlsrow.CreateCell( 1 ).SetCellValue( TEL );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "FAX:" );
            xlsrow.CreateCell( 1 ).SetCellValue( FAX );
            rowIndex++;
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 4 ).SetCellValue( "发票号码: " );
            string ContractNumber=string.Empty;
            foreach ( string strname in shippingNumbers )
            {
                ContractNumber+=strname+"/";
            }
            xlsrow.CreateCell( 5 ).SetCellValue( ContractNumber.Substring( 0 , ContractNumber.Length-1 )+"-"+i.ToString());
            rowIndex++;
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 4 ).SetCellValue( "SHIPMENT DATE  装运日期: " );
            xlsrow.CreateCell( 7 ).SetCellValue( ShipmentDate );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 4 ).SetCellValue( "LOADING PORT 装船口岸: " );
            xlsrow.CreateCell( 7 ).SetCellValue( LoadingPort );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 4 ).SetCellValue( "DELIVERY PORT 目的港: " );
            xlsrow.CreateCell( 7 ).SetCellValue( DeliveryPort );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 4 ).SetCellValue( "INCOTERM  价格条款:" );
            xlsrow.CreateCell( 7 ).SetCellValue( Incoterm );
            rowIndex++;

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 4 , 6 ) );
            //xlsrow.CreateCell( 0 ).SetCellValue( "" );
            //xlsrow.CreateCell( 1 ).SetCellValue( "" );
            xlsrow.CreateCell( 2 ).SetCellValue( "SPECIFICATION 品名" );
            xlsrow.CreateCell( 3 ).SetCellValue( "ENGLISH  SPECIFICATION" );
            xlsrow.CreateCell( 4 ).SetCellValue( "QTY 数量" );
            //xlsrow.CreateCell( 5 ).SetCellValue( "G.W.\r\n毛重" );
            xlsrow.CreateCell( 7 ).SetCellValue( "AMOUNT 总额" );
            xlsrow.CreateCell( 8 ).SetCellValue( "币制" );
            int sumQUANTITY=0;
            decimal sumTOTAL_VALUE=0;
            decimal sumGROSS_WEIGHT=0;
            decimal sumNET_WEIGHT=0;
            int sumParcel_Number=0;
            decimal sumVolume=0; 
            foreach ( DataRow row in schDT.Rows )
            {   //双循环写入sourceTable中的数据
                //rowIndex++;
                xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++);
                //sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 4 , 6 ) );
                //xlsrow.CreateCell( 0 ).SetCellValue( index.ToString( )+"#" );
                xlsrow.CreateCell( 2 ).SetCellValue( row["中文品名"].ToString( ) );
                xlsrow.CreateCell( 3 ).SetCellValue( row["英文品名"].ToString( ) );
                xlsrow.CreateCell( 4 ).SetCellValue( row["QUANTITY"].ToString( ));
                xlsrow.CreateCell( 5 ).SetCellValue( "PCS" );
                //sumQUANTITY+=int.Parse( row["QUANTITY"].ToString( ) );
                xlsrow.CreateCell( 7 ).SetCellValue(row["TOTAL_VALUE"].ToString( ) );
                xlsrow.CreateCell( 8 ).SetCellValue( Currency);
                sumTOTAL_VALUE+=decimal.Parse( row["TOTAL_VALUE"].ToString( ) );
                sumQUANTITY+=int.Parse( row["QUANTITY"].ToString( ) );
                sumGROSS_WEIGHT+=decimal.Parse( row["GROSS_WEIGHT"].ToString( ) );
                sumNET_WEIGHT+=decimal.Parse( row["NET_WEIGHT"].ToString( ) );
                //sumParcel_Number+=int.Parse( row["CartonQty"].ToString( ) );
                //sumVolume+=decimal.Parse( row["Volume"].ToString( ) );
                //xlsrow.CreateCell( 6 ).SetCellValue( row["NET_WEIGHT"].ToString( ) );
                //sumNET_WEIGHT+=decimal.Parse( row["NET_WEIGHT"].ToString( ) );
            }
            sumParcel_Number=sDT.Select( "BillNO="+i ).AsEnumerable( ).Sum( p => p.Field<int>( "CartonQty" ) );
            sumVolume=sDT.Select( "BillNO="+i ).AsEnumerable( ).Sum( p => p.Field<decimal>( "Volume" ) );   
            //rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow = (HSSFRow)sheet1.CreateRow(rowIndex++);
            sheet1.AddMergedRegion( new CellRangeAddress( rowIndex , rowIndex , 4 , 6 ) );
            //xlsrow.CreateCell( 0 ).SetCellValue( "" );
            //xlsrow.CreateCell( 1 ).SetCellValue( "" );
            xlsrow.CreateCell( 2 ).SetCellValue( "TOTAL" );
            xlsrow.CreateCell( 3 ).SetCellValue( "" );
            xlsrow.CreateCell( 4 ).SetCellValue( sumQUANTITY.ToString() );
            xlsrow.CreateCell( 5 ).SetCellValue( "PCS" );
            xlsrow.CreateCell( 7 ).SetCellValue( sumTOTAL_VALUE.ToString() );
            xlsrow.CreateCell( 8 ).SetCellValue( Currency );
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "G. WEIGHT(KG)毛重:" );
            xlsrow.CreateCell( 1 ).SetCellValue("");
            xlsrow.CreateCell( 2 ).SetCellValue( sumGROSS_WEIGHT.ToString()+"KG" );
            xlsrow.CreateCell( 3 ).SetCellValue( "shipping mark：" );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "N. WEIGHT(KG)净重:" );
            xlsrow.CreateCell( 1 ).SetCellValue( "" );
            xlsrow.CreateCell( 2 ).SetCellValue( sumNET_WEIGHT.ToString( )+"KG" );
            xlsrow.CreateCell( 3 ).SetCellValue( ShippingMark );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "TOTAL CARTONS总箱数:" );
            xlsrow.CreateCell( 1 ).SetCellValue( "" );
            xlsrow.CreateCell( 2 ).SetCellValue( sumParcel_Number.ToString( )+"parcels箱" );
            xlsrow.CreateCell( 3 ).SetCellValue( "To:"+Destination );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 0 ).SetCellValue( "TOTAL VOLUME总体积:" );
            xlsrow.CreateCell( 1 ).SetCellValue( "" );
            xlsrow.CreateCell( 2 ).SetCellValue( sumVolume.ToString( )+"M3" );
            rowIndex++;
            rowIndex++;
            rowIndex++;
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 5 ).SetCellValue( "Etienne Callafe" );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 5 ).SetCellValue( "经理" );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 5 ).SetCellValue( "MANAGER" );

        }
        private static void CreateCustomsDeclarationSheet( ref HSSFWorkbook hssfworkbook , ref int i , DataTable sDT,DataTable schDT,ref int indexX,
            ArrayList shippingNumbers ,string BuyerCN , string BuyerEN , string AddressCN , string AddressEN , string TEL , string FAX , string ShipmentDate , string ShipmentType ,
            string LoadingPort , string DeliveryPort , string PaymentTerm , string DeliveryCountryCN , string DeliveryCountryEN , string DomesticSources ,
            string PackingSpecifications , string Incoterm , string TransportMode , string ShippingMark , string Destination , string Currency
            )
        {
            int rowIndex = 3;//从第二行开始，因为前两行是模板里面的内容 
            //int colIndex = 0;
            string sheetname="报关单"+i.ToString( )+"-"+indexX.ToString( );
            ISheet sheet1 = hssfworkbook.CreateSheet( sheetname );
            //sheet1.AutoSizeColumn( 1 , true );
            //sheet1.AutoSizeColumn( 2 , true );
            //sheet1.AutoSizeColumn( 3 , true ); 
            sheet1.SetColumnWidth( 1 , 30 * 256 );
            sheet1.SetColumnWidth( 2 , 20 * 256 );
            sheet1.SetColumnWidth( 3 , 20 * 256 );
            //HSSFRow rowHead = (HSSFRow)sheet1.CreateRow( 0 );
            //sheet1.AddMergedRegion( new CellRangeAddress( 0 , 0 , 0 , 8 ) );
            //ICell cell = rowHead.CreateCell( 0 );//在行中添加一列
            //cell.SetCellValue( "INVOICE" );//设置列的内容
            //cell.CellStyle=GetCellStyle( hssfworkbook , GetFontStyle( hssfworkbook , "华文彩云" , HSSFColor.SKY_BLUE.index , 16 ) , HSSFColor.WHITE.index , FillPatternType.SOLID_FOREGROUND , HSSFColor.SKY_BLUE.index , HorizontalAlignment.CENTER , VerticalAlignment.CENTER );
            ////setCellStyle( hssfworkbook , cell );
            HSSFRow xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 2 ).SetCellValue(LoadingPort);
            xlsrow.CreateCell( 7 ).SetCellValue( ShipmentDate );
            //sheet1.AddMergedRegion( new CellRangeAddress( 1 , 1 , 0 , 8 ) );
            //cell = xlsrow.CreateCell( 0 );//在行中添加一列
            //cell.SetCellValue( "发票" );//设置列的内容
            //cell.CellStyle=GetCellStyle( hssfworkbook , GetFontStyle( hssfworkbook , "宋体" , HSSFColor.BLACK.index , 22 ) , HSSFColor.WHITE.index , FillPatternType.SOLID_FOREGROUND , HSSFColor.WHITE.index , HorizontalAlignment.CENTER , VerticalAlignment.CENTER );
            //setCellStyle( hssfworkbook , cell );
            rowIndex++;
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 0 , 0 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 1 , 4 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 0 , 6 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 7 , 10 ) );
            xlsrow.CreateCell( 3 ).SetCellValue( TransportMode);
            //xlsrow.CreateCell( 1 ).SetCellValue( "迪卡侬（昆山）仓储有限公司" );
            //xlsrow.CreateCell( 8 ).SetCellValue( "FOR EXPORT" );
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 0 , 0 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 1 , 4 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 0 , 6 ) );
            //sheet1.AddMergedRegion( new CellRangeAddress( 3 , 3 , 7 , 10 ) );
            xlsrow.CreateCell( 3 ).SetCellValue( "一般贸易" );
            xlsrow.CreateCell( 10 ).SetCellValue( PaymentTerm );
            //xlsrow.CreateCell( 8 ).SetCellValue( "出口专用" );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 3 ).SetCellValue( DeliveryCountryEN );
            xlsrow.CreateCell( 7 ).SetCellValue( DeliveryPort );
            xlsrow.CreateCell( 10 ).SetCellValue( DomesticSources );

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 2 ).SetCellValue( Incoterm );
            //xlsrow.CreateCell( 1 ).SetCellValue( "No.3204604052" );
            int sumParcel_Number=0;
            decimal sumGROSS_WEIGHT=0;
            decimal sumNET_WEIGHT=0;
            foreach ( DataRow row in schDT.Rows )
            {
                //sumParcel_Number+=int.Parse( row["CartonQty"].ToString( ) );
                sumGROSS_WEIGHT+=decimal.Parse( row["GROSS_WEIGHT"].ToString( ) );
                sumNET_WEIGHT+=decimal.Parse( row["NET_WEIGHT"].ToString( ) );
            }
            sumParcel_Number=sDT.Select( "BillNO="+i ).AsEnumerable( ).Sum( p => p.Field<int>( "CartonQty" ) );
            sumGROSS_WEIGHT=sDT.Select( "BillNO="+i ).AsEnumerable( ).Sum( p => p.Field<decimal>( "GW" ) );
            sumNET_WEIGHT=sDT.Select( "BillNO="+i ).AsEnumerable( ).Sum( p => p.Field<decimal>( "NW" ) );  
            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 2 ).SetCellValue( sumParcel_Number.ToString() );
            xlsrow.CreateCell( 6 ).SetCellValue( PackingSpecifications );
            xlsrow.CreateCell( 7 ).SetCellValue( sumGROSS_WEIGHT.ToString() );
            xlsrow.CreateCell( 10 ).SetCellValue( sumNET_WEIGHT.ToString() );
            rowIndex++;
            rowIndex++;
            rowIndex++;
            rowIndex++;
            rowIndex++;
            rowIndex++;
            rowIndex++;
            rowIndex++;

            int indexY=(indexX-1)*5;
            int sumQUANTITY=0;
            decimal sumTOTAL_VALUE=0;
            foreach ( DataRow row in schDT.Rows )
            {
                indexY++;
                xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
                xlsrow.CreateCell( 0 ).SetCellValue( indexY );
                xlsrow.CreateCell( 1 ).SetCellValue( row["HS_CODE_(IN_CAT)"].ToString());
                xlsrow.CreateCell( 2 ).SetCellValue( row["中文品名"].ToString( ) );
                xlsrow.CreateCell( 3 ).SetCellValue( row["英文品名"].ToString( ) );
                xlsrow.CreateCell( 4 ).SetCellValue( row["QUANTITY"].ToString( ) );
                sumQUANTITY+=int.Parse( row["QUANTITY"].ToString( ) );
                xlsrow.CreateCell( 5 ).SetCellValue( row["法定计量单位"].ToString( ) );
                xlsrow.CreateCell( 6 ).SetCellValue( DeliveryCountryCN );
                xlsrow.CreateCell( 7 ).SetCellValue( row["NET_WEIGHT"].ToString( ) );
                xlsrow.CreateCell( 8 ).SetCellValue( Currency );
                xlsrow.CreateCell( 10 ).SetCellValue( row["TOTAL_VALUE"].ToString( ) );
                sumTOTAL_VALUE+=decimal.Parse( row["TOTAL_VALUE"].ToString( ) );
                xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
                //xlsrow.CreateCell( 0 ).SetCellValue( "地址: " );
                xlsrow.CreateCell( 1 ).SetCellValue( row["申报要素"].ToString( ) );
                rowIndex++;
            }

            xlsrow = (HSSFRow)sheet1.CreateRow( rowIndex++ );
            xlsrow.CreateCell( 3 ).SetCellValue( "TOTAL " );
            xlsrow.CreateCell( 4 ).SetCellValue( sumQUANTITY.ToString() );
            xlsrow.CreateCell( 10 ).SetCellValue( sumTOTAL_VALUE.ToString() );
        }
        /// <summary>
        /// 设置单元格样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="cell"></param>
        private static void setCellStyle( HSSFWorkbook workbook , ICell cell )
        {
            HSSFCellStyle fCellStyle = (HSSFCellStyle)workbook.CreateCellStyle( );
            HSSFFont ffont = (HSSFFont)workbook.CreateFont( );
            ffont.FontHeight = 20 * 20;
            ffont.FontName = "宋体";
            ffont.Color = HSSFColor.RED.index;
            fCellStyle.SetFont( ffont );
            fCellStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            fCellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PINK.index;
            
            fCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;//垂直对齐
            fCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;//水平对齐
            cell.CellStyle = fCellStyle;
        }
        /// <summary>
        /// 获取字体样式
        /// </summary>
        /// <param name="hssfworkbook">Excel操作类</param>
        /// <param name="fontname">字体名</param>
        /// <param name="fontcolor">字体颜色</param>
        /// <param name="fontsize">字体大小</param>
        /// <returns></returns>
        public static IFont GetFontStyle( HSSFWorkbook hssfworkbook , string fontfamily , short fontcolor , int fontsize )
        {
            IFont font1 = hssfworkbook.CreateFont( );
            if ( string.IsNullOrEmpty( fontfamily ) )
            {
                font1.FontName = fontfamily;
            }
            if ( fontcolor != null )
            {
                font1.Color = fontcolor;
            }
            font1.IsItalic = false;
            font1.Boldweight = (short)FontBoldWeight.BOLD;//字体加粗样式
            font1.FontHeightInPoints = (short)fontsize;
            return font1;
        }
        /// <summary>
        /// 获取单元格样式
        /// </summary>
        /// <param name="hssfworkbook">Excel操作类</param>
        /// <param name="font">单元格字体</param>
        /// <param name="fillForegroundColor">图案的颜色</param>
        /// <param name="fillPattern">图案样式</param>
        /// <param name="fillBackgroundColor">单元格背景</param>
        /// <param name="ha">垂直对齐方式</param>
        /// <param name="va">垂直对齐方式</param>
        /// <returns></returns>
        public static ICellStyle GetCellStyle( HSSFWorkbook hssfworkbook , IFont font , short fillForegroundColor , FillPatternType fillPattern , short fillBackgroundColor , HorizontalAlignment ha , VerticalAlignment va )
        {
            ICellStyle cellstyle = hssfworkbook.CreateCellStyle( );
            cellstyle.FillPattern = fillPattern;
            cellstyle.Alignment = ha;
            cellstyle.VerticalAlignment = va;
            if ( fillForegroundColor != null )
            {
                cellstyle.FillForegroundColor = fillForegroundColor;
            }
            if ( fillBackgroundColor != null )
            {
                cellstyle.FillBackgroundColor = fillBackgroundColor;
            }
            if ( font != null )
            {
                cellstyle.SetFont( font );
            }
            //有边框
            //cellstyle.BorderBottom = CellBorderType.THIN;
            //cellstyle.BorderLeft = CellBorderType.THIN;
            //cellstyle.BorderRight = CellBorderType.THIN;
            //cellstyle.BorderTop = CellBorderType.THIN;
            return cellstyle;
        }
        
    }

}
