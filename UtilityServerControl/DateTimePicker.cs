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
    /// <summary>
    /// Control DateTimePicker.
    /// </summary>
    /// <remarks>Requiere librería jquery 1.7.1 o superior.</remarks>
    [ToolboxData("<{0}:DateTimePicker runat=server></{0}:DateTimePicker>")]
    public class DateTimePicker : WebControl
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

        protected string _Style = "width: 105px; height: 24px; margin-right: 2px; font-size: 12px;";
        public string Style
        {
            get { return _Style; }
            set { _Style = value; }
        }
        #endregion

        #region Atributos invariables
        private static readonly string CSSPath = "UtilityServerControl.css.jquery.datetimepicker.min.css";
        private static readonly string JSPath = "UtilityServerControl.js.jquery.datetimepicker.min.js";

        private static readonly string ScriptLink = "<script src='{0}' type='text/javascript'></script>";
        private static readonly string StyleLink = "<link rel='stylesheet' text='text/css' href='{0}'></link>";
        private static readonly char DateDelimiter = '/';
        private static readonly char TimeDelimiter = ':';
        #endregion

        #region Métodos públicos
        /// <summary>
        /// Convierte un input datetimepicker en un objeto DateTime.
        /// </summary>
        /// <param name="dateTimePicker">input Text generado por la selección de fecha en el DateTimePicker</param>
        /// <returns>Retorna un objeto DateTime en base al JQ dateTimePicker(DDMMYYYY hh:mm:ss)</returns>
        public static DateTime GetDateTimeFromDateTimePicker(string dateTimePicker)
        {
            try
            {
                string dateOnly = dateTimePicker.Substring(0, 10);
                string timeOnly = dateTimePicker.Substring(11, 5);

                string[] ddMMYYYY = dateOnly.Split(DateDelimiter);
                string[] hhMM = timeOnly.Split(TimeDelimiter);

                if ((ddMMYYYY.Count() == 3) && (hhMM.Count() == 2))
                {
                    return new DateTime(Convert.ToInt32(ddMMYYYY[2]), Convert.ToInt32(ddMMYYYY[1]), Convert.ToInt32(ddMMYYYY[0]), Convert.ToInt32(hhMM[0]), Convert.ToInt32(hhMM[1]), 00);
                }
            }
            catch
                { 
                 return new DateTime(); 
                }

           return new DateTime();
        }

        /// <summary>
        /// Genera string correspondiente al tipo de dato datetime de SqlServer.
        /// </summary>
        /// <param name="dateTimePicker">input Text generado por la selección de fecha en el DateTimePicker</param>
        /// <returns>Retorna string formateado al tipo de dato datetime de SqlServer.</returns>
        public static string GetSqlServerDateTimeFormat(string dateTimePicker)
        {
            return GetDateTimeFromDateTimePicker(dateTimePicker).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Genera referencia al css necesario.
        /// </summary>
        /// <param name="page">Page utilizando DateTimePicker.</param>
        /// <returns>Retorna etiqueta link con la referencia al css necesario.</returns>
        public static string GetCSSReferences(Page page)
        {
            string location = page.ClientScript.GetWebResourceUrl(typeof(UtilityServerControl.DateTimePicker), CSSPath);

            return String.Format(StyleLink, location);
        }

        /// <summary>
        /// Genera referencia al js necesario.
        /// </summary>
        /// <param name="page">Page utilizando DateTimePicker.</param>
        /// <returns>Retorna etiqueta script con la referencia al js necesario.</returns>
        public static string GetJSReferences(Page page)
        {           
            string location = page.ClientScript.GetWebResourceUrl(typeof(UtilityServerControl.DateTimePicker), JSPath);

            return String.Format(ScriptLink, location);
        }
        #endregion

        #region Métodos WebControl redefinidos
        protected override void RenderContents(HtmlTextWriter output)
        {          
            output.AddAttribute("id", Id);
            output.AddAttribute("name", Name);
            output.AddAttribute("type", "text");
            output.AddAttribute("class", Class);
            output.AddAttribute("style", Style);
            output.RenderBeginTag("input");
            output.RenderEndTag();

            output.AddAttribute("id", Id + "_open");
            output.AddAttribute("type", "button");
            output.AddAttribute("class", "calendar-b64-image");
            output.RenderBeginTag("input");
            output.RenderEndTag();
      
            output.RenderBeginTag(HtmlTextWriterTag.Script);
            output.Write("$('#" + Id + "').datetimepicker({ lang: 'es', timepicker: true, format: 'd/m/Y H:i', maxDate: '-1' });");
            output.Write("$('#" + Id + "_open').click(function(){ $('#" + Id + "').datetimepicker('show'); });");
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