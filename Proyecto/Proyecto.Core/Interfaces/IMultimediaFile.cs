using Microsoft.AspNetCore.Http;
using Proyecto.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Core.Interfaces
{
    public interface IMultimediaFile
    {
        Task<IEnumerable<MultimediaFile>> GetFiles();
        Task<MultimediaFile> GetFile(int id);
        Task InsertFile(MultimediaFile file);
        Task<MultimediaFile> UploadFile(IFormFile file);
        Task<bool> UpdateFile(MultimediaFile file);
        Task<bool> DeleteFile(int id);
    }
}
