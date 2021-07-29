using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace API.DTOs
{
    public class FaceApiDto
    {
        public FaceApiDto(string imageUrl, List<FaceItemDto> items)
        {
            ImageUrl = imageUrl;
            Items = items;
        }

        public string ImageUrl { get; set; }
        
        public List<FaceItemDto> Items { get; set; }

    }
    public class FaceItemDto
    {
        public FaceItemDto(string age, string emotion, string hair, string gender, string smile)
        {
            Age = age;
            Emotion = emotion;
            Hair = hair;
            Gender = gender;
            Smile = smile;
        }

        public string Age { get; set; }
        public string Emotion { get; set; }
        public string Hair { get; set; }
        public string Gender { get; set; }
        public string Smile { get; set; }
    }
}