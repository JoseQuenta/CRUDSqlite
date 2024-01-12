using SQLite;

//Aqui se crea la tabla Alumno con sus respectivos campos y se le asigna la llave primaria y el autoincremento a la llave primaria para que se vaya incrementando cada vez que se inserte un nuevo registro
namespace CRUDSqlite.Models
{
    public class Alumno
    {
        [PrimaryKey, AutoIncrement]
        public int IdAlumno { get; set; }
        [MaxLength (50)]
        public string Nombre { get; set; }
        [MaxLength(50)]
        public string ApellidoPaterno { get; set; }
        [MaxLength(50)]
        public string ApellidoMaterno { get; set; }
        public int Edad { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
    }
}
