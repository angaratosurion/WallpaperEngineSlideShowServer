using System.Diagnostics;

namespace WallpaperEngineSlideShowServer.Win
{
    public partial class frmMain : Form
    {
        Process proccess;
        public frmMain()
        {
            InitializeComponent();
            System.Diagnostics.ProcessStartInfo start =
      new System.Diagnostics.ProcessStartInfo();
            start.FileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),
                "WallpaperEngineSlideShowServer.exe");
            start.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; //Hides GUI
            start.CreateNoWindow = true; //Hides console
            proccess = new Process();
            proccess.StartInfo = start;
            proccess.Start();

            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
            this.Text=String.Format("{0} - {1} ",Application.ProductName,Application.ProductVersion);
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.notifyIcon1.Icon = this.Icon;
            this.notifyIcon1.Text = this.Text;
            

        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            if( proccess != null)
            {
                proccess.Kill();
            }
            Application.Exit();
        }
    }
}