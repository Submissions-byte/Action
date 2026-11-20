using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace RaceDay.Services
{
    public class BlobService
    {
        //handles all communication with blob storage
        // it uploads pet images and retrieves them when needed
        private readonly BlobContainerClient _container;

        public BlobService(IConfiguration configuration)
        {
            // get connection string from appsettings.json
            var connectionString = configuration.GetConnectionString("BlobConnection");

            //create the main blob service client
            //blob client -> container
            var blobServiceClient = new BlobServiceClient(connectionString);

            // this container will store all movie images
            // first run creates the container, otherwise it connects to the existing container 
            _container = blobServiceClient.GetBlobContainerClient("venue-images");

           // create container if it doesn't exist, skips otherwise
              _container.CreateIfNotExists();
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            // create unique file name so that two files with the same name don't overwrite each other in blob storage
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            //creates a blob client that points to the specific file inside the container
            var blobClient = _container.GetBlobClient(fileName);

            //open the uploaded file as a stream so that it can be sent to blob storage
            using var stream = file.OpenReadStream();

            // set/store content type so browser knows it's an image/what kind of file it is
            var headers = new BlobHttpHeaders
            {
                ContentType = file.ContentType
            };
            //upload the file into blob storage
            await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = headers });

            // return only the file name (not full url)
            //this is what we'll store in the database as the blob reference
            return fileName;
        }

        public async Task<(Stream?, string?)> GetFileAsync(string fileName)
        {
            //Blob is compatible with Regex

            //create a blob client for the requested file
            var blobClient = _container.GetBlobClient(fileName);

            //check whether the blob actually exists
            var exists = await blobClient.ExistsAsync();

            //if the file does not exist, return null values
            if (!exists.Value)
                return (null, null);

            //download the blob
            var response = await blobClient.DownloadAsync();

            //return the file stream and content type
            return (response.Value.Content, response.Value.Details.ContentType);
        }
    }
}