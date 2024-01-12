using CRUDSqlite.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDSqlite.Data
{
    public class SQLiteHelper
    {
        //Se crea la conexion a la base de datos, se crea un objeto de tipo SQLiteAsyncConnection y se le pasa como parametro el path de la base de datos
        SQLiteAsyncConnection db;
        //se crea el constructor de la clase y se le pasa como parametro el path de la base de datos
        public SQLiteHelper(string dbPath)
        {
            //el db es igual a una nueva conexion a la base de datos y se le pasa como parametro el path de la base de datos, el dbPath es el path que se le pasa como parametro al constructor de la clase SQLiteHelper en la clase MainPage.xaml.cs
            db = new SQLiteAsyncConnection(dbPath);
            //se crea la tabla Alumno en la base de datos y se espera a que se cree la tabla de manera asincrona para que no se bloquee la aplicacion mientras se crea la tabla en la base de datos
            db.CreateTableAsync<Alumno>().Wait();
        }


        //se crea el metodo GetAlumnosAsync que retorna una lista de tipo Alumno, este metodo se encarga de obtener todos los registros de la tabla Alumno de la base de datos
        public Task <int> SaveAlumnoAsync(Alumno alum)
        {
            if (alum.IdAlumno == 0) 
            {
                return db.InsertAsync(alum);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Recuperar todos los alumnos de la base de datos de manera asincrona para que no se bloquee la aplicacion mientras se recuperan los registros de la base de datos
        /// </summary>
        /// <returns></returns>
        public Task<List<Alumno>> GetAlumnosAsync()
        {
            return db.Table<Alumno>().ToListAsync();

        }

        /// <summary>
        /// Recuperar un alumno por su id de manera asincrona para que no se bloquee la aplicacion mientras se recuperan los registros de la base de datos
        /// </summary>
        /// <param name="idAlumno"> Id del alumno que se requiere</param>
        /// <returns></returns>
        public Task<Alumno> GetAlumnoByIdAsync(int idAlumno)
        {
            return db.Table<Alumno>().Where(a => a.IdAlumno == idAlumno).FirstOrDefaultAsync();

        }
    }
}