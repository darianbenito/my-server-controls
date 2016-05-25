using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UtilityServerControl
{
     [ToolboxData("<{0}:AutoCompleteButton runat=server></{0}:AutoCompleteButton>")]
     public class AutoCompleteButton : AutoComplete
    {
        #region Atributos        
         private string _ButtonStyle = string.Empty;
         public string ButtonStyle
        {
            get { return _ButtonStyle; }
            set { _ButtonStyle = value; }
        }

         private string _ButtonClass = string.Empty;
         public string ButtonClass
         {
             get { return _ButtonClass; }
             set { _ButtonClass = value; }
         }     
        #endregion

        #region Métodos WebControl redefinidos
        protected override void RenderContents(HtmlTextWriter output)
        {       
                output.AddAttribute("id", Id);
                output.AddAttribute("name", Id + "_name");
                output.AddAttribute("type", "text"); 
                output.AddAttribute("placeholder", PlaceHolder);
                output.AddAttribute("class", InputClass);
                output.AddAttribute("style", InputStyle);
                output.RenderBeginTag("input");               
                            output.AddAttribute("id", "open" + Id);
                            output.AddAttribute("name", "open" + Name);
                            output.AddAttribute("type", "button");
                            output.AddAttribute("class", "button-b64-image");
                            output.AddAttribute("style", ButtonStyle);
                            output.RenderBeginTag("button");
                            output.RenderEndTag();              
                output.RenderEndTag();
      
         output.RenderBeginTag(HtmlTextWriterTag.Script);
         output.Write("var inputs =" + GetInputs() + ";$('#" + Id + "').autocomplete({ source:[inputs]});");
         output.RenderEndTag();

         output.RenderBeginTag(HtmlTextWriterTag.Script);
         output.Write("$('button#open" + Id + "').click(function(){ $('#" + Id + "').trigger('updateContent open'); $('#" + Id + "').focus(); });");
         output.RenderEndTag();
         
         output.RenderBeginTag(HtmlTextWriterTag.Script);
         output.Write("$('button#open" + Id + "').click(function(){ $('#" + Id + "').trigger('updateContent open'); $('#" + Id + "').focus(); });");
         output.RenderEndTag();
        }
        #endregion
    }
}
