using ClosedXML.Excel;
using CRUDSqlite.Models;
using System;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRUDSqlite
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LlenarDatos();
        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos() == true)
            {
                Alumno alum = new Alumno
                {
                    Nombre = txtNombre.Text,
                    ApellidoPaterno = txtApellidoPaterno.Text,
                    ApellidoMaterno = txtApellidoMaterno.Text,
                    Edad = Convert.ToInt32(txtEdad.Text),
                    Email = txtEmail.Text
                };
                await App.SQLiteDB.SaveAlumnoAsync(alum);
                txtNombre.Text = "";
                txtApellidoPaterno.Text = "";
                txtApellidoMaterno.Text = "";
                txtEdad.Text = "";
                txtEmail.Text = "";
                await DisplayAlert("Exito", "Alumno guardado correctamente", "Aceptar");
                txtNombre.Focus();


            }
            else
            {
                await DisplayAlert("Error", "Faltan datos por llenar", "Aceptar");
            }
        }

        public async void LlenarDatos()
        {
            var alumnoList = await App.SQLiteDB.GetAlumnosAsync();

            if (alumnoList != null)
            {
                LstAlumnos.ItemsSource = alumnoList;
            }
        }

        public bool ValidarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellidoPaterno.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellidoMaterno.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private async void LstAlumnos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Alumno)e.SelectedItem;
            BtnGuardar.IsVisible = false;
            txtIdAlumno.IsVisible = true;
            BtnActualizar.IsVisible = true;
            BtnEliminar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.IdAlumno.ToString()))
            {
                var alumno = await App.SQLiteDB.GetAlumnoByIdAsync(obj.IdAlumno);
                if (alumno != null)
                {
                    txtIdAlumno.Text = alumno.IdAlumno.ToString();
                    txtNombre.Text = alumno.Nombre;
                    txtApellidoPaterno.Text = alumno.ApellidoPaterno;
                    txtApellidoMaterno.Text = alumno.ApellidoMaterno;
                    txtEdad.Text = alumno.Edad.ToString();
                    txtEmail.Text = alumno.Email;
                }
            }
        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdAlumno.Text))
            {
                Alumno alum = new Alumno
                {
                    IdAlumno = Convert.ToInt32(txtIdAlumno.Text),
                    Nombre = txtNombre.Text,
                    ApellidoPaterno = txtApellidoPaterno.Text,
                    ApellidoMaterno = txtApellidoMaterno.Text,
                    Edad = Convert.ToInt32(txtEdad.Text),
                    Email = txtEmail.Text
                };

                await App.SQLiteDB.SaveAlumnoAsync(alum);
                await DisplayAlert("Exito", "Alumno actualizado correctamente", "Aceptar");
                LimpiarControles();
                txtIdAlumno.IsVisible = false;
                BtnGuardar.IsVisible = true;
                BtnActualizar.IsVisible = false;
                LlenarDatos();
            }
        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            var alumno = await App.SQLiteDB.GetAlumnoByIdAsync(Convert.ToInt32(txtIdAlumno.Text));
            if (alumno != null)
            {
                await App.SQLiteDB.DeleteAlumnoAsync(alumno);
                await DisplayAlert("Exito", "Alumno eliminado correctamente", "Aceptar");

                LimpiarControles();
                LlenarDatos();
                txtIdAlumno.IsVisible = false;
                BtnGuardar.IsVisible = true;
                BtnActualizar.IsVisible = false;
                BtnEliminar.IsVisible = false;
                LlenarDatos();
            }
        }

        public void LimpiarControles()
        {
            txtIdAlumno.Text = "";
            txtNombre.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            txtEdad.Text = "";
            txtEmail.Text = "";
        }

        private void BtnExportar_Clicked(object sender, EventArgs e)
        {
            // Crear un nuevo documento Excel
            var workbook = new XLWorkbook();

            // Agregar una hoja al documento
            var worksheet = workbook.Worksheets.Add("Alumnos");

            // Agregar encabezados
            var headers = new string[] { "IdAlumno", "Nombre", "ApellidoPaterno", "ApellidoMaterno", "Edad", "Email" };
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
            }

            // Agregar datos
            foreach (var alumno in LstAlumnos.ItemsSource.Cast<Alumno>())
            {
                worksheet.Cell(worksheet.LastRowUsed().RowNumber() + 1, 1).Value = alumno.IdAlumno;
                worksheet.Cell(worksheet.LastRowUsed().RowNumber(), 2).Value = alumno.Nombre;
                // Agrega otras propiedades de Alumno según sea necesario
            }

            string path;

            // Guardar el archivo Excel en la carpeta de documentos compartidos
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Alumnos.xlsx");
            //guardar en la carpeta de descargas

            workbook.SaveAs(path);

            // Mostrar un mensaje de éxito
            DisplayAlert("Éxito", $"Datos exportados a {path}", "Aceptar");
        }

        //metodo para compartir el archivo excel generado en el metodo anterior 
        private async void BtnCompartir_Clicked(object sender, EventArgs e)
        {
            // Crear un nuevo documento Excel
            var workbook = new XLWorkbook();

            // Agregar una hoja al documento
            var worksheet = workbook.Worksheets.Add("Alumnos");

            // Agregar encabezados
            var headers = new string[] { "IdAlumno", "Nombre", "ApellidoPaterno", "ApellidoMaterno", "Edad", "Email" };
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
            }

            // Agregar datos
            foreach (var alumno in LstAlumnos.ItemsSource.Cast<Alumno>())
            {
                worksheet.Cell(worksheet.LastRowUsed().RowNumber() + 1, 1).Value = alumno.IdAlumno;
                worksheet.Cell(worksheet.LastRowUsed().RowNumber(), 2).Value = alumno.Nombre;
                worksheet.Cell(worksheet.LastRowUsed().RowNumber(), 3).Value = alumno.ApellidoPaterno;
                worksheet.Cell(worksheet.LastRowUsed().RowNumber(), 4).Value = alumno.ApellidoMaterno;
                worksheet.Cell(worksheet.LastRowUsed().RowNumber(), 5).Value = alumno.Edad;
                worksheet.Cell(worksheet.LastRowUsed().RowNumber(), 6).Value = alumno.Email;
                // Agrega otras propiedades de Alumno según sea necesario

            }

            string path;

            // Guardar el archivo Excel en la carpeta de documentos compartidos
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Alumnos.xlsx");
            //guardar en la carpeta de descargas

            workbook.SaveAs(path);

            // Mostrar un mensaje de éxito
            //DisplayAlert("Éxito", $"Datos exportados a {path}", "Aceptar");

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Compartir archivo",
                File = new ShareFile(path)
            });
        }

        private async void BtnGuardarExterno_Clicked(object sender, EventArgs e)
        {
            // Crear un nuevo documento Excel
            var workbook = new XLWorkbook();

            // Agregar una hoja al documento
            var worksheet = workbook.Worksheets.Add("Alumnos");

            // Agregar encabezados
            var headers = new string[] { "IdAlumno", "Nombre", "ApellidoPaterno", "ApellidoMaterno", "Edad", "Email" };
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
            }

            // Agregar datos
            foreach (var alumno in LstAlumnos.ItemsSource.Cast<Alumno>())
            {
                worksheet.Cell(worksheet.LastRowUsed().RowNumber() + 1, 1).Value = alumno.IdAlumno;
                worksheet.Cell(worksheet.LastRowUsed().RowNumber(), 2).Value = alumno.Nombre;
                worksheet.Cell(worksheet.LastRowUsed().RowNumber(), 3).Value = alumno.ApellidoPaterno;
                worksheet.Cell(worksheet.LastRowUsed().RowNumber(), 4).Value = alumno.ApellidoMaterno;
                worksheet.Cell(worksheet.LastRowUsed().RowNumber(), 5).Value = alumno.Edad;
                worksheet.Cell(worksheet.LastRowUsed().RowNumber(), 6).Value = alumno.Email;
                // Agrega otras propiedades de Alumno según sea necesario
            }

            // Obtener el directorio de la memoria externa específico de Android
            var externalStorageDirectory = DependencyService.Get<IFileSystemService>().GetExternalStorageDirectory();

            // Crear un directorio para tu aplicación, si aún no existe (opcional), y obtener su ruta. Se puede cambiar de nombre, pero no de ubicación
            var appDirectory = Path.Combine(externalStorageDirectory, "MiAppNombre");

            // Verificar si el directorio ya existe antes de intentar crearlo nuevamente
            if (!Directory.Exists(appDirectory))
            {
                Directory.CreateDirectory(appDirectory);
            }

            // Guardar el archivo Excel en el directorio de la aplicación
            var filePath = Path.Combine(appDirectory, "Alumnos.xlsx");
            workbook.SaveAs(filePath);

            // Mostrar un mensaje de éxito
            await DisplayAlert("Éxito", $"Datos exportados a {filePath}", "Aceptar");
        }
    }
}