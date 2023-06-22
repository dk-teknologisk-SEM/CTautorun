using System;
using System.Windows.Forms;
using Modbus.Device;
using System.Net.Sockets;

namespace CTAutorun
{
    public partial class CTAutorun : Form
    {
        public CTAutorun()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(CTAutorun_KeyDown);
            this.FormClosing += CTAutorun_FormClosing;
            Global.CTAutorun = this;
            Global.debugLog = textBox1;
        }

        private void CTAutorun_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

    private void ConnectApp_click(object sender, EventArgs e)
        {
            UIauto.find_dlgs();
            ConnectApp.Enabled = false;

            //StartModbusTcpSlave();
            //if(Global.thread4.ThreadState == System.Threading.ThreadState.Unstarted)
            //{ Global.thread4.Start(); }
        }
        /// <summary>
        ///     Simple Modbus TCP slave example.
        /// </summary>
        public static void StartModbusTcpSlave()
        { 
            //if(Global.client.Connected)
            //{
            //    return;
            //}
            try
            {

                Global.client = new TcpClient("192.168.1.10", 502);

                Global.master = ModbusIpMaster.CreateIp(Global.client);

                Global.master.WriteSingleRegister(130, 100);
                var rr = Global.master.ReadHoldingRegisters(130, 1);
                if (rr == null)
                {
                    Global.debug_log("Could not connect to robot");
                }
                else if (rr[0] != 100)
                {
                    Global.debug_log("Read " + rr[0] + " from robot, expected 100");
                }
                else
                {
                    Global.debug_log("Connected to robot");
                }
            }
            catch (Exception ex)
            {
                Global.debug_log("Error connecting to robot: " +  ex.Message);
            }
            // prevent the main thread from exiting
            //Thread.Sleep(Timeout.Infinite);

        }

        private void CTAutorun_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //Cross thread bullet communication
        //INIT,
        //LOAD_TRAY,
        //SELECT_TASK,
        //SCAN,
        //QUERY_FILES,
        //QUERY_ROBOT,
        //EXIT,
        //CONNECT_ROBOT,
        //DISCONNECT_ROBOT,
        //STOP,
        //TERMINATED

        private delegate void BulletDelegate(bool b);
        public void SetInitBullet(bool b)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new BulletDelegate(SetInitBullet), new object[] { b });

                return;
            }

            Global.CTAutorun.initBullet.Checked = b;
        }
        public void SetLoad_trayBullet(bool b)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new BulletDelegate(SetLoad_trayBullet), new object[] { b });

                return;
            }

            Global.CTAutorun.load_trayBullet.Checked = b;
        }
        public void SetSelect_taskBullet(bool b)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new BulletDelegate(SetSelect_taskBullet), new object[] { b });

                return;
            }

            Global.CTAutorun.select_taskBullet.Checked = b;
        }
        public void SetScanBullet(bool b)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new BulletDelegate(SetScanBullet), new object[] { b });

                return;
            }

            Global.CTAutorun.scanBullet.Checked = b;
        }
        public void SetQuery_filesBullet(bool b)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new BulletDelegate(SetQuery_filesBullet), new object[] { b });

                return;
            }

            Global.CTAutorun.query_filesBullet.Checked = b;
        }
        public void SetQuery_robotBullet(bool b)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new BulletDelegate(SetQuery_robotBullet), new object[] { b });

                return;
            }

            Global.CTAutorun.query_robotBullet.Checked = b;
        }
        public void SetExitBullet(bool b)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new BulletDelegate(SetExitBullet), new object[] { b });

                return;
            }

            Global.CTAutorun.exitBullet.Checked = b;
        }
        public void SetConnect_robotBullet(bool b)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new BulletDelegate(SetConnect_robotBullet), new object[] { b });

                return;
            }

            Global.CTAutorun.connect_robotBullet.Checked = b;
        }
        public void SetDisconnect_robotBullet(bool b)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new BulletDelegate(SetDisconnect_robotBullet), new object[] { b });

                return;
            }

            Global.CTAutorun.disconnect_robotBullet.Checked = b;
        }
        public void SetStopBullet(bool b)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new BulletDelegate(SetStopBullet), new object[] { b });

                return;
            }

            Global.CTAutorun.stopBullet.Checked = b;
        }
        public void SetInactiveBullet(bool b)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new BulletDelegate(SetInactiveBullet), new object[] { b });

                return;
            }

            Global.CTAutorun.inactiveBullet.Checked = b;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Global.thread1.IsAlive) { 
                Global.thread1.Name = "StateMachine";
                Global.thread1.IsBackground = true;
                Global.thread1.Start();
            }
            Global.ForceNewState = ProcessState.INIT;
            Global.ForceState = true;

            Global.AppStopFlag = false;
            Global.AppPausedFlag = false;
            startButton.Enabled = false;
            stop_button.Enabled = true;
            pause_button.Enabled = true;
        }

        private void comm_test_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            Global.run_comm_test = comm_test_checkBox.Checked;
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Global.ForceNewState = ProcessState.INIT;
            Global.ForceState = true;
        }

        private void load_trayBullet_CheckedChanged(object sender, EventArgs e)
        {
            Global.ForceNewState = ProcessState.LOAD_TRAY;
            Global.ForceState = true;
        }

        private void select_taskBullet_CheckedChanged(object sender, EventArgs e)
        {
            Global.ForceNewState = ProcessState.SELECT_TASK;
            Global.ForceState = true;
        }

        private void scanBullet_CheckedChanged(object sender, EventArgs e)
        {
            Global.ForceNewState = ProcessState.SCAN;
            Global.ForceState = true;
        }

        private void query_filesBullet_CheckedChanged(object sender, EventArgs e)
        {
            Global.ForceNewState = ProcessState.QUERY_FILES;
            Global.ForceState = true;
        }

        private void query_robotBullet_CheckedChanged(object sender, EventArgs e)
        {
            Global.ForceNewState = ProcessState.QUERY_ROBOT;
            Global.ForceState = true;
        }

        private void exitBullet_CheckedChanged(object sender, EventArgs e)
        {
            Global.ForceNewState = ProcessState.EXIT;
            Global.ForceState = true;
        }

        private void connect_robotBullet_CheckedChanged(object sender, EventArgs e)
        {
            Global.ForceNewState = ProcessState.CONNECT_ROBOT;
            Global.ForceState = true;
        }

        private void disconnect_robotBullet_CheckedChanged(object sender, EventArgs e)
        {
            Global.ForceNewState = ProcessState.DISCONNECT_ROBOT;
            Global.ForceState = true;
        }

        private void stopBullet_CheckedChanged(object sender, EventArgs e)
        {
            Global.ForceNewState = ProcessState.STOP;
            Global.ForceState = true;
        }

        private void inactiveBullet_CheckedChanged(object sender, EventArgs e)
        {
            Global.ForceNewState = ProcessState.INACTIVE;
            Global.ForceState = true;
        }

        private void CTAutorun_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.Shift && e.KeyCode == Keys.F12)
                if(Global.debugging)
                {
                    initBullet.Enabled = false;
                    load_trayBullet.Enabled = false;
                    select_taskBullet.Enabled = false;
                    scanBullet.Enabled = false;
                    query_filesBullet.Enabled = false;
                    query_robotBullet.Enabled = false;
                    exitBullet.Enabled = false;
                    connect_robotBullet.Enabled = false;
                    disconnect_robotBullet.Enabled = false;
                    stopBullet.Enabled = false;
                    inactiveBullet.Enabled = false;

                    Global.debugging = false;
                }
                else
                {
                    initBullet.Enabled = true;
                    load_trayBullet.Enabled = true;
                    select_taskBullet.Enabled = true;
                    scanBullet.Enabled = true;
                    query_filesBullet.Enabled = true;
                    query_robotBullet.Enabled = true;
                    exitBullet.Enabled = true;
                    connect_robotBullet.Enabled = true;
                    disconnect_robotBullet.Enabled = true;
                    stopBullet.Enabled = true;
                    inactiveBullet.Enabled = true;

                    Global.debugging = true;
                }
        }


        delegate void SetTextCallback(string text);

        public void AddText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AddText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.AppendText(text + Environment.NewLine);
            }
        }

        private void pause_button_Click(object sender, EventArgs e)
        {
            Global.AppPausedFlag = !Global.AppPausedFlag;
            pause_button.Text = Global.AppPausedFlag ? "Continue" : "Pause";
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            Global.ForceNewState = ProcessState.INACTIVE;
            Global.ForceState = true;
            Global.AppStopFlag = true;
            stop_button.Enabled = false;
            pause_button.Enabled = false;
            startButton.Enabled = true;
        }
    }



}
//INIT,
        //LOAD_TRAY,
        //SELECT_TASK,
        //SCAN,
        //QUERY_FILES,
        //QUERY_ROBOT,
        //EXIT,
        //CONNECT_ROBOT,
        //DISCONNECT_ROBOT,
        //STOP,
        //TERMINATED