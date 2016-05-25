using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UtilityServerControl
{
    /// <summary>
    /// Control UpDown.
    /// </summary>
    /// <remarks>Requiere librería jquery 1.7.1 o superior.</remarks>
    [ToolboxData("<{0}:UpDown runat=server></{0}:UpDown>")]
    public class UpDown : WebControl
    {
        #region Atributos
        protected string _Id;
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        protected string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        protected string _Class = string.Empty;
        public string Class
        {
            get { return _Class; }
            set { _Class = value; }
        }

        protected string _Style = string.Empty;
        public string Style
        {
            get { return _Style; }
            set { _Style = value; }
        }

        protected int _IncrementSteps = 1;
        public int IncrementSteps
        {
            get { return _IncrementSteps; }
            set { _IncrementSteps = value; }
        }

        protected string _UpImgUrl = string.Empty;
        public string UpImgUrl
        {
            get { return _UpImgUrl; }
            set { _UpImgUrl = value; }
        }

        protected string _DownImgUrl = string.Empty;
        public string DownImgUrl
        {
            get { return _DownImgUrl; }
            set { _DownImgUrl = value; }
        }
        #endregion

        #region Métodos WebControl redefinidos
        protected override void OnPreRender(EventArgs e)
        {
            // Define the name and type of the client scripts on the page.        
            String csname = "UpDownClientScript";
            Type cstype = this.GetType();

            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;          

            // Check to see if the client script is already registered.
            if (!cs.IsClientScriptBlockRegistered(cstype, csname))
            {
                StringBuilder cstext = new StringBuilder();
                cstext.Append("<script type=\"text/javascript\">");
                cstext.Append("function decrement(n,a){\"0\"!=$(\"#\"+n).val()&&$(\"#\"+n).val(+$(\"#\"+n).val()+a)}function increment(n,a){$(\"#\"+n).val(+$(\"#\"+n).val()+a)}</");
                cstext.Append("script>");
                cs.RegisterClientScriptBlock(cstype, csname, cstext.ToString(), false);
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.AddAttribute("id", Id);
            output.AddAttribute("name", Name);
            output.AddAttribute("type", "text");
            output.AddAttribute("class", Class);
            output.AddAttribute("style", Style);
            output.RenderBeginTag("div");
                output.AddAttribute("id", Id + "DownImg");
                output.AddAttribute("src", DownImgUrl);
                output.AddAttribute("onclick", "javascript: decrement('" + Id + "spanNumber" + "', -" + IncrementSteps + ")");
                output.RenderBeginTag("img");
                    output.AddAttribute("id", Id + "spanNumber");
                    output.AddAttribute("type", "number"); 
                    output.AddAttribute("value", "0"); 
                    output.AddAttribute("style", "width: 60px;");
                    output.AddAttribute("readonly", string.Empty);
                    output.RenderBeginTag("input");                 
                    output.RenderEndTag();
                output.AddAttribute("id", Id + "UpImg");
                output.AddAttribute("src", UpImgUrl);
                output.AddAttribute("onclick", "javascript: increment('" + Id + "spanNumber" + "', " + IncrementSteps + ")");
                output.RenderBeginTag("img");             
            output.RenderEndTag();

            //output.RenderBeginTag(HtmlTextWriterTag.Script);
            /*function decrement(spanNumber, val) {
                                     if ($("#" + spanNumber).val() != "0") {
                                         $("#" + spanNumber).val(+$("#" + spanNumber).val() + val);                                   
                                     }
                                 }
              function increment(spanNumber, val) {
                                     $("#" + spanNumber).val(+$("#" + spanNumber).val() + val);                                      
            }*/
            //output.Write("function decrement(n,a){\"0\"!=$(\"#\"+n).val()&&$(\"#\"+n).val(+$(\"#\"+n).val()+a)}function increment(n,a){$(\"#\"+n).val(+$(\"#\"+n).val()+a)}");          
            //output.RenderEndTag();            
        }

        /// <summary>
        /// Para eliminar etiqueta span(etiqueta agregada por defecto)
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            this.RenderContents(writer);
        }
        #endregion
    }
}
