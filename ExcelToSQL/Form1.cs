using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace ExcelToSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            //选择文件
            openFileDialog1.Filter = "XLS文件|*.xls|XLSX文件|*.xlsx";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                InputWorkbook(openFileDialog1.FileName);//执行导入
            }
            openFileDialog1.Dispose();
        }
        
        #region 导入工作簿
        private void InputWorkbook(string filePath)
        {
            if (filePath != "")
            {
                try
                {
                    string fileType = filePath.Substring(filePath.LastIndexOf(".") + 1);//获取文件后缀
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    //判断文件类型
                    bool isXls = true;
                    if (fileType == "xlsx")
                    {
                        isXls = false;
                    }
                    IWorkbook workbook = CreateWorkbook(isXls, fs);
                    ISheet sheet = workbook.GetSheetAt(0);
                    int rowCount = sheet.LastRowNum + 1;
                    int colCount = sheet.GetRow(0).LastCellNum;
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    for (int c = 0; c < colCount; c++)
                    {
                        ICell cell = sheet.GetRow(0).GetCell(c);
                        dataGridView1.Columns.Add(c.ToString() + cell.ToString(), cell.ToString());
                    }

                    for (int r = 1; r < rowCount; r++)
                    {
                        IRow row = sheet.GetRow(r);
                        int index = dataGridView1.Rows.Add();
                        colCount = row.LastCellNum;
                        for (int c = 0; c < colCount; c++)
                        {
                            ICell cell = row.GetCell(c);
                            if (cell == null)
                            {
                                continue;
                            }
                            dataGridView1.Rows[index].Cells[c].Value = cell.ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("导入失败" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("请选择Excel文件");
            }
        }
        #endregion
        private static IWorkbook CreateWorkbook(bool isXls, FileStream fs)
        {
            if (isXls)
            {
                return new HSSFWorkbook(fs);
            }
            else
            {
                return new XSSFWorkbook(fs);
            }
        }

        private void btnToSQL_Click(object sender, EventArgs e)
        {
            //链接字符串
            string constring = @"Data Source=.;Initial Catalog=test;User ID=sa;Password=Sql123";
            //链接数据库
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    SqlCommand comm = new SqlCommand();
                    string N1 = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                    string N2 = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                    string N3 = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value);
                    string N4 = Convert.ToString(dataGridView1.Rows[i].Cells[3].Value);
                    string N5 = Convert.ToString(dataGridView1.Rows[i].Cells[4].Value);
                    string N6 = Convert.ToString(dataGridView1.Rows[i].Cells[5].Value);
                    string N7 = Convert.ToString(dataGridView1.Rows[i].Cells[6].Value);
                    string N8 = Convert.ToString(dataGridView1.Rows[i].Cells[7].Value);
                    string N9 = Convert.ToString(dataGridView1.Rows[i].Cells[8].Value);
                    string N10 = Convert.ToString(dataGridView1.Rows[i].Cells[9].Value);
                    string N11 = Convert.ToString(dataGridView1.Rows[i].Cells[10].Value);
                    string N12 = Convert.ToString(dataGridView1.Rows[i].Cells[11].Value);
                    string N13 = Convert.ToString(dataGridView1.Rows[i].Cells[12].Value);
                    string N14 = Convert.ToString(dataGridView1.Rows[i].Cells[13].Value);
                    string N15 = Convert.ToString(dataGridView1.Rows[i].Cells[14].Value);
                    string N16 = Convert.ToString(dataGridView1.Rows[i].Cells[15].Value);
                    string N17 = Convert.ToString(dataGridView1.Rows[i].Cells[16].Value);
                    string N18 = Convert.ToString(dataGridView1.Rows[i].Cells[17].Value);
                    string N19 = Convert.ToString(dataGridView1.Rows[i].Cells[18].Value);

                    string SqlStr = "INSERT tb_test(N1,N2,N3,N4,N5,N6,N7,N8,N9,N10,N11,N12,N13,N14,N15,N16,N17,N18,N19) VALUES ('" + N1 + "','" + N2 + "','" + N3 + "','" + N4 + "','" + N5 + "','" + N6 + "','" + N7 + "','" + N8 + "','" + N9 + "','" + N10 + "','" + N11 + "','" + N12 + "','" + N13 + "','" + N14 + "','" + N15 + "','" + N16 + "','" + N17 + "','" + N18 + "','" + N19 + "')";
                    comm.CommandText = SqlStr;
                    comm.Connection = con;
                    comm.ExecuteNonQuery();
                }
                
                MessageBox.Show("数据插入成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新失败,失败原因:" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }   
    }
}
