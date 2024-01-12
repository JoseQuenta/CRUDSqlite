using CRUDSqlite.Data;
using System;
using System.IO;
using Xamarin.Forms;

namespace CRUDSqlite
{
    //Esta clase se crea con la finalidad de poder acceder a los metodos de la clase SQLiteHelper desde cualquier parte de la aplicacion, para ello se crea la propiedad statica Db de tipo SQLiteHelper y se le asigna el valor de una nueva instancia de la clase SQLiteHelper, de esta manera se puede acceder a los metodos de la clase SQLiteHelper desde cualquier parte de la aplicacion sin necesidad de crear una nueva instancia de la clase SQLiteHelper         
    public partial class App : Application
    {
        //se crea el objeto db de tipo SQLiteHelper para poder acceder a los metodos de la clase SQLiteHelper
        static SQLiteHelper db;

        //se crea la propiedad statica Db de tipo SQLiteHelper para poder acceder a los metodos de la clase SQLiteHelper desde cualquier parte de la aplicacion
        public static SQLiteHelper SQLiteDB
        {
            get
            {
                //si el objeto db es igual a null entonces se crea una nueva instancia de la clase SQLiteHelper y se le pasa como parametro el path de la base de datos
                if (db == null)
                {
                    // el path de la base de datos se obtiene con el metodo Combine de la clase Path que se encuentra en el namespace System.IO, el metodo Combine recibe como parametro el path de la carpeta LocalApplicationData que se obtiene con el metodo GetFolderPath de la clase Environment que se encuentra en el namespace System y el nombre de la base de datos que es Escuela.db3
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Escuela.db3"));
                }
                //se retorna el objeto db
                return db;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
