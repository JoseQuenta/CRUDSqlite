using Android.OS;
using CRUDSqlite.Droid;

//El assembly es el nombre del proyecto y el namespace de la clase que implementa la interfaz IFileSystemService, en este caso el proyecto se llama CRUDSqlite y el namespace de la clase que implementa la interfaz IFileSystemService es CRUDSqlite.Droid y se encuentra en el archivo FileSystemService.cs
[assembly: Xamarin.Forms.Dependency(typeof(FileSystemService))]
namespace CRUDSqlite.Droid
{
    //se implementa la interfaz IFileSystemService, esto se hace para poder acceder al metodo GetExternalStorageDirectory desde la clase MainPage.xaml.cs
    public class FileSystemService : IFileSystemService
    {
        public string GetExternalStorageDirectory()
        {
            //Aqui se obtiene la ruta de la carpeta de descargas, pero se puede cambiar por la ruta que se desee obtene de la memoria externa del dispositivo Android ... IMPORTANTE AGREGAR EL PERMISO DE LECTURA Y ESCRITURA EN EL MANIFEST DE ANDROID PARA QUE FUNCIONE
            return Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads).AbsolutePath;
            //return Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
        }
    }
}