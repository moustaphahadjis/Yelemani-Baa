using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yelemani
{
    public partial class Graph : Form
    {
        DataTable ds, Names;
        double[] Values;
        public Graph()
        {
            InitializeComponent();
            ds = new DataTable();
        }

        private void Graph_Load(object sender, EventArgs e)
        {
            refresh();
        }

        void refresh()
        {
            
            Database.historique tmp2 = new Database.historique();
            ds = tmp2.getChartValue();

            //fill up the combobox
            Database.stock stock = new Database.stock();
            Names = new DataTable();
            Names = stock.getNames();
            DataColumn dc = new DataColumn();
            dc.DataType = typeof(double);
            dc.ColumnName = "Values";
            Names.Columns.Add(dc);

            for(int i=0; i<Names.Rows.Count; i++)
            {
                Names.Rows[i][1] = 0;
            }
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //calculate duration to match combobox value
            if(comboBox2.SelectedIndex==0)
            {
                for(int i=0; i<ds.Rows.Count;i++)
                {
                    TimeSpan duration = (DateTime.Now.Subtract(Convert.ToDateTime(ds.Rows[i].ItemArray[2].ToString())));
                    if(duration.TotalDays<117)
                    {
                        for(int j=0; j<Names.Rows.Count;j++)
                        {
                            if(ds.Rows[i].ItemArray[0].ToString()==Names.Rows[j].ItemArray[0].ToString())
                            {
                                Names.Rows[j][1]= Convert.ToDouble(Names.Rows[j].ItemArray[1].ToString()) + Convert.ToDouble(ds.Rows[i].ItemArray[1].ToString());
                            }
                        }
                    }
                    
                }

                //show result
                for (int i = 0; i < Names.Rows.Count; i++)
                {
                    chart1.Series.Add(i.ToString());
                    chart1.Series[i].Points.AddXY(i, Names.Rows[i].ItemArray[1]);
                }
            }
        }
    }
}
