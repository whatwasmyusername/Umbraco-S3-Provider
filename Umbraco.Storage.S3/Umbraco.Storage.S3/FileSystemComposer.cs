using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Composing;
using Umbraco.Core.Composing.CompositionExtensions;
using Umbraco.Core;
using Umbraco.Storage.S3.Services.Impl;
using Umbraco.Core.IO;
using System.Configuration;

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
			timeToLive = ConfigurationManager.AppSettings["MediaFileSystem.TimeToLive"];
			cannedACL = ConfigurationManager.AppSettings["MediaFileSystem.CannedACL"];
			serverSideEncryptionMethod = ConfigurationManager.AppSettings["MediaFileSystem.ServerSideEncryptionMethod"];
			CachedBucketFileSystem fs = new CachedBucketFileSystem(bucketName, bucketHostName, bucketKeyPrefix, region, cachePath, timeToLive, cannedACL, serverSideEncryptionMethod);
			composition.RegisterUniqueFor<IFileSystem, IMediaFileSystem>(fs);
		}
	}
}
