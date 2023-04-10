using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Test_Editor
{
    public partial class Form1 : Form
    {
        private const string defoultFont = "Calibri";
        private const int fontsDefoulSize = 14;
        private int countPages = 0;
        private Font fontsSelector;
        public Form1()
        {
            InitializeComponent();
        }
        public RichTextBox getCurrentDoc
        {
            get { return (RichTextBox)tabControl1.SelectedTab.Controls["Page"]; }
        }
    private void AddPage()
    {
            RichTextBox newpage = new RichTextBox
            {//текстовое поле где писать
                Name = "Page ",
                AcceptsTab = true,
                Dock = DockStyle.Fill,
                ContextMenuStrip = contextMenuStrip1,
                Font = this.fontsSelector
            };
            this.countPages++; // счетчик страниц 
            string pageName = "Страница - " + this.countPages; //заготовка имени страницы
            TabPage newPage = new TabPage// страничка = новый файл 
            {
                Name = pageName,
                Text = pageName
            };
            newPage.Controls.Add(newpage); // добавлет текстовое поле в страницу
            tabControl1.TabPages.Add(newPage);  // добавлят страницу в табконтрол
    }
        private void DeletePages()
        {
            if(tabControl1.TabPages.Count != 1)
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
            else
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                this.countPages = 0;
                AddPage();
            }
        }
        private void DeleteAllPages()
        {
            foreach(TabPage Page in tabControl1.TabPages)
            {
                tabControl1.TabPages.Remove(Page);
            }
            this.countPages = 0;
            AddPage(); 
        }
        // здесь будут еще методы по контролю численности папок табконтроля

        //конец отступа
        private void Open()
        {
            ofdNew.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofdNew.Filter = "RTF Document (RTF)|*.rtf";
            if(ofdNew.ShowDialog() == DialogResult.OK) 
            {
                if(ofdNew.FileName.Length> 0) 
                {
                    try
                    {
                        AddPage();
                        tabControl1.SelectedTab = tabControl1.TabPages["Страница - " + this.countPages];
                        getCurrentDoc.LoadFile(ofdNew.FileName, RichTextBoxStreamType.RichText);
                        string numbArchiv = Path.GetFileName(ofdNew.FileName);
                        tabControl1.SelectedTab.Text = numbArchiv;
                        tabControl1.SelectedTab.Name= numbArchiv;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void SaveDoc()
        {
            saveFDDoc.FileName = tabControl1.SelectedTab.Name;
            saveFDDoc.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFDDoc.Filter = "RTF Document (RTF)|*.rtf";
            saveFDDoc.Title = "Сохранить документ!";
            if(saveFDDoc.ShowDialog() == DialogResult.OK)
            {
                if (saveFDDoc.FileName.Length > 0)
                {
                    try
                    {
                        getCurrentDoc.SaveFile(saveFDDoc.FileName, RichTextBoxStreamType.RichText);
                         string numbArchiv = Path.GetFileName(saveFDDoc.FileName);
                        tabControl1.SelectedTab.Text= numbArchiv;
                        tabControl1.SelectedTab.Name = numbArchiv;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPage();
        }
    }
}
