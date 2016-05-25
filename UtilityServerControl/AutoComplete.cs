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
    public abstract class AutoComplete : WebControl
    {
        #region Atributos
        private string _Id;
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Inputs = string.Empty;
        public string Inputs
        {
            get { return _Inputs; }
            set {
                
                 _Inputs = value;

                 lItems = new List<ListItem>();

                 string[] arrInputs = Inputs.Split(InputsDelimiterOne);

                 for (int i = 0; i < arrInputs.Length; i++)
                 {
                     string[] subItemsArr = arrInputs[i].Split(InputsDelimiterTwo);
                     ListItem newListItem = new ListItem();
                     newListItem.Text = subItemsArr[0];
                     newListItem.Value = subItemsArr[1];
                     lItems.Add(newListItem);                    
                 }
             }
        }

        private char _InputsDelimiterOne = ';';
        public char InputsDelimiterOne
        {
            get { return _InputsDelimiterOne; }
            set { _InputsDelimiterOne = value; }
        }

        private char _InputsDelimiterTwo = '|';
        public char InputsDelimiterTwo
        {
            get { return _InputsDelimiterTwo; }
            set { _InputsDelimiterTwo = value; }
        }

        private string _PlaceHolder = "Ingrese...";
        public string PlaceHolder
        {
            get { return _PlaceHolder; }
            set { _PlaceHolder = value; }
        }

        private string _InputStyle = "width: 192px; height: 24px; padding-left: 6px; font-size: 12px;";
        public string InputStyle
        {
            get { return _InputStyle; }
            set { _InputStyle = value; }
        }

        private string _InputClass;
        public string InputClass
        {
            get { return _InputClass; }
            set { _InputClass = value; }
        }
        #endregion

        #region Atributos invariables       
        private static readonly string CSSPath = "UtilityServerControl.css.jquery.autocomplete.min.css";
        private static readonly string JSPath = "UtilityServerControl.js.jquery.autocomplete.min.js";

        private static readonly string ScriptLink = "<script src='{0}' type='text/javascript' ></script>";
        private static readonly string StyleLink = "<link rel='stylesheet' text='text/css' href='{0}' ></link>";

        private List<ListItem> lItems;
        #endregion

        #region Métodos públicos       
        /// <summary>
        /// Genera referencia al css necesario.
        /// </summary>
        /// <param name="page">Page utilizando DateTimePicker.</param>
        /// <returns>Retorna etiqueta link con la referencia al css necesario.</returns>
        public static string GetCSSReferences(Page page)
        {
            string location = page.ClientScript.GetWebResourceUrl(typeof(UtilityServerControl.AutoComplete), CSSPath);

            return String.Format(StyleLink, location);
        }

        /// <summary>
        /// Genera referencia al js necesario.
        /// </summary>
        /// <param name="page">Page utilizando DateTimePicker.</param>
        /// <returns>Retorna etiqueta script con la referencia al js necesario.</returns>
        public static string GetJSReferences(Page page)
        {
            string location = page.ClientScript.GetWebResourceUrl(typeof(UtilityServerControl.AutoComplete), JSPath);

            return String.Format(ScriptLink, location);
        }

        public string GetName()
        {
            return Id + "_name";
        }

        /// <summary>
        /// Obtiene el valor asociado a la key solicitada.
        /// </summary>
        /// <param name="key">Campo text del cual se requiere el value.</param>
        /// <returns>Retorna el valor asociado a la key solicitada o string.Empty en caso de no encontrar la key.</returns>
        public string GetValue(string key) 
        {
            foreach (ListItem item in lItems)
            {
                if (item.Text.Equals(key))
                {
                    return item.Value;
                }
            }

            return string.Empty;
        }
        #endregion

        #region Métodos WebControl redefinidos
        /// <summary>
        /// Para eliminar etiqueta span(etiqueta agregada por defecto)
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            this.RenderContents(writer);
        }
        #endregion

        #region Métodos Protegidos
        protected string GetInputs()
        {
            lItems = new List<ListItem>();

            string output = "[";

            string[] arrInputs = Inputs.Split(InputsDelimiterOne);

            for (int i = 0; i < arrInputs.Length; i++)
            {
                string[] subItemsArr = arrInputs[i].Split(InputsDelimiterTwo);
                ListItem newListItem = new ListItem();
                newListItem.Text = subItemsArr[0];
                newListItem.Value = subItemsArr[1];
                lItems.Add(newListItem);

                output = (i != arrInputs.Length - 1) ? output + "'" + subItemsArr[0] + "'," : output + "'" + subItemsArr[0] + "'";
            }

            output = output + "]";

            return output;
        }
        #endregion
    }
}