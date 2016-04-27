using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using DecathlonDataProcessSystem.BLL;
using DecathlonDataProcessSystem.Model;

namespace DecathlonDataProcessSystem.App
{
    public partial class FrmLogin : Form
    {
        public static string UserId;
        public static int LoginId;
        public static string StrIP;
        public static string StrMAC;
        private EmployeeLoginBLL elBLL = new EmployeeLoginBLL( );
        private EmployeeBLL bll = new EmployeeBLL( );
        public FrmLogin( )
        {
            InitializeComponent( );
        }

        private void txtPassword_KeyDown( object sender , KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Enter )
            {
                this.btnLogin.PerformClick( );
            }
        }

        private void btnLogin_Click( object sender , EventArgs e )
        {
            try
            {
                CommonBLL.SetLocalSystemDateTime( );
                bool flag = false;
                string strUser = txtUserNo.Text.Trim( );
                string strPassword = txtPassword.Text.Trim( );
                if ( strUser.Length == 0 )
                {

                    MessageBox.Show( "工号不能为空！" , "系统提示" , MessageBoxButtons.OK , MessageBoxIcon.Question );
                    this.txtUserNo.Focus( );
                    return;
                }
                if ( strPassword.Length == 0 )
                {
                    MessageBox.Show( "密码不能为空！" , "系统提示" , MessageBoxButtons.OK , MessageBoxIcon.Question );
                    this.txtPassword.Focus( );
                    return;
                }
                flag = bll.EmployeeExists( strUser );
                if ( !flag )
                {
                    MessageBox.Show( "工号输入错误！" , "系统提示" , MessageBoxButtons.OK , MessageBoxIcon.Question );
                    txtUserNo.Select( 0 , txtUserNo.Text.Trim( ).Length );
                    this.txtUserNo.Focus( );
                    return;
                }
                flag = bll.ValidateEmployee( strUser , strPassword );
                if ( !flag )
                {

                    MessageBox.Show( "密码输入错误！" , "系统提示" , MessageBoxButtons.OK , MessageBoxIcon.Question );
                    txtPassword.Select( 0 , txtPassword.Text.Trim( ).Length );
                    this.txtPassword.Focus( );
                    return;
                }
                DataSet dsEmployeeInfo = elBLL.GetDsEmployeeInfo( strUser );
                if ( dsEmployeeInfo.Tables[0].Rows.Count > 0 )
                {
                    UserId = dsEmployeeInfo.Tables[0].Rows[0]["EmployeeId"].ToString( );
                }
                elBLL.DeleteEmployeeLogin( UserId , StrIP );//将非法退出的记录进行删除
                DataTable dt = elBLL.GetEmployeeLoginByEmployeeId( UserId ).Tables[0];
                DateTime curDT = DateTime.Parse( CommonBLL.GetDate( "yyyy-MM-dd HH:mm:ss" ) );
                bool flag1 = false;
                if ( dt.Rows.Count > 0 )
                {
                    foreach ( DataRow row in dt.Rows )
                    {
                        if ( bool.Parse( row["Flag"].ToString( ) ) )
                        {
                            DateTime tempdt = DateTime.Parse( row["CurrentTime"].ToString( ) );
                            if ( curDT > tempdt )
                            {
                                TimeSpan ts = curDT - tempdt;
                                //ts.TotalSeconds
                                //DateTime diffDT = curDT.Subtract(tempdt);
                                if ( ts.TotalSeconds >= 0 && ts.TotalSeconds <= 60 )
                                {
                                    flag1 = true;
                                }
                            }
                            else
                            {
                                flag1 = true;
                            }
                        }
                        else
                        {
                            DateTime tempdt = DateTime.Parse( row["LoginTime"].ToString( ) );
                            if ( curDT > tempdt )
                            {
                                TimeSpan ts = curDT - tempdt;
                                //ts.TotalSeconds
                                //DateTime diffDT = curDT.Subtract(tempdt);
                                if ( ts.TotalSeconds >= 0 && ts.TotalSeconds <= 60 )
                                {
                                    flag1 = true;
                                }
                            }
                            else
                            {
                                flag1 = true;
                            }
                        }

                    }
                }
                if ( flag1 )
                {
                    MessageBox.Show( "该用户正在登陆系统，该系统不允许重复登陆！" , "系统提示" , MessageBoxButtons.OK , MessageBoxIcon.Question );
                    this.Dispose( );
                    Application.Exit( );
                    return;
                }
                else
                {
                    EmployeeLoginEntity elEntity = new EmployeeLoginEntity( );
                    StrMAC = "1111111111111111111";
                    elEntity.EmployeeId = UserId;
                    elEntity.IpAddress = StrIP;
                    elEntity.MacAddress = StrMAC;
                    elEntity.LoginTime = DateTime.Parse( CommonBLL.GetDate( "yyyy-MM-dd HH:mm:ss" ) );
                    elEntity.Flag = false;
                    LoginId = elBLL.AddEmployeeLogin( elEntity );
                    FrmMain main = new FrmMain( );
                    this.Hide( );
                    main.Show( );
                    return;
                }

                //FrmMain main = new FrmMain();
                //this.Hide();
                //main.Show();
            }
            catch ( Exception Err )
            {
                MessageBox.Show( "无法连接服务器，请联系管理员.\n" + Err.Message , "系统提示" );
                Environment.Exit( 0 );
            }
        }

        private void FrmLogin_Load( object sender , EventArgs e )
        {

            Win32.SetMid( this );
            MachineInformation mainfo = new MachineInformation( );
            StrIP = mainfo.getLocalIP( );
            StrMAC = mainfo.getLocalMac( );
            this.txtUserNo.Focus( );
        }


        private void FrmLogin_FormClosing( object sender , FormClosingEventArgs e )
        {
            Environment.Exit( 0 );
        }
    }
}
