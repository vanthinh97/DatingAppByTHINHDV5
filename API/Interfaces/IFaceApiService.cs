using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface IFaceApiService
    {
        Task<FaceApiDto> DetectFaceExtract(string url);
    }
}