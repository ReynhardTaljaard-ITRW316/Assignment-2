﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{

    public partial class Form1 : Form
    {
        private List<Procedure> pList = new List<Procedure>();
        
        private int counter = 0;
    
        public Form1()
        {
            InitializeComponent();
        }

        private void Display()
        {
            int length = pList.Count;

            string total = "";

            for (int i = 0; i<length; i++)
            {
                total += " "+pList[i].ProcedureName; 
            }

            lstProc.Items.Add(total);
        }

        private  void Priority()
        {

            pList.Sort();
            string disp = "";
            for (int i =0;i<pList.Count;i++)
            {
                for(int j =pList[i].CycleLength;j>0;j--)
                {
                    disp += pList[i].ProcedureName + " ";
                }
            }
            lstProc.Items.Add(disp);
        }

        private void RoundRobin()
        {
            string disp = "";
            int par = Convert.ToInt32(txtParam.Text);
            
            while (pList.Count != 0)
            {
                Procedure temp = new Procedure();
                temp.ProcedureName = pList[0].ProcedureName;
                temp.CycleLength = pList[0].CycleLength;

                pList.Remove(pList[0]);

                for (int i = 1; i <= par; i++)
                {
                    if ((temp.CycleLength != 0)&&(temp.CycleLength >0))
                    {
                        disp += temp.ProcedureName + "";
                        temp.CycleLength--;


                    }
                    /* else
                     {
                         temp.ProcedureName = pList[0].ProcedureName;
                         temp.CycleLength = pList[0].CycleLength;
                         temp.CycleLength--;
                     }*/
                }

                if (temp.CycleLength != 0)
                {
                    pList.Add(temp);
                }
            }

            lstProc.Items.Add(disp);
        }

        private void Queueing()
        {

            string disp = "";
            Queue<Procedure> q1 = new Queue<Procedure>();
            Queue<Procedure> q2 = new Queue<Procedure>();
            Queue<Procedure> q3 = new Queue<Procedure>();
            Queue<Procedure> q4 = new Queue<Procedure>();

            for (int i = 0; i< pList.Count;i++)
            {
                q1.Enqueue(pList[i]);
                
            }

            while (q1.Count !=0)
            {
                Procedure temp = q1.Dequeue();
                disp += temp.ProcedureName + " ";
                temp.CycleLength--;
                if (temp.CycleLength != 0)
                    q2.Enqueue(temp);
            }

            while (q2.Count != 0)
            {
                Procedure temp2 = q2.Dequeue();
                for (int i = 1; i <= 2; i++)
                {
                    if ((temp2.CycleLength != 0) && (temp2.CycleLength > 0))
                    {
                        disp += temp2.ProcedureName + " ";
                        temp2.CycleLength--;
                    }
                }
                if (temp2.CycleLength != 0)
                    q3.Enqueue(temp2);
            }

            while (q3.Count != 0)
            {
                Procedure temp3 = q3.Dequeue();
             
                for (int i = 1; i <= 4; i++)
                {
                    if ((temp3.CycleLength != 0) && (temp3.CycleLength > 0))
                    {
                        disp += temp3.ProcedureName + " ";
                        temp3.CycleLength--;
                    }
                }
                if (temp3.CycleLength != 0)
                   q3.Enqueue(temp3);

            }


            lstProc.Items.Add(disp);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            counter++;
            Procedure obj = new Procedure();
            obj.ProcedureName = "P" + Convert.ToString(counter);
            obj.CycleLength = Convert.ToInt32( txtLength.Text);
            pList.Add(obj);

            lstProc.Items.Add(obj.ToString());
            txtLength.Clear();
            txtLength.Focus();
           // Display();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Priority();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RoundRobin();
            counter = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Queueing();
        }
    }
}
