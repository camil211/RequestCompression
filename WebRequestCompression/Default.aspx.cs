using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRequestCompression
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {          
            CreateInputFields(500);
        }        

        /// <summary>
        /// Tworzenie elementó typu input będąctch reprezentantem dużego formularza
        /// </summary>
        /// <param name="number"></param>
        void CreateInputFields(int number)
        {
            for (int i = 0; i < number; i++)
            {              
                StringBuilder stringBuilder = new StringBuilder("<input type='hidden'");
                stringBuilder.Append("name='testInput_" + i +"' ");
                stringBuilder.Append("id='testInput_" + i + "' ");
                stringBuilder.Append("value='" + CreateString(1000) + "' />");

                LiteralControl literal = new LiteralControl(stringBuilder.ToString());
                form1.Controls.Add(literal);
            }
        }     
        
        /// <summary>
        /// Tworzenie tekstowego ciągu znaków o długości okreśonej w parametrze
        /// </summary>
        /// <param name="stringLength"></param>
        /// <returns></returns>
        string CreateString(int stringLength)
        {
            Random rd = new Random();
            const string allowedChars = "ABCDEFGHJKLMNOPabcdefghijkmnop0123456789";
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }         
       
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>executeAfter();</script>", false);
        }
    }
}