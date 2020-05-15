using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;
using System.IO;

namespace Yelemani.Database
{
    class printOut
    {
        string path;
        public printOut()
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\Yelemani\";
        }
        public void print(DataGridView data, string Client, string Total, string numero, string lieu)
        {
            
                Word.Application app = new Word.Application();
                app.Visible = false;

                Word.Document doc = app.Documents.Open(path+@"template.docx");

                //Proforma NUmber
                Database.Bill bill = new Bill();
                int BillNum = bill.getNum();

                app.Selection.Find.Execute(FindText: "billnum", ReplaceWith: BillNum.ToString()+"/"+DateTime.Now.Year.ToString(), Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "Nom", ReplaceWith: Client, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "Date",ReplaceWith: "le " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() , Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "NUMERO", ReplaceWith:numero, Replace: Word.WdReplace.wdReplaceAll);
                app.Selection.Find.Execute(FindText: "VILLE", ReplaceWith:lieu, Replace: Word.WdReplace.wdReplaceAll);


                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        doc.Tables[1].Rows[2].Cells[1].Range.Text = "0" + (i + 1).ToString();
                        doc.Tables[1].Rows[2].Cells[2].Range.Text = data.Rows[i].Cells[0].Value.ToString();
                        doc.Tables[1].Rows[2].Cells[3].Range.Text = data.Rows[i].Cells[2].Value.ToString();
                        doc.Tables[1].Rows[2].Cells[4].Range.Text = data.Rows[i].Cells[1].Value.ToString();
                        doc.Tables[1].Rows[2].Cells[5].Range.Text = (Convert.ToDouble(data.Rows[i].Cells[2].Value.ToString()) * Convert.ToDouble(data.Rows[i].Cells[1].Value.ToString())).ToString();
                    }
                    else
                    {
                        doc.Tables[1].Rows[2].Cells[1].Range.Text = "0" + (i + 1).ToString();
                        doc.Tables[1].Rows.Add(doc.Tables[1].Rows[2]);
                        doc.Tables[1].Rows[2].Cells[2].Range.Text = data.Rows[i].Cells[0].Value.ToString();
                        doc.Tables[1].Rows[2].Cells[3].Range.Text = data.Rows[i].Cells[2].Value.ToString();
                        doc.Tables[1].Rows[2].Cells[4].Range.Text = data.Rows[i].Cells[1].Value.ToString();
                        doc.Tables[1].Rows[2].Cells[5].Range.Text = (Convert.ToDouble(data.Rows[i].Cells[2].Value.ToString()) * Convert.ToDouble(data.Rows[i].Cells[1].Value.ToString())).ToString();
                    }
                }

                doc.Tables[1].Rows.Last.Cells[2].Range.Text = Total;

                doc.SaveAs2(path+BillNum.ToString()+".docx");

            doc.Close();
            //app.Quit();
            doc= app.Documents.Open(path + BillNum.ToString() + ".docx");
            app.Visible = true;
        }
    }
}
