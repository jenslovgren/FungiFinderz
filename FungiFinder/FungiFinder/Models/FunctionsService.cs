﻿using FungiFinder.Models.Entities;
using FungiFinder.Models.TensorFlow;
using FungiFinder.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace FungiFinder.Models
{
    public class FunctionsService
    {
        private readonly FungiFinderContext context;
        private readonly UserManager<MyIdentityUser> userManager;
        private readonly IHttpContextAccessor accessor;


        public FunctionsService(FungiFinderContext context, UserManager<MyIdentityUser> userManager, IHttpContextAccessor accessor)
        {
            this.context = context;
            this.userManager = userManager;
            this.accessor = accessor; // hej
        }

        static readonly string _assetsPath = Path.Combine(Environment.CurrentDirectory, "wwwroot");
        static readonly string _imagesFolder = Path.Combine(_assetsPath, "Images");
        static readonly string _TsvFolder = Path.Combine(_assetsPath, "Tsv");
        static readonly string _uploadedImages = Path.Combine(_assetsPath, "Images/Uploads");
        static readonly string _trainTagsTsv = Path.Combine(_TsvFolder, "tags.tsv");
        static readonly string _testTagsTsv = Path.Combine(_TsvFolder, "test-tags.tsv");
        static string _predictSingleImage = Path.Combine(_imagesFolder, "startup.jpg");
        static readonly string _inceptionTensorFlowModel = Path.Combine(_assetsPath, "inception", "tensorflow_inception_graph.pb");

        public FunctionMainResultPartialVM PredictImage(string urlInput)
        {
            MLContext mlContext = new MLContext();
            ITransformer model = GenerateModel(mlContext);
            _predictSingleImage = Path.Combine(_uploadedImages, urlInput);

            var ret = ClassifySingleImage(mlContext, model);
            return ret;
        }

        private struct InceptionSettings
        {
            public const int ImageHeight = 224;
            public const int ImageWidth = 224;
            public const float Mean = 117;
            public const float Scale = 1;
            public const bool ChannelsLast = true;
        }

        public FunctionLibraryResultPartialVM[] GetMushroomsFromSearch(string searchQuery)
        {
            IQueryable<Mushrooms> matchedMushrooms = null;
            var resultList = new List<FunctionLibraryResultPartialVM>();
            if (searchQuery == "emptySearchQuery")
                matchedMushrooms = context.Mushrooms;
            else
                matchedMushrooms = context.Mushrooms.Where(o => o.Name.Contains(searchQuery) || o.LatinName.Contains(searchQuery));

            foreach (var mushroom in matchedMushrooms)
            {
                resultList.Add(new FunctionLibraryResultPartialVM
                {
                    Name = mushroom.Name,
                    LatinName = mushroom.LatinName,
                    Info = mushroom.Info,
                    Edible = mushroom.Edible,
                    ImgUrl = mushroom.ImageUrl,
                    Rating = mushroom.Rating
                });
            }
            
            return resultList.ToArray();
        }

        internal async Task<FunctionsMapLocationsVM[]> GetUserLocations()
        {
            var user = await userManager.GetUserAsync(accessor.HttpContext.User);
            var vm = context.MapLocation.Where(u => u.UserId == user.Id)
                .Select(o => new FunctionsMapLocationsVM
                {
                    Longitude = o.Longitude,
                    Latitude = o.Latitude,

                }).ToArray();

            return vm;

        }

        internal async Task SaveLocation(FunctionMapVM vm)
        {
           

            var user = await userManager.GetUserAsync(accessor.HttpContext.User);
            context.MapLocation.Add(new MapLocation { UserId = user.Id, LocationName = vm.LocationName, Latitude = vm.Latitude, Longitude = vm.Longitude });
            context.SaveChanges();


            




        }

        private ITransformer GenerateModel(MLContext mlContext)
        {
            IEstimator<ITransformer> pipeline = mlContext.Transforms.LoadImages(outputColumnName: "input", imageFolder: _imagesFolder, inputColumnName: nameof(ImageData.ImagePath))
            // The image transforms transform the images into the model's expected format.
            .Append(mlContext.Transforms.ResizeImages(outputColumnName: "input", imageWidth: InceptionSettings.ImageWidth, imageHeight: InceptionSettings.ImageHeight, inputColumnName: "input"))
            .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "input", interleavePixelColors: InceptionSettings.ChannelsLast, offsetImage: InceptionSettings.Mean))

            .Append(mlContext.Model.LoadTensorFlowModel(_inceptionTensorFlowModel).ScoreTensorFlowModel(outputColumnNames: new[] { "softmax2_pre_activation" }, inputColumnNames: new[] { "input" }, addBatchDimensionInput: true))
            .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "LabelKey", inputColumnName: "Label"))
            .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(labelColumnName: "LabelKey", featureColumnName: "softmax2_pre_activation"))
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabelValue", "PredictedLabel"))
            .AppendCacheCheckpoint(mlContext);

            IDataView trainingData = mlContext.Data.LoadFromTextFile<ImageData>(path: _trainTagsTsv, hasHeader: false);

            ITransformer model = pipeline.Fit(trainingData);

            IDataView testData = mlContext.Data.LoadFromTextFile<ImageData>(path: _testTagsTsv, hasHeader: false);
            IDataView predictions = model.Transform(testData);
            

            // Create an IEnumerable for the predictions for displaying results
            IEnumerable<ImagePrediction> imagePredictionData = mlContext.Data.CreateEnumerable<ImagePrediction>(predictions, true);
            DisplayResults(imagePredictionData);

            //MulticlassClassificationMetrics metrics =
            //mlContext.MulticlassClassification.Evaluate(predictions,
            //labelColumnName: "LabelKey",
            //predictedLabelColumnName: "PredictedLabel");
            ////Console.WriteLine("=============== Classification metrics ===============");
            ////Console.WriteLine($"LogLoss is: {metrics.LogLoss}");
            ////Console.WriteLine($"PerClassLogLoss is: {String.Join(" , ", metrics.PerClassLogLoss.Select(c => c.ToString()))}");

            return model;
        }
        FunctionMainResultPartialVM ClassifySingleImage(MLContext mlContext, ITransformer model)
        {
            var imageData = new ImageData()
            {
                ImagePath = _predictSingleImage
            };
            // Make prediction function (input = ImageData, output = ImagePrediction)
            var predictor = mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(model);
            var prediction = predictor.Predict(imageData);
            //var result = context.Mushrooms.Where(m => m.Name == prediction.PredictedLabelValue).FirstOrDefault();
            var result = context.Mushrooms.SingleOrDefault(m => m.Name.Replace(" ", string.Empty).ToLower() == prediction.PredictedLabelValue.ToLower())/*.Select(m => new FunctionMainResultPartialVM { Name = m.Name, EdibleOrPosinous = m.Edible, ProcentResult = prediction.Score.Max() * 100, UrlMatchedMushroom = m.ImageUrl }*/;
            context.LatestSearches.Add(new LatestSearches { Mushroom = ConvertFirstLetterToUpper(prediction.PredictedLabelValue), SearchDate = DateTime.Now, UserId = userManager.GetUserId(accessor.HttpContext.User) });
            context.SaveChanges();
            return new FunctionMainResultPartialVM { Name = ConvertFirstLetterToUpper(result.Name), ProcentResult = prediction.Score.Max() * 100, Edible = result.Edible, UrlMatchedMushroom = result.ImageUrl, Info = result.Info, LatinName = result.LatinName, Rating = (int)result.Rating };
            //Console.WriteLine($"Image: {Path.GetFileName(imageData.ImagePath)}                  predicted as: {prediction.PredictedLabelValue}                  with score: {prediction.Score.Max()} ");
        }
        private string ConvertFirstLetterToUpper(string stringToConvert)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(stringToConvert);
        }

        private void DisplayResults(IEnumerable<ImagePrediction> imagePredictionData)
        {
            //Console.WriteLine("=============== Training classification model ===============");
            //foreach (ImagePrediction prediction in imagePredictionData)
            //{
            //    Console.WriteLine($"Image: {Path.GetFileName(prediction.ImagePath)} predicted as: {prediction.PredictedLabelValue} with score: {prediction.Score.Max()} ");
            //}
        }

        private IEnumerable<ImageData> ReadFromTsv(string file, string folder)
        {
            return File.ReadAllLines(file)
            .Select(line => line.Split('\t'))
            .Select(line => new ImageData()
            {
                ImagePath = Path.Combine(folder, line[0])
            });
        }
        //string _inceptionTensorFlowModel = Path.Combine(_assetsPath, "inception", "tensorflow_inception_graph.pb");
    }


}
