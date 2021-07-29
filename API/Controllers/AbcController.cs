// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Threading.Tasks;
// using API.DTOs;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Azure.CognitiveServices.Vision.Face;
// using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

// namespace API.Controllers
// {
//     public class AbcController : BaseApiController // Tạo 1 lớp ở client có kiểu dữ liệu là string url vs object[] xxx
//     {
//         // From your Face subscription in the Azure portal, get your subscription key and endpoint.
//         const string SUBSCRIPTION_KEY = "cad6ea0c19814a999aa45f6e5ab12999";
//         const string ENDPOINT = "https://soldiers-cog1.cognitiveservices.azure.com";
//         // Used for all examples.
//         // URL for the images.
//         const string IMAGE_BASE_URL = "https://csdx.blob.core.windows.net/resources/Face/Images/";
//         const string RECOGNITION_MODEL4 = RecognitionModel.Recognition04;
//         public AbcController()
//         {
//         }

//         [HttpGet("demo")]
//         public async Task<ActionResult<List<FaceApiDto>>> Abc()
//         {

//             // Authenticate.
//             IFaceClient client = Authenticate(ENDPOINT, SUBSCRIPTION_KEY);

//             // Detect - get features from faces.
//             var result = await DetectFaceExtract(client, IMAGE_BASE_URL, RECOGNITION_MODEL4);
//             // Find Similar - find a similar face from a list of faces.
          
//             return result;
//         }
//         public static IFaceClient Authenticate(string endpoint, string key)
//         {
//             return new FaceClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
//         }
//         /* 
//         * DETECT FACES
//         * Detects features from faces and IDs them.
//         */
        
//         public static async Task<List<FaceApiDto>> DetectFaceExtract(IFaceClient client, string url, string recognitionModel)
//         {

//             // Create a list of images
//             List<string> imageFileNames = new List<string>
//                     {
//                         "detection1.jpg",    // single female with glasses
//                         "detection2.jpg", // (optional: single man)
//                         "detection3.jpg", // (optional: single male construction worker)
//                         "detection4.jpg", // (optional: 3 people at cafe, 1 is blurred)
//                         "detection5.jpg",    // family, woman child man
//                         "detection6.jpg"     // elderly couple, male female
//                     };

//             List<FaceApiDto> data = new List<FaceApiDto>();
//             foreach (var imageFileName in imageFileNames)
//             {
//                 IList<DetectedFace> detectedFaces;

//                 // Detect faces with all attributes from image url.
//                 detectedFaces = await client.Face.DetectWithUrlAsync($"{url}{imageFileName}",
//                 returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.Accessories, FaceAttributeType.Age,
//                     FaceAttributeType.Blur, FaceAttributeType.Emotion, FaceAttributeType.Exposure, FaceAttributeType.FacialHair,
//                     FaceAttributeType.Gender, FaceAttributeType.Glasses, FaceAttributeType.Hair, FaceAttributeType.HeadPose,
//                     FaceAttributeType.Makeup, FaceAttributeType.Noise, FaceAttributeType.Occlusion, FaceAttributeType.Smile },
//                         // We specify detection model 1 because we are retrieving attributes.
//                         detectionModel: DetectionModel.Detection01,
//                         recognitionModel: recognitionModel);

//                 List<FaceItemDto> itemDto = new List<FaceItemDto>();     
//                 foreach (var item in detectedFaces)
//                 {
//                     var age = item.FaceAttributes.Age;
//                     var emotion = item.FaceAttributes.Emotion;
//                     var hair = item.FaceAttributes.Hair;
//                     var gender = item.FaceAttributes.Gender;
//                     var smile = item.FaceAttributes.Smile;
//                     var itemMap = new FaceItemDto(age, emotion, hair, gender, smile);
//                     itemDto.Add(itemMap);
//                 }
                      

//                 data.Add(new FaceApiDto(imageFileName, itemDto));

//             }
//             return data;
//         }
       
//     }
// }