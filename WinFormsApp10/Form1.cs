namespace WinFormsApp10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SaveFileDialog of2;
        OpenFileDialog of;
        string path,path1;
        FileStream fs,fs1;
        StreamReader sr;
        StreamWriter sw,sw1;
        private void openbtn_Click(object sender, EventArgs e)
        {
            of = new OpenFileDialog();
            of.ShowDialog();
            path=of.FileName;
            fs = new FileStream(path, FileMode.OpenOrCreate,FileAccess.ReadWrite);
            sr = new StreamReader(fs);
            sw = new StreamWriter(fs);
        }

        private void insertbtn_Click(object sender, EventArgs e)
        {
            sw.Write(idtxtbx.Text + "|" + fntxtbx.Text + "|" + lntxtbx.Text + "|" + agetxtbx.Text + "\r\n");
            sw.Flush();
        }

        private void closebtn_Click(object sender, EventArgs e)
        {  
            Environment.Exit(0);
        }

        private void readbtn_Click(object sender, EventArgs e)
        {
            fs.Seek(0, SeekOrigin.Begin);
            string l;
            if((l= sr.ReadLine()) != null)
            {
                if(l[0]!='*')
                readtxtbx.Text += l +"\r\n";
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            fs.Seek(0, SeekOrigin.Begin);
            string l;
            while ((l = sr.ReadLine()) != null)
            {
                string[] arr = l.Split('|');
                if(arr[0]==idtxtbx.Text)
                {
                    MessageBox.Show("Found!");
                    readtxtbx.Text = arr[0] + "|" + arr[1] + "|" + arr[2] + "|" + arr[3];
                    return;
                }
            }
            MessageBox.Show("Not Found!");
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            fs.Seek(0, SeekOrigin.Begin);
            int c = 0;
            string l;
            while ((l = sr.ReadLine()) != null)
            {
                string[] arr = l.Split('|');
                if (arr[0] == idtxtbx.Text)
                {
                    fs.Seek(c, SeekOrigin.Begin);
                    sw.Write("*");
                    sw.Flush();
                    MessageBox.Show("Deleted!");
                    return;
                }
                c += l.Length + 2;
            }
            MessageBox.Show("Not Found!");
        }
        SaveFileDialog sf;
        private void sqzbtn_Click(object sender, EventArgs e)
        {
            fs.Seek(0, SeekOrigin.Begin);

            of2 = new SaveFileDialog();
            of2.ShowDialog();
            path1 = of2.FileName;

            fs1 = new FileStream(path1, FileMode.OpenOrCreate, FileAccess.Write);
            sw1 = new StreamWriter(fs1);
            string l;
            while ((l = sr.ReadLine()) != null)
            {
                if (l[0] != '*')
                    sw1.Write(l + "\r\n");
            }
            sw1.Flush();
            MessageBox.Show("Squeezed in : " + path1);
        }
    }
}