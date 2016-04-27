using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DecathlonDataProcessSystem.App
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main( )
        {
            Application.EnableVisualStyles( );
            Application.SetCompatibleTextRenderingDefault( false );
            //string strtemp="AGE 3 / 98 CMS";
            //string[] ssreresf=strtemp.Split( new[] { "AGE" } , StringSplitOptions.RemoveEmptyEntries );

            //string strtemp11="EU 24-25  UK C7-8";
            //string[] ssreresf11=strtemp11.Split( new[] { "MONTHS" } , StringSplitOptions.RemoveEmptyEntries );
            //string sss=   Regex.Replace( strtemp , "[a-z]" , "" , RegexOptions.IgnoreCase );  
            //string[] ss=strtemp.Split( new[] { "AGE" } , StringSplitOptions.RemoveEmptyEntries );
            //var outputNumber1 = strtemp.Replace( "EU" , "/" ).Trim( ).Split( '/' );
            ////strSize.Split( new[] { "AGE" } , StringSplitOptions.RemoveEmptyEntries )
            //string sss33333="EU 39 UK 5.5 US 6";
            //var outputNumber2324 =sss33333.Split( new[] { "EU" } , StringSplitOptions.RemoveEmptyEntries );
            //var outputNumber232456 =outputNumber2324[0].Split( new[] { "UK" } , StringSplitOptions.RemoveEmptyEntries );

            //var outputNumber = strtemp.Replace( "EU" , " " ).Replace( "UK" , " " ).Replace( "US" , " " ).Trim( ).Split( ' ' );
            //strtemp   =   Regex.Replace( strtemp , " " , "/" , RegexOptions.IgnoreCase );
            //strtemp   =   Regex.Replace( strtemp , " " , "" , RegexOptions.IgnoreCase );
            Application.Run( new FrmMain( ) );
        }
    }
}
