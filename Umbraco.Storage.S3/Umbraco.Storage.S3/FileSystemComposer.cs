using System.Configuration;
using Umbraco.Core.Composing;
using Umbraco.Core.IO;

namespace Umbraco.Storage.S3
{
	public class FileSystemComposer : Umbraco.Core.Composing.IComposer
	{
		public void Compose(Composition composition)
		{
			string bucketName, bucketHostName, bucketKeyPrefix, region, cachePath, timeToLive, cannedACL, serverSideEncryptionMethod;
			bucketName = ConfigurationManager.AppSettings["MediaFileSystem.BucketName"];
			bucketHostName = ConfigurationManager.AppSettings["MediaFileSystem.BucketHostName"];
			bucketKeyPrefix = ConfigurationManager.AppSettings["MediaFileSystem.BucketKeyPrefix"];
			region = ConfigurationManager.AppSettings["MediaFileSystem.Region"];
			cachePath = ConfigurationManager.AppSettings["MediaFileSystem.CachePath"];
			cannedACL = ConfigurationManager.AppSettings["MediaFileSystem.CannedACL"];
			serverSideEncryptionMethod = ConfigurationManager.AppSettings["MediaFileSystem.ServerSideEncryptionMethod"];
			timeToLive = ConfigurationManager.AppSettings["MediaFileSystem.TimeToLive"];
			if (string.IsNullOrEmpty(bucketHostName))
			{
				return;
			}
			IFileSystem fs;
			if (string.IsNullOrEmpty(timeToLive) || string.IsNullOrEmpty(cachePath))
			{
				fs = new BucketFileSystem(
					bucketName: bucketName,
					bucketHostName: bucketHostName,
					bucketKeyPrefix: bucketKeyPrefix,
					region: region,
					cannedACL: cannedACL,
					serverSideEncryptionMethod: serverSideEncryptionMethod
					);
			}
			else
			{
				fs = new CachedBucketFileSystem(
					bucketName: bucketName,
					bucketHostName: bucketHostName,
					bucketKeyPrefix: bucketKeyPrefix,
					region: region,
					cachePath: cachePath,
					timeToLive: timeToLive,
					cannedACL: cannedACL,
					serverSideEncryptionMethod: serverSideEncryptionMethod
					);
			}
			composition.RegisterUniqueFor<IFileSystem, IMediaFileSystem>(fs);
			composition.Logger.Info(typeof(FileSystemComposer), "Bucket File System Setup: Bucket Name: {0}, Host Name: {1}, Region: {2}, Type: {3}", bucketName, bucketHostName, region, fs.GetType());
		}
	}
}
