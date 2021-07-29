using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace API.Services
{
    public class FaceApiService : IFaceApiService
    {
        public FaceApiService()
        {
        }

        const string SUBSCRIPTION_KEY = "cad6ea0c19814a999aa45f6e5ab12999";
        const string ENDPOINT = "https://soldiers-cog1.cognitiveservices.azure.com";

        // Used for all examples.
        // URL for the images.

        public static IFaceClient Authenticate(string endpoint, string key)
        {
            return new FaceClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
        }

        /* 
        * DETECT FACES
        * Detects features from faces and IDs them.
        */

        public async Task<FaceApiDto> DetectFaceExtract(string url)
        {
            IFaceClient client = Authenticate(ENDPOINT, SUBSCRIPTION_KEY);
            const string recognitionModel = RecognitionModel.Recognition04;

            // Create a list of images
            string imageFileName = url;

            FaceApiDto data;
            IList<DetectedFace> detectedFaces;

            // Detect faces with all attributes from image url.
            detectedFaces = await client.Face.DetectWithUrlAsync($"{imageFileName}",
            returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.Accessories, FaceAttributeType.Age,
                FaceAttributeType.Blur, FaceAttributeType.Emotion, FaceAttributeType.Exposure, FaceAttributeType.FacialHair,
                FaceAttributeType.Gender, FaceAttributeType.Glasses, FaceAttributeType.Hair, FaceAttributeType.HeadPose,
                FaceAttributeType.Makeup, FaceAttributeType.Noise, FaceAttributeType.Occlusion, FaceAttributeType.Smile },
                    // We specify detection model 1 because we are retrieving attributes.
                    detectionModel: DetectionModel.Detection01,
                    recognitionModel: recognitionModel);

            List<FaceItemDto> itemDto = new List<FaceItemDto>();



            string age = null;
            string emotionType = string.Empty;
            string color = null;
            string gender = null;
            string smile = null;
            foreach (var item in detectedFaces)
            {
                age = item.FaceAttributes.Age.ToString();

                double emotionValue = 0.0;
                Emotion emotion = item.FaceAttributes.Emotion;
                if (emotion.Anger > emotionValue) { emotionValue = emotion.Anger; emotionType = "Anger"; }
                if (emotion.Contempt > emotionValue) { emotionValue = emotion.Contempt; emotionType = "Contempt"; }
                if (emotion.Disgust > emotionValue) { emotionValue = emotion.Disgust; emotionType = "Disgust"; }
                if (emotion.Fear > emotionValue) { emotionValue = emotion.Fear; emotionType = "Fear"; }
                if (emotion.Happiness > emotionValue) { emotionValue = emotion.Happiness; emotionType = "Happiness"; }
                if (emotion.Neutral > emotionValue) { emotionValue = emotion.Neutral; emotionType = "Neutral"; }
                if (emotion.Sadness > emotionValue) { emotionValue = emotion.Sadness; emotionType = "Sadness"; }
                if (emotion.Surprise > emotionValue) { emotionType = "Surprise"; }


                Hair hair = item.FaceAttributes.Hair;
                
                if (hair.HairColor.Count == 0) { if (hair.Invisible) { color = "Invisible"; } else { color = "Bald"; } }
                HairColorType returnColor = HairColorType.Unknown;
                double maxConfidence = 0.0f;
                foreach (HairColor hairColor in hair.HairColor)
                {
                    if (hairColor.Confidence <= maxConfidence) { continue; }
                    maxConfidence = hairColor.Confidence; returnColor = hairColor.Color; color = returnColor.ToString();
                }


                gender = item.FaceAttributes.Gender == 0 ? "Male" : "Female";
                smile = item.FaceAttributes.Smile == 0 ? "No" 
                            : ( (item.FaceAttributes.Smile > 0 && item.FaceAttributes.Smile < 0.3) ? 
                                "May be": "Yes");
                var itemMap = new FaceItemDto(age, emotionType, color, gender, smile);
                itemDto.Add(itemMap);
            }

            // data.Add(new FaceApiDto(imageFileName, itemDto));
            data = new FaceApiDto(imageFileName, itemDto);

            return data;
        }
    }
}