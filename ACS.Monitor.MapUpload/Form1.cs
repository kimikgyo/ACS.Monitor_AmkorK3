using log4net;
using Monitor.Data;
using Monitor.Map;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ACS.Monitor.MapUpload
{
    public partial class Form1 : Form
    {
        private readonly static ILog EventLogger = LogManager.GetLogger("Event"); //Function 실행관련 Log
        private string mapName;
        private readonly UnitOfWork uow;

        public Form1()
        {
            InitializeComponent();
            uow = new UnitOfWork();
            radioButton1.CheckedChanged += mapSelectionChanged;
            radioButton2.CheckedChanged += mapSelectionChanged;
            radioButton1.Checked = true; // select 1st.
        }

        private void mapSelectionChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton btn && btn.Checked)
                mapName = btn.Text;
        }

        private void btnCustomMapUpload_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog { Title = "select image to upload", DefaultExt = "png", Filter = "PNG File(*.png)|*.png" };
            dlg.FileName = $"custom_map_{mapName}";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                btnCustomMapUpload.Enabled = false;
                try
                {
                    // get image from file
                    using (FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
                    using (Image image = Image.FromStream(fs))
                    {
                        // save image data to db
                        string imageData = uow.CustomMaps.ConvertImageToEncodedString(image);
                        uow.CustomMaps.SetMapImageData(mapName, imageData);
                        AddLog($"custom map upload ({mapName})");
                    }
                }
                catch (Exception ex)
                {
                    AddLog(ex.ToString());
                }
                btnCustomMapUpload.Enabled = true;
            }
        }

        private void btnCustomMapDownload_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog { Title = "save image", DefaultExt = "png", Filter = "PNG File(*.png)|*.png" };
            dlg.FileName = $"custom_map_{mapName}";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                btnCustomMapDownload.Enabled = false;
                try
                {
                    // get image data from db
                    string imageData = uow.CustomMaps.GetMapImageData(mapName);

                    // save image to file
                    using (Image image = uow.CustomMaps.ConvertEncodedStringToImage(imageData))
                    {
                        image.Save(dlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        AddLog($"custom map download ({mapName})");
                    }
                }
                catch (Exception ex)
                {
                    AddLog(ex.ToString());
                }
                btnCustomMapDownload.Enabled = true;
            }
        }

        private void btnMapDownload_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog { Title = "save image", DefaultExt = "png", Filter = "PNG File(*.png)|*.png" };
            dlg.FileName = $"fleet_map_{mapName}";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                btnMapDownload.Enabled = false;
                try
                {
                    // get map data
                    var uriStr = "http://10.141.26.107/";
                    //var uriStr = "http://localhost:5000/";
                    var mapID = mapName == "M3F" ? textBox1.Text : textBox2.Text;
                    var mapProcessor = new FleetMapProcessor(EventLogger, uriStr, mapName);
                    var map = mapProcessor.GetMap(mapID, readPositions: false);

                    if (map != null && map.Image != null)
                    {
                        map.Image.Save(dlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        AddLog($"fleet map download ({mapName})");
                    }
                    else
                    {
                        AddLog($"fleet map download error! ({mapName})");
                    }
                }
                catch (Exception ex)
                {
                    AddLog($"{ex} ({mapName})");
                }
                btnMapDownload.Enabled = true;
            }
        }

        private void AddLog(string m)
        {
            try
            {
                EventLogger.Info(m);
                listBox1.Items.Add(m);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
