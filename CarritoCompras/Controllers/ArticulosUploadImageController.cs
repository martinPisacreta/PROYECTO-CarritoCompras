using CarritoCompras.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarritoCompras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosUploadImageController : ControllerBase
    {


        private readonly IConfiguration _configuration;
        private readonly CarritoComprasContext _context;

        public class Articulo_Con_Menos_Atributos
        {
            public string id_articulo { get; set; }

            public string path_img { get; set; }
        }
        public ArticulosUploadImageController(CarritoComprasContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }



        const long MAX_PHOTO_SIZE = 102400; //100KB
        const long MIN_PHOTO_SIZE = 0;

        //DE FORMA PREDETERMINADA EL VALOR MAXIMO PARA SUBIR IMAGENES ES DE 30000000 BYTES
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> uploadFilesInServer(List<IFormFile> conjuntoImagenes)
        {
            try
            {
                List<string> imagenes_bien_subidas = new List<string>() { };
                List<string> imagenes_atencion = new List<string>() { };
                List<string> imagenes_errores = new List<string>() { };
                Articulo_Con_Menos_Atributos articulo_que_modico_path_img_en_pc_local_maxi = null;
                string path_img = "";
                string extension_imagen = "";
                string nombre_imagen_sin_extension = "";
                string nombre_imagen_con_extension_png = "";

                foreach (var imagen_original_tipo_IFormFile in conjuntoImagenes)
                {

                    if (imagen_original_tipo_IFormFile.Length > 0)
                    {
                        using (var dbContextTransaction = _context.Database.BeginTransaction())
                        {
                            try
                            {
                                extension_imagen = Path.GetExtension(imagen_original_tipo_IFormFile.FileName).Trim();
                                nombre_imagen_sin_extension = Path.GetFileNameWithoutExtension(imagen_original_tipo_IFormFile.FileName).Trim();
                                nombre_imagen_con_extension_png = (nombre_imagen_sin_extension + _configuration["Upload_Image:ftpExtensionArchivos"]).Trim();

                                var articulo_db = _context.Articulos.Where(a => a.IdArticulo.ToString() == nombre_imagen_sin_extension).FirstOrDefault();

                                if (articulo_db == null) //SI NO SE ENCONTRO NINGUN ARTICULO ... 
                                {
                                    imagenes_atencion.Add("Atención : el nombre de la imagen no se relaciona con ningun articulo de la base de datos ----> Imagen: " + imagen_original_tipo_IFormFile.FileName);
                                    continue;
                                }

                                if (articulo_db.PathImg == "" || articulo_db.PathImg == null) //SI PATH_IMG NO TIENE ALGO ESCRITO ...
                                {

                                }
                                else //SI TIENE ALGO ESCRITO
                                {
                                    imagenes_atencion.Add("Atención : la imagen que desea cargar , ya posee una imagen en la base de datos ----> Imagen: " + imagen_original_tipo_IFormFile.FileName);
                                    continue;
                                }


                                using (var imagen_original_en_memoria = new MemoryStream())
                                {
                                    //"imagen_tipo_IFormFile" genera una secuencia cuyo almacenamiento es la memoria.
                                    await imagen_original_tipo_IFormFile.CopyToAsync(imagen_original_en_memoria);

                                    //se crea IMAGEN a partir de imagen en memoria (ORIGINAL)
                                    using (Image imagen_original_tipo_Image = Image.FromStream(imagen_original_en_memoria))
                                    {
                                        //seteo IMAGEN_NUEVA , con la IMAGEN_ORIGINAL
                                        Image imagen_nueva_tipo_Image = imagen_original_tipo_Image;

                                        //si la IMAGEN_NUEVA tiene un ancho o un largo mayor a 250px...
                                        if (imagen_original_tipo_Image.Size.Height > 250 || imagen_original_tipo_Image.Size.Width > 250)
                                        {
                                            //redimensiono a escala de 250x250
                                            imagen_nueva_tipo_Image = ScaleImage(imagen_original_tipo_Image, 250, 250);

                                        }

                                        //seteo IMAGEN_NUEVA_MEMORIA , con la imagen IMAGEN_ORIGINAL_MEMORIA
                                        MemoryStream imagen_nueva_en_memoria = imagen_original_en_memoria;

                                        //si la extension de la imagen es <>  ".png" 
                                        if (extension_imagen != _configuration["Upload_Image:ftpExtensionArchivos"])
                                        {
                                            //convierto IMAGEN_NUEVA_MEMORIA -> a PNG
                                            imagen_nueva_tipo_Image.Save(imagen_nueva_en_memoria, ImageFormat.Png);
                                        }


                                        //ANALIZO EL PESO IMAGEN
                                        if (imagen_nueva_en_memoria.Length >= MAX_PHOTO_SIZE)
                                        {
                                            imagenes_atencion.Add("Atención : la imagen es mayor o igual a " + MAX_PHOTO_SIZE / 1024 + "KB ----> Imagen: " + imagen_original_tipo_IFormFile.FileName);
                                            continue;
                                        }
                                        else if (imagen_nueva_en_memoria.Length <= MIN_PHOTO_SIZE)
                                        {
                                            imagenes_atencion.Add("Atención : la imagen debe ser mayor a " + MIN_PHOTO_SIZE + "KB ----> Imagen: " + imagen_original_tipo_IFormFile.FileName);
                                            continue;
                                        }

                                        //YA PASO LAS VALIDACIONES ... Y VIENE ESTA PARTE
                                        path_img = _configuration["Upload_Image:pathImgBD"] + nombre_imagen_con_extension_png;

                                        if (path_img.Length > 400)
                                        {
                                            throw new Exception("Error : pathImg con mas de 400 caracteres  ----> Imagen: " + imagen_original_tipo_IFormFile.FileName);
                                        }

                                        //PREPARO UPDATE "ARTICULO" DEL SERVIDOR WEB
                                        articulo_db.PathImg = path_img;
                                        _context.Update(articulo_db);

                                        //SUBO LA IMAGEN AL SERVIDOR
                                        bool bandera_create_imagen = CreateImageFtp(imagen_nueva_en_memoria, nombre_imagen_con_extension_png, _configuration["Upload_Image:ftpUser"], _configuration["Upload_Image:ftpPass"], _configuration["Upload_Image:ftpServerIP"], _configuration["Upload_Image:ftpUbicacionImagenesArticulos"]);
                                        if (bandera_create_imagen == false)
                                        {
                                            throw new Exception("Error : fallo CreateImageFtp  ----> Imagen: " + imagen_original_tipo_IFormFile.FileName);
                                        }

                                        /* NO VOY A PASAR LOS DATOS A LA PC DE MAXI POR EL MOMENTO
                                        //PREPARO "ARTICULO" QUE VOY A MANDAR A LA PC LOCAL PARA QUE SE ACTUALICEN LOS DATOS AHI TAMBIEN
                                        articulo_que_modico_path_img_en_pc_local_maxi = new Articulo_Con_Menos_Atributos();
                                        articulo_que_modico_path_img_en_pc_local_maxi.id_articulo = articulo_db.IdArticulo.ToString();
                                        articulo_que_modico_path_img_en_pc_local_maxi.path_img = path_img;
                                            
                                        //GRABA LOS DATOS DEL "ARTICULO" EN LA BD LOCAL DE MAXI
                                        var json = JsonConvert.SerializeObject(articulo_que_modico_path_img_en_pc_local_maxi);
                                        var data = new StringContent(json, Encoding.UTF8, "application/json");
                                        var url = _configuration["WebApiQueCorreEnPcMaxi"];
                                        using var client = new HttpClient();
                                        var response = await client.PostAsync(url, data);
                                        string result = response.Content.ReadAsStringAsync().Result;
                                        if(result != "1") // "1" EN LA WEB API , SIGNIFICA QUE TODO ESTUVO BIEN
                                        {
                                            throw new Exception(result);
                                        }
                                        */

                                        //AGREGO LA IMAGEN EN LISTA DE imagenes_bien_subidas
                                        imagenes_bien_subidas.Add(imagen_original_tipo_IFormFile.FileName);

                                        //GRABO LOS DATOS DE "ARTICULO" EN EL SERVIDOR WEB
                                        await _context.SaveChangesAsync();
                                        dbContextTransaction.Commit();


                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                //CARGO EL ERROR
                                imagenes_errores.Add("Error : " + ex.Message + " ----> Imagen: " + imagen_original_tipo_IFormFile.FileName);

                                //RETROCEDO LOS CAMBIOS EN EL SERVIDOR WEB
                                dbContextTransaction.Rollback();

                                //BORRO LA IMAGEN EN EL SERVIDOR SI ES QUE EXISTE
                                bool bandera_existe_imagen_en_servidor = CheckIfFileExistsOnServer(nombre_imagen_con_extension_png, _configuration["Upload_Image:ftpUser"], _configuration["Upload_Image:ftpPass"], _configuration["Upload_Image:ftpServerIP"], _configuration["Upload_Image:ftpUbicacionImagenesArticulos"]);
                                if (bandera_existe_imagen_en_servidor == true)
                                {
                                    bool bandera_delete_imagen = DeleteImageFtp(nombre_imagen_con_extension_png, _configuration["Upload_Image:ftpUser"], _configuration["Upload_Image:ftpPass"], _configuration["Upload_Image:ftpServerIP"], _configuration["Upload_Image:ftpUbicacionImagenesArticulos"]);
                                    if (bandera_delete_imagen == false)
                                    {
                                        throw new Exception("Error : fallo DeleteImageFtp  ----> Imagen: " + imagen_original_tipo_IFormFile.FileName);
                                    }
                                }

                                //HAGO UN NEXT AL FOREACH
                                continue;
                            }
                        }
                    }
                    else //articulo.Length es menor o igual a 0
                    {
                        imagenes_atencion.Add("Atención : la imagen debe ser mayor a " + MIN_PHOTO_SIZE + "KB ----> Imagen: " + imagen_original_tipo_IFormFile.FileName);
                        continue;
                    }
                }


                return Ok(new
                {
                    imagenes_a_subir_cantidad = conjuntoImagenes.Count,
                    imagenes_bien_subidas_cantidad = imagenes_bien_subidas.Count,
                    imagenes_bien_subidas,
                    imagenes_atencion_cantidad = imagenes_atencion.Count,
                    imagenes_atencion,
                    imagenes_errores_cantidad = imagenes_errores.Count,
                    imagenes_errores
                });

            }
            catch (Exception ex)
            {
                //retorno el error
                return BadRequest(new { message = ex.Message });
            }

        }

        private static bool CheckIfFileExistsOnServer(string nombre_imagen_con_extension_png, string user, string pass, string ftpServerIP, string ftpUbicacionImagenesArticulos)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + ftpUbicacionImagenesArticulos + nombre_imagen_con_extension_png));
            request.Credentials = new NetworkCredential(user, pass);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return false;
        }
        public static bool DeleteImageFtp(string nombre_imagen_con_extension_png, string user, string pass, string ftpServerIP, string ftpUbicacionImagenesArticulos)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + ftpUbicacionImagenesArticulos + nombre_imagen_con_extension_png));
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(user, pass);
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return false;
        }

        public static bool CreateImageFtp(MemoryStream imagen_nueva_en_memoria, string nombre_imagen_con_extension_png, string user, string pass, string ftpServerIP, string ftpUbicacionImagenesArticulos)
        {

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + ftpUbicacionImagenesArticulos + nombre_imagen_con_extension_png));
            request.Credentials = new NetworkCredential(user, pass);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            try
            {
                using (Stream ftpStream = request.GetRequestStream())
                {

                    imagen_nueva_en_memoria.WriteTo(ftpStream);
                    return true;
                }
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return false;
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            //pongo la imagen horizontal 
            if (Array.IndexOf(image.PropertyIdList, 274) > -1)
            {
                var orientation = (int)image.GetPropertyItem(274).Value[0];
                switch (orientation)
                {
                    case 1:
                        // No rotation required.
                        break;
                    case 2:
                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case 3:
                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 4:
                        image.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;
                    case 5:
                        image.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case 6:
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case 7:
                        image.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case 8:
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }
                // This EXIF data is now invalid and should be removed.
                image.RemovePropertyItem(274);
            }

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);




            return newImage;
        }


    }
}
