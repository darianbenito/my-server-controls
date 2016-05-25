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
    /// Control MyFileUpload.
    /// </summary>
    [ToolboxData("<{0}:MyFileUpload runat=server></{0}:MyFileUpload>")]
    public class MyFileUpload : FileUpload
    {
        #region Atributos    
        private string _AllowedExtensions = string.Empty;
        public string AllowedExtensions
        {
            get { return _AllowedExtensions; }
            set { _AllowedExtensions = value; }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _RenamedFileName;
        public string RenamedFileName
        {
            get { return _RenamedFileName; }
            set { _RenamedFileName = value; }
        }
        #endregion

        #region Atributos invariables
        private enum UploadState
        {
            UPLOADEDWITHOTHERNAME,
            UPLOADED,
            NOTUPLOADED,
            NOTALLOWEXTENSION,
        };
        #endregion
       
        #region Métodos públicos
        /// <summary>
        /// Sube un archivo a una dirección específica. 
        /// </summary>
        /// <param name="serverLocation">Directorio del servidor en donde se subirá el archivo.</param>
        /// <param name="RenameFile">En caso de desear renombrar el archivo a subir, se debe indicar el nuevo nombre en éste parámetro, caso contrario se deberá pasar un string vacio(string.Empty).</param>
        /// <param name="strict">Éste parámetro en true indica que, en caso de existir el archivo en el servidor éste será reemplazado. Caso false, se subirá con un nombre diferente.</param>
        /// <returns>Se retorna el estado del proceso de upload. 0 Archivo no subido. 1 Archivo subido. 2 Archivo subido con un nombre diferente. 3 Archivo no subido por no tener una extensión permitida.</returns>
        public int UploadFilesToServer(string serverLocation, string RenameFile, bool strict)
        {
            int resultState = 0;

            UploadState uploadState = SaveFile(serverLocation, RenameFile, strict);
            switch (uploadState)
            {
                case UploadState.NOTUPLOADED:
                    resultState = 0;
                    break;
                case UploadState.UPLOADED:
                    resultState = 1;
                    break;
                case UploadState.UPLOADEDWITHOTHERNAME:
                    resultState = 2;
                    break;
                case UploadState.NOTALLOWEXTENSION:
                    resultState = 3;
                    break;
            }

            return resultState;
        }
        #endregion

        #region Métodos privados
        private UploadState SaveFile(string serverLocation, string RenameFile, bool strict)
        {
            try
            {
                if (this.HasFile)
                {                  
                    if (AllowExtension())
                    {
                        UploadState uploadState = UploadState.NOTUPLOADED;

                        // Obtenemos el nombre del archivo a subir, renombramos en caso de ser solicitado.
                        string fileName = RenameFile.Equals(string.Empty) ? this.FileName : RenameFile;

                        if (!strict)
                        {
                            // Creamos ruta con nombre del archivo a subir para controlar duplicados.
                            string pathToCheck = serverLocation + fileName;

                            string fileNameBefore = fileName;
                            fileName = GetNewFileName(serverLocation, fileName, pathToCheck);

                            // El archivo, en caso de no haber excepción, se subirá con un nombre diferente o con 
                            // el nombre original, dependiendo de su existencia o no en el servidor.
                            if (!fileNameBefore.Equals(fileName))
                            {
                                uploadState = UploadState.UPLOADEDWITHOTHERNAME;
                                this.RenamedFileName = fileName;
                            }
                            else
                            {
                                uploadState = UploadState.UPLOADED;
                            }                          
                        }
                        else
                        {
                            // El archivo, en caso de no haber excepción, se subirá con el nombre original.
                            uploadState = UploadState.UPLOADED;
                        }

                        // Agregamos a la ruta del servidor, el nombre del archivo.
                        serverLocation += fileName;

                        // Llamado al método SaveAs para subir el archivo 
                        // en el directorio especificado.
                        this.SaveAs(serverLocation);
                        return uploadState;
                    }
                    else
                    {
                        return UploadState.NOTALLOWEXTENSION;
                    }
               }
                else
                    {
                        return UploadState.NOTUPLOADED;
                    }
                }           
            catch
            {
                return UploadState.NOTUPLOADED;
            }
        }

        private bool AllowExtension()
        {
            string fileExtension = System.IO.Path.GetExtension(this.FileName).ToLower();

            string[] extensions = AllowedExtensions.Split(' ');
            foreach (string extension in extensions)
            {
                if (extension.Equals(fileExtension))
                {
                 return true;
                }
            }

            return false;
        }

        private string GetNewFileName(string serverLocation, string fileName, string pathToCheck)
        {           
            // Check to see if a file already exists with the
            // same name as the file to upload.        
            if (System.IO.File.Exists(pathToCheck))
            {
                // Create a temporary file name to use for checking duplicates.
                string newfileName = string.Empty;
                int counter = 2;
                while (System.IO.File.Exists(pathToCheck))
                {
                    // if a file with this name already exists,
                    // prefix the filename with a number.
                    newfileName = counter.ToString() + fileName;
                    pathToCheck = serverLocation + newfileName;
                    counter++;
                }

                return newfileName;
            }

            return fileName;
        }
        #endregion

        #region Métodos WebControl redefinidos
        protected override void Render(HtmlTextWriter output)
        {
            output.RenderBeginTag("span");
            output.Write(Description);
            output.RenderEndTag();  
            base.Render(output);
                      
       }
        #endregion
    }
}