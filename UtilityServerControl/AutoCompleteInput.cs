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
    /// Control AutoCompleteInput.
    /// </summary>
    /// <remarks>Requiere librería jquery 1.7.1 o superior.</remarks>
    [ToolboxData("<{0}:AutoCompleteInput runat=server></{0}:AutoCompleteInput>")]
    public class AutoCompleteInput : AutoComplete
    {
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

            output.RenderBeginTag(HtmlTextWriterTag.Script);
            output.Write("var inputs =" + GetInputs() + ";$('#" + Id + "').autocomplete({ source:[inputs] });");
            output.RenderEndTag();
        }     
        #endregion    
    }
}
