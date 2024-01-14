using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDSqlite
{
    public interface IFileSystemService
    {
        //se crea el metodo GetExternalStorageDirectory que retorna un string, este metodo se implementara en las plataformas Android y iOS para obtener la ruta de la carpeta de descargas de la memoria externa del dispositivo 
        string GetExternalStorageDirectory();
    }
}
