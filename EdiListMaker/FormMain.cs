using EdiUtils.Common;
using kr.co.kalpa.common.CommonUtil;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EdiUtils
{
    public partial class FormMain : Form
    {

        System.Windows.Forms.ListView currentListView = null;
        System.Windows.Forms.TextBox currentTextBoxDir = null;
        System.Windows.Forms.TextBox currentTextBoxLog = null;

        List<Invoice810> invoice810s = new List<Invoice810>();
        List<PurchaseOrder850> purchaseOrder850s = new List<PurchaseOrder850>();
        List<FreightInvoice210> freightInvoice210s = new List<FreightInvoice210>();
        List<WarehouseShippingOrder945> warehouseShippingOrders = new List<WarehouseShippingOrder945>();

        IConfig config; // config
        string configPath = "";
        string programPath = "";

        public FormMain()
        {
            InitializeComponent();

            //버젼을 타이틀에 표시
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.Text = this.Text + version;

            ListViewInitialize(listView810, "Invoice 810 List");
            ListViewInitialize(listView850, "Purchase Order 850 List");
            ListViewInitialize(listView210, "Freight Invoice 210List");
            ListViewInitialize(listView945, "Warehouse Shipping Order 945");

            tabContainer.SelectedTab = tab810;
            currentListView = listView810;
            currentTextBoxDir = targetDir810;
            currentTextBoxLog = textBoxLog810;

            this.Size = new System.Drawing.Size(800, 600);

            //상신일
            maskedTextBox1.Text = DateTime.Now.ToString("yyyyMMdd");

            //Config생성
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            programPath = System.IO.Path.GetDirectoryName(strExeFilePath);
            configPath = $"{programPath}/EdiUtils.config";
            config = new FileConfig();
            config.Load(configPath);

            targetDir850.Text = config.Get("850LastSourceFolder");
            targetDir810.Text = config.Get("810LastSourceFolder");
            targetDir210.Text = config.Get("210LastSourceFolder");
            targetDir945.Text = config.Get("945LastSourceFolder");

            //event handler
            ExcelUtils.MessageEventHandler += ExcelUtils_MessageEventHandler;
        }
        private void InitializeProcess(object sender, EventArgs e)
        {
            listView810.Items.Clear();
            listView850.Items.Clear();
            listView210.Items.Clear();
            targetDir810.Text = targetDir850.Text = targetDir210.Text = txtResultFolder.Text = "";

            tabContainer.SelectedTab = tab810;
            currentListView = listView810;
            currentTextBoxDir = targetDir810;
            currentTextBoxLog = textBoxLog810;

            invoice810s.Clear();
            purchaseOrder850s.Clear();
            freightInvoice210s.Clear();

            //btn Run
            btnRun810.Enabled = true;
            btnPo850OnlyCreate.Enabled = true;
            btnRunResult.Enabled = true;
            btnEach810Create.Enabled = true;
            //810 btn list browser text readonly 해제
            btnList810.Enabled = true;
            btnOpenDir810.Enabled = true;
            targetDir810.ReadOnly = false;
            //850 btn list browser text readonly 해제
            btnList850.Enabled = true;
            btnOpenDir850.Enabled = true;
            targetDir850.ReadOnly = false;
            //210 btn 해제
            btnList210.Enabled = true;
            btnOpenDir210.Enabled = true;
            targetDir210.ReadOnly = false;
            //945 btn 해제
            btnList945.Enabled = true;
            btnOpenDir945.Enabled = true;
            targetDir945.ReadOnly = false;

            //result
            txtResultFolder.ReadOnly = false;
            btnResultFolderDialog.Enabled = true;

            //log box clear
            textBoxLog810.Text = textBoxLog850.Text = txtBoxLog210.Text = txtBoxLog945.Text = txtResultLog.Text = "";

        }

        private void ListViewInitialize(System.Windows.Forms.ListView listView, string text)
        {
            listView.View = View.Details;
            listView.GridLines = true;
            listView.Columns.Add("No", 30, HorizontalAlignment.Right);
            listView.Columns.Add("File name", 500, HorizontalAlignment.Left);
            listView.Columns.Add("Full Path", 0, HorizontalAlignment.Left);
            listView.Text = text;
            listView.FullRowSelect = true;
            listView.MultiSelect = false;
        }
        /// <summary>
        /// 메세지 핸들러 - ExcelUtil클래스에서 나오는 메세지를 로깅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelUtils_MessageEventHandler(object sender, MessageEventArgs e)
        {
            WriteLog(e.Message);
        }
        /// <summary>
        /// 메뉴 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuClickProcess(object sender, EventArgs e)
        {
            if (sender == mnuFileAbout)
            {
                AboutBox1 about = new AboutBox1();
                about.ShowDialog();
            }
            else if (sender == mnuFileQuit)
            {
                System.Windows.Forms.Application.Exit();
            }
        }
        /// <summary>
        /// 폴더열기버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderOpenButtonClick(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                currentListView?.Items.Clear();
                DialogResult result = fbd.ShowDialog();

                fbd.ShowNewFolderButton = false;
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    currentTextBoxDir.Text = fbd.SelectedPath;
                }
            }
        }
        /// <summary>
        /// 리스트 만들기 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListClick(object sender, EventArgs e)
        {

            string dir = currentTextBoxDir.Text;
            if (Directory.Exists(dir) == false)
            {
                MsgBox.Error("대상폴더가 존재하지 않습니다");
                return;
            }
            currentListView.Items.Clear();

            string[] files = Directory.GetFiles(dir);
            int i = 1;
            foreach (var file in files)
            {
                bool isHidden = File.GetAttributes(file).HasFlag(FileAttributes.Hidden);
                if (file.EndsWith(".xlsx") && !file.StartsWith("~") && isHidden == false)
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(i));
                    item.SubItems.Add(file.Substring(dir.Length + 1));
                    item.SubItems.Add(file);
                    currentListView.Items.Add(item);

                    i++;
                }
            }
            if (currentListView.Items.Count == 0)
            {
                MsgBox.Warning("대상폴더에 엑셀 파일이 없습니다");
                return;
            }
            if (currentTextBoxDir == targetDir810)
            {
                config.Set("810LastSourceFolder", dir);
            }
            else if (currentTextBoxDir == targetDir850)
            {
                config.Set("850LastSourceFolder", dir);
            }
            else if (currentTextBoxDir == targetDir210)
            {
                config.Set("210LastSourceFolder", dir);
            }
            else if (currentTextBoxDir == targetDir945)
            {
                config.Set("945LastSourceFolder", dir);
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabContainer.SelectedTab == tab810)
            {
                currentListView = listView810;
                currentTextBoxDir = targetDir810;
                currentTextBoxLog = textBoxLog810;
            }
            else if (tabContainer.SelectedTab == tab850)
            {
                currentListView = listView850;
                currentTextBoxDir = targetDir850;
                currentTextBoxLog = textBoxLog850;
            }
            else if (tabContainer.SelectedTab == tab210)
            {
                currentListView = listView210;
                currentTextBoxDir = targetDir210;
                currentTextBoxLog = txtBoxLog210;
            }
            else if (tabContainer.SelectedTab == tab945)
            {
                currentListView = listView945;
                currentTextBoxDir = targetDir945;
                currentTextBoxLog = txtBoxLog945;
            }
            else if (tabContainer.SelectedTab == tabResult)
            {
                currentListView = null;
                currentTextBoxDir = txtResultFolder;
                currentTextBoxLog = txtResultLog;
            }
        }
        /// <summary>
        /// 로그 출력
        /// </summary>
        /// <param name="msg"></param>
        private void WriteLog(string msg)
        {
            var text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " > " + msg + "\r\n";
            currentTextBoxLog.AppendText(text);
            currentTextBoxLog.ScrollToCaret();
        }


        /// <summary>
        /// 생성된 엑셀을 저장할 폴더를 체크한다. 
        /// </summary>
        /// <returns>정상: true</returns>
        private bool TargetFolderCheck(string targetFolder)
        {
            if (string.IsNullOrEmpty(targetFolder))
            {
                MsgBox.Error("대상폴더가 비어 있습니다.");
                return false;
            }
            if (Directory.Exists(targetFolder) == false)
            {
                DialogResult dr = MsgBox.Confirm($"{targetFolder} 가 존재하지 않습니다\r\n생성하시겠습니까?");
                if (dr == DialogResult.Yes)
                {
                    Directory.CreateDirectory(targetFolder);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            config.Save(configPath);
        }

        #region 결과탭의 버튼들 동작
        /// <summary>
        /// 최종 엑셀파일들을 만든다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRunResult_Click(object sender, EventArgs e)
        {
            var targetFolder = txtResultFolder.Text;
            if (TargetFolderCheck(targetFolder) == false) { return; }

            if (invoice810s.Count == 0)
            {
                MsgBox.Error("인보이스 810의 자료가 없습니다");
                return;
            }
            if (purchaseOrder850s.Count == 0)
            {
                MsgBox.Error("발주서 850의 자료가 없습니다");
                return;

            }
            WriteLog("인보이스 리스트 엑셀파일 만들기 시작...");
            var template810Path = "";
            var template850Path = "";
            try
            {
                //btnRunResult.Enabled = false;
                //txtResultFolder.ReadOnly = true;
                //btnResultFolderDialog.Enabled = false;

                //1. 템플릿이 되는 파일을 리소스에서 가져와서 생성한다.
                template810Path = Path.Combine(targetFolder, CommonUtil.RandomFilename("t810.xlsx"));
                File.WriteAllBytes(template810Path, EdiListMaker.Properties.Resources.Template_810_list);

                template850Path = Path.Combine(targetFolder, CommonUtil.RandomFilename("t850.xlsx"));
                File.WriteAllBytes(template850Path, EdiListMaker.Properties.Resources.template_850_list);

                //2. 810 결과를 만든다.
                var ssDate = maskedTextBox1.Text;
                string output810Path = ExcelUtils.CreateList810(template810Path, invoice810s, purchaseOrder850s, ssDate);
                WriteLog($"{output810Path} 가 만들어졌습니다");
                CommonUtil.DeleteFile(template810Path);

                //3. 850 결과를 만든다.
                string output850Path = ExcelUtils.CreateList850(template850Path, purchaseOrder850s);
                WriteLog($"{output850Path} 가 만들어졌습니다");
                CommonUtil.DeleteFile(template850Path);

            }
            catch (Exception ex)
            {

                MsgBox.Error(ex.Message);

            }
            finally
            {
                CommonUtil.DeleteFile(template810Path);
                CommonUtil.DeleteFile(template850Path);
            }
        }

        private void btnPo850OnlyCreate_Click(object sender, EventArgs e)
        {
            var targetFolder = txtResultFolder.Text;
            if (TargetFolderCheck(targetFolder) == false) { return; }

            if (purchaseOrder850s.Count == 0)
            {
                MsgBox.Error("발주서 850의 자료가 없습니다");
                return;

            }
            WriteLog("Purchase Order 리스트 엑셀파일 만들기 시작...");
            var template850Path = "";
            try
            {
                //btnRunResult.Enabled = false;
                //txtResultFolder.ReadOnly = true;
                //btnResultFolderDialog.Enabled = false;


                //template850Path = Path.Combine(targetFolder, CommonUtil.RandomFilename("t850.xlsx"));
                //File.WriteAllBytes(template850Path, Properties.Resources.template_850_List2);
                var templateOrg = $"{programPath}/template_850_List2.xlsx";
                template850Path = Path.Combine(targetFolder, CommonUtil.RandomFilename("t850.xlsx"));
                File.Copy(templateOrg, template850Path);
                //3. 850 결과를 만든다.
                string output850Path = ExcelUtils.CreateList850_2(template850Path, purchaseOrder850s, config);
                WriteLog($"{output850Path} 가 만들어졌습니다");
                CommonUtil.DeleteFile(template850Path);

            }
            catch (Exception ex)
            {
                //WriteLog(string.Format("\nMessage ---\n{0}", ex.Message));
                //WriteLog(string.Format("\nSource ---\n{0}", ex.Source));
                //WriteLog(string.Format("\nStackTrace ---\n{0}", ex.StackTrace));
                //WriteLog(string.Format("\nTargetSite ---\n{0}", ex.TargetSite));
                WriteLog(ex.ToString());
                MsgBox.Error(ex.Message);

            }
            finally
            {
                CommonUtil.DeleteFile(template850Path);
            }
        }
        /// <summary>
        /// PO 850에 해당하는 Invoice 810을 만든다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEach810Create_Click(object sender, EventArgs e)
        {
            var targetFolder = txtResultFolder.Text;

            if (TargetFolderCheck(targetFolder) == false) { return; }
            if (purchaseOrder850s.Count == 0)
            {
                MsgBox.Error("발주서 850의 자료가 없습니다\r\nPO 850탭에서 엑셀데이터를 로딩해주십시오");
                return;

            }
            //btnRunResult.Enabled = false;
            //btnEach810Create.Enabled = false;
            //btnResultFolderDialog.Enabled = false;
            //btnPo850OnlyCreate.Enabled = false;
            //txtResultFolder.ReadOnly = true;

            var template810Invoice = "";
            try
            {
                WriteLog("purchace order 850으로부터 invoice 810을 생성하는 작업이 시작되었습니다.");
                template810Invoice = Path.Combine(targetFolder, CommonUtil.RandomFilename("t810.xlsx"));
                File.WriteAllBytes(template810Invoice, EdiListMaker.Properties.Resources.template_810_invoice);
                int totalCount = purchaseOrder850s.Count;
                int cnt = 1;
                foreach (var po850 in purchaseOrder850s)
                {
                    string output810Path = ExcelUtils.Po850ToInvoice810(template810Invoice, po850);
                    WriteLog($"{cnt++} / {totalCount} :  {output810Path} 가 만들어졌습니다");
                }
                WriteLog("\r\n모든 작업이 끝났습니다");
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);
            }
            finally
            {
                CommonUtil.DeleteFile(template810Invoice);
            }


        }
        private void btn210ListCreate_Click(object sender, EventArgs e)
        {
            var targetFolder = txtResultFolder.Text;
            if (TargetFolderCheck(targetFolder) == false) { return; }

            if (freightInvoice210s.Count == 0)
            {
                MsgBox.Error("Invoice 210의 자료가 없습니다");
                return;

            }
            WriteLog("Freight Invoice 리스트 엑셀파일 만들기 시작...");
            var template210Path = "";
            try
            {
                template210Path = Path.Combine(targetFolder, CommonUtil.RandomFilename("t210.xlsx"));
                File.WriteAllBytes(template210Path, EdiListMaker.Properties.Resources.template_210_list);

                // 210 List 엑셀를 만든다.
                string output210Path = ExcelUtils.CreateList210(template210Path, freightInvoice210s, config);
                WriteLog($"{output210Path} 가 만들어졌습니다");
                CommonUtil.DeleteFile(template210Path);

            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
                MsgBox.Error(ex.Message);

            }
            finally
            {
                CommonUtil.DeleteFile(template210Path);
            }
        }

        private void btn945ListCreate_Click(object sender, EventArgs e)
        {
            var targetFolder = txtResultFolder.Text;
            if (TargetFolderCheck(targetFolder) == false) { return; }

            if (warehouseShippingOrders.Count == 0)
            {
                MsgBox.Error("Warehouse Shipping Order 945의 자료가 없습니다");
                return;

            }
            WriteLog("Warehouse Shipping Order 리스트 엑셀파일 만들기 시작...");
            var template945Path = "";
            try
            {
                template945Path = Path.Combine(targetFolder, CommonUtil.RandomFilename("t945.xlsx"));
                File.WriteAllBytes(template945Path, EdiListMaker.Properties.Resources.template_945_List);

                // 945 List 엑셀를 만든다.
                string output945Path = ExcelUtils.CreateList945(template945Path, warehouseShippingOrders, config);
                WriteLog($"{output945Path} 가 만들어졌습니다");
                CommonUtil.DeleteFile(template945Path);

            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
                MsgBox.Error(ex.Message);

            }
            finally
            {
                CommonUtil.DeleteFile(template945Path);
            }
        }

        #endregion

        #region 각 탭의 엑셀로딩버튼 클릭
        private void Run945ButtonClick(object sender, EventArgs e)
        {
            if (currentListView.Items.Count < 1)
            {
                MsgBox.Error("목록이 없습니다");
                return;
            }

            currentTextBoxLog.Text = "";

            btnRun810.Enabled = false;
            btnList810.Enabled = false;
            btnOpenDir810.Enabled = false;
            targetDir810.ReadOnly = true;

            warehouseShippingOrders.Clear();

            for (int i = 0; i < currentListView.Items.Count; i++)
            {
                string fileName = currentListView.Items[i].SubItems[1].Text;
                string fullPath = currentListView.Items[i].SubItems[2].Text;
                WriteLog($"{i + 1} : {fileName} 작업시작됩니다....");
                currentListView.Items[i].Selected = true;
                currentListView.Items[i].Focused = true;

                DataTable table = ExcelUtils.DataTableFromExcel(fullPath);
                var a1 = table.CellAsString("A1").ToUpper();
                if (a1.Contains("WAREHOUSE") == false)
                {
                    MsgBox.Error($"{fileName} 은 적합한 Warehouse Shipping Order 945 파일이 아닙니다");
                    return;
                }
                WriteLog($"{i + 1} :  엑셀파일 {fileName} 읽어서 DataTable로 만들었습니다...");

                CommonUtil.PrintDataTable(table);
                WarehouseShippingOrder945 order945 = ExcelUtils.GetWarehouseShippingOrder945(table);
                WriteLog($"{i + 1} : DataTable로부터 invoice810 생성됨...");
                WriteLog(order945.ToString());
                WriteLog($"{i + 1} : {fileName} 작업종료합니다....");
                warehouseShippingOrders.Add(order945);
            }
            this.currentListView.SelectedItems.Clear();
            WriteLog("");
            WriteLog("리스트의 모든 파일을 읽어들였습니다");
            WriteLog("");
        }
        /// <summary>
        /// 810 엑셀만들기 버튼 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Run810ButtonClick(object sender, EventArgs e)
        {
            if (currentListView.Items.Count < 1)
            {
                MsgBox.Error("목록이 없습니다");
                return;
            }

            currentTextBoxLog.Text = "";

            invoice810s.Clear();
            btnRun810.Enabled = false;
            btnList810.Enabled = false;
            btnOpenDir810.Enabled = false;
            targetDir810.ReadOnly = true;


            for (int i = 0; i < currentListView.Items.Count; i++)
            {
                string fileName = currentListView.Items[i].SubItems[1].Text;
                string fullPath = currentListView.Items[i].SubItems[2].Text;
                WriteLog($"{i + 1} : {fileName} 작업시작됩니다....");
                currentListView.Items[i].Selected = true;
                currentListView.Items[i].Focused = true;

                DataTable table = ExcelUtils.DataTableFromExcel(fullPath);
                if (table.CellAsString("A1").Contains("INVOICE") == false)
                {
                    MsgBox.Error($"{fileName} 은 적합한 invoice 810 파일이 아닙니다");
                    return;
                }
                WriteLog($"{i + 1} :  엑셀파일 {fileName} 읽어서 DataTable로 만들었습니다...");

                CommonUtil.PrintDataTable(table);
                Invoice810 invoice810 = ExcelUtils.GetInvoice810(table);
                WriteLog($"{i + 1} : DataTable로부터 invoice810 생성됨...");
                WriteLog(invoice810.ToString());
                WriteLog($"{i + 1} : {fileName} 작업종료합니다....");
                invoice810s.Add(invoice810);
            }
            this.currentListView.SelectedItems.Clear();
            WriteLog("");
            WriteLog("리스트의 모든 파일을 읽어들였습니다");
            WriteLog("");

        }
        /// <summary>
        /// 850 엑셀만들기 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Run850ButtonClick(object sender, EventArgs e)
        {
            if (currentListView.Items.Count < 1)
            {
                MsgBox.Error("목록이 없습니다");
                return;
            }

            currentTextBoxLog.Text = "";
            btnRun850.Enabled = false;
            btnList850.Enabled = false;
            btnOpenDir850.Enabled = false;
            targetDir850.ReadOnly = true;

            purchaseOrder850s.Clear();
            for (int i = 0; i < currentListView.Items.Count; i++)
            {
                string fileName = currentListView.Items[i].SubItems[1].Text;
                string fullPath = currentListView.Items[i].SubItems[2].Text;
                WriteLog($"{i + 1} / {currentListView.Items.Count} : {fileName} 작업시작됩니다....");
                currentListView.Items[i].Selected = true;
                currentListView.Items[i].Focused = true;
                DataTable table = ExcelUtils.DataTableFromExcel(fullPath);
                //적합한 파일인지 체크
                if (table.CellAsString("A1").Contains("PURCHASE") == false)
                {
                    MsgBox.Error($"{fileName} 은 적합한 purchase order 850 파일이 아닙니다");
                    return;
                }
                WriteLog($"{i + 1} / {currentListView.Items.Count} :  엑셀파일 {fileName} 읽어서 DataTable로 만들었습니다...");

                CommonUtil.PrintDataTable(table);
                PurchaseOrder850 po850 = ExcelUtils.GetPurchaceOrder850(table, config);
                po850.ExcelFileName = fileName; //엑셀파일명

                WriteLog($"{i + 1} / {currentListView.Items.Count} : DataTable로부터 invoice810 생성됨...");
                WriteLog(po850.ToString());

                purchaseOrder850s.Add(po850);
                WriteLog($"{i + 1} / {currentListView.Items.Count} : {fileName} 작업종료됩니다....");
                WriteLog("");
            }

            //Sorting
            purchaseOrder850s.Sort(new Po850Compare());

            this.currentListView.SelectedItems.Clear();
            WriteLog("");
            WriteLog("리스트의 모든 엑셀파일을 로드했습니다");
            WriteLog("");
        }
        /// <summary>
        /// 210 엑셀만들기 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Run210ButtonClick(object sender, EventArgs e)
        {
            if (currentListView.Items.Count < 1)
            {
                MsgBox.Error("목록이 없습니다");
                return;
            }

            currentTextBoxLog.Text = "";
            btnRun210.Enabled = false;
            btnList210.Enabled = false;
            btnOpenDir210.Enabled = false;
            targetDir210.ReadOnly = true;

            freightInvoice210s.Clear();
            WriteLog("Freight Invoice Excel");
            for (int i = 0; i < currentListView.Items.Count; i++)
            {
                string fileName = currentListView.Items[i].SubItems[1].Text;
                string fullPath = currentListView.Items[i].SubItems[2].Text;
                WriteLog($"{i + 1} / {currentListView.Items.Count} : {fileName} 작업시작됩니다....");
                currentListView.Items[i].Selected = true;
                currentListView.Items[i].Focused = true;
                DataTable table = ExcelUtils.DataTableFromExcel(fullPath);
                //적합한 파일인지 체크
                if (table.CellAsString("A1").Contains("FREIGHT INVOICE") == false)
                {
                    MsgBox.Error($"{fileName} 은 적합한 freight invoice 210 파일이 아닙니다");
                    return;
                }
                WriteLog($"{i + 1} / {currentListView.Items.Count} :  엑셀파일 {fileName} 읽어서 DataTable로 만들었습니다...");

                CommonUtil.PrintDataTable(table);
                FreightInvoice210 invoice210 = ExcelUtils.GetFreightInvoice210(table, config);
                invoice210.ExcelFileName = fileName; //엑셀파일명

                WriteLog($"{i + 1} / {currentListView.Items.Count} : DataTable로부터 invoice810 생성됨...");
                WriteLog(invoice210.ToString());

                freightInvoice210s.Add(invoice210);
                WriteLog($"{i + 1} / {currentListView.Items.Count} : {fileName} 작업종료됩니다....");
                WriteLog("");
            }


            this.currentListView.SelectedItems.Clear();
            WriteLog("");
            WriteLog("리스트의 모든 엑셀파일을 로드했습니다");
            WriteLog("");
        }
        #endregion
    }
}
