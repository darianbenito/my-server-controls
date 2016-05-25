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
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:BetweenDateTimePicker runat=server></{0}:BetweenDateTimePicker>")]
    public class BetweenDateTimePicker : DateTimePicker
    {
        #region Atributos
        private string _IdStart;
        public string IdStart
        {
            get { return _IdStart; }
            set { _IdStart = value; }
        }
      
        private string _IdEnd;
        public string IdEnd
        {
            get { return _IdEnd; }
            set { _IdEnd = value; }
        }
        #endregion

        #region Métodos públicos
        public string GetStartName() 
        {
            return IdStart + "_name";
        }

        public string GetEndName()
        {
            return IdEnd + "_name";
        } 
        #endregion

        #region Métodos WebControl redefinidos
        protected override void RenderContents(HtmlTextWriter output)
        {
            output.RenderBeginTag("p");
            output.Write("Desde ");
            
                output.AddAttribute("id", IdStart);
                output.AddAttribute("name", IdStart + "_name");
                output.AddAttribute("type", "text");
                output.AddAttribute("class", base.Class);
                output.AddAttribute("style", base.Style);             
                output.RenderBeginTag("input");
                output.RenderEndTag();

                output.AddAttribute("id", IdStart + "_open");
                output.AddAttribute("type", "button");
                output.AddAttribute("class", "calendar-b64-image");
                output.AddAttribute("style", "margin-right: 10px;");               
                output.RenderBeginTag("input");
                output.RenderEndTag();
        
                output.Write("Hasta ");
            
                output.AddAttribute("id", IdEnd);
                output.AddAttribute("name", IdEnd + "_name");
                output.AddAttribute("type", "text");
                output.AddAttribute("class", base.Class);
                output.AddAttribute("style", base.Style);
                output.RenderBeginTag("input");
                output.RenderEndTag();

                output.AddAttribute("id", IdEnd + "_open");
                output.AddAttribute("type", "button");
                output.AddAttribute("class", "calendar-b64-image");
                output.AddAttribute("runat", "server");
                output.RenderBeginTag("input");
                output.RenderEndTag();

            output.RenderEndTag();

            output.RenderBeginTag(HtmlTextWriterTag.Script);
            output.Write("jQuery(function(){ jQuery('#" + IdStart + "').datetimepicker({ lang: 'es', format:'d/m/Y H:i', maxDate: '-1', onShow:function( ct ){ this.setOptions({ maxDate:jQuery('#" + IdEnd + "').val()?jQuery('#" + IdEnd + "').val():false }) }, timepicker:true }); jQuery('#" + IdEnd + "').datetimepicker({ lang: 'es', format:'d/m/Y H:i', maxDate: '-1', onShow:function( ct ){ this.setOptions({ minDate:jQuery('#" + IdStart + "').val()?jQuery('#" + IdStart + "').val():false }) }, timepicker:true }); });");
            output.Write("$('#" + IdStart + "_open').click(function(){ $('#" + IdStart + "').datetimepicker('show'); });");
            output.Write("$('#" + IdEnd + "_open').click(function(){ $('#" + IdEnd + "').datetimepicker('show'); });");
            output.RenderEndTag();      
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
