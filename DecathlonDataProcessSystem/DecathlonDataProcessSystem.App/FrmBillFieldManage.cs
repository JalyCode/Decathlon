using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DecathlonDataProcessSystem.App
{
    public partial class FrmBillFieldManage : Form
    {
        public FrmBillFieldManage( )
        {
            InitializeComponent( );
            this.cboBuyerForCN.SelectedIndex=0;
        }
        //private void initControls( )
        //{
        //    this.cboBuyerForCN.SelectedIndex=0;
        //    //cboBuyerForCN
        //}
        private void btnOK_Click( object sender , EventArgs e )
        {
            FrmMain main=(FrmMain)this.Owner;
            main.BuyerCN=this.cboBuyerForCN.SelectedItem.ToString( );
            main.BuyerEN=this.txtBuyerForEN.Text.Trim( );
            main.AddressCN=this.txtAddressForCN.Text.Trim( );
            main.AddressEN=this.txtAddressForEN.Text.Trim( );
            main.TEL=this.txtTel.Text.Trim( );
            main.FAX=this.txtFAX.Text.Trim( );
            main.ShipmentDate=this.dtShipmentDate.Value.ToShortDateString( );
            main.ShipmentType=this.txtShipmentType.Text.Trim( );
            main.LoadingPort=this.cboShippingPort.SelectedItem.ToString( );
            main.DeliveryPort=this.cboDeliveryPort.SelectedItem.ToString( );
            main.PaymentTerm=this.txtPaymentTerm.Text.Trim( );
            main.DeliveryCountryCN=this.txtDeliveryCountryCN.Text.Trim( );
            main.DeliveryCountryEN=this.txtDeliveryCountryEN.Text.Trim( );
            main.DomesticSources=this.txtDomesticSources.Text.Trim( );
            main.PackingSpecifications=this.txtPackingSpecifications.Text.Trim( );
            main.Incoterm=this.cboIncoterm.SelectedItem.ToString( );
            main.TransportMode=this.txtTransportMode.Text.Trim( );
            main.ShippingMark=this.txtShippingMark.Text.Trim( );
            main.Destination=this.txtDestination.Text.Trim( );
            main.Currency=this.cboCurrency.SelectedItem.ToString( );
            this.Close( );
        }

        private void btnCancel_Click( object sender , EventArgs e )
        {
            this.Close( );
        }

        private void cboBuyerForCN_SelectedIndexChanged( object sender , EventArgs e )
        {
            if ( this.cboBuyerForCN.SelectedItem.ToString( )=="台湾迪卡侬有限公司" )
            {
                this.txtBuyerForEN.Text="DECATHLON TAIWAN CO., LTD";
                this.txtAddressForCN.Text="台中市南屯区永春东一路812号";
                this.txtAddressForEN.Text="812 YUNG CHUN EAST 1ST ROAD, - NAN TUN DISTRICT,TAICHUNG，TAIWAN.";
                this.txtTel.Text="+886-4-23748140";
                this.txtFAX.Text="+886-4-23833810";
                this.cboShippingPort.Items.Clear();
                this.cboShippingPort.Items.Add("TAICANG");
                this.cboShippingPort.Items.Add("SHANGHAI");
                this.cboShippingPort.SelectedIndex=0;
                this.cboDeliveryPort.Items.Clear( );
                this.cboDeliveryPort.Items.Add( "TAICHUNG" );
                this.cboDeliveryPort.Items.Add( "KEELUNG" );
                this.cboDeliveryPort.SelectedIndex=0;
                this.txtDeliveryCountryEN.Text="TAIWAN";
                this.txtDeliveryCountryCN.Text="台湾";
                this.txtDestination.Text="TW";
                this.txtShippingMark.Text="DECATHLON TAIWAN CO., LTD";
                this.cboIncoterm.SelectedIndex=0;
                this.cboCurrency.SelectedIndex=0;
            }
            else if ( this.cboBuyerForCN.SelectedItem.ToString( )=="韩国迪卡侬有限公司" )
            {
                this.txtBuyerForEN.Text="Blue sports co ltd";
                this.txtAddressForCN.Text="KTBD4F, 19gil9 Yeoksamdong,Gangnamgu, Seoul Korea";
                this.txtAddressForEN.Text="KTBD4F, 19gil9 Yeoksamdong,Gangnamgu, Seoul Korea";
                this.txtTel.Text="82 2 538 5716";
                this.txtFAX.Text="";
                this.cboShippingPort.Items.Clear( );
                this.cboShippingPort.Items.Add( "TAICANG" );
                this.cboShippingPort.Items.Add( "SHANGHAI" );
                this.cboShippingPort.SelectedIndex=0;
                this.cboDeliveryPort.Items.Clear( );
                this.cboDeliveryPort.Items.Add( "INCHEON" );
                this.cboDeliveryPort.SelectedIndex=0;
                this.txtDeliveryCountryEN.Text="Korea";
                this.txtDeliveryCountryCN.Text="韩国";
                this.txtDestination.Text="KR";
                this.txtShippingMark.Text="Blue sports co ltd";
                this.cboIncoterm.SelectedIndex=0;
                this.cboCurrency.SelectedIndex=0;
            }
            else if ( this.cboBuyerForCN.SelectedItem.ToString( )=="日本迪卡侬有限公司" )
            {
                this.txtBuyerForEN.Text="NATURUM-ECOMMERCE CO.,LTD.";
                this.txtAddressForCN.Text="OE BLDG 10F, 1-1-22 NONINBASHI, CHUO-KU, OSAKA,JAPAN";
                this.txtAddressForEN.Text="OE BLDG 10F, 1-1-22 NONINBASHI, CHUO-KU, OSAKA,JAPAN";
                this.txtTel.Text="06-6910-6634";
                this.txtFAX.Text="06-6910-0040";
                this.cboShippingPort.Items.Clear( );
                this.cboShippingPort.Items.Add( "TAICANG" );
                this.cboShippingPort.Items.Add( "SHANGHAI" );
                this.cboShippingPort.SelectedIndex=0;
                this.cboDeliveryPort.Items.Clear( );
                this.cboDeliveryPort.Items.Add( "OSAKA" );
                this.cboDeliveryPort.SelectedIndex=0;
                this.txtDeliveryCountryEN.Text="Japan";
                this.txtDeliveryCountryCN.Text="日本";
                this.txtDestination.Text="JP";
                this.txtShippingMark.Text="NATURUM-ECOMMERCE CO.,LTD.";
                this.cboIncoterm.SelectedIndex=0;
                this.cboCurrency.SelectedIndex=0;
            }
        }
    }
}
