using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

class foo
{
  protected delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam); 
  [DllImport("user32.dll", CharSet = CharSet.Unicode)] 
  protected static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount); 
  [DllImport("user32.dll", CharSet = CharSet.Unicode)] 
  protected static extern int GetWindowTextLength(IntPtr hWnd); 
  [DllImport("user32.dll")] 
  protected static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam); 
  [DllImport("user32.dll")] 
  protected static extern bool IsWindowVisible(IntPtr hWnd); 
  
  protected static bool EnumTheWindows(IntPtr hWnd, IntPtr lParam) 
  { 
    int size = GetWindowTextLength(hWnd); 
    if (size++ > 0 && IsWindowVisible(hWnd)) 
    { 
      StringBuilder sb = new StringBuilder(size); 
      GetWindowText(hWnd, sb, size); 
      Console.WriteLine(sb.ToString()); 
    } 
    return true; 
  } 
  
  public static void Main()
  {
    /*
    Process[] processes = Process.GetProcesses();
    foreach( var item in processes )
    {
      if( item.MainWindowTitle.Length > 0 )
      {
        Console.WriteLine( item.MainWindowTitle );
      }
    }
    */
    EnumWindows( new EnumWindowsProc( EnumTheWindows ), IntPtr.Zero );
    Console.ReadKey();
  }
}
