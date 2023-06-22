using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CTAutorun;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;
using System.Windows.Automation;
using System.Net.Sockets;
using Modbus.Device;
using System.Timers;
using System.Runtime.CompilerServices;

namespace CTAutorun
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {           
            //Global.thread2.Name = "RobotConnectionCheck";
            //Global.thread3.Name = "Monitor Popup";

            
            //Global.thread2.Start();
            //Global.thread3.Start();

            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CTAutorun());
        }
    }
}


public enum RobotState
{
    IDLE,
    OPENING_DOOR,
    DOOR_OPEN,
    INSERTING_ITEM,
    ITEM_INSERTED,
    CLOSING_DOOR,
    DOOR_CLOSED,
    REMOVING_ITEM,
    ITEM_REMOVED,
    DONE
}

public enum ProcessState
{
    INIT,
    LOAD_TRAY,
    SELECT_TASK,
    SCAN,
    QUERY_FILES,
    QUERY_ROBOT,
    EXIT,
    CONNECT_ROBOT,
    DISCONNECT_ROBOT,
    STOP,
    INACTIVE
}

public static class Global
{
    public static CTAutorun.CTAutorun CTAutorun = new CTAutorun.CTAutorun();
    //public static Process process = new Process();

    public static TcpClient client = new TcpClient();
    public static ModbusIpMaster master = null;

    public static bool run_comm_test = false;

    public static bool ForceState = false;
    public static bool debugging = false;
    public static bool AppPausedFlag = false;
    public static bool AppStopFlag = false;
    public static bool robotConnected;
    public static bool updateRobotCommunicationTextbox = false;

    // State Machine states
    public static ProcessState ForceNewState = ProcessState.INACTIVE;
    public static ProcessState LastState = ProcessState.INACTIVE;
    public static ProcessState process = ProcessState.INACTIVE;

    public static string password = "dti1234";
    public static string AutoRunFile = "";
    public static bool SendData = false;
    public static string DataToSend = "";
    public static string ReceivedData = "";

    public static System.Windows.Forms.TextBox debugLog = null;

    // App Attach
    public static FlaUI.Core.Application app = new FlaUI.Core.Application(0);
    public static FlaUI.Core.Application checkapp = app;

    // Monitor popup event
    public static bool PopUpAppeared = false;
    public static string PopUpType = "";
    public static Thread thread4 = new Thread(comms_test);
   
    //public static Thread thread3 = new Thread(() => UIauto.MonitorPopupEvent("Reload"));
    //public static Thread thread3 = new Thread(() => UIauto.MonitorPopupEvent(Global.app, "Printout will be generated"));

    // Path of the template file
    public static string templatePath = @"C:\Users\fkl\source\repos\Bobot 2.0 App\Bobot 2.0 App\File\temp.arn";
    public static string robotCommunicationLogPath = @"C:\Users\fkl\source\repos\Bobot 2.0 App\Bobot 2.0 App\bin\Debug\RobotCommunication.log";
    public static string newFilePath = @"C:\Users\fkl\source\repos\Bobot 2.0 App\Bobot 2.0 App\File\test";
    public static string appPath = @"C:\\Windows\\System32\\notepad.exe";//@"C:\Program Files\Zeiss\METROTOM OS\Bin\MetrotomOS.exe";//@"notepad.exe";//C:\Program Files\Notepad++\notepad++.exe";

    private delegate void LogDelegate(string str);
    public static void debug_log(string msg)
    {
        Global.CTAutorun.AddText(msg);
        //debugLog.AppendText(msg + Environment.NewLine);
    }

    public static void comms_test()
    {
        if (Global.run_comm_test)
        {

            var old_state = Global.master.ReadHoldingRegisters(130, 1);
            var robot_new_state = Global.master.ReadHoldingRegisters(128, 1);
            if (robot_new_state[0] != old_state[0])
            {
                Global.master.WriteSingleRegister(130, robot_new_state[0]);
            }

        }
        Thread.Sleep(2000);
    }

    //public static void checkConnection()
    //{
    //    while (true)
    //    {

    //        if (SocketServer.IsConnected(SocketServer.getConnection()))
    //        {
    //            Global.CMMautorun.ChangeRobottext("Connected");
    //        }
    //        else
    //        {
    //            Global.CMMautorun.ChangeRobottext("Not Connected");
    //        }

    //        try
    //        {
    //            if (Global.app != null)
    //            {
    //                if (Global.app != Global.checkapp)
    //                {
    //                    if (Global.app.HasExited)
    //                    {
    //                        Global.app.Dispose();
    //                        Global.app = null;
    //                    }
    //                    else if (Global.app.ProcessId != 0)
    //                    {
    //                        Global.CMMautorun.ChangeCMMtext("Connected");
    //                    }
    //                    else
    //                    {
    //                        Global.CMMautorun.ChangeCMMtext("Not Connected");
    //                    }
    //                }
    //                else
    //                {
    //                    Global.CMMautorun.ChangeCMMtext("Not Connected");
    //                }
    //            }
    //            else
    //            {
    //                Global.CMMautorun.ChangeCMMtext("Not Connected");
    //            }
    //        }
    //        catch
    //        {
    //        }
    //        Thread.Sleep(500);
    //    }
    //}

    public static Thread thread1 = new Thread(StateMachine.SwitchState);
    //public static Thread thread2 = new Thread(checkConnection);

    public static void BulletHandle()
    {
        Global.CTAutorun.SetInitBullet(false);
        Global.CTAutorun.SetLoad_trayBullet(false);
        Global.CTAutorun.SetSelect_taskBullet(false);
        Global.CTAutorun.SetScanBullet(false);
        Global.CTAutorun.SetQuery_filesBullet(false);
        Global.CTAutorun.SetQuery_robotBullet(false);
        Global.CTAutorun.SetExitBullet(false);
        Global.CTAutorun.SetConnect_robotBullet(false);
        Global.CTAutorun.SetDisconnect_robotBullet(false);
        Global.CTAutorun.SetStopBullet(false);
        Global.CTAutorun.SetInactiveBullet(false);


        switch (Global.process)
        {
            case ProcessState.INIT:
                Global.CTAutorun.SetInitBullet(true);
                break;

            case ProcessState.LOAD_TRAY:
                Global.CTAutorun.SetLoad_trayBullet(true);
                break;

            case ProcessState.SELECT_TASK:
                Global.CTAutorun.SetSelect_taskBullet(true);
                break;

            case ProcessState.SCAN:
                Global.CTAutorun.SetScanBullet(true);
                break;

            case ProcessState.QUERY_FILES:
                Global.CTAutorun.SetQuery_filesBullet(true);
                break;

            case ProcessState.QUERY_ROBOT:
                Global.CTAutorun.SetQuery_robotBullet(true);
                break;

            case ProcessState.EXIT:
                Global.CTAutorun.SetExitBullet(true);

                break;

            case ProcessState.STOP:
                Global.CTAutorun.SetStopBullet(true);
                break;

            case ProcessState.INACTIVE:
                Global.CTAutorun.SetInactiveBullet(true);
                break;
        }

    }
}

public class StateMachine
{


    public static void SwitchState()
    {
        ProcessState NewState = ProcessState.INACTIVE;

        while (true)
        {

            if (Global.ForceState)
            {
                Global.process = Global.ForceNewState;
                /*
                //switch (Global.ForceNewState)
                //{
                //    case ProcessState.Initialize:
                //        Global.process.MoveNext(Command.ForceInit);

                //        break;

                //    case ProcessState.ConnectApp:
                //        Global.process.MoveNext(Command.ForceConnectApp);

                //        break;

                //    case ProcessState.ConnectRobot:
                //        Global.process.MoveNext(Command.ForceConnectRobot);

                //        break;

                //    case ProcessState.RobotActiv:
                //        Global.process.MoveNext(Command.ForceRobotActiv);

                //        break;

                //    case ProcessState.CMMActiv:
                //        Global.process.MoveNext(Command.ForceCMMActiv);

                //        break;

                //    case ProcessState.Scanning:
                //        Global.process.MoveNext(Command.ForceScanning);

                //        break;

                //    case ProcessState.Paused:
                //        Global.process.MoveNext(Command.ForcePaused);

                //        break;

                //    case ProcessState.Inactive:
                //        Global.process.MoveNext(Command.ForceInactiv);

                //        break;

                //    case ProcessState.Terminated:
                //        Global.process.MoveNext(Command.ForceDisconnectApp);

                //        break;
                //}
                */
                Global.ForceState = false;
            }

            if (!Global.AppPausedFlag && !Global.AppStopFlag)
            {
                switch (Global.process)
                {
                    case ProcessState.INIT:
                        ///
                        /// Initialize buttons
                        /// Launch and connect to program (METROMO?)
                        ///
                        // Connect or open app
                        if(Global.app.ProcessId == 0)
                        {
                            Global.debug_log("Connect to app first");
                        }
                        //Global.app = UIauto.ConnectApp(Global.appPath);
                        Console.WriteLine(Global.app);
                        Thread.Sleep(2000);

                        Global.process = ProcessState.LOAD_TRAY;
                        break;
                    case ProcessState.CONNECT_ROBOT:
                        ///
                        /// Establish connection to robot via. MODBUS
                        ///



                        Global.process = ProcessState.QUERY_ROBOT;
                        break;

                    case ProcessState.QUERY_ROBOT:
                        ///
                        /// Query the robot for its current state and update local state to match
                        /// Stay in this state until the robot reports that it's done
                        ///
                        Global.process = ProcessState.DISCONNECT_ROBOT; //| ProcessState.QUERY_ROBOT;
                        break;

                    case ProcessState.DISCONNECT_ROBOT:
                        ///
                        /// Close the current MODBUS connection to the robot
                        ///
                        Global.process = ProcessState.SELECT_TASK;
                        break;

                    case ProcessState.SELECT_TASK:
                        ///
                        /// Click the "Open Next Task" button in the program
                        /// then wait for the task inspector to open
                        /// If the task inspector dosn't open, there are no more tasks
                        /// and we can shutdown
                        ///

                        Global.process = ProcessState.SCAN;//| ProcessState.STOP;
                        break;

                    case ProcessState.SCAN:
                        ///
                        /// Click the button to start the scan, then close the popup
                        /// dialog that appears
                        ///
                        Global.process = ProcessState.QUERY_FILES;
                        break;

                    case ProcessState.QUERY_FILES:
                        ///
                        /// Stay in this stay until scan is complete
                        /// Wait a bit, then close popup
                        ///

                        Global.process = ProcessState.LOAD_TRAY;// | ProcessState.QUERY_FILES;
                        break;

                    case ProcessState.LOAD_TRAY:
                        ///
                        /// Set loader angle to 0
                        /// then click the load button
                        ///

                        Global.process = ProcessState.CONNECT_ROBOT;
                        break;

                    case ProcessState.EXIT:
                    ///
                    /// Ask for user confirmation, then commit suduko
                    ///

                    default:
                        break;
                }
            }

            if (NewState != Global.process)
            {
                Global.LastState = Global.process;
                NewState = Global.process;
                Console.WriteLine(NewState);
            }

            Global.BulletHandle();
            Thread.Sleep(200);
        }
    }
}

// UI Automation
public class UIauto
{

    public static int timeout = 5;
    public static FlaUI.Core.AutomationElements.AutomationElement dlg_metro, dlg_sess, dlg_ctrl, dlg_explorer, dlg_status, dlg_pos_sys, dlg_task, dlg_inspector, dlg_angle;
    public static string MetrotomPath = @"C:\Program Files\ZEISS\METROTOM OS\Bin\MetrotomOS.exe";

    public static FlaUI.Core.AutomationElements.AutomationElement dlg_menu, dlg_filer;
    public static string TestAppPath = @"notepad.exe";
    public static bool testing = true;

    public static FlaUI.Core.Application ConnectApp(string path)
    {
        Global.debug_log("in ConnectApp");
        try
        {
            Global.debug_log("attempting launch");
            //launches or attaches app
            var processStartInfo = new ProcessStartInfo(path);
            var app = FlaUI.Core.Application.AttachOrLaunch(processStartInfo);
            if(app == null)
            {
                throw new Exception("Could not attatch to application");
            }
            Global.debug_log("waiting for app");
            app.WaitWhileBusy();
            Global.debug_log("app launch success");

            return app;
            //ProcessStartInfo startInfo = new ProcessStartInfo(path);
            //Process process = new Process();
            //process.StartInfo = startInfo;
            //process.Start();
            //return process;
        }
        catch (Exception e)
        {
            Global.debug_log(e.Message);
            return null;
        }


    }

    public static FlaUI.Core.AutomationElements.AutomationElement GetChildByAutoID(FlaUI.Core.Application app, string autoId)
    {
        Global.debug_log("1");
        try
        {
            Global.debug_log("2");
            using (var automation = new UIA3Automation())
            {
                Global.debug_log("3");
                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
                var window = app.GetMainWindow(automation, TimeSpan.FromSeconds(timeout));
                if (window != null)
                {
                    Global.debug_log(window.Name);
                    foreach (var item in window.FindAllChildren())
                    {
                        Global.debug_log("    " + item.Name);
                    }
                    return window.FindFirstDescendant(cf.ByAutomationId(autoId));

                }
                else
                {
                    Global.debug_log("Could not get window");
                    FlaUI.Core.AutomationElements.AutomationElement temp = null;
                    return temp;
                }
            }
        }
        catch (Exception e)
        {
            Global.debug_log("4");
            Global.debug_log(e.Message);
            return null; 
        }
    }

    public static FlaUI.Core.AutomationElements.AutomationElement GetChildByName(FlaUI.Core.Application app, string Name)
    {
        try
        {
            using (var automation = new UIA3Automation())
            {
                if (app != null)
                {
                    ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
                    var window = app.GetMainWindow(automation, TimeSpan.FromSeconds(timeout));
                    if (window != null)
                    {
                        return window.FindFirstDescendant(cf.ByName(Name));
                    }
                    else
                    {
                        FlaUI.Core.AutomationElements.AutomationElement temp = null;
                        return temp;
                    }
                }
                else { return null; }
            }
        }
        catch { return null; }
    }

    public static void ClickAtCoordInApp(FlaUI.Core.Application app, int x, int y)
    {
        try
        {
            using (var automation = new UIA3Automation())
            {
                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
                var window = app.GetMainWindow(automation, TimeSpan.FromSeconds(timeout));
                //x = x + window.BoundingRectangle.X;
                //y = y + window.BoundingRectangle.Y;
                FlaUI.Core.Input.Mouse.Position = new System.Drawing.Point(x, y);//MoveTo(x, y);
                FlaUI.Core.Input.Mouse.Click();
            }
        }
        catch { }
    }

    public static void DoubleClickAtCoordInApp(FlaUI.Core.Application app, int x, int y)
    {
        try
        {
            using (var automation = new UIA3Automation())
            {
                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
                var window = app.GetMainWindow(automation, TimeSpan.FromSeconds(timeout));
                x = x + window.BoundingRectangle.X;
                y = y + window.BoundingRectangle.Y;
                Mouse.MoveTo(x, y);
                Mouse.DoubleClick();
            }
        }
        catch { }
    }

    public static void ClickElement(FlaUI.Core.Application app, FlaUI.Core.AutomationElements.AutomationElement element)
    {
        int x = element.GetClickablePoint().X;
        int y = element.GetClickablePoint().Y;
        ClickAtCoordInApp(app, x, y);
    }

    public static void DoubleClickElement(FlaUI.Core.Application app, FlaUI.Core.AutomationElements.AutomationElement element)
    {
        int x = element.GetClickablePoint().X;
        int y = element.GetClickablePoint().Y;
        DoubleClickAtCoordInApp(app, x, y);
    }
    public static void SetFocus(FlaUI.Core.Application app)
    {
        try
        {
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation, TimeSpan.FromSeconds(timeout));
                //Console.WriteLine(window.GetType());
                if (window != null)
                {
                    window.Focus();
                    window.FocusNative();
                    Console.WriteLine(window);

                    if (window.IsOffscreen)
                    {
                        FlaUI.Core.Input.Keyboard.Press(VirtualKeyShort.ENTER);
                    }
                }
            }
        }
        catch { }

    }

    //Not Done - FKL
    public static string ScanStatus(FlaUI.Core.Application app)
    {
        dlg_status = GetChildByAutoID(app, "statusStrip1").AsProgressBar();

        if (dlg_status != null)
        {
            SetFocus(app);
            return dlg_status.ToString(); // Not Done - FKL
        }
        else
        {
            return null;
        }
    }

    public static bool ClickLoadButton(FlaUI.Core.Application app)
    {
        SetFocus(app);
        dlg_pos_sys = GetChildByAutoID(app, "bbMachine");

        if (dlg_pos_sys != null)
        {
            var btn_height = dlg_pos_sys.BoundingRectangle.Height / 9;
            var left = dlg_pos_sys.BoundingRectangle.Left;
            var top = dlg_pos_sys.BoundingRectangle.Top + (btn_height * 2);
            var bottom = (dlg_pos_sys.BoundingRectangle.Top + (btn_height * 3));
            var right = dlg_pos_sys.BoundingRectangle.Right;

            int x = Convert.ToInt32((right + left) / 2);
            int y = Convert.ToInt32((bottom + top) / 2);

            ClickAtCoordInApp(app, x, y);

            return true;

        }
        else { return false; }
    }

    public static bool OpenNextTask(FlaUI.Core.Application app)
    {
        SetFocus(app);
        dlg_task = GetChildByAutoID(app, "tvMainTree");

        if (dlg_task != null)
        {

            var btn_height = 24;
            var btn_width = dlg_task.BoundingRectangle.Width - 38;
            var left = dlg_task.BoundingRectangle.Right - btn_width;
            var top = dlg_task.BoundingRectangle.Top + 280;
            var right = dlg_task.BoundingRectangle.Right;
            var bottom = top + btn_height;

            int x = Convert.ToInt32((right + left) / 2);
            int y = Convert.ToInt32((bottom + top) / 2);

            DoubleClickAtCoordInApp(app, x, y);

            return true;

        }
        else { return false; }

    }

    public static bool ClickPlayButton(FlaUI.Core.Application app)
    {
        SetFocus(app);
        dlg_inspector = GetChildByAutoID(app, "bbMeasure");

        if (dlg_inspector != null)
        {
            var btn_width = 35.5;
            var left = dlg_inspector.BoundingRectangle.Left;
            var top = dlg_inspector.BoundingRectangle.Top;
            var bottom = dlg_inspector.BoundingRectangle.Bottom;
            var right = dlg_inspector.BoundingRectangle.Left + btn_width;

            int x = Convert.ToInt32((right + left) / 2);
            int y = Convert.ToInt32((bottom + top) / 2);

            ClickAtCoordInApp(app, x, y);

            return true;

        }
        else { return false; }

    }

    public static bool TaskInspectorOpen(FlaUI.Core.Application app)
    {
        SetFocus(app);
        dlg_inspector = GetChildByAutoID(app, "CTInspector");

        if (dlg_inspector != null)
        {
            return false;

        }
        else { return true; }

    }

    public static bool WriteLoaderAngle(FlaUI.Core.Application app)
    {
        SetFocus(app);
        dlg_angle = GetChildByAutoID(app, "nudAngle");

        if (dlg_angle != null)
        {
            FlaUI.Core.Input.Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
            FlaUI.Core.Input.Keyboard.Press(VirtualKeyShort.KEY_0);
            FlaUI.Core.Input.Keyboard.Press(VirtualKeyShort.ENTER);

            return true;

        }
        else { return false; }

    }

    public static void CloseDialog(FlaUI.Core.Application app, string dialogName, string buttonName)
    {
        try
        {
            FlaUI.Core.AutomationElements.AutomationElement window = GetChildByName(app, dialogName);
            if (window != null)
            {
                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
                FlaUI.Core.AutomationElements.AutomationElement button = window.FindFirstDescendant(cf.ByName(buttonName));
                button.Click();
            }
        }
        catch { }
    }

    public static void find_dlgs()
    {
        Global.debug_log("in find_dlgs");
        if (testing)
        {
            FlaUI.Core.Application app = ConnectApp(TestAppPath);
            if (app == null)
            {
                Global.CTAutorun.Controls["ConnectApp"].Enabled = true;
                return;
            }
            SetFocus(app);
            Global.debug_log("finding children");
            dlg_menu = GetChildByAutoID(app, "MenuBar");
            if (dlg_menu == null)
            {
                Global.debug_log("Could not find MenuBar");
                return;
            }
            Global.debug_log("Found menubar");
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
            dlg_filer = dlg_menu.FindFirstChild(cf.ByName("Filer"));
            if (dlg_filer == null)
            {
                Global.debug_log("Could not find File");
                return;
            }
            //dlg_filer = GetChildByName(app, "File");
            Global.debug_log("found 'file'");
            //ClickElement(app, dlg_filer);
            dlg_filer.Click();

        }
        else
        {
            Global.debug_log("launching metrotom");
            FlaUI.Core.Application app = ConnectApp(Global.appPath);
            if(app == null)
            {
                Global.CTAutorun.Controls["ConnectApp"].Enabled = true;
                return;
            }
            SetFocus(app);
            Global.debug_log("finding children");
            dlg_metro = GetChildByAutoID(app, "MainForm");
            if(dlg_metro == null)
            {
                Global.debug_log("Could not find MainForm");
                return;
            }
            
            dlg_sess = GetChildByAutoID(app, "DockingMainWnd");
            if( dlg_sess == null)
            {
                Global.debug_log("Could not find DockingMainWnd");
                return;
            }
            
            dlg_ctrl = GetChildByAutoID(app, "uipControlPanel");
            if(dlg_ctrl == null)
            {
                Global.debug_log("Could not find uipControlPanel");
                return;
            }
            
            dlg_explorer = GetChildByAutoID(app, "uipSessionExplorer");
            if(dlg_explorer == null)
            {
                Global.debug_log("Could not find uipSessionExplorer");
                return;
            }
            

            var status = ScanStatus(app);
            Global.debug_log("Status: " + status);
        }
    }

}






//// UI Automation
//public class UIauto
//{

//    public static int timeout = 5;
//    public static FlaUI.Core.AutomationElements.AutomationElement dlg_metro, dlg_sess, dlg_ctrl, dlg_explorer, dlg_status, dlg_pos_sys, dlg_task, dlg_inspector, dlg_angle;
//    public static string MetrotomPath = @"C:\Program Files\ZEISS\METROTOM OS\Bin\MetrotomOS.exe";

//    public static FlaUI.Core.AutomationElements.AutomationElement dlg_menu, dlg_filer;
//    public static string TestAppPath = @"notepad.exe";
//    public static bool testing = false;
//    public static System.Windows.Forms.TextBox textBox = null;

//    public static FlaUI.Core.Application ConnectApp(string path)
//    {
//        Global.debug_log("in ConnectApp");
//        try
//        {
//            Global.debug_log("attempting launch");
//            //launches or attaches app
//            var processStartInfo = new ProcessStartInfo(path);
//            var app = FlaUI.Core.Application.AttachOrLaunch(processStartInfo);
//            app.WaitWhileBusy();

//            return app;
//        }
//        catch (Exception e)
//        {
//            Global.debug_log(e.Message);
//            return null;
//        }


//    }

//    public static FlaUI.Core.AutomationElements.AutomationElement GetChildByAutoID(FlaUI.Core.Application app, string autoId)
//    {
//        try
//        {
//            using (var automation = new UIA3Automation())
//            {
//                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
//                var window = app.GetMainWindow(automation, TimeSpan.FromSeconds(timeout));
//                if (window != null)
//                {
//                    return window.FindFirstDescendant(cf.ByAutomationId(autoId));

//                }
//                else
//                {
//                    FlaUI.Core.AutomationElements.AutomationElement temp = null;
//                    return temp;
//                }
//            }
//        }
//        catch { return null; }
//    }

//    public static FlaUI.Core.AutomationElements.AutomationElement GetChildByName(FlaUI.Core.Application app, string Name)
//    {
//        try
//        {
//            using (var automation = new UIA3Automation())
//            {
//                if (app != null)
//                {
//                    ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
//                    var window = app.GetMainWindow(automation, TimeSpan.FromSeconds(timeout));
//                    if (window != null)
//                    {
//                        return window.FindFirstDescendant(cf.ByName(Name));
//                    }
//                    else
//                    {
//                        FlaUI.Core.AutomationElements.AutomationElement temp = null;
//                        return temp;
//                    }
//                }
//                else { return null; }
//            }
//        }
//        catch { return null; }
//    }

//    public static void ClickAtCoordInApp(FlaUI.Core.Application app, int x, int y)
//    {
//        try
//        {
//            using (var automation = new UIA3Automation())
//            {
//                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
//                var window = app.GetMainWindow(automation, TimeSpan.FromSeconds(timeout));
//                //x = x + window.BoundingRectangle.X;
//                //y = y + window.BoundingRectangle.Y;
//                FlaUI.Core.Input.Mouse.MoveTo(x, y);
//                FlaUI.Core.Input.Mouse.Click();
//            }
//        }
//        catch { }
//    }

//    public static void DoubleClickAtCoordInApp(FlaUI.Core.Application app, int x, int y)
//    {
//        try
//        {
//            using (var automation = new UIA3Automation())
//            {
//                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
//                var window = app.GetMainWindow(automation, TimeSpan.FromSeconds(timeout));
//                x = x + window.BoundingRectangle.X;
//                y = y + window.BoundingRectangle.Y;
//                Mouse.MoveTo(x, y);
//                Mouse.DoubleClick();
//            }
//        }
//        catch { }
//    }

//    public static void ClickElement(FlaUI.Core.Application app, FlaUI.Core.AutomationElements.AutomationElement element)
//    {
//        int x = element.GetClickablePoint().X;
//        int y = element.GetClickablePoint().Y;
//        ClickAtCoordInApp(app, x, y);
//    }

//    public static void DoubleClickElement(FlaUI.Core.Application app, FlaUI.Core.AutomationElements.AutomationElement element)
//    {
//        int x = element.GetClickablePoint().X;
//        int y = element.GetClickablePoint().Y;
//        DoubleClickAtCoordInApp(app, x, y);
//    }
//    public static void SetFocus(FlaUI.Core.Application app)
//    {
//        try
//        {
//            using (var automation = new UIA3Automation())
//            {
//                var window = app.GetMainWindow(automation, TimeSpan.FromSeconds(timeout));
//                //Console.WriteLine(window.GetType());
//                if (window != null)
//                {
//                    window.Focus();
//                    window.FocusNative();
//                    Console.WriteLine(window);

//                    if (window.IsOffscreen)
//                    {
//                        FlaUI.Core.Input.Keyboard.Press(VirtualKeyShort.ENTER);
//                    }
//                }
//            }
//        }
//        catch { }

//    }

//    //Not Done - FKL
//    public static string ScanStatus(FlaUI.Core.Application app)
//    {
//        dlg_status = GetChildByAutoID(app, "statusStrip1").AsProgressBar();

//        if (dlg_status != null)
//        {
//            SetFocus(app);
//            return dlg_status.ToString(); // Not Done - FKL
//        }
//        else
//        {
//            return null;
//        }
//    }

//    public static bool ClickLoadButton(FlaUI.Core.Application app)
//    {
//        SetFocus(app);
//        dlg_pos_sys = GetChildByAutoID(app, "bbMachine");

//        if (dlg_pos_sys != null)
//        {
//            var btn_height = dlg_pos_sys.BoundingRectangle.Height / 9;
//            var left = dlg_pos_sys.BoundingRectangle.Left;
//            var top = dlg_pos_sys.BoundingRectangle.Top + (btn_height * 2);
//            var bottom = (dlg_pos_sys.BoundingRectangle.Top + (btn_height * 3));
//            var right = dlg_pos_sys.BoundingRectangle.Right;

//            int x = Convert.ToInt32((right + left) / 2);
//            int y = Convert.ToInt32((bottom + top) / 2);

//            ClickAtCoordInApp(app, x, y);

//            return true;

//        }
//        else { return false; }
//    }

//    public static bool OpenNextTask(FlaUI.Core.Application app)
//    {
//        SetFocus(app);
//        dlg_task = GetChildByAutoID(app, "tvMainTree");

//        if (dlg_task != null)
//        {

//            var btn_height = 24;
//            var btn_width = dlg_task.BoundingRectangle.Width - 38;
//            var left = dlg_task.BoundingRectangle.Right - btn_width;
//            var top = dlg_task.BoundingRectangle.Top + 280;
//            var right = dlg_task.BoundingRectangle.Right;
//            var bottom = top + btn_height;

//            int x = Convert.ToInt32((right + left) / 2);
//            int y = Convert.ToInt32((bottom + top) / 2);

//            DoubleClickAtCoordInApp(app, x, y);

//            return true;

//        }
//        else { return false; }

//    }

//    public static bool ClickPlayButton(FlaUI.Core.Application app)
//    {
//        SetFocus(app);
//        dlg_inspector = GetChildByAutoID(app, "bbMeasure");

//        if (dlg_inspector != null)
//        {
//            var btn_width = 35.5;
//            var left = dlg_inspector.BoundingRectangle.Left;
//            var top = dlg_inspector.BoundingRectangle.Top;
//            var bottom = dlg_inspector.BoundingRectangle.Bottom;
//            var right = dlg_inspector.BoundingRectangle.Left + btn_width;

//            int x = Convert.ToInt32((right + left) / 2);
//            int y = Convert.ToInt32((bottom + top) / 2);

//            ClickAtCoordInApp(app, x, y);

//            return true;

//        }
//        else { return false; }

//    }

//    public static bool TaskInspectorOpen(FlaUI.Core.Application app)
//    {
//        SetFocus(app);
//        dlg_inspector = GetChildByAutoID(app, "CTInspector");

//        if (dlg_inspector != null)
//        {
//            return false;

//        }
//        else { return true; }

//    }

//    public static bool WriteLoaderAngle(FlaUI.Core.Application app)
//    {
//        SetFocus(app);
//        dlg_angle = GetChildByAutoID(app, "nudAngle");

//        if (dlg_angle != null)
//        {
//            FlaUI.Core.Input.Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_A);
//            FlaUI.Core.Input.Keyboard.Press(VirtualKeyShort.KEY_0);
//            FlaUI.Core.Input.Keyboard.Press(VirtualKeyShort.ENTER);

//            return true;

//        }
//        else { return false; }

//    }

//    public static void CloseDialog(FlaUI.Core.Application app, string dialogName, string buttonName)
//    {
//        try
//        {
//            FlaUI.Core.AutomationElements.AutomationElement window = GetChildByName(app, dialogName);
//            if (window != null)
//            {
//                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
//                FlaUI.Core.AutomationElements.AutomationElement button = window.FindFirstDescendant(cf.ByName(buttonName));
//                button.Click();
//            }
//        }
//        catch { }
//    }

//    public static void find_dlgs(System.Windows.Forms.TextBox textBox1)
//    {
//        textBox = textBox1;
//        Global.debug_log("in find_dlgs");
//        if (testing)
//        {
//            FlaUI.Core.Application app = ConnectApp(TestAppPath);
//            SetFocus(app);
//            dlg_menu = GetChildByAutoID(app, "MenuBar");
//            dlg_filer = GetChildByName(app, "File");
//            ClickElement(app, dlg_filer);

//        }
//        else
//        {
//            Global.debug_log("launching metrotom");
//            FlaUI.Core.Application app = ConnectApp(MetrotomPath);
//            SetFocus(app);
//            dlg_metro = GetChildByAutoID(app, "MainForm");
//            dlg_sess = GetChildByAutoID(app, "DockingMainWnd");
//            dlg_ctrl = GetChildByAutoID(app, "uipControlPanel");
//            dlg_explorer = GetChildByAutoID(app, "uipSessionExplorer");

//            var status = ScanStatus(app);
//        }
//    }

//}