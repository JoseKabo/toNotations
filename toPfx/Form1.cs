using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace toPfx
{
    public partial class body : Form
    {
        public body()
        {
            InitializeComponent();
        }

        

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            proccessingEntry(txt_notation.Text);
        }

        private Stack<String> stack_pfx_operands = new Stack<string>();
        private Stack<String> stack_pfx_operators = new Stack<string>();

        private Stack<String> stack_ptfx = new Stack<string>();

        private string[] operatorsAccepted  = {"+","-", "/","*"};

        void proccessingEntry(string cadena)
        {
            this.lbl_topfx.Text = this.toPrefix(cadena);
            this.lbl_toptfx.Text = this.toPostfix(this.lbl_topfx.Text);
        }

        string toPrefix(string cadena)
        {
            for (int i = 0; i < cadena.Length; i++)
            {
                if (cadena[i] == '(')
                {
                    stack_pfx_operators.Push(cadena[i].ToString());
                }
                else if (cadena[i] == ')')
                {
                    while (stack_pfx_operators.Count != 0 && stack_pfx_operators.Peek() != "(")
                    {
                        String op1 = stack_pfx_operands.Peek();
                        stack_pfx_operands.Pop();

                        String op2 = stack_pfx_operands.Peek();
                        stack_pfx_operands.Pop();

                        string op = stack_pfx_operators.Peek();
                        stack_pfx_operators.Pop();

                        String tmp = op + op2 + op1;
                        stack_pfx_operands.Push(tmp);
                    }
                    stack_pfx_operators.Pop();
                }
                else if (!this.operatorsAccepted.Contains(cadena[i].ToString()))
                {
                    stack_pfx_operands.Push(cadena[i] + "");
                }
                else
                {
                    while (stack_pfx_operators.Count != 0 && getOperatorPriority(cadena[i]) <= getOperatorPriority(stack_pfx_operators.Peek()[0]))
                    {

                        String op1 = stack_pfx_operands.Pop();
                        String op2 = stack_pfx_operands.Pop();

                        string op = stack_pfx_operators.Pop();

                        String tmp = op + op2 + op1;
                        stack_pfx_operands.Push(tmp);
                    }
                    stack_pfx_operators.Push(cadena[i].ToString());
                }
            }

            while (stack_pfx_operators.Count != 0)
            {
                String op1 = stack_pfx_operands.Peek();
                stack_pfx_operands.Pop();

                String op2 = stack_pfx_operands.Peek();
                stack_pfx_operands.Pop();

                string op = stack_pfx_operators.Peek();
                stack_pfx_operators.Pop();

                String tmp = op + op2 + op1;
                stack_pfx_operands.Push(tmp);
            }
            return this.rotateEntry(stack_pfx_operands.Peek());
        }

        string rotateEntry(string entry)
        {
            char[] cadenaComoCaracteres = entry.ToCharArray();
            Array.Reverse(cadenaComoCaracteres);
            return new string(cadenaComoCaracteres);
        }

        string toPostfix(string cadena)
        {
            return "Pendiente";
        }

        int getOperatorPriority(char C)
        {
            if (C == '-' || C == '+')
                return 1;
            else if (C == '*' || C == '/')
                return 2;
            else
                return 0;
        }
    }
}
