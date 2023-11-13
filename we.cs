  using System;
  using System.IO;
  using System.Text;
  using System.Runtime.InteropServices;
  using System.Diagnostics;
  using System.Windows;
  using System.Windows.Forms;
  using System.Drawing;
  using System.Collections.Generic;
  using System.Threading;
  using Microsoft.VisualBasic;
//csc we.cs /Reference:Microsoft.VisualBasic.dll 
//TODO: set up SystemParametersInfo ( minimized...) around line 320 to make WM_SHELL messages work.
//TODO: make volume work.
  public static class Interop
  {
      public enum ShellEvents : int
      {
        HSHELL_WINDOWCREATED = 1,
        HSHELL_WINDOWDESTROYED = 2,
        HSHELL_ACTIVATESHELLWINDOW = 3,
        HSHELL_WINDOWACTIVATED = 4,
        HSHELL_GETMINRECT = 5,
        HSHELL_REDRAW = 6,
        HSHELL_TASKMAN = 7,
        HSHELL_LANGUAGE = 8,
        HSHELL_ACCESSIBILITYSTATE = 11,
        HSHELL_APPCOMMAND = 12
    }
    public enum ShowWindowCmdShow : uint
    {
      SW_FORCEMINIMIZE = 11,
      SW_HIDE = 0,
      SW_MAXIMIZE = 3,
      SW_MINIMIZE = 6,
      SW_RESTORE = 9,
      SW_SHOW = 5,
      SW_SHOWDEFAULT = 10,
      SW_SHOWMAXIMIZED = 3,
      SW_SHOWMINIMIZED = 2,
      SW_SHOWMINNOACTIVE = 7,
      SW_SHOWNA = 8, //SW_SHOW with no activate
      SW_SHOWNOACTIVATE = 4, //SW_SHOWNORMAL without activate
      SW_SHOWNORMAL = 1
    }
    public enum GetWindow_Cmd : uint
    {
        GW_HWNDFIRST = 0,
        GW_HWNDLAST = 1,
        GW_HWNDNEXT = 2,
        GW_HWNDPREV = 3,
        GW_OWNER = 4,
        GW_CHILD = 5,
        GW_ENABLEDPOPUP = 6
    }
    [DllImport("user32.dll", EntryPoint = "RegisterWindowMessageW", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern int RegisterWindowMessage(string lpString);
    [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern int DeregisterShellHookWindow(IntPtr hWnd);
    [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern int RegisterShellHookWindow(IntPtr hWnd);
    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    public static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32", EntryPoint = "GetWindowTextW", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern int GetWindowText(IntPtr hwnd, System.Text.StringBuilder lpString, int cch);
    [DllImport("user32", EntryPoint = "GetWindowTextW", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern int GetWindowText(int hwnd, System.Text.StringBuilder lpString, int cch);
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern bool SetWindowText(int hwnd, String lpString);
    [DllImport("user32", EntryPoint = "GetWindowTextLengthW", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern int GetWindowTextLength(IntPtr hwnd);
    [DllImport("user32.dll")]
    public static extern int SetActiveWindow(int hwnd) ;
    [DllImportAttribute("user32.dll")]
    public static extern int FindWindow(String ClassName, String WindowName);
    [DllImportAttribute("User32.dll")]
    public static extern IntPtr SetForegroundWindow(int hWnd);
    [DllImport("user32.dll")] 
    public static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam); 
    [DllImport("user32.dll")] 
    public static extern bool IsWindowVisible(IntPtr hWnd);  
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ShowWindow(int hWnd, uint nCmdShow);
    [DllImport("user32.dll")]
    public static extern int GetActiveWindow();
    [DllImport("user32.dll")]
    public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);
    [DllImport("user32.dll")]
    public static extern bool CloseWindow(int hWnd);
    
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PostMessage(int hWnd, uint Msg, int wParam, int lParam);
    
    [DllImport("user32.dll", SetLastError=true)]
    public static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
    
    [DllImport("user32.dll", SetLastError=true)]
    public static extern bool GetWindowRect(int hwnd, out RECT lpRect);
    
    [DllImport("user32.dll")]
    public static extern bool EnableMenuItem(int hMenu, uint uIDEnableItem, uint uEnable);

    [DllImport("user32.dll")]
    public static extern int GetSystemMenu(int hWnd, bool bRevert);

    [DllImport("user32.dll", SetLastError=true)]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);
    

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsIconic( int hWnd );
    
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SystemParametersInfo(
                                int uiAction,
                                int uiParam,
                                ref RECT pvParam,
                                int fWinIni);

    public const Int32 SPIF_SENDWININICHANGE = 2;
    public const Int32 SPIF_UPDATEINIFILE = 1;
    public const Int32 SPIF_change = SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE;
    public const Int32 SPI_SETWORKAREA = 47;
    public const Int32 SPI_GETWORKAREA = 48;
    public const Int32 SPI_SETMINIMIZEDMETRICS = 0x2C;
    public const Int32 SPI_GETMINIMIZEDMETRICS = 0x2B;

    public const int WM_SETICON = 0x80;
    
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
       public int Left;        // x position of upper-left corner
       public int Top;         // y position of upper-left corner
       public int Right;       // x position of lower-right corner
       public int Bottom;      // y position of lower-right corner
    }

    [StructLayout(LayoutKind.Sequential)]
    struct MinimizedMetrics
    {
       uint cbSize;
       int iWidth;
       int iHorzGap;
       int iVertGap;
       MinimizedMetricsArrangement iArrange;
    }
    [Flags]
    enum MinimizedMetricsArrangement
    {
       BottomLeft = 0,
       BottomRight = 1,
       TopLeft = 2,
       TopRight = 3,
       Left = 0,
       Right = 0,
       Up = 4,
       Down = 4,
       Hide = 8
    }


    public const uint WINEVENT_OUTOFCONTEXT = 0;
    public const uint EVENT_SYSTEM_FOREGROUND = 3;
    public const uint EVENT_OBJECT_NAMECHANGE = 0x800C;

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();
     
    public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam); 
    public delegate void EventHandler(object sender, string data);
    
    //SHRunFileDlg
    [DllImport("shell32.dll", CharSet = CharSet.Auto, EntryPoint = "#61", SetLastError = true)]

    public static extern bool SHRunFileDialog(IntPtr hwndOwner, 
                         IntPtr hIcon, 
                         string lpszPath,
                         string lpszDialogTitle, 
                         string lpszDialogTextBody, 
                         RunFileDialogFlags uflags);
    
    public enum RunFileDialogFlags : uint
    {
      None = 0x0,
      NoBrowse = 0x1,
      NoDefault = 0x2,
      CalcDirectory = 0x4,
      NoLabel = 0x8,
      NoSeparateMemory = 0x20
    }

    //RunDialog
    public static void DoRun()
    {
      SHRunFileDialog( IntPtr.Zero, IntPtr.Zero, "C:\\", "Run...", "Type the name of a program, folder or internet address and Windows will open that for you.", Interop.RunFileDialogFlags.CalcDirectory | Interop.RunFileDialogFlags.NoBrowse);
    }
//Icon stuff
public const int GCL_HICONSM = -34;
public const int GCL_HICON = -14;
 
public const int ICON_SMALL = 0;
public const int ICON_BIG = 1;
public const int ICON_SMALL2 = 2;
 
public const int WM_GETICON = 0x7F;
 
public static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
{
  if (IntPtr.Size > 4)
    return GetClassLongPtr64(hWnd, nIndex);
  else
    return new IntPtr(GetClassLongPtr32(hWnd, nIndex));
}
 
[DllImport("user32.dll", EntryPoint = "GetClassLong")]
public static extern uint GetClassLongPtr32(IntPtr hWnd, int nIndex);
 
[DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
public static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);
 
[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
public static extern IntPtr SendMessage(int hWnd, int Msg, int wParam, int lParam);

//Hotkey stuff
        public static int MOD_ALT = 0x1;
        public static int MOD_CONTROL = 0x2;
        public static int MOD_SHIFT = 0x4;
        public static int MOD_WIN = 0x8;
        public static int WM_HOTKEY = 0x312;

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);



  //Volume hotkeys
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;

        public static void VolumeMute(int hwnd)
        {
            SendMessage( hwnd, WM_APPCOMMAND, hwnd,
                APPCOMMAND_VOLUME_MUTE);
        }

        public static void VolumeDown(int hwnd)
        {
            SendMessage(hwnd, WM_APPCOMMAND, hwnd,
                APPCOMMAND_VOLUME_DOWN);
        }

        public static void VolumeUp(int hwnd)
        {
            SendMessage(hwnd, WM_APPCOMMAND, hwnd,
                APPCOMMAND_VOLUME_UP);
        }
        
}
//End Interop Class


public class SystemProcessHookForm : Form
{
    private readonly int msgNotify;
    public delegate void EventHandler(object sender, string data);

    public event EventHandler WindowEvent;
    public int lastActiveWindow = 0;
    ContextMenu ci;
    Interop.WinEventDelegate dele = null;
    Interop.WinEventDelegate deleName = null;

    protected virtual void OnWindowEvent(string data)
    {
        var handler = WindowEvent;
        if (handler != null)
        {
            handler(this, data);
        }
    }

    public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
    {
      int thwnd = GetActiveWindowHandle();
      if( thwnd != this.Handle.ToInt32() )
      {
        lastActiveWindow = thwnd;
        /*
        Console.WriteLine("WindowChanged {1} \"{0}\"\r\n",
          GetActiveWindowTitle(),
          GetActiveWindowHandle() );//+= GetActiveWindowTitle() + "\r\n";
        */
      }
    }
  
    public void WinEventProcNameChange(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
    {
      if( idObject != 0 || idChild != 0 )
      {
        return;
      }
      const int nChars = 256;

      StringBuilder Buff = new StringBuilder(nChars );
      Interop.GetWindowText( hwnd, Buff, nChars );
      /*
      Console.WriteLine("Text of hwnd changed {0:x8} {1}",
        hwnd.ToInt32(),
        Buff.ToString()
      );
      */
      fixButtonText( hwnd.ToInt32(), Buff.ToString() );
    }

    private int GetActiveWindowHandle()
    {
      IntPtr hwnd = IntPtr.Zero;
      hwnd = Interop.GetForegroundWindow();
      return hwnd.ToInt32();
    }

    private string GetActiveWindowTitle()
    {
      const int nChars = 256;
      IntPtr hwnd = IntPtr.Zero;
      hwnd = Interop.GetForegroundWindow();
      StringBuilder Buff = new StringBuilder(nChars );
      if( Interop.GetWindowText( hwnd, Buff, nChars ) > 0 )
      {
        return Buff.ToString();
      }
      return null;
    }
    
    public SystemProcessHookForm()
    {

        // Hook on to the shell
        msgNotify = Interop.RegisterWindowMessage("SHELLHOOK");
        Interop.RegisterShellHookWindow(this.Handle);
        
        //Call SystemParametersInfo(SPI_SETMINIMIZEDMETRICS)
        
        //add start button
        this.addButton("~", 0);

        //now audit existing programs        
        Interop.EnumWindows( new Interop.EnumWindowsProc( EnumTheWindows ), IntPtr.Zero );

        //now hook for active window.
        dele = new Interop.WinEventDelegate( WinEventProc );
        deleName = new Interop.WinEventDelegate( WinEventProcNameChange );

        IntPtr m_hhook = Interop.SetWinEventHook( Interop.EVENT_SYSTEM_FOREGROUND,
          Interop.EVENT_SYSTEM_FOREGROUND,
          IntPtr.Zero,
          dele,
          0,
          0,
          Interop.WINEVENT_OUTOFCONTEXT );
        
        IntPtr n_hhook = Interop.SetWinEventHook( Interop.EVENT_OBJECT_NAMECHANGE, 
          Interop.EVENT_OBJECT_NAMECHANGE,
          IntPtr.Zero,
          deleName,
          0,
          0,
          Interop.WINEVENT_OUTOFCONTEXT );
        

        //create context menu
        ci = new ContextMenu();

        //add clock
        Label clkLabel = new Label();
        clkLabel.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom );
        clkLabel.Location = new Point( 0, this.Size.Height - 90 );
        clkLabel.Size = new Size( this.Size.Width, 90);
        clkLabel.Font = new Font("Arial Narrow", 8);
        clkLabel.Text = "Clock\r\nClock\r\nClock";
        clkLabel.TextAlign = ContentAlignment.TopLeft;
        this.Controls.Add(clkLabel);
        
        System.Windows.Forms.Timer clkUpdater = new System.Windows.Forms.Timer();

        clkUpdater.Tick += new System.EventHandler(delegate (Object o, EventArgs a) {
          clkLabel.Text = DateTime.Now.ToString("HH:mm:ss\nMM/dd/yyyy\n") + 
          ( SystemInformation.PowerStatus.BatteryLifePercent * 100).ToString() + "%" + 
          ( SystemInformation.PowerStatus.PowerLineStatus ).ToString();
        });
        clkUpdater.Interval = 1000;
        clkUpdater.Start();

        //hotkey
        //Interop.RegisterHotKey(this, Keys.R | Keys.LWin);
        
        bool kbool = true;
        Interop.RegisterHotKey(this.Handle, 'R', Interop.MOD_WIN, (int)Keys.R);
        Interop.RegisterHotKey(this.Handle, 'E', Interop.MOD_WIN, (int)Keys.E);
        Interop.RegisterHotKey(this.Handle, 'Q', Interop.MOD_WIN, (int)Keys.Q);
        //Interop.RegisterHotKey(this.Handle, 'W', 0, (int)0x73);//5B//LWIN
        kbool = Interop.RegisterHotKey(this.Handle, 'W', Interop.MOD_WIN, (int)0x5B);//5C RWIN
        if( !kbool ) MessageBox.Show("Test not work");
        Interop.RegisterHotKey(this.Handle, 0xAE, 0, (int)Keys.VolumeDown);
        Interop.RegisterHotKey(this.Handle, 0xAF, 0, (int)Keys.VolumeUp);
        Interop.RegisterHotKey(this.Handle, 0xAD, 0, (int)Keys.VolumeMute);
        
    }

    protected override void WndProc(ref Message m)
    {
        //Console.WriteLine("WndProc");
        if (m.Msg == msgNotify)
        { 
          //Console.WriteLine("SHELL");
            // Receive shell messages
            switch ((Interop.ShellEvents)m.WParam.ToInt32())
            {
                case Interop.ShellEvents.HSHELL_WINDOWCREATED:      
                  {
                    string wName = GetWindowName(m.LParam);
                    var action = (Interop.ShellEvents)m.WParam.ToInt32();
                    Console.WriteLine("WindowCreated");
                    OnWindowEvent(string.Format("{0} - {1}: {2}", action, m.LParam, wName));
                    addButton( wName, (int)m.LParam );
                  }
                    break;
                case Interop.ShellEvents.HSHELL_WINDOWDESTROYED:
                  {
                    var action = (Interop.ShellEvents)m.WParam.ToInt32();
                                        Console.WriteLine("WindowDestroyed");
                    OnWindowEvent(string.Format("{0} - {1}", action, m.LParam));
                    removeButton( (int) m.LParam );
                  }
                  break;
                case Interop.ShellEvents.HSHELL_WINDOWACTIVATED:
                  {
                    string wName = GetWindowName(m.LParam);
                                        Console.WriteLine("WindowActivated");
                    var action = (Interop.ShellEvents)m.WParam.ToInt32();
                    OnWindowEvent(string.Format("{0} - {1}: {2}", action, m.LParam, wName));
                  }
                break;
            }
        }
        else if( m.Msg == Interop.WM_HOTKEY )
        {
          Console.WriteLine("hotkey {0}", m.WParam.ToInt32() );
          if( m.WParam.ToInt32() == 0xAE )
          {
            Console.WriteLine("VolumeDown");
            Interop.VolumeDown((int) this.Handle);
          }
          else if( m.WParam.ToInt32() == 0xAF )
          {
            Console.WriteLine("VolumeUp");
            Interop.VolumeUp((int) this.Handle);
          }
          else if( m.WParam.ToInt32() == 0xAD )
          {
            Console.WriteLine("VolumeMute");
            Interop.VolumeMute((int) this.Handle);
          }
          else if( m.WParam.ToInt32() == 87 )
          {
            //Windows Key pressed.
            //doLaunchMenu( appButtons[0] );
          }
          else if( m.WParam.ToInt32() == 82 ) 
          {
            //run
            Interop.DoRun();
          }
          else if( m.WParam.ToInt32() == 69 ) 
          {
            //Win+E
            launch("shell:mycomputerfolder");
          }
        }
        base.WndProc(ref m);
    }

    private string GetWindowName(IntPtr hwnd)
    {
        StringBuilder sb = new StringBuilder();
        int longi = Interop.GetWindowTextLength(hwnd) + 1;
        sb.Capacity = longi;
        Interop.GetWindowText(hwnd, sb, sb.Capacity);
        return sb.ToString();
    }

    protected override void Dispose(bool disposing)
    {
        try { Interop.DeregisterShellHookWindow(this.Handle); }
        catch { }
        base.Dispose(disposing);
    }
    
    //for initialization
    protected bool EnumTheWindows(IntPtr hWnd, IntPtr lParam) 
    { 
      int size = Interop.GetWindowTextLength(hWnd); 
      if (size++ > 0 && Interop.IsWindowVisible(hWnd)) 
      { 
        StringBuilder sb = new StringBuilder(size); 
        Interop.GetWindowText(hWnd, sb, size); 
        addButton( sb.ToString(), (int) hWnd);
        //Console.WriteLine(sb.ToString()); 
      } 
      return true; 
    }

    //display stuff.
    Dictionary <int, Button> appButtons = new Dictionary<int, Button>();
    Dictionary <int, string> appNames = new Dictionary<int, string>();

    public void focusButton(int h)
    {
      if( appButtons.ContainsKey( h ) )
      {
        appButtons[h].Focus();
      }
    }
    //set the text of button associated with h to s.
    public void fixButtonText(int h, string s)
    {
      //get text.
      if( appButtons.ContainsKey( h ) && appNames.ContainsKey( h ) )
      {
        appButtons[h].Text = "    " + s;
        appNames[h] = s;
      }
    }

    public void addButton(string s,int h)
    {
      Button bt = new Button();
      appButtons.Add( h, bt );
      appNames.Add( h, s );
      bt.Text = "    " + s;
      bt.Click += new System.EventHandler(delegate (Object o, EventArgs a) 
        {
            //if( ((MouseEventArgs)a ).Button == System.Windows.Forms.MouseButtons.Right){MessageBox.Show("Right click");}
           if( h == 0 ) 
            {
              //start
              //removeButton( 0 );
              doLaunchMenu(bt);
            }
            else
            {
              if( h == lastActiveWindow )
              {
                if( Interop.IsIconic( h ) )
                {
                  Interop.ShowWindow( h, (uint)Interop.ShowWindowCmdShow.SW_RESTORE );
                  Interop.SetForegroundWindow( h );
                }
                else
                {
                  Interop.ShowWindow( h, (int)Interop.ShowWindowCmdShow.SW_MINIMIZE );
                }
              }
              else
              {
                Interop.ShowWindow( h, (uint)Interop.ShowWindowCmdShow.SW_RESTORE );
                Interop.SetForegroundWindow( h );
              }
            }
        });//bt.Click handler.
        
      bt.MouseUp += new System.Windows.Forms.MouseEventHandler( delegate( Object o, MouseEventArgs e){
        if( e.Button == System.Windows.Forms.MouseButtons.Right )
        {
          ci.MenuItems.Clear();
          ci.MenuItems.Add("Restore", new System.EventHandler( delegate(Object o2, EventArgs a)
            {
              Interop.ShowWindow( h, (uint)Interop.ShowWindowCmdShow.SW_RESTORE );
              Interop.SetForegroundWindow( h );
            }) );
          ci.MenuItems.Add("Minimize", new System.EventHandler( delegate(Object o2, EventArgs a)
            {
              Interop.ShowWindow( h, (uint)Interop.ShowWindowCmdShow.SW_MINIMIZE );
//              Interop.SetForegroundWindow( h );
            }) );
          ci.MenuItems.Add("Maximize", new System.EventHandler( delegate(Object o2, EventArgs a)
            {
              Interop.ShowWindow( h, (uint)Interop.ShowWindowCmdShow.SW_MAXIMIZE );
              Interop.SetForegroundWindow( h );
            }) );
          ci.MenuItems.Add("-");
          ci.MenuItems.Add("Make Always On Top", new System.EventHandler( delegate(Object o2, EventArgs a)
            {
              //Interop.ShowWindow( h, (uint)Interop.ShowWindowCmdShow.SW_MAXIMIZE );
              Interop.RECT rct;
              Interop.GetWindowRect( h, out rct);
//              Interop.SetWindowPos( h, -1, rct.Left, rct.Top, rct.Right - rct.Left + 1, rct.Bottom - rct.Top + 1, 0);
              Interop.SetWindowPos( h, -1, 0, 0, 0, 0, 0x0002 | 0x0001 );
              Interop.SetForegroundWindow( h );
            }) );
          ci.MenuItems.Add("Make Not Always On Top", new System.EventHandler( delegate(Object o2, EventArgs a)
            {
              Interop.SetWindowPos( h, -2, 0, 0, 0, 0, 0x0002 | 0x0001 );
              Interop.SetForegroundWindow( h );
            }) );
          ci.MenuItems.Add("Set Position...", new System.EventHandler( delegate(Object o2, EventArgs a)
            {
              Form pform = new Form();
              pform.TopMost = true;
              pform.Text = "Set Position";
              Interop.RECT rct;
              Interop.GetWindowRect( h, out rct);

              TextBox tX = new TextBox();
              tX.Text = (rct.Left).ToString();
              tX.Location = new Point(100, 0);
              pform.Controls.Add(tX );
              Label lX = new Label();
              lX.Text = "X";
              lX.Location = new Point(0, 0);
              pform.Controls.Add(lX );
              
              TextBox tY = new TextBox();
              tY.Text = (rct.Top).ToString();
              tY.Location = new Point(100, 30);
              pform.Controls.Add(tY );
              Label lY = new Label();
              lY.Text = "Y";
              lY.Location = new Point(0, 30);
              pform.Controls.Add(lY );
              
              TextBox tWidth = new TextBox();
              tWidth.Text = (rct.Right - rct.Left + 1).ToString();
              tWidth.Location = new Point(100, 60);
              pform.Controls.Add(tWidth );
              Label lWidth = new Label();
              lWidth.Text = "Width";
              lWidth.Location = new Point(0, 60);
              pform.Controls.Add(lWidth );

              TextBox tHeight = new TextBox();
              tHeight.Text = (rct.Bottom - rct.Top + 1).ToString();
              tHeight.Location = new Point(100, 90);
              pform.Controls.Add(tHeight );
              Label lHeight = new Label();
              lHeight.Text = "Height";
              lHeight.Location = new Point(0, 90);
              pform.Controls.Add(lHeight );
              pform.Show();
              pform.ClientSize = new Size( 200, 160) ;

              Button b = new Button();
              b.Text = "OK";
              b.Location = new Point( 0, 120);
              b.Click += new System.EventHandler(delegate (Object bcio, EventArgs bcia) 
              {
                Interop.SetWindowPos( h, 0,
                  Int32.Parse(tX.Text),
                  Int32.Parse(tY.Text),
                  Int32.Parse(tWidth.Text),
                  Int32.Parse(tHeight.Text),                
                  0);
                Interop.SetForegroundWindow( h );
                pform.Close();
              });
              pform.Controls.Add(b);
            }) );
          ci.MenuItems.Add("Set Window Text...", new System.EventHandler( delegate(Object o2, EventArgs a)
            {
              int nChars = 254;
              StringBuilder Buff = new StringBuilder(nChars );
              Interop.GetWindowText( h, Buff, nChars );
              string newText = Interaction.InputBox("Set Window Text", string.Format("Original: \"{0}\"", Buff.ToString() ), Buff.ToString(), 0, 0);
              Interop.SetWindowText( h, newText );
            }) );
          ci.MenuItems.Add("Set Window Icon (big)...", new System.EventHandler( delegate(Object o2, EventArgs a)
            {
                try
                {
                  string newText = Interaction.InputBox("Which file's icon?", "", @"C:\Windows\Explorer.exe", 0, 0);
                  Icon ico = Icon.ExtractAssociatedIcon( newText );
                  Interop.SendMessage( h, Interop.WM_SETICON, Interop.ICON_BIG, ico.Handle.ToInt32() );
                }
                catch(Exception ex)
                {
                  MessageBox.Show( ex.ToString() );
                }
            }) );
          ci.MenuItems.Add("Set Window Icon (small only)...", new System.EventHandler( delegate(Object o2, EventArgs a)
            {
              Interop.SendMessage( h, Interop.WM_SETICON, Interop.ICON_SMALL, SystemIcons.Information.Handle.ToInt32() );
            }) );
          ci.MenuItems.Add("Disable Close Button", new System.EventHandler( delegate(Object o2, EventArgs a)
            {
              Interop.EnableMenuItem( Interop.GetSystemMenu( h, false ), 0xF060, 0x0 | 0x1 ); //SC_CLOSE, MF_BYCOMMAND, MF_GRAYED
            }) );
          ci.MenuItems.Add("Enable Close Button", new System.EventHandler( delegate(Object o2, EventArgs a)
            {
              Interop.EnableMenuItem( Interop.GetSystemMenu( h, false ), 0xF060, 0x0  );
            }) );
          ci.MenuItems.Add("Get PID", new System.EventHandler( delegate(Object o2, EventArgs a)
            {
              uint iProcessId;
              Interop.GetWindowThreadProcessId( (IntPtr)h, out iProcessId );
              MessageBox.Show( String.Format("PID for \"{1}\" \n {0}", iProcessId.ToString() , appNames[h].ToString() ) );
            }) );
          ci.MenuItems.Add("-");
          ci.MenuItems.Add("Close", new System.EventHandler( delegate(Object o2, EventArgs a)
            {
              Interop.PostMessage( h, 0x10, 0, 0 );//WM_CLOSE 0x10
            })
            );
          ci.MenuItems.Add("Kill...", new System.EventHandler( delegate(Object o2, EventArgs a)
            {
              uint iProcessId;
              Interop.GetWindowThreadProcessId( (IntPtr)h, out iProcessId );
              
              if( System.Windows.Forms.DialogResult.Yes == 
                MessageBox.Show( String.Format("Really Kill\nPID {0}\n\"{1}\"\n{2}",
                  iProcessId.ToString(),
                  appNames[h].ToString(),
                  ""
                  ),
                "Kill?", MessageBoxButtons.YesNo ) )
                {
                  Process.GetProcessById( (int)iProcessId ).Kill();
                }
            }) );
          
          ci.Show( bt, new Point(e.X, e.Y) );
        }
      });//bt.MouseUp Handler.
      
      bt.Paint += new System.Windows.Forms.PaintEventHandler( delegate( Object o, PaintEventArgs e )
      {
        Icon appIcon = GetAppIcon( h  );
        if( appIcon != null)
        {
          e.Graphics.DrawIcon( appIcon , new Rectangle( 2,3,16,16) );
        }

      });

      this.Controls.Add( bt );
      updateButtonLocations();
    }

    public void removeButton(int h)
    {
      if( appButtons.ContainsKey(h) )
      {
        this.Controls.Remove( appButtons[h] );
        appButtons.Remove( h );
        appNames.Remove( h );
        updateButtonLocations();
      }
    }

    public void updateButtonLocations()
    {
      int y = 0;
      for(var en = appButtons.Values.GetEnumerator(); en.MoveNext(); y+=24  )
      {
        en.Current.Location = new Point( 0, y );
        en.Current.Size = new Size( ClientSize.Width, 22 );
        en.Current.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top );
      }
    }

    public Icon GetAppIcon(int hwnd)
    {
      IntPtr iconHandle = Interop.SendMessage(hwnd,Interop.WM_GETICON,Interop.ICON_SMALL2,0);
      if(iconHandle == IntPtr.Zero)
        iconHandle = Interop.SendMessage(hwnd,Interop.WM_GETICON,Interop.ICON_SMALL,0);
      if(iconHandle == IntPtr.Zero)
        iconHandle = Interop.SendMessage(hwnd,Interop.WM_GETICON,Interop.ICON_BIG,0);
      if (iconHandle == IntPtr.Zero)
        iconHandle = Interop.GetClassLongPtr(new IntPtr(hwnd), Interop.GCL_HICON);
      if (iconHandle == IntPtr.Zero)
        iconHandle = Interop.GetClassLongPtr(new IntPtr(hwnd), Interop.GCL_HICONSM);
     
      if(iconHandle == IntPtr.Zero)
        return null;
     
      Icon icn = Icon.FromHandle(iconHandle);
     
      return icn;
    }
    
    public bool launch(string execCmdLine)
    {
      bool retVal = false;
      try
      {
        Process.Start(execCmdLine);
        retVal = true;
      }
      catch(Exception exc)
      {
        MessageBox.Show("Couldn't Start '" + execCmdLine + "': '" + exc.ToString() + "'");
      }
      return retVal;
    }

    public void doLaunchMenu(Control bt)
    {

      if(  false /*ci.IsOpen*/ )
      {
        //ci.Hide();
      }
      else
      {
        ci.MenuItems.Clear();
        ci.MenuItems.Add("Explore...", new System.EventHandler( delegate(Object startcio2, EventArgs startcia)
        {
          launch("shell:mycomputerfolder");
        }) );
        ci.MenuItems.Add("Programs (User)", new System.EventHandler( delegate(Object startcio2, EventArgs startcia)
        {
          launch("shell:Start Menu");
        }) );
        ci.MenuItems.Add("Programs (Public)", new System.EventHandler( delegate(Object startcio2, EventArgs startcia)
        {
          launch("shell:Common Start Menu");
        }) );
        ci.MenuItems.Add("-");
        ci.MenuItems.Add("Run...", new System.EventHandler( delegate(Object startcio2, EventArgs startcia)
        {
          Interop.DoRun();
        }) );
        
        ci.MenuItems.Add("Shut Down...", new System.EventHandler( delegate(Object startcio2, EventArgs startcia)
        {
          //do nothing temporarily. 
        }) );
        ci.MenuItems.Add("-");
        ci.MenuItems.Add("Exit...", new System.EventHandler( delegate(Object startcio2, EventArgs startcia)
        {
          if( System.Windows.Forms.DialogResult.Yes == MessageBox.Show("Quit?", "", MessageBoxButtons.YesNo) )
          {
            Environment.Exit(0);//exit
          }
        }) );
        ci.MenuItems.Add("-");
        ci.MenuItems.Add("Move Bar", new System.EventHandler( delegate(Object startcio2, EventArgs startcia)
        {
          WorkArea wx = new WorkArea( 0, 0,System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 50, 
            System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
          wx.SetWorkingArea();
          this.Size = new Size(50, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
          this.Location = new Point( System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 49, 0);
        }) );
        
        ci.Show( bt, new Point( bt.Width, 22) ); //22 if side or top, 0 if bottom. (bar position)
      }
    }
  }
  //end of form class


public class WorkArea
{
  [System.Runtime.InteropServices.DllImport("user32.dll",  EntryPoint="SystemParametersInfoA")]
  private static extern Int32 SystemParametersInfo(Int32 uAction, Int32 uParam, IntPtr lpvParam, Int32 fuWinIni);

  private const Int32 SPI_SETWORKAREA = 47;
  public WorkArea(Int32 Left,Int32 Right,Int32 Top,Int32 Bottom)
  {
    _WorkArea.Left = Left;
    _WorkArea.Top = Top;
    _WorkArea.Bottom = Bottom;
    _WorkArea.Right = Right;
  }

  public struct RECT
  {
    public Int32 Left;
    public Int32 Right;
    public Int32 Top;
    public Int32 Bottom;
  }
    [StructLayout(LayoutKind.Sequential)]
    struct MinimizedMetrics
    {
       public Int32 cbSize; //uint
       public Int32 iWidth; //int
       public Int32 iHorzGap; //int
       public Int32 iVertGap; //int
       public MinimizedMetricsArrangement iArrange; //int
    }
    [Flags]
    enum MinimizedMetricsArrangement : int
    {
       BottomLeft = 0,
       BottomRight = 1,
       TopLeft = 2,
       TopRight = 3,
       Left = 0,
       Right = 0,
       Up = 4,
       Down = 4,
       Hide = 8
    }

  private RECT _WorkArea;
  private MinimizedMetrics _MinimizedMetrics;

  public void SetWorkingArea()
  {
    IntPtr ptr = IntPtr.Zero;
    ptr = Marshal.AllocHGlobal(Marshal.SizeOf(_WorkArea));
    Marshal.StructureToPtr(_WorkArea,ptr,false);
    int i = SystemParametersInfo(SPI_SETWORKAREA,0,ptr,0);
  }

  public void HideMinimizedWindows()
  {
    IntPtr ptr = IntPtr.Zero;
    _MinimizedMetrics.cbSize = (int)Marshal.SizeOf(  _MinimizedMetrics );
    _MinimizedMetrics.iArrange = MinimizedMetricsArrangement.Hide; //ARW_HIDE

    ptr = Marshal.AllocHGlobal( Marshal.SizeOf( _MinimizedMetrics) );
    Marshal.StructureToPtr( _MinimizedMetrics, ptr, false);
    int i = SystemParametersInfo( SPI_SETWORKAREA, _MinimizedMetrics.cbSize, ptr, 0 );
  }
}


class foo
{
  public static void Main(string[] args)
  {
    WorkArea wx = new WorkArea( 0, 0,System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 50, 
      System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
    wx.SetWorkingArea();
    var f = new SystemProcessHookForm();    

    bool beTaskBar = true;
    bool beToolWin = false;
    //parse args
    foreach( var a in args )
    {
      switch( a.ToUpper() )
      {
        case "/W":
          beTaskBar = false;
        break;
        case "/WT":
          beTaskBar = false; 
          beToolWin = true;
        break;          
        default:
          Console.WriteLine("unknown command line option '{0}'", a );
        break;
      }
    }

    if( beTaskBar )
    {
      f.launch("wemin.exe");
      f.FormBorderStyle = FormBorderStyle.None;
      f.Size = new Size(50, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
      f.Location = new Point( System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 49, 0);
    }
    else
    {
      f.Size = new Size(200,300);
      if( beToolWin )
      {
        f.FormBorderStyle = FormBorderStyle.SizableToolWindow;
      }
    }
    /*
    f.Size = new Size(60, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height);
    f.Location = new Point(0, 0);
    */



    f.Text = "rtasks";
    f.FormClosing += new FormClosingEventHandler(delegate (Object o, FormClosingEventArgs a) 
        {
          Environment.Exit(0);          
        });

    f.Show();
    f.TopMost = true;
    f.WindowEvent += (sender, data) => Console.WriteLine(data); 
    while (true)
    {
      Thread.Sleep(1);
      Application.DoEvents();
    }
  }
}
