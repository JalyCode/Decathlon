using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using DecathlonDataProcessSystem.DAL;

namespace DecathlonDataProcessSystem.BLL
{
    public class CommonBLL
    {
        public static string GetDate( string strFormat )
        {
            return CommonDAL.GetDate( strFormat );
        }

        public static void SetLocalSystemDateTime( )
        {
            DateTime t = Convert.ToDateTime( CommonBLL.GetDate( "yyyy-MM-dd HH:mm:ss" ) );
            CommonBLL.SystemTime s = new CommonBLL.SystemTime( );
            s.wYear = Convert.ToUInt16( t.Year );
            s.wMonth = Convert.ToUInt16( t.Month );
            s.wDay = Convert.ToUInt16( t.Day );
            s.wHour = Convert.ToUInt16( t.Hour );
            s.wMinute = Convert.ToUInt16( t.Minute );
            s.wSecond = Convert.ToUInt16( t.Second );
            CommonBLL.SetLocalTime( ref s );
        }

        [StructLayout( LayoutKind.Sequential )]
        private struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMiliseconds;
        }

        [DllImport( "Kernel32.dll" )]
        private static extern bool SetLocalTime( ref SystemTime sysTime );

        private static bool SetLocalTimeByStr( string timestr )
        {
            bool flag = false;
            SystemTime sysTime = new SystemTime( );
            DateTime dt = Convert.ToDateTime( timestr );
            sysTime.wYear = Convert.ToUInt16( dt.Year );
            sysTime.wMonth = Convert.ToUInt16( dt.Month );
            sysTime.wDay = Convert.ToUInt16( dt.Day );
            sysTime.wHour = Convert.ToUInt16( dt.Hour );
            sysTime.wMinute = Convert.ToUInt16( dt.Minute );
            sysTime.wSecond = Convert.ToUInt16( dt.Second );
            try
            {
                flag = SetLocalTime( ref sysTime );
            }
            catch ( Exception e )
            {
                Console.WriteLine( "SetSystemDateTime函数执行异常" + e.Message );
            }
            return flag;
        }


    }
}
