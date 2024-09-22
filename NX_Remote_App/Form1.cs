using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NX_Remote_App
{


   
    public partial class Form1 : Form
    {
        private int _ElapsedTime = 0;
        private System.Drawing.Rectangle _ScreenSize;

        public Form1()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct KeyboardInput
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct MouseInput
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct HardwareInput
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
        public struct InputUnion
        {
            [System.Runtime.InteropServices.FieldOffset(0)] public MouseInput mi;
            [System.Runtime.InteropServices.FieldOffset(0)] public KeyboardInput ki;
            [System.Runtime.InteropServices.FieldOffset(0)] public HardwareInput hi;
        }
        public struct Input
        {
            public int type;
            public InputUnion u;
        }

        [System.Flags]
        public enum InputType
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        [System.Flags]
        public enum KeyEventF
        {
            KeyDown = 0x0000,
            ExtendedKey = 0x0001,
            KeyUp = 0x0002,
            Unicode = 0x0004,
            Scancode = 0x0008
        }

        [System.Flags]
        public enum MouseEventF
        {
            Absolute = 0x8000,
            HWheel = 0x01000,
            Move = 0x0001,
            MoveNoCoalesce = 0x2000,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            VirtualDesk = 0x4000,
            Wheel = 0x0800,
            XDown = 0x0080,
            XUp = 0x0100
        }

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.text1;
            textBox2.Text = Properties.Settings.Default.text2;
            textBox3.Text = Properties.Settings.Default.text3;
            textBox4.Text = Properties.Settings.Default.text4;
            textBox5.Text = Properties.Settings.Default.text5;

            timer1.Interval = 1000;
            //timer1.Enabled = true;
            
            timer2.Interval = 100;
            timer2.Enabled = true;

            timer3.Interval = 1000;
            timer3.Enabled = true;

            _ScreenSize = Screen.PrimaryScreen.Bounds;
            richTextBox1.AppendText("Screen Size=" + _ScreenSize.ToString() + System.Environment.NewLine);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _ElapsedTime = _ElapsedTime + timer1.Interval;

            //SetCursorPos(100, 100);

            if (_ElapsedTime >= 9000)
            {
                timer1.Enabled = false;
            }
            else if (_ElapsedTime >= 8000 & _ElapsedTime < 9000)
            {
                try
                {
                    System.String[] dummy1 = textBox5.Text.Split(',');
                    System.Drawing.Point p1 = new System.Drawing.Point(System.Convert.ToInt32(dummy1[0]), System.Convert.ToInt32(dummy1[1]));
                    funClickOnPoint(p1); //clicks on nx shortcut
                }
                catch (System.Exception ex)
                {
                    richTextBox1.AppendText(ex.Message);
                    richTextBox1.AppendText(System.Environment.NewLine);
                }

            }
            else if (_ElapsedTime >= 7000 & _ElapsedTime < 8000)
            {
                try
                {
                    System.String[] dummy1 = textBox4.Text.Split(',');
                    System.Drawing.Point p1 = new System.Drawing.Point(System.Convert.ToInt32(dummy1[0]), System.Convert.ToInt32(dummy1[1]));
                    funClickOnPoint(p1); //clicks on start button
                }
                catch (System.Exception ex)
                {
                    richTextBox1.AppendText(ex.Message);
                    richTextBox1.AppendText(System.Environment.NewLine);
                }
                
            }
            else if (_ElapsedTime >= 6000 & _ElapsedTime < 7000)
            {
                try
                {
                    System.String[] dummy1 = textBox3.Text.Split(',');
                    System.Drawing.Point p1 = new System.Drawing.Point(System.Convert.ToInt32(dummy1[0]), System.Convert.ToInt32(dummy1[1]));
                    funClickOnPoint(p1); //clicks on connect button
                }
                catch (System.Exception ex)
                {
                    richTextBox1.AppendText(ex.Message);
                    richTextBox1.AppendText(System.Environment.NewLine);
                }

            }
            else if(_ElapsedTime >= 5000 & _ElapsedTime < 6000)
            {
                try
                {
                    System.String[] dummy1 = textBox2.Text.Split(',');
                    System.Drawing.Point p1 = new System.Drawing.Point(System.Convert.ToInt32(dummy1[0]), System.Convert.ToInt32(dummy1[1]));
                    funClickOnPoint(p1); //clicks on treenode
                }
                catch (System.Exception ex)
                {
                    richTextBox1.AppendText(ex.Message);
                    richTextBox1.AppendText(System.Environment.NewLine);
                }
            }
            else if(_ElapsedTime >= 2000 & _ElapsedTime < 3000)
            {
                try
                {
                    System.String[] dummy = textBox1.Text.Split(',');
                    System.Drawing.Point p1 = new System.Drawing.Point(System.Convert.ToInt32(dummy[0]), System.Convert.ToInt32(dummy[1]));

                    funDoubleClickOnPoint(p1); //double clicks app shortcut
                }
                catch(System.Exception ex)
                {
                    richTextBox1.AppendText(ex.Message);
                    richTextBox1.AppendText(System.Environment.NewLine);
                }
                
            }
        }

        private void funPressKeyboardButton(ushort key)
        {
            Input[] inputs = new Input[2];

            inputs[0] = new Input();
            inputs[0].type = (int)InputType.Keyboard;
            inputs[0].u = new InputUnion();
            inputs[0].u.ki = new KeyboardInput();
            inputs[0].u.ki.wVk = key;
            inputs[0].u.ki.wScan = 0;
            inputs[0].u.ki.dwFlags = (uint)(KeyEventF.KeyDown);
            inputs[0].u.ki.dwExtraInfo = GetMessageExtraInfo();

            inputs[1] = new Input();
            inputs[1].type = (int)InputType.Keyboard;
            inputs[1].u = new InputUnion();
            inputs[1].u.ki = new KeyboardInput();
            inputs[1].u.ki.wVk = key;
            inputs[1].u.ki.wScan = 0;
            inputs[1].u.ki.dwFlags = (uint)(KeyEventF.KeyUp);
            inputs[1].u.ki.dwExtraInfo = GetMessageExtraInfo();

            SendInput((uint)inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Input)));
        }

        private void funPressDoubleKeyboardButton(ushort key1, ushort key2)
        {
            Input[] inputs = new Input[4];

            inputs[0] = new Input();
            inputs[0].type = (int)InputType.Keyboard;
            inputs[0].u = new InputUnion();
            inputs[0].u.ki = new KeyboardInput();
            inputs[0].u.ki.wVk = key1;
            inputs[0].u.ki.wScan = 0;
            inputs[0].u.ki.dwFlags = (uint)(KeyEventF.KeyDown);
            inputs[0].u.ki.dwExtraInfo = GetMessageExtraInfo();

            inputs[1] = new Input();
            inputs[1].type = (int)InputType.Keyboard;
            inputs[1].u = new InputUnion();
            inputs[1].u.ki = new KeyboardInput();
            inputs[1].u.ki.wVk = key2;
            inputs[1].u.ki.wScan = 0;
            inputs[1].u.ki.dwFlags = (uint)(KeyEventF.KeyDown);
            inputs[1].u.ki.dwExtraInfo = GetMessageExtraInfo();

            inputs[2] = new Input();
            inputs[2].type = (int)InputType.Keyboard;
            inputs[2].u = new InputUnion();
            inputs[2].u.ki = new KeyboardInput();
            inputs[2].u.ki.wVk = key1;
            inputs[2].u.ki.wScan = 0;
            inputs[2].u.ki.dwFlags = (uint)(KeyEventF.KeyUp);
            inputs[2].u.ki.dwExtraInfo = GetMessageExtraInfo();

            inputs[3] = new Input();
            inputs[3].type = (int)InputType.Keyboard;
            inputs[3].u = new InputUnion();
            inputs[3].u.ki = new KeyboardInput();
            inputs[3].u.ki.wVk = key2;
            inputs[3].u.ki.wScan = 0;
            inputs[3].u.ki.dwFlags = (uint)(KeyEventF.KeyUp);
            inputs[3].u.ki.dwExtraInfo = GetMessageExtraInfo();

            SendInput((uint)inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Input)));
        }

        private void funDoubleClickOnPoint(System.Drawing.Point p)
        {
            System.Drawing.Point pn = funConvertToNormalizedCoords(p);

            Input[] inputs = new Input[4];

            inputs[0].type = (int)InputType.Mouse;
            inputs[0].u = new InputUnion();
            inputs[0].u.mi = new MouseInput();
            inputs[0].u.mi.dx = pn.X;
            inputs[0].u.mi.dy = pn.Y;
            inputs[0].u.mi.dwFlags = (uint)(MouseEventF.Move | MouseEventF.Absolute | MouseEventF.LeftDown);
            inputs[0].u.mi.dwExtraInfo = GetMessageExtraInfo();

            inputs[1].type = (int)InputType.Mouse;
            inputs[1].u = new InputUnion();
            inputs[1].u.mi = new MouseInput();
            inputs[1].u.mi.dwFlags = (uint)(MouseEventF.LeftUp);
            inputs[1].u.mi.dwExtraInfo = GetMessageExtraInfo();

            inputs[2].type = (int)InputType.Mouse;
            inputs[2].u = new InputUnion();
            inputs[2].u.mi = new MouseInput();
            inputs[2].u.mi.dx = pn.X;
            inputs[2].u.mi.dy = pn.Y;
            inputs[2].u.mi.dwFlags = (uint)(MouseEventF.Move | MouseEventF.Absolute | MouseEventF.LeftDown);
            inputs[2].u.mi.dwExtraInfo = GetMessageExtraInfo();

            inputs[3].type = (int)InputType.Mouse;
            inputs[3].u = new InputUnion();
            inputs[3].u.mi = new MouseInput();
            inputs[3].u.mi.dwFlags = (uint)(MouseEventF.LeftUp);
            inputs[3].u.mi.dwExtraInfo = GetMessageExtraInfo();

            SendInput((uint)inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Input)));
            richTextBox1.AppendText("Double clicked on p =" + p.ToString() + System.Environment.NewLine);
            richTextBox1.AppendText("Double clikced on pnorm = " + pn.ToString() + System.Environment.NewLine);

        }

        private void funClickOnPoint(System.Drawing.Point p)
        {

            System.Drawing.Point pn = funConvertToNormalizedCoords(p);

            Input[] inputs = new Input[4];

            inputs[0].type = (int)InputType.Mouse;
            inputs[0].u = new InputUnion();
            inputs[0].u.mi = new MouseInput();
            inputs[0].u.mi.dx = pn.X;
            inputs[0].u.mi.dy = pn.Y;
            inputs[0].u.mi.dwFlags = (uint)(MouseEventF.Move | MouseEventF.Absolute | MouseEventF.LeftDown);
            inputs[0].u.mi.dwExtraInfo = GetMessageExtraInfo();

            inputs[1].type = (int)InputType.Mouse;
            inputs[1].u = new InputUnion();
            inputs[1].u.mi = new MouseInput();
            inputs[1].u.mi.dwFlags = (uint)(MouseEventF.LeftUp);
            inputs[1].u.mi.dwExtraInfo = GetMessageExtraInfo();      

            SendInput((uint)inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Input)));
            richTextBox1.AppendText("Clicked on p =" + p.ToString() + System.Environment.NewLine);
            richTextBox1.AppendText("Clicked on pnorm = " + pn.ToString() + System.Environment.NewLine);

        }

        private System.Drawing.Point funConvertToNormalizedCoords(System.Drawing.Point p)
        {
            /* SendInput() function recives normalized mouse coordinates. When you add
           MOUSEEVENTF_ABSOLUTE to Flags, (0,0) is the upper-left corner, (65535,65535)
           is lower-right corner. */

            int resx = _ScreenSize.Width;
            int resy = _ScreenSize.Height;
            double normX = (double)(p.X) / (double)(resx) * 65535;
            double normY = (double)(p.Y) / (double)(resy) * 65535;

            System.Drawing.Point p1 = new System.Drawing.Point(System.Convert.ToInt32(normX), System.Convert.ToInt32(normY));

            return (p1);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            System.Drawing.Point pnt = System.Windows.Forms.Cursor.Position;
            label3.Text = pnt.X.ToString() + "," + pnt.Y.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.text1 = textBox1.Text;
            Properties.Settings.Default.text2 = textBox2.Text;
            Properties.Settings.Default.text3 = textBox3.Text;
            Properties.Settings.Default.text4 = textBox4.Text;
            Properties.Settings.Default.text5 = textBox5.Text;
            Properties.Settings.Default.Save();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //timer3.Enabled = false;

            _ElapsedTime = _ElapsedTime + timer1.Interval;

            if (_ElapsedTime >= 9000)
            {
                timer3.Enabled = false;
            }
            else if (_ElapsedTime >= 8000 & _ElapsedTime < 9000)
            {
                try
                {
                    funPressKeyboardButton(0x48);
                    funPressKeyboardButton(0x45);
                    funPressKeyboardButton(0x48);
                    funPressKeyboardButton(0x48);
                }
                catch (System.Exception ex)
                {
                    richTextBox1.AppendText(ex.Message);
                    richTextBox1.AppendText(System.Environment.NewLine);
                }

            }
            else if (_ElapsedTime >= 7000 & _ElapsedTime < 8000)
            {
                try
                {
                    System.IntPtr hWnd = FindWindow("Notepad", null);
                    SwitchToThisWindow(hWnd, true);
                }
                catch (System.Exception ex)
                {
                    richTextBox1.AppendText(ex.Message);
                    richTextBox1.AppendText(System.Environment.NewLine);
                }

            }
            else if (_ElapsedTime >= 6000 & _ElapsedTime < 7000)
            {
                try
                {
                    funPressKeyboardButton(0x4E);
                    funPressKeyboardButton(0x4F);
                    funPressKeyboardButton(0x54);
                    funPressKeyboardButton(0x45);
                    funPressKeyboardButton(0x50);
                    funPressKeyboardButton(0x41);
                    funPressKeyboardButton(0x44);
                    funPressKeyboardButton(0x0D);
                }
                catch (System.Exception ex)
                {
                    richTextBox1.AppendText(ex.Message);
                    richTextBox1.AppendText(System.Environment.NewLine);
                }

            }
            else if (_ElapsedTime >= 5000 & _ElapsedTime < 6000)
            {
                try
                {
                    System.IntPtr hWnd = FindWindow("Run", null);
                    SwitchToThisWindow(hWnd, true);
                }
                catch (System.Exception ex)
                {
                    richTextBox1.AppendText(ex.Message);
                    richTextBox1.AppendText(System.Environment.NewLine);
                }

            }
            else if (_ElapsedTime >= 4000 & _ElapsedTime < 5000)
            {
                try
                {
                    funPressDoubleKeyboardButton(0x5B, 0x52);
                }
                catch (System.Exception ex)
                {
                    richTextBox1.AppendText(ex.Message);
                    richTextBox1.AppendText(System.Environment.NewLine);
                }

            }
        }
    }
}
