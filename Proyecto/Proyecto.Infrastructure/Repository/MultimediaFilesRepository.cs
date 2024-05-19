using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Proyecto.Core.Entities;
using Proyecto.Core.Interfaces;
using Proyecto.Infrastructure.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Infrastructure.Repository
{
    public class MultimediaFilesRepository : IMultimediaFile
    {
        private readonly ContentManagementContext _context;
        DateTime currentDate = DateTime.Now;
        public MultimediaFilesRepository(ContentManagementContext context)
        {
            
            _context = context;
        }
        public async Task<IEnumerable<MultimediaFile>> GetFiles()
        {
            var Files = await _context.MultimediaFiles.ToListAsync();
            return Files;
        }
        public async Task<MultimediaFile> GetFile(int id)
        {
            var File = await _context.MultimediaFiles.FirstOrDefaultAsync(x => x.FileId == id);
            return File;
        }

        public async Task<MultimediaFile> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new ArgumentException("Invalid file.");

                // Cambia la ruta para guardar en la carpeta de documentos
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Files");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string fullPath = Path.Combine(path, uniqueFileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Determinar el tipo de archivo basado en la extensión
                string fileType = DetermineFileType(file.FileName);

                // Crear una nueva instancia de MultimediaFile con los datos del archivo
                var multimediaFile = new MultimediaFile
                {
                    FileName = uniqueFileName,
                    FilePath = fullPath,
                    FileType = fileType,
                    CreatedAt = DateTime.Now
                };

                _context.MultimediaFiles.Add(multimediaFile);
                await _context.SaveChangesAsync();

                return multimediaFile;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        public async Task InsertFile(MultimediaFile file)
        {
            _context.MultimediaFiles.Add(file);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateFile(MultimediaFile file)
        {
            var result = await GetFile(file.FileId);
            result.FileName = file.FileName;
            result.FilePath = file.FilePath;
            result.FileType = file.FileType;
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
        public async Task<bool> DeleteFile(int id)
        {
            var fileToDelete = await GetFile(id);
            if (fileToDelete == null)
            {
                return false;
            }

            try
            {
                if (System.IO.File.Exists(fileToDelete.FilePath))
                {
                    System.IO.File.Delete(fileToDelete.FilePath);
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar el error si la eliminación del archivo falla
                Console.WriteLine($"Error eliminando el archivo: {ex.Message}");
                return false; 
            }

            _context.Remove(fileToDelete);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        private string DetermineFileType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();

            // Diccionario que mapea las extensiones de archivo a los tipos de archivo
            Dictionary<string, string> fileTypes = new Dictionary<string, string>
            {
                { ".jpg", "Image" },
                { ".jpeg", "Image" },
                { ".png", "Image" },
                { ".gif", "Image" },
                { ".mp4", "Video" },
                { ".avi", "Video" },
                { ".mov", "Video" },
                { ".doc", "Document" },
                { ".docx", "Document" },
                { ".pdf", "Document" }
            };

            // Si la extensión está en el diccionario, devuelve el tipo de archivo correspondiente; de lo contrario, devuelve "Other"
            return fileTypes.ContainsKey(extension) ? fileTypes[extension] : "Other";
        }
    }
}
