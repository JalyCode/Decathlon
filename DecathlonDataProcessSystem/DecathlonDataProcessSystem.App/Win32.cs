using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DecathlonDataProcessSystem.App
{
    public class Win32
    {
        public const Int32 AW_HOR_POSITIVE = 0x00000001; // �����Ҵ򿪴���
        public const Int32 AW_HOR_NEGATIVE = 0x00000002; // ���ҵ���򿪴���
        public const Int32 AW_VER_POSITIVE = 0x00000004; // ���ϵ��´򿪴���
        public const Int32 AW_VER_NEGATIVE = 0x00000008; // ���µ��ϴ򿪴���
        public const Int32 AW_CENTER = 0x00000010; //��ʹ����AW_HIDE��־����ʹ���������ص�����δʹ��AW_HIDE��־����ʹ����������չ��
        public const Int32 AW_HIDE = 0x00010000; //���ش��ڣ�ȱʡ����ʾ���ڡ�
        public const Int32 AW_ACTIVATE = 0x00020000; //����ڡ���ʹ����AW_HIDE��־��Ҫʹ�������־��
        public const Int32 AW_SLIDE = 0x00040000; //ʹ�û������͡�ȱʡ��Ϊ�����������͡���ʹ��AW_CENTER��־ʱ�������־�ͱ����ԡ�
        public const Int32 AW_BLEND = 0x00080000; //ʹ�õ���Ч����ֻ�е�hWndΪ���㴰�ڵ�ʱ��ſ���ʹ�ô˱�־��
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool AnimateWindow(
          IntPtr hwnd, // handle to window 
          int dwTime, // duration of animation 
          int dwFlags // animation type 
          );
        /// <summary>
        /// ҳ�����
        /// </summary>
        public static void SetMid(Form form)
        {
            // Center the Form on the user's screen everytime it requires a Layout.
            form.SetBounds((Screen.GetBounds(form).Width / 2) - (form.Width / 2),
                (Screen.GetBounds(form).Height / 2) - (form.Height / 2),
                form.Width, form.Height, BoundsSpecified.Location);
        }
    }

}
